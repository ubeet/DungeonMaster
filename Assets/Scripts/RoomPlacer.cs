using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room startRoom;
    public Corridor corVert;
    public Corridor corHor;

    private Room[,] spawnedRooms;
    private List<Corridor> spawnedCorridors;


    private void Start()
    {
        if (File.Exists(SaveSystem.worldPath))
        {
            WorldData data = SaveSystem.LoadWorldData();
            List<Corridor> corridorsToSpawn = data.spawnedCorridors;
            Room[,] roomsToSpawn = data.spawnedRooms;
            for (int i = 0; i < roomsToSpawn.GetLength(0); i++)
            {
                for (int j = 0; j < roomsToSpawn.GetLength(1); j++)
                {
                    if (roomsToSpawn[i, j] != null)
                        Instantiate(roomsToSpawn[i, j]);
                }
            }

            foreach (Corridor cor in corridorsToSpawn)
                Instantiate(cor);
        }
            
        else
        {
            spawnedCorridors = new List<Corridor>();
            spawnedRooms = new Room[11, 11];
            spawnedRooms[5, 5] = startRoom;
            for (int i = 0; i < 6; i++)
            {
                PlaceOneRoom();
            }
        }

    }

    public Room[,] GetSpawnedRooms()
    {
        return spawnedRooms;
    }

    public List<Corridor> GetSpawnedCorridors()
    {
        return spawnedCorridors;
    }


    private void PlaceOneRoom()
    {
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;
                
                int maxX = spawnedRooms.GetLength(0) - 1;
                int maxY = spawnedRooms.GetLength(1) - 1;

                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));

            }
        }

        Room newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
        
        
        int limit = 25;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectToRooms(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * 28, (position.y - 5) * 24, 0);
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
        
    }

    private bool ConnectToRooms(Room room, Vector2Int pos)
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;
        
        List<Vector2Int> neighbours = new List<Vector2Int>();
        
        if(room.doorU != null && pos.y < maxY && spawnedRooms[pos.x, pos.y + 1]?.doorD != null)  neighbours.Add(Vector2Int.up);
        if(room.doorL != null && pos.x > 0 && spawnedRooms[pos.x - 1, pos.y]?.doorR != null)  neighbours.Add(Vector2Int.left);
        if(room.doorD != null && pos.y > 0 && spawnedRooms[pos.x, pos.y - 1]?.doorU != null)  neighbours.Add(Vector2Int.down);
        if(room.doorR != null && pos.x < maxX && spawnedRooms[pos.x + 1, pos.y]?.doorL != null)  neighbours.Add(Vector2Int.right);

        if (neighbours.Count == 0) return false;
        
        Vector2Int selectedDirections = neighbours[Random.Range(0, neighbours.Count)];
        int x = pos.x + selectedDirections.x;
        int y = pos.y + selectedDirections.y;
        
        Room selectedRoom = spawnedRooms[x, y];
        if (selectedDirections == Vector2Int.up)
        {
            room.doorU.SetActive(false);
            selectedRoom.doorD.SetActive(false);
            Corridor cor = Instantiate(corVert, new Vector3((pos.x - 5) * 28, (pos.y - 5) * 24, 0), Quaternion.identity);
            spawnedCorridors.Add(cor);
        }
        else if (selectedDirections == Vector2Int.right)
        {
            room.doorR.SetActive(false);
            selectedRoom.doorL.SetActive(false);
            Corridor cor = Instantiate(corHor, new Vector3((pos.x - 5) * 28, (pos.y - 5) * 24, 0), Quaternion.identity);            
            spawnedCorridors.Add(cor);
        }
        else if (selectedDirections == Vector2Int.down)
        {
            room.doorD.SetActive(false);
            selectedRoom.doorU.SetActive(false);
            Corridor cor = Instantiate(corVert, new Vector3((pos.x - 5) * 28, (pos.y - 5) * 24 - 24, 0), Quaternion.identity);
            spawnedCorridors.Add(cor);
        }
        else if (selectedDirections == Vector2Int.left)
        {
            room.doorL.SetActive(false);
            selectedRoom.doorR.SetActive(false);
            Corridor cor = Instantiate(corHor, new Vector3((pos.x - 5) * 28 - 28, (pos.y - 5) * 24, 0), Quaternion.identity);            
            spawnedCorridors.Add(cor);
        }
        spawnedRooms[x, y] = selectedRoom;
        spawnedRooms[pos.x, pos.y] = room;

        return true;
    }
    public void SaveGame()
    {
        SaveSystem.SaveWorldData(this);
    }
}

