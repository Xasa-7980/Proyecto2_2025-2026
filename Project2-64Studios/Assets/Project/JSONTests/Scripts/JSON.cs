using System.IO;
using UnityEngine;

public class JSON : MonoBehaviour
{
    public string path; 
    public string jsonFile;

    public virtual void SetJson() { }
    public virtual void SaveJson() { }

    public virtual void LoadJson() { }

    public virtual void LoadLevel() { } 
}
