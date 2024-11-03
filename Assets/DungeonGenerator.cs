using UnityEngine;
using System.Collections.Generic;

public class DungeonGenerator : MonoBehaviour
{
    public GameObject roomPrefab;         // Room prefab with entry and exit points
    public GameObject corridorPrefab;     // Corridor prefab to connect rooms
    public int gridWidth = 10;            // Width of the grid (number of cells horizontally)
    public int gridHeight = 10;           // Height of the grid (number of cells vertically)
    public int cellSize = 20;             // Distance between rooms (to prevent overlap)
    public float roomPlacementChance = 0.6f; // Chance of a room being placed in each cell

    private List<Vector2Int> roomPositions = new List<Vector2Int>(); // Store room positions
    private Dictionary<Vector2Int, GameObject> roomInstances = new Dictionary<Vector2Int, GameObject>(); // Track instantiated rooms

    void Start()
    {
        GenerateDungeon();
    }

    void GenerateDungeon()
    {
        // Step 1: Place rooms in the grid
        PlaceRooms();

        // Step 2: Connect rooms with corridors
        ConnectRooms();
    }

    void PlaceRooms()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                // Randomly decide if this cell should contain a room
                if (Random.value < roomPlacementChance)
                {
                    Vector2Int cellPosition = new Vector2Int(x, y);
                    Vector3 worldPosition = new Vector3(x * cellSize, 0, y * cellSize);

                    // Instantiate the room prefab and add it to our list and dictionary
                    GameObject newRoom = Instantiate(roomPrefab, worldPosition, Quaternion.identity);
                    roomPositions.Add(cellPosition);
                    roomInstances[cellPosition] = newRoom;
                }
            }
        }
    }

    void ConnectRooms()
    {
        // Connect each room to one of its neighbors
        foreach (var roomPos in roomPositions)
        {
            // Try to connect to the room directly to the right
            Vector2Int rightNeighbor = roomPos + Vector2Int.right;
            if (roomInstances.ContainsKey(rightNeighbor))
            {
                CreateCorridorBetweenRooms(roomInstances[roomPos], roomInstances[rightNeighbor]);
            }

            // Try to connect to the room directly above
            Vector2Int topNeighbor = roomPos + Vector2Int.up;
            if (roomInstances.ContainsKey(topNeighbor))
            {
                CreateCorridorBetweenRooms(roomInstances[roomPos], roomInstances[topNeighbor]);
            }
        }
    }

    void CreateCorridorBetweenRooms(GameObject roomA, GameObject roomB)
    {
        // Find the exit point of roomA and entry point of roomB
        Transform exitPoint = roomA.transform.Find("exitPoint");
        Transform entryPoint = roomB.transform.Find("entryPoint");

        if (exitPoint != null && entryPoint != null)
        {
            Vector3 start = exitPoint.position;
            Vector3 end = entryPoint.position;

            // Create a corridor from start to end
            Vector3 currentPosition = start;

            // Move horizontally first
            while (Mathf.RoundToInt(currentPosition.x) != Mathf.RoundToInt(end.x))
            {
                Instantiate(corridorPrefab, currentPosition, Quaternion.identity);
                currentPosition.x += currentPosition.x < end.x ? 1 : -1;
            }

            // Then move vertically
            while (Mathf.RoundToInt(currentPosition.z) != Mathf.RoundToInt(end.z))
            {
                Instantiate(corridorPrefab, currentPosition, Quaternion.identity);
                currentPosition.z += currentPosition.z < end.z ? 1 : -1;
            }
        }
    }
}
