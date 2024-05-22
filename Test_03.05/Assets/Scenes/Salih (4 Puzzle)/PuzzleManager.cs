using UnityEngine;
using System.Collections.Generic;

public class PuzzleManager : MonoBehaviour
{
    public Transform[] puzzleTiles; // Contains puzzle transform controlss
    public Transform emptyTile; // Empty blank

    public int shuffleCount = 20; // The movement time for shuffling

    void Start()
    {
        ShufflePuzzle();
    }

    // Suffle Slices randomly
    void ShufflePuzzle()
    {
        for (int i = 0; i < shuffleCount; i++)
        {
            int randomIndex = Random.Range(0, puzzleTiles.Length); // Chose random tile
            Transform randomTile = puzzleTiles[randomIndex];

            // Change the position of empty and selected tile
            Vector3 tempPosition = randomTile.position;
            randomTile.position = emptyTile.position;
            emptyTile.position = tempPosition;
        }
    }
}
