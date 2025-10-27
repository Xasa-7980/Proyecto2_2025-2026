using System.IO;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class Watcher : MonoBehaviour
{
    public FileSystemWatcher watcher;
    public bool fileChanged; 
    public void SetWatcher(string path)
    {
        //Primero indicamos que archivo y que cambios queremos que vigile
        watcher = new FileSystemWatcher();
        watcher.Path = Path.GetDirectoryName("C:\\Users\\andre\\AppData\\LocalLow\\DefaultCompany\\Project2 - 64Studios");
        watcher.Filter = Path.GetFileName("Level3.json");
        watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;
        watcher.Changed += OnFileChanged;
        watcher.EnableRaisingEvents = true;
    }
    public void OnFileChanged(object sender, FileSystemEventArgs e)
    {
        fileChanged = true;
    }
}
