using UnityEngine;
using UnityEngine.UI; // Import UI library
using Photon.Pun;

public class StartingCountdown : MonoBehaviourPun
{
    public float timeToDisappear = 10f;
    public Text countdownText; // Drag & drop your UI Text component here

    private float timer;

    void Start()
    {
        timer = timeToDisappear;

        // Initialize the countdown text (only on the master client)
        if (PhotonNetwork.IsMasterClient && countdownText != null)
        {
            countdownText.gameObject.SetActive(true); // Ensure it's visible
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            timer -= Time.deltaTime;

            // Update countdown text
            if (countdownText != null)
            {
                countdownText.text = Mathf.CeilToInt(timer).ToString();
            }

            if (timer <= 0f)
            {
                // Destroy the object
                PhotonNetwork.Destroy(gameObject);

                // Hide the countdown text
                if (countdownText != null)
                {
                    countdownText.gameObject.SetActive(false);
                }
            }
        }
    }
}