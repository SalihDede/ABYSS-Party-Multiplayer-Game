using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviourPunCallbacks
{

    public bool Goal;
    public GameObject BallPrefab;

    public int Player1;
    public int Player2;
    public int Player3;
    public int Player4;

    public TMP_Text Player1Text;
    public TMP_Text Player2Text;

    public TMP_Text LastTouch;

    public GameObject Spawn0;
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject BallSpawn;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (!photonView.IsMine)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.isKinematic = true; // Diðer oyuncularýn topu kontrol etmemesi için Rigidbody'yi kinematik yapýn
        }

        Goal = true;
        RandomMapGenerator();
    }

    IEnumerator GoalCoroutine()
    {
        yield return new WaitForSeconds(3);

        PhotonNetwork.Instantiate("Soccer Ball", BallSpawn.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Goal)
        {
            Goal = false;
            StartCoroutine(GoalCoroutine());
        }
        Player1Text.text = "Player 1\n" + Player1;
        Player2Text.text = "Player 2\n" + Player2;
    }

    public void RandomMapGenerator()
    {
        PhotonNetwork.Instantiate("TemplatePlayer", BallSpawn.transform.position, Quaternion.identity);
    }
}
