using System.IO;
using UnityEngine;
using System;

public class FileWatcher : MonoBehaviour
{
    public string path; 
    private DateTime lastWriteTime;

    private void Start()
    {
        if (File.Exists(path))
            lastWriteTime = File.GetLastWriteTime(path);
        else
            Debug.Log("File on " +  path + " doesn't exist.");
    }
    public bool FileChanged()
    {
        DateTime currentWriteTime = File.GetLastWriteTime(path);

        if (currentWriteTime != lastWriteTime)
        {
            lastWriteTime = currentWriteTime;
            return true; 
        }

        return false;
    }
}
