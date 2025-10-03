using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;

public class FileReader : MonoBehaviour
{
    string filePath;
    [Serializable]
    public struct GameObjectReferencesDictionary
    {
        public char key;
        public GameObject value;
    }
    [SerializeField] List<GameObjectReferencesDictionary> gameObjectReferencedInText = new List<GameObjectReferencesDictionary>();

    void Start()
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, "Level-3");
        if (!Directory.Exists(persistentDir))
        { 
                Directory.CreateDirectory(persistentDir); 
        }

        filePath = Path.Combine(Application.persistentDataPath, "Level-3/Puzzle.txt");

        // Copiar archivo desde StreamingAssets si no existe
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
    }

    void Update()
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
            ReadTextFile(filePath);
        }
    }

    string[] lines;
    void ReadTextFile(string fileName)
    {
        lines = File.ReadLines(fileName).ToArray();
        foreach (string line in lines)
        {
            foreach(char c in line)
            {
                for (int i = 0; i < gameObjectReferencedInText.Count; i++)
                {
                    if (c == gameObjectReferencedInText[c].key)
                    {
                        gameObjectReferencedInText[c].value.SetActive(false);
                    }
                }
            }
        }
    }
}