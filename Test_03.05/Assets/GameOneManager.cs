using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameOneManager : MonoBehaviourPunCallbacks
{
    public TMP_Text Player1Text;
    public TMP_Text Player2Text;

    private int Player1Score;
    private int Player2Score;

    void Start()
    {
        // Oyun başlangıcında skorları sıfırla ve skorları güncelle
        Player1Score = 0;
        Player2Score = 0;
        UpdateScoreTexts();
    }

    // Player 1'in gol attığını diğer oyunculara bildirir
    [PunRPC]
    void Player1Scores()
    {
        Player1Score++;
        UpdateScoreTexts();
    }

    // Player 2'in gol attığını diğer oyunculara bildirir
    [PunRPC]
    void Player2Scores()
    {
        Player2Score++;
        UpdateScoreTexts();
    }

    // Skor metinlerini günceller
    void UpdateScoreTexts()
    {
        Player1Text.text = "Player 1\n" + Player1Score;
        Player2Text.text = "Player 2\n" + Player2Score;
    }

    // Oyuncular gol attığında bu metod çağrılır ve RPC'leri kullanarak diğer oyunculara bildirir
    public void PlayerScores(int playerNumber)
    {
        if (playerNumber == 1)
        {
            photonView.RPC("Player1Scores", RpcTarget.All);
        }
        else if (playerNumber == 2)
        {
            photonView.RPC("Player2Scores", RpcTarget.All);
        }
    }
}
