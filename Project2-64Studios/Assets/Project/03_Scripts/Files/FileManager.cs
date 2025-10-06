using System;
using System.Collections.Concurrent;
using System.IO;
using UnityEngine;

public abstract class FileManager
{
    public string filePath;
    public bool fileChanged;
    public string[] lines;
    public FileSystemWatcher systemWatcher;
    ConcurrentQueue<Action> actionQueue = new ConcurrentQueue<Action>();
    public FileManager ( string _dirPath, string _filePath )
    {
        InitializeDirectory(_dirPath,_filePath);
        InitializeSystemWatcher();
        systemWatcher.Changed += OnFileChanged;

    }
    public abstract void OnFileChanged ( object source, FileSystemEventArgs e );
    public abstract string[] GetFileContent ( );
    public abstract void LevelMechanics ( );
    public abstract void InitializeDirectory ( string directoryPath, string filePath );
    public abstract void OpenDirectory ( );
    public abstract void InitializeSystemWatcher ( );
}
