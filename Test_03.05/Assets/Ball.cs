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
        if (other.tag == "Bullet")
        {
            GetComponent<PhotonView>().TransferOwnership(other.gameObject.GetComponent<PhotonView>().Controller);
            Debug.Log(other.gameObject.GetComponent<PhotonView>().Controller);
        }
    }
}