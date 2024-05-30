using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviourPunCallbacks
{
    public Text finishMessageText; // Reference to the UI Text component for displaying the finish message

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerFinishedGame(other.gameObject);
        }
    }

    private void PlayerFinishedGame(GameObject playerObject)
    {
        // Get the player's Photon view
        PhotonView playerPhotonView = playerObject.GetComponent<PhotonView>();

        // Check if the player is the local player
        if (playerPhotonView.IsMine)
        {
            // Call a RPC to notify all clients that the player finished the game
            playerPhotonView.RPC("NotifyPlayerFinished", RpcTarget.All, PhotonNetwork.LocalPlayer.ActorNumber);
        }
    }

    [PunRPC]
    private void NotifyPlayerFinished(int playerActorNumber)
    {
        string finishMessage = $"Player {playerActorNumber} finished the game!";
        Debug.Log(finishMessage);

        // Call a RPC to display the finish message on all clients
        photonView.RPC("DisplayFinishMessage", RpcTarget.All, finishMessage);
    }

    [PunRPC]
    private void DisplayFinishMessage(string message)
    {
        finishMessageText.text = message;
    }
}