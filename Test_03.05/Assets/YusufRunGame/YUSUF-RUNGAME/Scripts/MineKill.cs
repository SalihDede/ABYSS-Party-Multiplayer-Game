using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineKill : MonoBehaviour
{
    public Transform checkpoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Move the player to the checkpoint position
            other.transform.position = checkpoint.position;
        }
    }
}
