using System.IO;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public string path; 
    public string jsonFile;

    public class JsonContent {}
    public JsonContent jsonContent;

    public FileSystemWatcher watcher;

    public void SetWatcher()
    {
        //Primero indicamos que archivo y que cambios queremos que vigile
        watcher = new FileSystemWatcher();
        watcher.Path = Path.GetDirectoryName(path);
        watcher.Filter = Path.GetFileName(path);
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        watcher.Changed += OnFileChanged;
        watcher.EnableRaisingEvents = true;
    }
    public void SaveJson()
    {
        File.WriteAllText(path, jsonFile);
    }

    public void LoadJson()
    {
        jsonFile = File.ReadAllText(path);
        jsonContent = JsonUtility.FromJson<JsonContent>(jsonFile); 
    }

    public void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        LoadJson(); 
    }

}
