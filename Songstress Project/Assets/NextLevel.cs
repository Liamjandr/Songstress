using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextSceneName = "Forest"; // Change to your actual scene name
    public float delayBeforeSceneSwitch = 1f; // Duration of animation
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("void"))
        {
            Debug.Log("hs");
            SceneManager.LoadScene(nextSceneName);
        }

    }
    void LoadNextScene()
    {
        
    }
}
