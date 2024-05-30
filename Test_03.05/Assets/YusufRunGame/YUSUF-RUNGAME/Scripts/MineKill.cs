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
            RespawnPlayer(other.gameObject);
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
    private void RespawnPlayer(GameObject playerObject)
    {
        // Get the checkpoint object
        GameObject checkpointObject = GameObject.FindGameObjectWithTag("FirstCheckpoint");

        // If the checkpoint object is found
        if (checkpointObject != null)
        {
            // Get the player's position and rotation components
            Transform playerTransform = playerObject.GetComponent<Transform>();

            // Teleport the player to the checkpoint position
            playerTransform.position = checkpointObject.transform.position;

            // Reset the player's rotation to match the checkpoint rotation
            playerTransform.rotation = checkpointObject.transform.rotation;

            // If the player has a CharacterController component, reset its velocity
       
        }
        else
        {
            Debug.LogWarning("No checkpoint object found in the scene.");
        }
    }
}