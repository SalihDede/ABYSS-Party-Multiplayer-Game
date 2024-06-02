using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class PuzzleManager : MonoBehaviourPunCallbacks
{
    public Transform[] puzzleTiles; // Holds controls for puzzle pieces
    public Transform emptyTile; // Represents an empty space
    public int shuffleCount = 20; // Number of times to shuffle
    public float positionTolerance = 2.0f; // Tolerance for checking positions
    public GameObject MainPlayerOfMap;

    public Dictionary<Transform, Vector3> originalPositions; // Stores where pieces originally were
    public Dictionary<Transform, Vector3> currentPositions;  // Stores where pieces are now

    public GameFiveManager GameFiveManager; // Reference to GameFiveManager

    void Start()
    {
        SaveOriginalPositions();
        PrintOriginalPositions();
        SaveCurrentPositions();
        ShufflePuzzle();
        StartCoroutine(UpdateCurrentPositionsPeriodically()); // Keep track of current positions
    }

    // Save where the puzzle pieces originally were
    void SaveOriginalPositions()
    {
        originalPositions = new Dictionary<Transform, Vector3>();
        foreach (Transform tile in puzzleTiles)
        {
            originalPositions[tile] = tile.position;
        }
        originalPositions[emptyTile] = emptyTile.position; // Add the empty tile position
    }

    // Save where the puzzle pieces are currently
    void SaveCurrentPositions()
    {
        currentPositions = new Dictionary<Transform, Vector3>();
        foreach (Transform tile in puzzleTiles)
        {
            currentPositions[tile] = tile.position;
        }
        currentPositions[emptyTile] = emptyTile.position; // Add the empty tile position
    }

    // Calculate the maximum distance allowed for shuffling
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

    // Shuffle the puzzle tiles
    public void ShufflePuzzle()
    {
        bool puzzleSolved = true;

        while (puzzleSolved)
        {
            for (int i = 0; i < shuffleCount; i++)
            {
                // Choose a random tile from the puzzleTiles array
                int randomIndex = Random.Range(0, puzzleTiles.Length);
                Transform randomTile = puzzleTiles[randomIndex];

                // Calculate the distance between the empty tile and the random tile
                float distance = Vector3.Distance(randomTile.position, emptyTile.position);

                // Calculate max distance dynamically
                float maxDistance = CalculateMaxDistance();

                // Check if the distance is within the limit
                if (distance < maxDistance)
                {
                    // Swap positions with the empty space
                    Vector3 tempPosition = randomTile.position;
                    randomTile.position = emptyTile.position;
                    emptyTile.position = tempPosition;

                    // Update current positions after the swap
                    SaveCurrentPositions();
                }
                else
                {
                    // Decrement the counter to ensure the required number of shuffles
                    i--;
                }
            }

            SaveCurrentPositions(); // Save positions after shuffling
            puzzleSolved = CheckIfPuzzleSolved(); // Check if the puzzle is solved for bool.
        }

        PrintCurrentPositions(); // Print current positions after shuffling
    }

    //Control flow output for developer
    // Print the original positions of all pieces
    void PrintOriginalPositions()
    {
        Debug.Log("Original Positions:");
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
            Debug.Log(kvp.Key.name + " (World Position): " + kvp.Value);
        }
    }

    //Control flow output for developer
    // Print the current positions of all pieces
    void PrintCurrentPositions()
    {
        if (CheckIfPuzzleSolved())
        {
            Debug.Log("Puzzle solved");

            if (MainPlayerOfMap.GetComponent<PhotonView>().IsMine)
            {
                Debug.Log("You solved the puzzle!");

                MainPlayerOfMap.GetComponent<GameFivePlayer>().IsHeSolve = true;

                GameFiveManager.PlayerCompletedPuzzle(); // Notify GameFiveManager
            }
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



    // Check if the puzzle is solved
    bool CheckIfPuzzleSolved()
    {
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
            if (kvp.Key == emptyTile)   // We don't need to work on the empty tile because there isn't any mesh to move it.
            {
                continue;
            }
            // Checking the positions of each tile except the empty one
            Vector3 currentPosition = currentPositions[kvp.Key];
            if (!IsWithinTolerance(kvp.Value, currentPosition))
            {
                return false;
            }
        }
        return true;
    }

    // Check if two positions are within the specified tolerance 
    bool IsWithinTolerance(Vector3 original, Vector3 current)
    {
        return Mathf.Abs(original.x - current.x) <= positionTolerance &&
               Mathf.Abs(original.y - current.y) <= positionTolerance &&
               Mathf.Abs(original.z - current.z) <= positionTolerance;
    }

    // For developer, we want to see the flow and find the bug with check the flow of implemented code.
    // Coroutine to update the current positions every second
    IEnumerator UpdateCurrentPositionsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(1); // Wait for 1 second
            SaveCurrentPositions(); // Update current positions
            PrintCurrentPositions(); // Print updated positions - for developer
        }
    }
}
