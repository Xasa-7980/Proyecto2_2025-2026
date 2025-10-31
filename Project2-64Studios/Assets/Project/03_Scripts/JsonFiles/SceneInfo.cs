using System.IO;
using UnityEngine;

public class SceneInfo
{
    public string filePath;
    public string fileName;
    public string directoryPath;
    protected FileSystemWatcher systemWatcher;
    private void InitializeDirectory(ref string filePath)
    {
        string persistentDir = Path.Combine(Application.persistentDataPath, Application.persistentDataPath);
        if (!Directory.Exists(persistentDir))
        {
            Directory.CreateDirectory(persistentDir);
        }

        filePath = Path.Combine(Application.persistentDataPath, directoryPath + "/" + fileName);

        if (!File.Exists(filePath))
        {
            string sourceFile = Path.Combine(Application.streamingAssetsPath, filePath);
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
    }

}