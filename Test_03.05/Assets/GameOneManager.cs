using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerr;
    public bool Goal;
    public GameObject BallPrefab;

    public int Player1;
    public int Player2;
    public int Player3;
    public int Player4;

    public TMP_Text Player1Text;
    public TMP_Text Player2Text;
    public TMP_Text Player3Text;
    public TMP_Text Player4Text;

    public TMP_Text LastTouch;

    public GameObject Spawn0;
    public GameObject Spawn1;
    public GameObject Spawn2;
    public GameObject Spawn3;
    public GameObject BallSpawn;

    private PhotonView photonView;

    void Start()
    {
        GameManagerr = GameObject.Find("GameManager");

        Goal = true;
        RandomMapGenerator();
    }

    IEnumerator GoalCoroutine()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.Instantiate("Soccer Ball", BallSpawn.transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Goal && PhotonNetwork.IsMasterClient)
        {
            Goal = false;
            StartCoroutine(GoalCoroutine());
        }
        Player1Text.text = "Player 1\n" + Player1;
        Player2Text.text = "Player 2\n" + Player2;
        Player3Text.text = "Player 3\n" + Player3;
        Player4Text.text = "Player 4\n" + Player4;
    }

    public void RandomMapGenerator()
    {
        GameObject player1 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn0.transform.position, Quaternion.identity);
        GameObject player2 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn1.transform.position, Quaternion.identity);
        GameObject player3 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn2.transform.position, Quaternion.identity);
        GameObject player4 = PhotonNetwork.Instantiate("TemplatePlayer", Spawn3.transform.position, Quaternion.identity);
    }
}
