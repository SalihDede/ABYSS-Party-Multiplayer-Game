using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameOneManager : MonoBehaviour
{
    [Header("SemihY�lmazGame")]
    public GameObject Spawn0;
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject BallSpawn;

    void Start()
    {
        RandomMapGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomMapGenerator()
    {
        PhotonNetwork.Instantiate("TemplatePlayer", BallSpawn.transform.position, Quaternion.identity);
    }


}
