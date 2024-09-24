using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static AsyncOperation LoadScene(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }

    /*public static AsyncOperation UnloadScene(string sceneName)
    {
        return SceneManager.UnloadSceneAsync(sceneName);
    }*/
}
