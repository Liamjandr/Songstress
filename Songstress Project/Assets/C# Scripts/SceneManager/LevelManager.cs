using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
