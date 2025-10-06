using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Controls;
using System.Text.RegularExpressions;
using UnityEngine.Rendering.Universal;

public class FileReader : MonoBehaviour
{
    bool fileChanged = false;
    private string previousContent;
        [Serializable]
        public struct GameObjectReferencesDictionary
        {
            public char key;
            public GameObject value;
        }
        [SerializeField] List<Level3.GameObjectReferencesDictionary> gameObjectReferencedInText = new List<Level3.GameObjectReferencesDictionary>();
    FileSystemWatcher systemWatcher;
    string filePath;
    string[] lines;
    [SerializeField] Light2D globalLight; //lvl3
    /// Nivel 5:
    [SerializeField] GameObject redTilesPrefab;
    [SerializeField] GameObject greenTilesPrefab;
    [SerializeField] float tileSize = 1;
    List<GameObject> instantiatedPrefabs = new List<GameObject>();
    Level3 level3;
    Level4 level4;
    Level5 level5;
    FileManager curLevelFiles;
    //Level 6
    public CharacterController characterController;
    void Start()
    {
        level3 = new Level3("Level-3", "Level-3/Puzzle.txt", gameObjectReferencedInText);
        level4 = new Level4("Level-4", "Level-4/Puzzle.txt", globalLight);
        level5 = new Level5("Level-5", "Level-5/Puzzle.txt", redTilesPrefab, greenTilesPrefab, tileSize);
        curLevelFiles = (FileManager)level3;
        string persistentDir = Path.Combine(Application.persistentDataPath, "Level-6");
        if (!Directory.Exists(persistentDir))
        { 
                Directory.CreateDirectory(persistentDir); 
        }

        filePath = Path.Combine(Application.persistentDataPath, "Level-6/Puzzle.txt");
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
         
        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, "Level-6/Puzzle.txt");
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
    void OnFileChanged ( object source, FileSystemEventArgs e )
    {
        UnityEngine.Debug.Log("El archivo fue modificado " + e.FullPath);
        fileChanged = true;
    }

    void Update ()
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
        //Level3(filePath);
        //Level4(filePath);
        Level6(filePath);
    }
    void Level3(string fileName)
    {

        if (fileChanged)
        {
            fileChanged = false;
            ReadTextFile(filePath);
        }

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
        }
        print(keyIndex);
        VanishElement(keyIndex);
        print("he llegado aqui");
    }
    void VanishElement ( int index )
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
    void Level4 ( string fileName )
    {
        string[] lines = File.ReadAllLines(fileName);

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

        if (valueFound >= 0)
        {
            globalLight.intensity = valueFound;
            UnityEngine.Debug.Log($"Intensidad actualizada a: {valueFound}");
        }
        else
        {
            UnityEngine.Debug.LogWarning("No se encontró ningún valor numérico válido en el archivo.");
        }
        valueFound = Mathf.Clamp(valueFound, 0f, 360f);
        globalLight.pointLightInnerAngle = valueFound;
    }
    void Level6 ( string fileName )
    {
        if (fileChanged)
        {
            fileChanged = false;
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                foreach(char c in line)
                {
                    switch (char.ToLower(c))
                    {
                        case 'r':
                            characterController.Move(Vector3.right);
                            break;
                        case 'l':
                            characterController.Move(Vector3.left);
                            break;
                        case 'u':
                            characterController.Move(Vector3.up);
                            break;
                        case 'd':
                            characterController.Move(Vector3.down);
                            break;
                        default:
                            characterController.Move(Vector3.right);
                            break;
                    }
                }
            } 
        }
    }
    void Level7 ( string fileName )
    {
        if (fileChanged)
        {
            fileChanged = false;
            string[] lines = File.ReadAllLines(fileName);

            foreach (string line in lines)
            {
                foreach (char c in line)
                {

                }
                if (line == "rullrdu")
                {
                    //Completado escenario
                }
            } 
        }
    }
    void Level5 ( string fileName )
    {
        if (fileChanged)
        {
            fileChanged = false;
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                UnityEngine.Debug.LogWarning("El archivo está vacío.");
                return;
            }
            int x = 0;
            int y = 0;

            if (instantiatedPrefabs.Count > 0)
            {
                for (int i = 0; i < instantiatedPrefabs.Count; i++)
                {
                    DestroyImmediate(instantiatedPrefabs[i]);
                }
                instantiatedPrefabs.Clear();
                instantiatedPrefabs = new List<GameObject>();
            }
            foreach (string line in lines)
            {

                foreach(char c in line)
                {
                    Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
                    Vector3 position = topLeft + new Vector3(0.5f + x * tileSize,- 0.5f + -y * tileSize, 0); x++;
                    GameObject prefabToUse = null;
                    switch (c)
                    {
                        case 'R':
                            prefabToUse = redTilesPrefab;
                            break;
                        case 'G':
                            prefabToUse = greenTilesPrefab;
                            break ;
                        default:
                            prefabToUse = redTilesPrefab;
                            break;
                    }
                    if(prefabToUse != null)
                    {
                        instantiatedPrefabs.Add(Instantiate (prefabToUse, position, Quaternion.identity,transform));
                    }
                }
                x= 0;
                y++;
            } 
        }
    }
}
/*
 HACER FULL HERENCIA CON LAS 3 ULTIMAS FUNCIONES


CONTENIDO PREVIO FUNCIONAL SIN CLEAN CODE

using UnityEngine;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem.Controls;
using System.Text.RegularExpressions;
using UnityEngine.Rendering.Universal;

public class FileReader : MonoBehaviour
{
    bool fileChanged = false;
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
    [SerializeField] Light2D globalLight; //lvl3
    /// <summary>
    /// Nivel 5:
    [SerializeField] GameObject redTilesPrefab;
    [SerializeField] GameObject greenTilesPrefab;
    [SerializeField] float tileSize = 1;
    /// </summary>
    void Start()
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, "Level-5");
        if (!Directory.Exists(persistentDir))
        { 
                Directory.CreateDirectory(persistentDir); 
        }

        filePath = Path.Combine(Application.persistentDataPath, "Level-5/Puzzle.txt");
        systemWatcher = new FileSystemWatcher();
        systemWatcher.Path = Path.GetDirectoryName(filePath);
        systemWatcher.Filter = Path.GetFileName(filePath);
        systemWatcher.NotifyFilter = NotifyFilters.LastWrite;
        systemWatcher.EnableRaisingEvents = true;
         
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
        previousContent = File.ReadAllText(filePath);
        systemWatcher.Changed += OnFileChanged;
    }
    void OnFileChanged ( object source, FileSystemEventArgs e )
    {
        UnityEngine.Debug.Log("El archivo fue modificado " + e.FullPath);
        fileChanged = true;
    }

    void Update ()
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
        //Level3(filePath);
        //Level4(filePath);
        Level5(filePath);
    }
    void Level3(string fileName)
    {

        if (fileChanged)
        {
            fileChanged = false;
            ReadTextFile(filePath);
        }

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
        }
        print(keyIndex);
        VanishElement(keyIndex);
        print("he llegado aqui");
    }
    void VanishElement ( int index )
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
    void Level4 ( string fileName )
    {
        string[] lines = File.ReadAllLines(fileName);

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

        if (valueFound >= 0)
        {
            globalLight.intensity = valueFound;
            UnityEngine.Debug.Log($"Intensidad actualizada a: {valueFound}");
        }
        else
        {
            UnityEngine.Debug.LogWarning("No se encontró ningún valor numérico válido en el archivo.");
        }
        valueFound = Mathf.Clamp(valueFound, 0f, 360f);
        globalLight.pointLightInnerAngle = valueFound;
    }
    List<GameObject> instantiatedPrefabs = new List<GameObject>();
    void Level5 ( string fileName )
    {
        if (fileChanged)
        {
            fileChanged = false;
            string[] lines = File.ReadAllLines(fileName);

            if (lines.Length == 0)
            {
                UnityEngine.Debug.LogWarning("El archivo está vacío.");
                return;
            }
            int x = 0;
            int y = 0;

            if (instantiatedPrefabs.Count > 0)
            {
                for (int i = 0; i < instantiatedPrefabs.Count; i++)
                {
                    DestroyImmediate(instantiatedPrefabs[i]);
                }
                instantiatedPrefabs.Clear();
                instantiatedPrefabs = new List<GameObject>();
            }
            foreach (string line in lines)
            {

                foreach(char c in line)
                {
                    Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, Camera.main.nearClipPlane));
                    Vector3 position = topLeft + new Vector3(0.5f + x * tileSize,- 0.5f + -y * tileSize, 0); x++;
                    GameObject prefabToUse = null;
                    switch (c)
                    {
                        case 'R':
                            prefabToUse = redTilesPrefab;
                            break;
                        case 'G':
                            prefabToUse = greenTilesPrefab;
                            break ;
                        default:
                            prefabToUse = redTilesPrefab;
                            break;
                    }
                    if(prefabToUse != null)
                    {
                        instantiatedPrefabs.Add(Instantiate (prefabToUse, position, Quaternion.identity,transform));
                    }
                }
                x= 0;
                y++;
            } 
        }
    }
} 
*/