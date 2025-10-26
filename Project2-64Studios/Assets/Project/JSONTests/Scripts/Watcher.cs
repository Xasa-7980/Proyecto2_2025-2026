using System.IO;
using UnityEngine;

public class Watcher : MonoBehaviour
{
    public FileSystemWatcher watcher;
    public bool fileChanged; 
    public void SetWatcher(string path)
    {
        //Primero indicamos que archivo y que cambios queremos que vigile
        watcher = new FileSystemWatcher();
        watcher.Path = Path.GetDirectoryName(Application.persistentDataPath);
        watcher.Filter = Path.GetFileName(path);
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        watcher.Changed += OnFileChanged;
        watcher.EnableRaisingEvents = true;
    }
    public void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        fileChanged = true;
    }
}
