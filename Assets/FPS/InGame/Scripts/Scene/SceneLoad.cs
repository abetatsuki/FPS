
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoad : MonoBehaviour
{
    public static SceneLoad Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        { 
            Destroy(gameObject);
        }
    }

    public  void SceneLoader(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
