using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    List<AsyncOperation> scenesToLoad;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = '1'; i < '3'; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
                SceneManager.LoadScene((int)i - (i - 1), LoadSceneMode.Additive);
            if (SceneManager.GetActiveScene().buildIndex == i)
            {
                //Abrir 
            }
        }
    }
    IEnumerator LoaderAsync(float it)
    {
        //float totalProgress = 0;
        //for (int i = 0; i < scenesToLoad.Count; i++)
        //{
        //    while (scenesToLoad[i])
        //    {
        //        totalProgress += scenesToLoad[i].progress;
        yield return null;
        //    }
        //}

    }
}
