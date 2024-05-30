using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public Transform[] puzzleTiles; // Holds controls for puzzle pieces
    public Transform emptyTile; // Represents an empty space
    public int shuffleCount = 20; // Number of times to shuffle
    public float positionTolerance = 2.0f; // Tolerance for checking positions

    private Dictionary<Transform, Vector3> originalPositions; // Stores where pieces originally were
    private Dictionary<Transform, Vector3> currentPositions;  // Stores where pieces are now
    private float maxDistance; // Maximum distance allowed for shuffling

    void Start()
    {
        DetachTilesFromParent();
        SaveOriginalPositions();
        PrintOriginalPositions();
        SaveCurrentPositions();
        CalculateMaxDistance(); // Calculate the maximum distance before shuffling
        ShufflePuzzle();
        StartCoroutine(UpdateCurrentPositionsPeriodically()); // Keep track of current positions
    }

    // Detach puzzle pieces from their original parent
    void DetachTilesFromParent()
    {
        foreach (Transform tile in puzzleTiles)
        {
            tile.SetParent(null);
        }
        emptyTile.SetParent(null);
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
    void CalculateMaxDistance()
    {
        maxDistance = 0f;
        Vector3 emptyTileOriginalPosition = originalPositions[emptyTile];

        foreach (Transform tile in puzzleTiles)
        {
            float dist = Vector3.Distance(tile.position, emptyTileOriginalPosition);
            if (dist > maxDistance)
            {
                maxDistance = dist;
            }
        }
    }

    // Shuffle the puzzle tiles
    void ShufflePuzzle()
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            // Choose a random tile from the puzzleTiles array
            int randomIndex = Random.Range(0, puzzleTiles.Length);
            Transform randomTile = puzzleTiles[randomIndex];

            // Calculate the distance between the empty tile and the random tile
            float distance = Vector3.Distance(randomTile.position, emptyTile.position);

            // Check if the distance is within the limit
            if (distance < maxDistance)
            {
                // Swap positions with the empty space
                Vector3 tempPosition = randomTile.position;
                randomTile.position = emptyTile.position;
                emptyTile.position = tempPosition;
            }
            else
            {
                // Decrement the counter to ensure the required number of shuffles
                i--;
            }
        }

        SaveCurrentPositions(); // Save positions after shuffling
        PrintCurrentPositions(); // Print current positions after shuffling
    }

    // Print the original positions of all pieces
    void PrintOriginalPositions()
    {
        Debug.Log("Original Positions:");
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
            Debug.Log(kvp.Key.name + " (World Position): " + kvp.Value);
        }
    }

    // Print the current positions of all pieces
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

    // Check if the puzzle is solved
    bool CheckIfPuzzleSolved()
    {
        foreach (KeyValuePair<Transform, Vector3> kvp in originalPositions)
        {
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

    // Coroutine to update the current positions every 5 seconds
    IEnumerator UpdateCurrentPositionsPeriodically()
    {
        while (true)
        {
            yield return new WaitForSeconds(5); // Wait for 5 seconds
            SaveCurrentPositions(); // Update current positions
            PrintCurrentPositions(); // Print updated positions
        }
    }
}
