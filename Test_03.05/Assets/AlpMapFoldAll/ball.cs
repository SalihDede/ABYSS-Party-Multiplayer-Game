using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ball : MonoBehaviourPun
{

    public GameObject DeathEffect;
    public GameObject GameManagerr;

    Rigidbody rb;

    void Start()
    {
        GameManagerr = GameObject.Find("SemihGame");
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
        }

        if(other.gameObject.name == "SemihGamePlayer1")
        {
            GameManagerr.GetComponent<GameOneManager>().Player1 += 1;
        }
        if (other.gameObject.name == "SemihGamePlayer2")
        {
            GameManagerr.GetComponent<GameOneManager>().Player2 += 1;
        }


        if (other.tag == "NET1")
        {
            GameManagerr.GetComponent<GameOneManager>().Goal = true;
            StartCoroutine(Effectt());
            Destroy(gameObject);
        }

    }
}