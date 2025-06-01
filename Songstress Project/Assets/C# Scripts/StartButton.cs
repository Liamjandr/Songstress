using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Animator songstressAnimator;
    public string animationTriggerName = "Title"; // Set this in Animator
    public string nextSceneName = "SecondScene"; // Change to your actual scene name
    public float delayBeforeSceneSwitch = 2f; // Duration of animation

    private bool clicked = false;

    void OnMouseDown()
    {
        if (clicked) return; // Prevent double clicking
        clicked = true;

        if (songstressAnimator != null)
        {
            songstressAnimator.SetTrigger(animationTriggerName);
        }

        Invoke("LoadNextScene", delayBeforeSceneSwitch);
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}
