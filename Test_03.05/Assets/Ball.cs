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
            // Photon ağı bağlantısı mevcut ise her oyuncunun topu kontrol etmesine izin verilir
            rb.isKinematic = false;
        }
        else
        {
            // Photon ağına bağlı değilse, yani oyuncular tek başına oyunu oynuyorsa, yalnızca yerel oyuncunun topu kontrol etmesine izin verilir
            rb.isKinematic = !photonView.IsMine;
        }
    }
}
