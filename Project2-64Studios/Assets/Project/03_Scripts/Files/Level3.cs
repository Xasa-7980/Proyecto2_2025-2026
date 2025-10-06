using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

public class Level3 : FileManager
{
    [Serializable]
    public struct GameObjectReferencesDictionary
    {
        public char key;
        public GameObject value;
    }
    [SerializeField] List<GameObjectReferencesDictionary> gameObjectReferencedInText = new List<GameObjectReferencesDictionary>();

    public Level3 ( string _directoryPath, string _filePath,List<GameObjectReferencesDictionary> _gameObjectList ) : base(_directoryPath, _filePath) 
    {
        gameObjectReferencedInText = _gameObjectList;
    }
    public override string[] GetFileContent ()
    {
        return File.ReadAllLines ( filePath );
    }
    public override void OnFileChanged ( object source, FileSystemEventArgs e )
    {

        fileChanged = true;
    }
    public override void LevelMechanics ()
    {
        lines = File.ReadAllLines ( filePath );
        if (lines.Length == 0)
        {
            UnityEngine.Debug.LogWarning("El archivo está vacío.");
            return;
        }
        lines = File.ReadLines(filePath).ToArray();

        string newText = File.ReadAllText(filePath);
        int keyIndex = 0;
        foreach (string line in lines)
        {
            if (!line.Contains('*'))
            {
                break;
            }
            string[] asteriscos = line.Trim().Split('*');
            keyIndex += asteriscos.Length - 1;
        }
        UnityEngine.Debug.Log(keyIndex);
        VanishElement(keyIndex);
        UnityEngine.Debug.Log("he llegado aqui");

    }
    public void VanishElement(int index )
    {
        if (index < 0 || index >= gameObjectReferencedInText.Count)
        {
            UnityEngine.Debug.LogError($"Índice fuera de rango: {index}");
            return;
        }

        GameObject go = gameObjectReferencedInText[index].value;

        if (go == null)
        {
            UnityEngine.Debug.LogError($"El GameObject en índice {index} es NULL (no asignado en el Inspector)");
            return;
        }

        UnityEngine.Debug.Log($" Desactivando: {go.name}");
        go.SetActive(false);

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
    public override void InitializeSystemWatcher ()
    {
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
    }
}

