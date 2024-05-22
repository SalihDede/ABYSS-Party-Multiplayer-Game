using UnityEngine;
using UnityEngine.SceneManagement;

public class TrailerControlScript : MonoBehaviour
{
    void Update()
    {
        // Check if the Enter key or Escape key is pressed
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene"); // Load the main game scene
        }
    }
}
