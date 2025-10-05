using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class FileReader : MonoBehaviour
{
    bool file_is_Open = false;
    private string previousContent;
    [Serializable]
    public struct GameObjectReferencesDictionary
    {
        public char key;
        public GameObject value;
    }
    [SerializeField] List<GameObjectReferencesDictionary> gameObjectReferencedInText = new List<GameObjectReferencesDictionary>();
    FileSystemWatcher systemWatcher;
    string filePath;
    string[] lines;
    void Start()
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, "Level-3");
        if (!Directory.Exists(persistentDir))
        { 
                Directory.CreateDirectory(persistentDir); 
        }

        filePath = Path.Combine(Application.persistentDataPath, "Level-3/Puzzle.txt");
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
         
        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, "Level-3/Puzzle.txt");
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
        previousContent = File.ReadAllText(filePath);
        systemWatcher.Changed += OnFileChanged;
    }

    void Update()
    {
        Level3(filePath);
    }
    void Level3(string fileName)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true }); //Abrir ventana
                file_is_Open = true;
            }
            else
            {
                UnityEngine.Debug.LogError("No se encontró el archivo: " + filePath);
            }
            //ReadTextFile(filePath);
        }

        //if (file_is_Open)
        //{
        //    ReadTextFile(filePath);
        //}

    }
    void OnFileChanged(object source, FileSystemEventArgs e )
    {
        UnityEngine.Debug.Log("El archivo fue modificado " + e.FullPath);
        ReadTextFile(filePath);
    }
    void ReadTextFile(string fileName)
    {
        lines = File.ReadLines(fileName).ToArray();

        string newText = File.ReadAllText(fileName);
        int keyIndex = 0;
        foreach (string line in lines)
        {
            if(!line.Contains('*'))
            {
                break;
            }
            string[] asteriscos = line.Trim().Split('*');
            keyIndex += asteriscos.Length - 1;
            print(keyIndex);
        }
        if (keyIndex > gameObjectReferencedInText.Count - 1)
        {
            VanishElement(gameObjectReferencedInText.Count - 1);
        }
        print("he llegado aqui");
        VanishElement(keyIndex);
    }
    void VanishElement ( int index )
    {
        if (index < 0 || index >= gameObjectReferencedInText.Count)
        {
            UnityEngine.Debug.LogError($"Índice fuera de rango: {index}");
            return;
        }

        var go = gameObjectReferencedInText[index].value;

        if (go == null)
        {
            UnityEngine.Debug.LogError($"El GameObject en índice {index} es NULL (no asignado en el Inspector)");
            return;
        }

        UnityEngine.Debug.Log($" Desactivando: {go.name}");
        go.SetActive(false);
    }
} 
/*
 HACER FULL HERENCIA CON LAS 3 ULTIMAS FUNCIONES
 */