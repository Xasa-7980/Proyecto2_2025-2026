using System.IO;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public string path; 
    public string jsonFile;
    public Watcher watcher; 

    public class JsonContent {}
    public JsonContent jsonContent;

    
    public void SaveJson()
    {
        File.WriteAllText(path, jsonFile);
    }

    public void LoadJson()
    {
        jsonFile = File.ReadAllText(path);
        jsonContent = JsonUtility.FromJson<JsonContent>(jsonFile); 
    }

    private void CheckFileChange()
    {
        if (watcher.fileChanged)
            LoadLevel();
    }
    public virtual void LoadLevel() { } 
}
