using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public abstract class FileManager
{
    public string filePath;
    public bool fileChanged;
    public string[] lines;
    public FileSystemWatcher systemWatcher;
    ConcurrentQueue<Action> actionQueue = new ConcurrentQueue<Action>();
    public FileManager ( string _dirPath, string _filePath )
    {
        if(_dirPath == "" && filePath == "")
        {
            UnityEngine.Debug.LogError("Los paths al archivo son incorrectos");
            return;
        }
        InitializeDirectory(_dirPath,_filePath);
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
        if (Input.GetKeyDown(KeyCode.Space))
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
    }
    private void InitializeDirectory ( string directoryPath, string filePath )
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, directoryPath);
        if (!Directory.Exists(persistentDir))
        {
            Directory.CreateDirectory(persistentDir);
        }

        filePath = Path.Combine(Application.persistentDataPath, "filePath");

        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, "Level-5/Puzzle.txt");
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
