using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public abstract class FileManager
{
    protected string fileName;
    protected string filePath;
    public bool fileChanged {  get; protected set; }
    protected string[] lines;
    protected FileSystemWatcher systemWatcher;

    public FileManager ( string _directoryPath, string _fileName )
    {
        fileName = _fileName;
        InitializeDirectory(_directoryPath, ref filePath);
        if(_directoryPath == "" && filePath == "")
        {
            UnityEngine.Debug.LogError("Los paths al archivo son incorrectos");
            return;
        }
        InitializeSystemWatcher();
        systemWatcher.Changed += OnFileChanged;

    }
    public virtual void OnFileChanged ( object source, FileSystemEventArgs e )
    {
        fileChanged = true;
    }
    public string[] GetFileContent ( )
    {
        return File.ReadAllLines(filePath);
    }
    public abstract void LevelMechanics ( );
    public void OpenDirectory ( )
    {
        if (File.Exists(filePath))
        {
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); //Abrir ventana
        }
        else
        {
            UnityEngine.Debug.LogError("No se encontró el archivo: " + filePath);
        }
    }
    private void InitializeDirectory ( string directoryPath, ref string filePath )
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, directoryPath);
        if (!Directory.Exists(persistentDir))
        {
            Directory.CreateDirectory(persistentDir);
        }

        filePath = Path.Combine(Application.persistentDataPath, directoryPath+"/"+fileName);

        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, filePath);
            if (File.Exists(sourceFile))
            {
                File.Copy(sourceFile, filePath);
                UnityEngine.Debug.Log("Archivo copiado a persistentDataPath");
            }
            else
            {
                UnityEngine.Debug.LogError("No se encontró el archivo original en StreamingAssets: " + sourceFile);
                return;
            }
        }
    }
    private void InitializeSystemWatcher ( )
    {
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
    }
}
