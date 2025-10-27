using System.IO;
using UnityEngine;

public class Level3Json : JSON
{
    public Level3Content level3Content; 
    public Level3Content originalLevel3Content; 
    public class Level3Content
    {
        public bool FirstWallActive = true;
        public bool SecondWallActive = true;
        public bool ThirdWallActive = true;
        public bool ForthWallActive = true;
        public bool FifthWallActive = true;
    }
    public override void SetJson()
    {
        path = Path.Combine(Application.persistentDataPath, "Level3.json");
        jsonFile = JsonUtility.ToJson(originalLevel3Content, true);
    }
    public override void SaveJson()
    {
        File.WriteAllText(path, jsonFile);
    }

    public override void LoadJson()
    {
        jsonFile = File.ReadAllText(path);
        level3Content = JsonUtility.FromJson<Level3Content>(jsonFile);
    }

    private void Awake()
    {
        SetJson();
        SaveJson();
        LoadJson();
    }
}
