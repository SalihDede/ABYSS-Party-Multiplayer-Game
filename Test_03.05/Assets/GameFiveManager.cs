using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameFiveManager : MonoBehaviourPunCallbacks
{
    public GameObject GameManagerrrr;
    public bool StartTime;
    public GameObject GameTwoGUI;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();
    public TMP_Text Win;
    public bool IsWin;
    public GameObject Spawn;
    public bool GameFinished;
    public TMP_Text countdownText;
    public float gameDuration = 60f; // Duration of the game in seconds
    private float elapsedTime = 0f; // Time elapsed since the start of the game
    private bool gameStarted = false; // Indicates if the game has started
    private int playerRank = 0; // Rank of players who complete the puzzle

    void Start()
    {
        GameManagerrrr = GameObject.Find("GameManager");
        StartCoroutine(StartCountdownCoroutine());
        RandomMapGenerator();
    }

    void Update()
    {


        if (gameStarted)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= gameDuration)
            {
                EndGame(true);
            }
            else
            {
                EndGame(false);
            }
        }
    }

    IEnumerator StartCountdownCoroutine()
    {
        int countdown = 15; // Initial countdown value
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false); // Hide the countdown text
        StartGame(); // Start the game after the countdown
    }

    public void RandomMapGenerator()
    {

            GameObject player = PhotonNetwork.Instantiate("SalihGamePlayer", Spawn.transform.position, Quaternion.identity);

    }

    public void PlayerCompletedPuzzle(GameObject player)
    {
        if (!Ranking.Contains(player))
        {
            
            Ranking.Add(player);
            //playerRank++;

            if (Ranking.Count == 1)
            {
                // The first player to complete the puzzle
                Win.text = "Player " + player.name + " wins!";
            }
            else
            {
                // For subsequent players
                Win.text += "\nPlayer " + player.name + " finished in position " + Ranking.Count;
            }

            // Check if all players have finished
            if (Ranking.Count == PhotonNetwork.PlayerList.Length)
            {
                EndGame(true);
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
    }

    void EndGame(bool isFinished)
    {

        if (!isFinished)
        {
            Win.text = "No one finished yet.";
        }
        else
        {
            gameStarted = false;
            GameFinished = true;
        }
    }
}
