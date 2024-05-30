using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MineKill : MonoBehaviourPunCallbacks
{
    public GameObject explosionPrefab; // Assign your explosion prefab in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the "Bullet" tag and if this object is the master client
        if (other.CompareTag("Bullet") && photonView.IsMine)
        {
            Debug.Log("MINE EXPLODE!!");

            // RespawnPlayer RPC'sini çaðýrýrken oyuncunun PhotonView'sini gönder
            photonView.RPC("RespawnPlayer", RpcTarget.All, other.GetComponent<PhotonView>());
            photonView.RPC("SpawnExplosionEffect", RpcTarget.All);
            PhotonNetwork.Destroy(gameObject);
        }
    }

    [PunRPC]
    private void SpawnExplosionEffect()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    }

    [PunRPC]
    private void RespawnPlayer(PhotonView playerView)
    {
        // Get the checkpoint object
        GameObject checkpointObject = GameObject.FindGameObjectWithTag("FirstCheckpoint");

        // If the checkpoint object is found
        if (checkpointObject != null)
        {
            // Check if the playerView belongs to this client
            if (playerView.IsMine)
            {
                // Get the player's position and rotation components
                Transform playerTransform = playerView.gameObject.GetComponent<Transform>();

                // Teleport the player to the checkpoint position
                playerTransform.position = checkpointObject.transform.position;

                // Reset the player's rotation to match the checkpoint rotation
                playerTransform.rotation = checkpointObject.transform.rotation;
               
            }
        }
        else
        {
            Debug.LogWarning("No checkpoint object found in the scene.");
        }
    }
}