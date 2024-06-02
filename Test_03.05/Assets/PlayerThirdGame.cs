using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using TMPro;

public class PlayerThirdGame : MonoBehaviour
{
    private PhotonView photonView;
    public GameObject GameManagerr;
    public GameObject CP2;
    public GameObject CP1;
    public bool isDeath;
    public bool isDeath2;


    void Start()
    {
        photonView = gameObject.GetComponent<PhotonView>();
        CP2 = GameObject.Find("seksenseksen");
        CP1 = GameObject.Find("seksen");
        GameManagerr = GameObject.Find("YusufGame");
        GameManagerr.GetComponent<GameThreeManager>().StartingMans.Add(gameObject);
    }

 
    void Update()
    {
  
        


        if(isDeath)
        {

            isDeath = false;
            if (photonView.IsMine)
            {
                PhotonNetwork.Instantiate("YusufGamePlayer", CP2.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }

        if (isDeath2)
        {

            isDeath2 = false;
            if (photonView.IsMine)
            {
                PhotonNetwork.Instantiate("YusufGamePlayer", CP1.transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }


    }
}
