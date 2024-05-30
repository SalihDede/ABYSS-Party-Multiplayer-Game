using UnityEngine;
using Photon.Pun;

public class ObjectDisabler : MonoBehaviourPun
{
    public float timeToDisappear = 10f; // Seconds until the object disappears

    private float timer;

    void Start()
    {
        timer = timeToDisappear; // Initialize the timer
    }

    void Update()
    {
        // Only the master client manages the timer
        if (PhotonNetwork.IsMasterClient)
        {
            timer -= Time.deltaTime;

            // Check if it's time to disable the object
            if (timer <= 0f)
            {
                PhotonNetwork.Destroy(gameObject); // Destroy for all players
            }
        }
    }
}