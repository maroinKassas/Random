using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static AsyncOperation LoadScene(string sceneName)
    {
        return SceneManager.LoadSceneAsync(sceneName);
    }
}
