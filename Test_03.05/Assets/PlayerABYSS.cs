using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerABYSS : MonoBehaviourPunCallbacks
{


    private PhotonView photonView;
    public GameObject GameOneManager;
    public Animator Anim;
    public int score;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        GameOneManager = GameObject.Find("SemihGame");

        GameOneManager.GetComponent<GameOneManager>().Starters.Add(gameObject);

        gameObject.name = "SemihGamePlayer" +gameObject.GetComponent<PhotonView>().OwnerActorNr.ToString();
    }

    // Update is called once per frame
    void Update()
    {


        if(score == 100)
        {
            photonView.RPC("Finito", RpcTarget.All, true);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            Anim.SetBool("IsRun", true);
        }
        else
        {
            Anim.SetBool("IsRun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Anim.SetBool("IsJump", true);
        }
        else
        {
            Anim.SetBool("IsJump", false);
        }
        
        
    }


    [PunRPC]
    void Finito(bool result)
    {
        GameOneManager.GetComponent<GameOneManager>().GameFinished = result;
    }

    private void OnTriggerStay(Collider other)
{
    if (other.tag == "Ball")
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();

            // Aşırma şut hedef noktası, topun şu anki konumu ve bir miktar öne, yukarı ve ileri alınarak belirlenebilir.
            Vector3 targetPoint = other.transform.position + other.transform.forward * 20f + Vector3.up * 1f + transform.forward * 20f;

            // Aşırma şut hızı ve yönü topun bulunduğu konumdan hedef noktasına doğru belirlenebilir.
            Vector3 shootDirection = (targetPoint - other.transform.position).normalized;
            float shootSpeed = 25f; // Aşırma şutun hızı

            // Topa kuvvet uygulanması
            rb.velocity = shootDirection * shootSpeed;
        }
    }
}

}



