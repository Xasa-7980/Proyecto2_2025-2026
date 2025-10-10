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
        redTilesPrefab = _redTilesPrefab;
        greenTilesPrefab = _greenTilesPrefab;
        tileSize = _tileSize;
        instantiatedPrefabs = new List<GameObject> ();
    }
    public override void LevelMechanics ( )
    {
        if (fileChanged)
        {
            fileChanged = false;
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
            int index = 0;
            foreach (string line in lines)
            {

                foreach (char c in line)
                {
                    Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
                    Vector3 position = topLeft + new Vector3(0.5f + x * tileSize, -0.5f + -y * tileSize, 0); x++;
                    GameObject prefabToUse = (c == 'R') ? redTilesPrefab : (c == 'G')? greenTilesPrefab : redTilesPrefab;
                    
                    if (prefabToUse != null && instantiatedPrefabs.Count <= 0)
                    {
                        instantiatedPrefabs.Add(MonoBehaviour.Instantiate(prefabToUse, position, Quaternion.identity));

                    }
                    else
                    {
                        SpriteRenderer spriteRenderer = instantiatedPrefabs[index].GetComponent<SpriteRenderer>();
                        spriteRenderer.color = (c == 'R') ? Color.red : (c == 'G') ? Color.green : Color.red;
                    }
                    index++;
                }
                x = 0;
                y++;
            }
        }
        fileChanged = false;
    }
}

