using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GameFiveManager : MonoBehaviourPunCallbacks
{
    public GameObject[] PuzzleMapList;
    public GameObject GameManagerrr;
    public bool StartTime;
    public GameObject GameTwoGUI;
    public List<GameObject> Ranking = new List<GameObject>();
    public List<GameObject> Starters = new List<GameObject>();
    public TMP_Text Win;
    public bool IsWin;
    public GameObject[] Spawns; // List of spawn points
    public bool GameFinished;
    public TMP_Text countdownText;
    public TMP_Text ElapsedTime;
    public float gameDuration = 120f; // Duration of the game in seconds
    private float elapsedTime = 0f; // Time elapsed since the start of the game
    public int countdown = 15;
    private bool gameStarted = false; // Indicates if the game has started
    public bool gameActive = false; // Indicates if the game has started
    private int currentSpawnIndex = 0; // Current spawn point index

    void Start()
    {
        GameManagerrr = GameObject.Find("GameManager");
    }

    void FixedUpdate()
    {
        if(gameActive)
        {
            gameActive = false;
            StartCoroutine(StartCountdownCoroutine());
        }

        if (Ranking.Count == 4)
        {
            GameManagerrr.GetComponent<GameManager>().Kamera.SetActive(true);
            gameObject.SetActive(false);

            GameManagerrr.GetComponent<GameManager>().PlayersTemp.Clear();
            for (int i = 0; i < 4; i++)
            {
                foreach (GameObject Player in GameManagerrr.GetComponent<GameManager>().PlayersSorted)
                {
                    if (Ranking[i].GetComponent<PhotonView>().ViewID / 1000 == Player.GetComponent<PhotonView>().ViewID / 1000)
                    {
                        GameManagerrr.GetComponent<GameManager>().PlayersTemp.Add(Player);
                    }
                }
            }

            GameManagerrr.GetComponent<GameManager>().PlayersSorted.Clear();

            if (GameManagerrr.GetComponent<GameManager>().PlayersSorted.Count != 4)
            {
                GameManagerrr.GetComponent<GameManager>().PlayersSorted.AddRange(GameManagerrr.GetComponent<GameManager>().PlayersTemp);
            }

            foreach(GameObject player in Ranking)
            {
                Destroy(player);
            }
            Ranking.Clear();
            Starters.Clear();
            GameManagerrr.GetComponent<GameManager>().MiniGameStarted = false;
            Cursor.lockState = CursorLockMode.None;
            gameObject.SetActive(false);
        }

        UpdateWinText();

        countdownText.text = countdown.ToString();
        ElapsedTime.text = ((int)(elapsedTime)).ToString();

        if (Ranking.Count == 0)
        {
            Win.text = "";
        }
        if (Ranking.Count == 1)
        {
            Win.text = Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n";
        }
        if (Ranking.Count == 2)
        {
            Win.text = Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[1].GetComponent<PhotonView>().Controller.NickName;
        }
        if (Ranking.Count == 3)
        {
            Win.text = Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[1].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[2].GetComponent<PhotonView>().Controller.NickName;
        }
        if (Ranking.Count == 4)
        {
            Win.text = Ranking[0].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[1].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[2].GetComponent<PhotonView>().Controller.NickName + "\n" + Ranking[3].GetComponent<PhotonView>().Controller.NickName;
        }

        if (gameStarted)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= gameDuration)
            {
                EndGame(true);
            }
        }
    }

    private void UpdateWinText()
    {
        if (Ranking.Count == 0)
        {
            Win.text = "";
        }
        else
        {
            Win.text = "";
            for (int i = 0; i < Ranking.Count; i++)
            {
                Win.text += Ranking[i].GetComponent<PhotonView>().Controller.NickName + "\n";
            }
        }
    }

    public IEnumerator StartCountdownCoroutine()
    {
        countdownText.gameObject.SetActive(true);
        ElapsedTime.gameObject.SetActive(false);
        countdown = 15; // Initial countdown value
        while (countdown > 0)
        {
            yield return new WaitForSeconds(1);
            countdown--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.gameObject.SetActive(false); // Hide the countdown text
        ElapsedTime.gameObject.SetActive(true); // Show the elapsed time text
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

    public void PlayerCompletedPuzzle()
    {
        if (!Ranking.Contains(gameObject))
        {
            Ranking.Add(gameObject);
        }

        if (Ranking.Count == PhotonNetwork.PlayerList.Length)
        {
            StartCoroutine(WaitAndContinue());
        }
    }

    // For developer to handle flow.
    IEnumerator WaitAndContinue()
    {
        yield return new WaitForSeconds(10); // Wait for 10 seconds before continuing
        StartCoroutine(EndingGame());
    }

    IEnumerator EndingGame()
    {
        yield return new WaitForSeconds(3);
        EndGame(true);
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
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5); // Wait for 5 seconds before restarting
        Win.text = ""; // Clear the win text
        Ranking.Clear(); // Clear the ranking list
        GameFinished = false;
        StartCoroutine(StartCountdownCoroutine());
    }

    public void ResetGame()
    {
        gameStarted = false;
        gameActive = false;
        elapsedTime = 0f;
        countdown = 15;
        currentSpawnIndex = 0;
        Ranking.Clear();

        // Reset puzzle managers
        foreach (var puzzleMap in PuzzleMapList)
        {
            var puzzleManager = puzzleMap.GetComponent<PuzzleManager>();
            puzzleManager.ResetPuzzle();
        }

    }
}
