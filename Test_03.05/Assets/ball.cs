using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BallController : MonoBehaviourPun
{
    Rigidbody rb;
    public float speed = 10f; // Adjust speed as needed

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if (!photonView.IsMine)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true; // Di�er oyuncular�n topu kontrol etmemesi i�in Rigidbody'yi kinematik yap�n
        }
    }


}
