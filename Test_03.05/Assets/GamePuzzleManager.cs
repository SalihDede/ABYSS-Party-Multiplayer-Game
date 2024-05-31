using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GamePuzzleManager : MonoBehaviourPunCallbacks
{
    public Transform[] puzzleTiles; // Holds controls for puzzle pieces
    public Transform emptyTile; // Represents an empty space
    public int shuffleCount = 20; // Number of times to shuffle
    public float positionTolerance = 2.0f; // Tolerance for checking positions

    private Dictionary<Transform, Vector3> originalPositions; // Stores where pieces originally were
    private Dictionary<Transform, Vector3> currentPositions;  // Stores where pieces are now

    public TMP_Text countdownText;
    public TMP_Text winText;
    public List<GameObject> ranking = new List<GameObject>();
    private bool gameFinished = false;
    private bool gameStarted = false;

    void Start()
    {
        SaveOriginalPositions();
        PrintOriginalPositions();
        SaveCurrentPositions();
        ShufflePuzzle();
        StartCoroutine(StartCountdownCoroutine());
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
        countdownText.gameObject.SetActive(false);
        gameStarted = true;
        StartCoroutine(UpdateCurrentPositionsPeriodically()); // Keep track of current positions
    }

    void Update()
    {
        if (!gameFinished && gameStarted)
        {
            if (CheckIfPuzzleSolved())
            {
                GameFinished(PhotonNetwork.LocalPlayer);
            }
        }
    }

    void SaveOriginalPositions()
    {
        originalPositions = new Dictionary<Transform, Vector3>();
        foreach (Transform tile in puzzleTiles)
        {
            originalPositions[tile] = tile.position;
        }
        originalPositions[emptyTile] = emptyTile.position; // Add the empty tile position
    }

    void SaveCurrentPositions()
    {
        currentPositions = new Dictionary<Transform, Vector3>();
        foreach (Transform tile in puzzleTiles)
        {
            currentPositions[tile] = tile.position;
        }
        currentPositions[emptyTile] = emptyTile.position; // Add the empty tile position
    }

    float CalculateMaxDistance()
    {
        float maxDistance = 0f;
        Vector3 emptyTileCurrentPosition = currentPositions[emptyTile];

        foreach (Transform tile in puzzleTiles)
        {
            float dist = Vector3.Distance(tile.position, emptyTileCurrentPosition);
            if (dist > maxDistance)
            {
                maxDistance = dist;
            }
        }

        return maxDistance;
    }

    void ShufflePuzzle()
    {
        bool puzzleSolved = true;

        while (puzzleSolved)
        {
            for (int i = 0; i < shuffleCount; i++)
            {
                int randomIndex = Random.Range(0, puzzleTiles.Length);
                Transform randomTile = puzzleTiles[randomIndex];

                float distance = Vector3.Distance(randomTile.position, emptyTile.position);

                float maxDistance = CalculateMaxDistance();

                if (distance < maxDistance)
                {
                    Vector3 tempPosition = randomTile.position;
                    randomTile.position = emptyTile.position;
                    emptyTile.position = tempPosition;

                    SaveCurrentPositions();
                }
                else
                {
                    i--;
                }
            }

            SaveCurrentPositions();
            puzzleSolved = CheckIfPuzzleSolved();
        }

        PrintCurrentPositions();
    }

    void PrintOriginalPositions()
    {
        Debug.Log("Original Positions:");
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
            Debug.Log(kvp.Key.name + " (World Position): " + kvp.Value);
        }
    }

    void PrintCurrentPositions()
    {
        if (CheckIfPuzzleSolved())
        {
            Debug.Log("Puzzle solved");
        }
        else
        {
            Debug.Log("Current Positions:");
            foreach (KeyValuePair<Transform, Vector3> kvp in currentPositions)
            {
                Debug.Log(kvp.Key.name + " (World Position): " + kvp.Value);
            }
        }
    }

    bool CheckIfPuzzleSolved()
    {
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
            if (kvp.Key == emptyTile)
            {
                continue;
            }
            Vector3 currentPosition = currentPositions[kvp.Key];
            if (!IsWithinTolerance(kvp.Value, currentPosition))
            {
                return false;
            }
        }
        return true;
    }

    bool IsWithinTolerance(Vector3 original, Vector3 current)
    {
        return Mathf.Abs(original.x - current.x) <= positionTolerance &&
               Mathf.Abs(original.y - current.y) <= positionTolerance &&
               Mathf.Abs(original.z - current.z) <= positionTolerance;
    }

    IEnumerator UpdateCurrentPositionsPeriodically()
    {
        while (!gameFinished)
        {
            yield return new WaitForSeconds(5); // Wait for 5 seconds
            SaveCurrentPositions(); // Update current positions
            PrintCurrentPositions(); // Print updated positions - for developer
        }
    }

    [PunRPC]
    void GameFinished(Player player)
    {
        if (!gameFinished)
        {
            gameFinished = true;
            Debug.Log("Puzzle solved by: " + player.NickName);
            winText.text = player.NickName + " solved the puzzle!";
            ranking.Insert(0, player.NickName);
            PhotonNetwork.RaiseEvent(0, player.NickName, RaiseEventOptions.Default, SendOptions.SendReliable);
        }
    }

    public void ResetGame()
    {
        SaveOriginalPositions();
        SaveCurrentPositions();
        ShufflePuzzle();
        gameFinished = false;
        gameStarted = false;
        ranking.Clear();
        countdownText.gameObject.SetActive(true);
        winText.text = "";
        StartCoroutine(StartCountdownCoroutine());
    }
}
