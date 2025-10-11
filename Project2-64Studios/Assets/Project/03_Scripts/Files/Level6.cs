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
            characterController.blockMovement = true;
            Vector3 dir = Vector3.zero;
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    switch (char.ToLower(c))
                    {
                        case 'r':
                            dir += Vector3.right;
                            characterController.Move(Vector3.right);
                            break;
                        case 'l':
                            dir += Vector3.left;
                            characterController.Move(Vector3.left);
                            break;
                        case 'u':
                            dir += Vector3.up;
                            characterController.Move(Vector3.up);
                            break;
                        case 'd':
                            dir += Vector3.down;
                            characterController.Move(Vector3.down);
                            break;
                        default:
                            dir += Vector3.right;
                            characterController.Move(Vector3.right);
                            break;
                    }
                }
            }
            if (dir == exit.transform.position)
            {
                isCompleted = true;
            }
        }
        fileChanged = false;
    }
}
