using UnityEngine;
using Photon.Pun;

public class PlayerSpawnerManager : MonoBehaviourPunCallbacks
{
    // Array of spawn points
    public Transform[] spawnPoints;

    void Start()
    {
        // Check if this is the local player's client
        if (photonView.IsMine)
        {
            // Get a random spawn point for the local player
            Transform spawnPoint = GetRandomSpawnPoint();

            // Instantiate the player prefab at the chosen spawn point
            PhotonNetwork.Instantiate("PlayerPrefab", spawnPoint.position, spawnPoint.rotation);
        }
    }

    // Method to get a random spawn point
    private Transform GetRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}