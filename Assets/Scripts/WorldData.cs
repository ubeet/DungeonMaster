using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData : MonoBehaviour
{
    public List<Corridor> spawnedCorridors;
    public Room[,] spawnedRooms;

    public WorldData(RoomPlacer rooms)
    {
        spawnedCorridors = rooms.GetSpawnedCorridors();
        spawnedRooms = rooms.GetSpawnedRooms();
    }
}
