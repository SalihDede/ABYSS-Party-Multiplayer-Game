using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviourPunCallbacks
{
    public GameObject BallPrefab;
    public TMP_Text Player1Text;
    public TMP_Text Player2Text;
    public GameObject BallSpawn;

    private bool goal;
    private GameObject ballInstance;

    void Start()
    {
        goal = true;
        RandomMapGenerator();
    }

    void Update()
    {
        if (goal)
        {
            goal = false;
            StartCoroutine(GoalCoroutine());
        }
        Player1Text.text = "Player 1\n" + Player1;
        Player2Text.text = "Player 2\n" + Player2;
    }

    IEnumerator GoalCoroutine()
    {
        yield return new WaitForSeconds(3);
        
        if (PhotonNetwork.IsMasterClient)
        {
            // Destroy the existing ball
            if (ballInstance != null)
            {
                PhotonNetwork.Destroy(ballInstance);
            }

            // Spawn a new ball and transfer ownership to a random player
            ballInstance = PhotonNetwork.Instantiate(BallPrefab.name, BallSpawn.transform.position, Quaternion.identity);
            PhotonView ballPhotonView = ballInstance.GetComponent<PhotonView>();
            int randomPlayerIndex = Random.Range(0, PhotonNetwork.PlayerList.Length);
            ballPhotonView.TransferOwnership(PhotonNetwork.PlayerList[randomPlayerIndex]);
        }
    }

    public void RandomMapGenerator()
    {
        PhotonNetwork.Instantiate("TemplatePlayer", BallSpawn.transform.position, Quaternion.identity);
    }

    public int Player1 { get; private set; }
    public int Player2 { get; private set; }

    // Method to increase player score
    public void IncreasePlayerScore(int playerIndex)
    {
        if (playerIndex == 1)
        {
            Player1++;
        }
        else if (playerIndex == 2)
        {
            Player2++;
        }
    }
}
