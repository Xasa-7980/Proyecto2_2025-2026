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
    [SerializeField] List<GameObject> gameObjectReferencedInText = new List<GameObject>();

    public Level3 ( string _directoryPath, string _fileName, List<GameObject> _gameObjectList ) : base(_directoryPath, _fileName) 
    {
        gameObjectReferencedInText = _gameObjectList;
        originalContent = File.ReadAllText(filePath);
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
            keyIndex += line.Trim().Split('*').Length - 1;
        }
        UnityEngine.Debug.Log(keyIndex);
        VanishElement(keyIndex);
        fileChanged = false;

    }
    public void VanishElement(int index )
    {
        if (index < 0 || index >= gameObjectReferencedInText.Count)
        {
            UnityEngine.Debug.LogError($"Índice fuera de rango: {index}");
            return;
        }

        GameObject go = gameObjectReferencedInText[index];

        if (go == null)
        {
            UnityEngine.Debug.LogError($"El GameObject en índice {index} es NULL (no asignado en el Inspector)");
            return;
        }

        UnityEngine.Debug.Log($" Desactivando: {go.name}");
        go.SetActive(false);
        isCompleted = true;
        ResetFiles();

    }
}

