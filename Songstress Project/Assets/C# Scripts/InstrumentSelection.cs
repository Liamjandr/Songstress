using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrumentSelection : MonoBehaviour
{
    public int buttonIndex;
    public string nextSceneName = "Forest";
    public void OnMouseDown()
    {
        InstrummentManager.SelectedInstrument = buttonIndex;
        Debug.Log("Instrument selected: " + buttonIndex);
        // Now load scene or proceed
        LoadNextScene();
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
