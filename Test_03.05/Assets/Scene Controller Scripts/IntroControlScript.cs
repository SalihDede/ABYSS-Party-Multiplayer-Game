using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroControlScript : MonoBehaviour
{
    // Time to wait before transitioning to the next scene
    public float transitionTime = 11f;

    // Name of the next scene to load
    public string nextSceneName = "Trailer";

    void Start()
    {
        // Invoke the TransitionToNextScene method after the specified time
        Invoke("TransitionToNextScene", transitionTime);
    }

    void TransitionToNextScene()
    {
        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
