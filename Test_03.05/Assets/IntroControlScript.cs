using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControlScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) // Check if the Enter key is pressed
        {
            SceneManager.LoadScene("SampleScene"); // Load the main game scene
        }
    }
}
