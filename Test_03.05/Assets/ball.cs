using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ball : MonoBehaviourPun
{

    public GameObject DeathEffect;
    public GameObject GameManagerr;
    public bool SomeoneTouch = false;

    Rigidbody rb;

    void Start()
    {
        GameManagerr = GameObject.Find("SemihGame");

        GameManagerr.GetComponent<GameOneManager>().SpawnedBall = gameObject;
        DeathEffect.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        



    }

    IEnumerator Effectt()
    {
        yield return new WaitForSeconds(3);
        DeathEffect.SetActive(true);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            GetComponent<PhotonView>().TransferOwnership(other.gameObject.GetComponent<PhotonView>().OwnerActorNr);
            Debug.Log(other.gameObject.GetComponent<PhotonView>().OwnerActorNr);
            SomeoneTouch = true;
        }


    }
}