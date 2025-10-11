using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class FileController : MonoBehaviour
{
    [SerializeField] FileManager curFileLevel;

    [SerializeField] List<GameObject> gameObjectReferencesDictionary = new List<GameObject>();
    [SerializeField] Light2D levelLight;
    [SerializeField] GameObject redTiles;
    [SerializeField] GameObject greenTiles;
    [SerializeField] float tileSize = 1;
    [SerializeField] CharacterController characterController;
    [SerializeField] GameObject exit;

    bool fileIsOpen;
    Dictionary<string,FileManager> files = new Dictionary<string,FileManager>();
    Timer sixSec_Timer;
    Timer threeSec_Timer;
    Timer oneSec_Timer;
    bool reachedExit;
    
    private void Start ( )
    {
        characterController = GetComponent<CharacterController>();
        sixSec_Timer = new Timer(this);
        threeSec_Timer = new Timer(this);
        oneSec_Timer = new Timer(this);
        if(SceneManager.GetActiveScene().name == "Level3")
        {
            SearchGameObjects();
        }
        else if (SceneManager.GetActiveScene().name == "Level4")
        {
            SearchLight();
        }
        else if (SceneManager.GetActiveScene().name == "Level6")
        {
            exit = SearchExit();

        }
        files["Level3"] = new Level3(Path.Combine(Application.streamingAssetsPath, "Level-3"), "Puzzle.txt", orderedObjects);
        files["Level4"] = new Level4(Path.Combine(Application.streamingAssetsPath, "Level-4"), "Puzzle.txt", levelLight);
        files["Level5"] = new Level5(Path.Combine(Application.streamingAssetsPath, "Level-5"), "Puzzle.txt", redTiles,greenTiles,tileSize);
        files["Level6"] = new Level6(Path.Combine(Application.streamingAssetsPath, "Level-6"), "Puzzle.txt", characterController, exit);
        files["Level7"] = new Level7(Path.Combine(Application.streamingAssetsPath, "Level-7"), "Puzzle.txt");
        
        curFileLevel = files[SceneManager.GetActiveScene().name];
        if (SceneManager.GetActiveScene().name == "Level5")
        {
            ((Level5)curFileLevel).DrawLevel();
        }
        if (SceneManager.GetActiveScene().name == "Level6")
        {
            characterController.blockMovement = true;
        }
        else
        {
            characterController.blockMovement = false;
        }
    }
    void SetFileLevel ( string fileName )
    {
        curFileLevel = files[fileName];
    }
    private void Update ( )
    {
        if(curFileLevel != null)
        {
            if (SceneManager.GetActiveScene().name == "Level3")
            {
                if (!threeSec_Timer.Timer_Started() && !fileIsOpen && !curFileLevel.GetState() )
                {
                    threeSec_Timer.StartTimer(3, 
                        ( ) => { curFileLevel.OpenDirectory(); fileIsOpen = true; },
                        Action_Timing.End);
                }
            }
            else if (SceneManager.GetActiveScene().name == "Level5")
            {
                if (!sixSec_Timer.Timer_Started())
                {
                    sixSec_Timer.StartTimer(6,
                        ( ) => { curFileLevel.OpenDirectory(); fileIsOpen = true; },
                        Action_Timing.End);
                }
            }
            else
            {
                if (!oneSec_Timer.Timer_Started())
                {
                    oneSec_Timer.StartTimer(1,
                        ( ) => { curFileLevel.OpenDirectory(); fileIsOpen = true; },
                        Action_Timing.End);
                }
            }
            if (curFileLevel.fileChanged)
            {
                curFileLevel.LevelMechanics();
                fileIsOpen = false;
            }
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

    }
    void SearchLight ( )
    {
        Light2D light = FindObjectOfType<Light2D>();
        levelLight = light;
    }
    GameObject SearchExit()
    {
        GameObject gO = FindObjectsOfType<GameObject>().Where(obj => obj.layer == 3).FirstOrDefault();
        return gO;
    }
    public void ResetFiles( )
    {
        foreach (var elem in files)
        {
            elem.Value.ResetFiles();
        }
    }
}
