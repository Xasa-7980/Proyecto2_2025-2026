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
    public void LoadNextScene ( )
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(sceneIndex > SceneManager.GetAllScenes().Length)
        {
            return;
        }
        SceneManager.LoadScene(sceneIndex);
    }
}
