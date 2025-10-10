using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FileController : MonoBehaviour
{
    [SerializeField] FileManager curFileLevel;

    [SerializeField] List<Level3.GameObjectReferencesDictionary> gameObjectReferencesDictionary = new List<Level3.GameObjectReferencesDictionary>();
    [SerializeField] Light2D levelLight;
    [SerializeField] GameObject redTiles;
    [SerializeField] GameObject greenTiles;
    [SerializeField] float tileSize = 1;
    [SerializeField] CharacterController characterController;

    Dictionary<string,FileManager> files = new Dictionary<string,FileManager>();
    private void Start ( )
    {
        characterController = GetComponent<CharacterController>();
        files["Level3"] = new Level3(Path.Combine(Application.streamingAssetsPath, "Level-3"), "Puzzle.txt", gameObjectReferencesDictionary);
        files["Level4"] = new Level4(Path.Combine(Application.streamingAssetsPath, "Level-4"), "Puzzle.txt", levelLight);
        files["Level5"] = new Level5(Path.Combine(Application.streamingAssetsPath, "Level-5"), "Puzzle.txt", redTiles,greenTiles,tileSize);
        files["Level6"] = new Level6(Path.Combine(Application.streamingAssetsPath, "Level-6"), "Puzzle.txt", characterController);
        files["Level7"] = new Level7(Path.Combine(Application.streamingAssetsPath, "Level-7"), "Puzzle.txt");
        
        curFileLevel = files["Level3"]; 
        SearchGameObjects();
        if(curFileLevel == files["Level4"])
        {
            SearchLight();
        }
    }
    void SetFileLevel ( string fileName )
    {
        curFileLevel = files[fileName];
    }
    private void Update ( )
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            curFileLevel.OpenDirectory();
        }
        if (curFileLevel.fileChanged)
        {
            curFileLevel.LevelMechanics();
        }
    }
    public List<GameObject> orderedObjects = new List<GameObject>();

    void SearchGameObjects ()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        var filtered = allObjects.Where(obj => obj.layer == 8);


        orderedObjects = filtered
            .OrderBy(obj =>
            {
                if (int.TryParse(obj.name, out int num))
                    return num;
                else
                    return int.MaxValue;
            })
            .ToList();

        // 4️ Mostrar el resultado por consola
        Debug.Log("Objetos encontrados y ordenados:");
        foreach (var obj in orderedObjects)
        {
            Debug.Log(obj.name);
        }
    }
    void SearchLight ( )
    {
        Light2D light = FindObjectOfType<Light2D>();
        levelLight = light;
    }
}
