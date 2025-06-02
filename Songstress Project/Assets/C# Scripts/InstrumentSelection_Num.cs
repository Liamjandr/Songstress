using UnityEngine;
using UnityEngine.SceneManagement;

public class InstrumentSelection_Num : MonoBehaviour
{
    public int buttonIndex; // Set this in the Inspector

    public void OnMouseDown()
    {
        Debug.Log("Button clicked: " + buttonIndex);
        // You can use this value for selection, scene change, etc.
        HandleSelection(buttonIndex);


    }

    private void HandleSelection(int index)
    {
        
    }
}
