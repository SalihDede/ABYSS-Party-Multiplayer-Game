using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameThreeManager : MonoBehaviour
{
    public GameObject GameManagerr;
    public GameObject CharacterSpawn;
    void Start()
    {
        GameManagerr = GameObject.Find("GameManager");


            RandomMapGenerator();
    
      
    }

    public void RandomMapGenerator()
    {
        GameObject player = PhotonNetwork.Instantiate("YusufGamePlayer", CharacterSpawn.transform.position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
