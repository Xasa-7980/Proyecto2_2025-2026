using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Text.RegularExpressions;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Level5 : FileManager
{

    public GameObject redTilesPrefab;
    public GameObject greenTilesPrefab;
    public float tileSize = 1;
    public List<GameObject> instantiatedPrefabs;
    public Level5 ( string _directoryPath, string _fileName, GameObject _redTilesPrefab, GameObject _greenTilesPrefab, float _tileSize) : base(_directoryPath, _fileName)
    {
        instantiatedPrefabs = new List<GameObject>();
        redTilesPrefab = _redTilesPrefab;
        greenTilesPrefab = _greenTilesPrefab;
        tileSize = _tileSize;
        originalContent = File.ReadAllText(filePath);
    }
    public void DrawLevel()
    {
        string[] lines = File.ReadAllLines(filePath);

        if (lines.Length == 0)
        {
            UnityEngine.Debug.LogWarning("El archivo está vacío.");
            return;
        }
        int x = 0;
        int y = 0;

        if (instantiatedPrefabs.Count > 0)
        {
            foreach (GameObject elem in instantiatedPrefabs)
            {
                instantiatedPrefabs.Remove(elem);
                MonoBehaviour.Destroy(elem);
            }
            instantiatedPrefabs.Clear();
            instantiatedPrefabs = new List<GameObject>();
            UnityEngine.Debug.Log("Delete list");
        }
        foreach (string line in lines)
        {

            foreach (char c in line)
            {
                Vector3 topLeft = new Vector3(-7, 2.5f, 0)/*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane))*/;
                Vector3 position = topLeft + new Vector3(x * tileSize, -y * tileSize, 0); x++;
                GameObject prefabToUse = (c == 'R') ? redTilesPrefab : (c == 'G') ? greenTilesPrefab : redTilesPrefab;
                UnityEngine.Debug.Log(prefabToUse);

                if (prefabToUse != null)
                {
                    instantiatedPrefabs.Add(MonoBehaviour.Instantiate(prefabToUse, position, Quaternion.identity));
                    UnityEngine.Debug.Log(instantiatedPrefabs.Count());
                    prefabToUse = null;
                }
            }
            x = 0;
            y++;
        }
    }    
    public override void LevelMechanics ( )
    {
        if (fileChanged)
        {
            string[] lines = File.ReadAllLines(filePath);

            if (lines.Length == 0)
            {
                UnityEngine.Debug.LogWarning("El archivo está vacío.");
                return;
            }
            int x = 0;
            int y = 0;

            if (instantiatedPrefabs.Count > 0)
            {
                foreach (GameObject elem in instantiatedPrefabs)
                {
                    elem.SetActive(false);
                }
                instantiatedPrefabs.Clear();
                instantiatedPrefabs = new List<GameObject>();
            }
            foreach (string line in lines)
            {

                foreach (char c in line)
                {
                    Vector3 topLeft = new Vector3(-7, 2.5f, 0)/*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane))*/;
                    Vector3 position = topLeft + new Vector3(x * tileSize, -y * tileSize, 0); x++;
                    GameObject prefabToUse = (c == 'R') ? redTilesPrefab : (c == 'G') ? greenTilesPrefab : redTilesPrefab;
                    UnityEngine.Debug.Log(prefabToUse);

                    if (prefabToUse != null)
                    {
                        instantiatedPrefabs.Add(MonoBehaviour.Instantiate(prefabToUse, position, Quaternion.identity));
                        UnityEngine.Debug.Log(instantiatedPrefabs.Count());
                        prefabToUse = null;
                    }
                }
                x = 0;
                y++;
            }
        }
        fileChanged = false;
    }
}

