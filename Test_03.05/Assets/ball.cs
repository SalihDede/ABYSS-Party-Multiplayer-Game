using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ball : MonoBehaviourPun
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {


        


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "0" || other.name == "1")
        {
            GetComponent<PhotonView>().TransferOwnership(other.gameObject.GetComponent<PhotonView>().Controller);
            
        }
    }
}