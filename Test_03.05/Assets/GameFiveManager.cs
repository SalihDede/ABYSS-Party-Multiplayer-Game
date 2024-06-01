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
    public GameObject[] Spawns; // List of spawn points
    public bool GameFinished;
    public TMP_Text countdownText;
    public float gameDuration = 60f; // Duration of the game in seconds
    private float elapsedTime = 0f; // Time elapsed since the start of the game
    private bool gameStarted = false; // Indicates if the game has started
    private int currentSpawnIndex = 0; // Current spawn point index

    void Start()
    {
        GameManagerrrr = GameObject.Find("GameManager");
        StartCoroutine(StartCountdownCoroutine());
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
        if (Spawns.Length == 0)
        {
            Debug.LogError("No spawn points available.");
            return;
        }

        // Get the player's index in the Photon player list
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        // Ensure the spawn index is within bounds
        int spawnIndex = playerIndex % Spawns.Length;

        // Instantiate the player at the corresponding spawn point
        PhotonNetwork.Instantiate("SalihGamePlayer", Spawns[spawnIndex].transform.position, Quaternion.identity);
    }

    public void PlayerCompletedPuzzle(GameObject player)
    {
        if (!Ranking.Contains(player))
        {
            Ranking.Add(player);

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
        elapsedTime = 0f; // Reset elapsed time
        RandomMapGenerator();
    }

    void EndGame(bool isFinished)
    {
        gameStarted = false;

        if (isFinished)
        {
            Win.text += "\nGame Over!";
        }
        else if (!GameFinished)
        {
            Win.text = "Time is up! No one finished the puzzle.";
        }

        GameFinished = true;
        // Additional logic to handle game end can be added here

        // Restart the game if needed
        StartCoroutine(RestartGame());
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds before restarting
        Win.text = ""; // Clear the win text
        Ranking.Clear(); // Clear the ranking list
        GameFinished = false;
        StartCoroutine(StartCountdownCoroutine());
    }
}