using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviour
{
    [Header("SemihYýlmazGame")]

    public TMP_Text LastTouch;
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
