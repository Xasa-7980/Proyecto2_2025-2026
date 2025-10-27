using System.IO;
using UnityEngine;

public class Level3Manager : MonoBehaviour
{
    public FileWatcher level3Watcher;
    public Level3Json level3Json;

    [SerializeField] private GameObject Wall1;
    [SerializeField] private GameObject Wall2;
    [SerializeField] private GameObject Wall3;
    [SerializeField] private GameObject Wall4;
    [SerializeField] private GameObject Wall5;

    private void Update()
    {
        if (level3Watcher.FileChanged())
            LoadLevel();
    }

    private void LoadLevel()    
    {
        Debug.Log("Se ha cambiado el archivo"); 
        Debug.Log(level3Json.level3Content.FirstWallActive);
        Wall1.SetActive(level3Json.level3Content.FirstWallActive);
        Wall2.SetActive(level3Json.level3Content.SecondWallActive);
        Wall3.SetActive(level3Json.level3Content.ThirdWallActive);
        Wall4.SetActive(level3Json.level3Content.ForthWallActive);
        Wall5.SetActive(level3Json.level3Content.FifthWallActive);
    }
}
