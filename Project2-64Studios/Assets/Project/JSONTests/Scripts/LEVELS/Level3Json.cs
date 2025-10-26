using System.IO;
using UnityEngine;

public class Level3Json : JSON
{
    public class Level3Content : JsonContent
    {
        public bool FirstWallActive = true;
        public bool SecondWallActive = true;
        public bool ThirdWallActive = true;
        public bool ForthWallActive = true;
        public bool FifthWallActive = true;
    }

    private void Awake()
    {
        jsonContent = new Level3Content();
        path = Path.Combine(Application.persistentDataPath, "Level3.json");
        jsonFile = JsonUtility.ToJson(jsonContent, true);

        SetWatcher(); 

        SaveJson();
        LoadJson();
    }

    public void Start()
    {
        
    }

}
