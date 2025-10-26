using System.IO;
using UnityEngine;

public class Level3Json : JSON
{
    public Level3Content level3Content;
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
        level3Content = new Level3Content();
        path = Path.Combine(Application.persistentDataPath, "Level3.json");
        jsonFile = JsonUtility.ToJson(level3Content, true);


        SaveJson();
        LoadJson();

        watcher.SetWatcher("Level3.json"); 
    }
}
