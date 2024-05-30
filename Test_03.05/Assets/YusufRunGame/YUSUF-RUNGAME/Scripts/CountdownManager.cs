using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class CountdownManager : MonoBehaviourPunCallbacks
{
    public Text countdownText; // Reference to the UI Text component for displaying the countdown
    public float countdownDuration = 10f; // Duration of the countdown in seconds

    private bool isCountdownStarted = false;

    private void Start()
    {
        // Ensure that only one client starts the countdown
        if (PhotonNetwork.IsMasterClient)
        {
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        isCountdownStarted = true;
        photonView.RPC("SyncCountdown", RpcTarget.Others); // Inform other clients to start the countdown
        StartCoroutine(CountdownCoroutine());
    }

    [PunRPC]
    private void SyncCountdown()
    {
        isCountdownStarted = true;
        StartCoroutine(CountdownCoroutine());
    }

    private System.Collections.IEnumerator CountdownCoroutine()
    {
        float currentTime = countdownDuration;

        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText(Mathf.CeilToInt(currentTime)); // Round up to the nearest second
            yield return null;
        }

        // Countdown finished, start the game
        StartGame();
    }

    private void UpdateCountdownText(int secondsRemaining)
    {
        countdownText.text = $"Game starts in {secondsRemaining} seconds";
    }

    private void StartGame()
    {
        // Implement your game start logic here
        Debug.Log("Game started!");
    }
}