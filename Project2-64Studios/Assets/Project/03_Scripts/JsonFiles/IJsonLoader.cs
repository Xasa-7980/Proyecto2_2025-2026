using UnityEngine;

public interface IJsonLoader
{
    void LoadJSON<T>(string path, out T content);
}