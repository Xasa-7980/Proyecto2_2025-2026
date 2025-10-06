using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Text.RegularExpressions;

public class Level4 : FileManager
{

    [SerializeField] Light2D globalLight; //lvl3
    public Level4 ( string _directoryPath, string _filePath, Light2D _light ) : base(_directoryPath, _filePath) 
    { 
        globalLight = _light;
    }
    public override void InitializeDirectory ( string directoryPath, string filePath )
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
    public override string[] GetFileContent ( )
    {
        return File.ReadAllLines(filePath);
    }
    public override void OpenDirectory ( )
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
    public override void InitializeSystemWatcher ( )
    {
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
    }
    public override void OnFileChanged ( object source, FileSystemEventArgs e )
    {

        fileChanged = true;
    }
    public override void LevelMechanics ( )
    {
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            UnityEngine.Debug.LogWarning("El archivo está vacío.");
            return;
        }

        float valueFound = -1f;

        foreach (string line in lines)
        {

            Match match = Regex.Match(line, @"[-+]?[0-9]*\.?[0-9]+");
            //Sirve para buscar el primer numero entero o decimal negativo o positivo

            if (match.Success)
            {
                // Convertir el texto del número en float
                if (float.TryParse(match.Value, System.Globalization.NumberStyles.Float,
                                    System.Globalization.CultureInfo.InvariantCulture, out float val))
                {
                    valueFound = val;
                    UnityEngine.Debug.Log($"Valor numérico encontrado: {valueFound}");
                    break;
                }
            }
        }

        valueFound = Mathf.Clamp(valueFound, 0f, 360f);
        ChangeLightIntensity(valueFound);
    }
    void ChangeLightIntensity(float value )
    {
        globalLight.pointLightInnerAngle = value;
    }
}

