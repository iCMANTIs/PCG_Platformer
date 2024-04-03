using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class WFCGenerator : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public GameObject[] tilePrefabs;

    public GameObject PlayerCharacter;
    public GameObject StartGameOBJ;
    public GameObject[] objectsToSpawn; 
    public float speedThreshold = 5f;
    private float initialHeight;
    private bool objectSpawned = false;

    private class Tile
    {
        public List<GameObject> possibleTiles = new List<GameObject>();
        public GameObject finalTile = null;
    }

    private Tile[,] map;

    void Start()
    {
        GenerateMap();
        GenerateSpecialGameObject();
        if (PlayerCharacter != null)
        {
            initialHeight = PlayerCharacter.transform.position.y;
        }
    }

    private void Update()
    {
        if (objectSpawned) return;

        if (PlayerCharacter != null)
        {
            float currentHeight = PlayerCharacter.transform.position.y;
            float verticalDistance = currentHeight - initialHeight;
            float verticalSpeed = PlayerCharacter.GetComponent<Rigidbody2D>().velocity.y;

            if (verticalDistance >= 10f && verticalSpeed > speedThreshold)
            {
                SpawnObjectAbovePlayer();
                initialHeight = PlayerCharacter.transform.position.y; // Reset the initial height to prepare for the next trigger.
            }
        }
    }

    void SpawnObjectAbovePlayer()
    {
        if (objectsToSpawn.Length == 0) return;

        // Randomly select an object from the list to instantiate.
        GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
        Vector2 spawnPosition = PlayerCharacter.transform.position + new Vector3(0, 1, 0); // Generate above the player.
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    void GenerateSpecialGameObject()
    {
        if (PlayerCharacter == null || StartGameOBJ == null) return;

        Vector2 playerPosition = PlayerCharacter.transform.position;
        Vector2 targetPosition = playerPosition + new Vector2(5, 2); // At a position (5, 2) in front of and above the player.

        Instantiate(StartGameOBJ, targetPosition, Quaternion.identity);
    }


    void GenerateMap()
    {
        InitializeMap();
        ApplyWFC();
        InstantiateMap();
    }

    void InitializeMap()
    {
        map = new Tile[2, 25];
        for (int x = 0; x < 2; x++) // two columns.
        {
            for (int y = 0; y < 25; y++) // Each column has 25 cells.
            {
                map[x, y] = new Tile
                {
                    possibleTiles = new List<GameObject>(tilePrefabs),
                    finalTile = null
                };
            }
        }
    }

    void ApplyWFC()
    {

        for (int x = 0; x < 2; x++) // With a width of 2, the range of x is 0.
        {
            for (int y = 0; y < 25; y++) // With a height of 25, the range of y is 0 to 24.
            {
                var cell = map[x, y];
                int indexOfA = 3;
                int indexOfB = 2;

                // If the current cell is not in the first row and the cell above it is A, then B is not allowed to be generated in this cell.
                if (y > 0 && map[x, y - 1].finalTile == tilePrefabs[indexOfA])
                {
                    List<GameObject> possibleWithoutB = new List<GameObject>(cell.possibleTiles);
                    possibleWithoutB.Remove(tilePrefabs[indexOfB]); // remove B
                    int index = Random.Range(0, possibleWithoutB.Count);
                    cell.finalTile = possibleWithoutB[index];
                }
                else
                {
                    int index = Random.Range(0, cell.possibleTiles.Count);
                    cell.finalTile = cell.possibleTiles[index];
                }
            }
        }
    }

    void InstantiateMap()
    {

        float cellWidth = 20f / 2; // Divide the map width by the number of columns.
        float cellHeight = 100f / 25; // Divide the map height by the number of cells in each column.

        for (int x = 0; x < 2; x++)
        {
            for (int y = 0; y < 25; y++) 
            {
                var cell = map[x, y];
                if (cell.finalTile != null)
                {

                    Vector2 position = new Vector2(x * cellWidth + cellWidth / 2 - 10, y * cellHeight + cellHeight / 2);
                    GameObject instantiatedObj = Instantiate(cell.finalTile, position, Quaternion.identity, transform);

                    DisappearOnContact disappearScript = instantiatedObj.AddComponent<DisappearOnContact>();
                    
                    disappearScript.maxContactTime = 3.0f; //max contact time 3
                }
            }
        }
    }
}
