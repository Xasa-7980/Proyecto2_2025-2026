using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSystem : MonoBehaviour
{
    public static SceneSystem instance {  get; private set; }
    private void Awake ( )
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public int sceneIndex = 0;
    public void LoadNextScene ( )
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //print("He tocado la salida " + SceneSystem.instance.sceneIndex);
        if(sceneIndex > 8)
        {
            return;
        }
        //print("He hola" + SceneSystem.instance.sceneIndex);
        SceneManager.LoadScene(sceneIndex);
    }
}
