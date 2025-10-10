using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level7 : FileManager
{
    public List<char> keysPressed;
    public Level7 ( string _directoryPath, string _fileName ) : base(_directoryPath, _fileName)
    {
        keysPressed = new List<char>();
        originalContent = File.ReadAllText(filePath);
    }
    public override void LevelMechanics ( )
    {
        Event e = Event.current;
        if (e.isKey)
        {
            keysPressed.Add(e.keyCode.ToString()[0]);
        }

        string storedLine = "";
        for (int i = 0; i < keysPressed.Count; i++)
        {
            storedLine += keysPressed[i];
        }
        if (storedLine != "rullrdu")
        {
            keysPressed.Clear();
            return;
        }
        //Completado escenario
    }

}
