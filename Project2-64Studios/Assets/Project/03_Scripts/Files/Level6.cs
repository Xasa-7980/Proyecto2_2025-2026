using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Level6 : FileManager
{
    public CharacterController characterController;
    public GameObject exit;
    public Level6 ( string _directoryPath, string _fileName, CharacterController characterController, GameObject _exit ) : base(_directoryPath, _fileName)
    {
        this.characterController = characterController;
        exit = _exit;
        originalContent = File.ReadAllText(filePath);
    }
    public override void LevelMechanics ( )
    {
        if (fileChanged)
        {
            fileChanged = false;
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    switch (char.ToLower(c))
                    {
                        case 'r':
                            characterController.Move(Vector3.right);
                            break;
                        case 'l':
                            characterController.Move(Vector3.left);
                            break;
                        case 'u':
                            characterController.Move(Vector3.up);
                            break;
                        case 'd':
                            characterController.Move(Vector3.down);
                            break;
                        default:
                            characterController.Move(Vector3.right);
                            break;
                    }
                }
            }
        }
        if(characterController.transform == exit.transform)
        {
            isCompleted = true;
        }
        fileChanged = false;
    }
}
