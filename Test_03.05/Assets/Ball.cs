using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ball : MonoBehaviourPun
{
    Rigidbody rb;
    Photon.Realtime.Player ownerPlayer; // Topun sahibini tutmak için

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ownerPlayer = photonView.Owner; // Topun sahibini ayarla
        if (PhotonNetwork.IsMasterClient) // Eğer mevcut oyuncu ev sahibi ise
        {
            photonView.TransferOwnership(ownerPlayer); // Topun sahibini belirle
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected && ownerPlayer != null && PhotonNetwork.LocalPlayer != ownerPlayer)
        {
            rb.isKinematic = true; // Sadece topun sahibi tarafından kontrol edilmesini sağla
        }
        else
        {
            rb.isKinematic = false; // Diğer oyuncuların topu kontrol etmesine izin ver
        }
    }
}
