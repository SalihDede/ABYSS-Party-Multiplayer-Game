using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageToggle : MonoBehaviour
{
    public Image targetImage;  // Reference to the UI Image component
    private bool isImageVisible = false;  // Initially, the image is not visible

    void Start()
    {
        // Hide the image initially
        targetImage.gameObject.SetActive(false);
        // Start a coroutine to show the image after a delay
        StartCoroutine(ShowImageAfterDelay(15f));
    }

    void Update()
    {
        // Toggle image visibility when the "M" key is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleImageVisibility();
        }
    }

    IEnumerator ShowImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // Make the image visible
        targetImage.gameObject.SetActive(true);
        isImageVisible = true;
    }

    void ToggleImageVisibility()
    {
        isImageVisible = !isImageVisible;
        targetImage.gameObject.SetActive(isImageVisible);
    }
}
