using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPun
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Photon ağına bağlı mı kontrolü yapılır
        if (PhotonNetwork.IsConnected)
        {
            // Tüm oyuncuların topu kontrol etmesine izin verilir
            rb.isKinematic = false;
        }
        else
        {
            // Photon ağına bağlı değilse, yani oyuncular tek başına oyunu oynuyorsa, yalnızca yerel oyuncunun topu kontrol etmesine izin verilir
            rb.isKinematic = !photonView.IsMine;
        }
    }

    // Topun pozisyonunu senkronize etmek için RPC
    [PunRPC]
    void SyncBallPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    // Topun rotasyonunu senkronize etmek için RPC
    [PunRPC]
    void SyncBallRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;
    }

    // Diğer oyunculara topun pozisyonunu senkronize eden metod
    public void SendBallPosition(Vector3 newPosition)
    {
        photonView.RPC("SyncBallPosition", RpcTarget.Others, newPosition);
    }

    // Diğer oyunculara topun rotasyonunu senkronize eden metod
    public void SendBallRotation(Quaternion newRotation)
    {
        photonView.RPC("SyncBallRotation", RpcTarget.Others, newRotation);
    }
}
