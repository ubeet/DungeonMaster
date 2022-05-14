using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room startRoom;
    public int dungeonSize;
    public int roomStartX;
    public int roomStartY;
    public int roomsAmount;
    public Corridor LeftHor;
    public Corridor RightHor;
    public Corridor CenterHor;
    public Corridor TopVert;
    public Corridor DownVert;
    public Corridor CenterVert;
    

    private Room[,] spawnedRooms;


    private void Start()
    {
        spawnedRooms = new Room[dungeonSize,dungeonSize];
        if (File.Exists(SaveSystem.WorldPath))
        {
            WorldData data = SaveSystem.LoadWorldData();

            for (int i = 0; i < spawnedRooms.GetLength(0); i++)
            {
                for (int j = 0; j < spawnedRooms.GetLength(1); j++)
                {
                    if (data.roomTag != null)
                    {
                        Room room = null;
                        if (data.roomTag[i, j] == startRoom.tag)
                        {
                            room = Instantiate(startRoom,
                                new Vector3(data.position[i, j, 0], data.position[i, j, 1], data.position[i, j, 2]),
                                Quaternion.identity);
                            
                        }

                        foreach (var el in roomPrefabs)
                        {
                            if (el.tag == data.roomTag[i, j])
                            {
                                room = Instantiate(el, new Vector3(data.position[i, j, 0], data.position[i, j, 1], data.position[i, j, 2]), Quaternion.identity);
                                break;
                            }
                        }

                        if (room != null)
                        {
                            if(!data.doorU[i, j])
                                if (room.doorU != null)
                                    room.doorU.SetActive(false);

                            if(!data.doorR[i, j])
                                if (room.doorR != null)
                                    room.doorR.SetActive(false);
                                
                            if(!data.doorD[i, j])
                                if (room.doorD != null)
                                    room.doorD.SetActive(false);
                                
                            if(!data.doorL[i, j])
                                if (room.doorL != null)
                                    room.doorL.SetActive(false);
                            
                            if (!data.triggers[i, j])
                                if (room.triggers != null)
                                    room.triggers.SetActive(false);
                            
                        }
                        
                        spawnedRooms[i, j] = room;
                    }
                }
            }
        }
            
        else
        {
            spawnedRooms[roomStartX, roomStartY] = startRoom;
            for (int i = 0; i < roomsAmount; i++)
            {
                PlaceOneRoom();
            }
        }
        SetCorridors();
    }

    public Room[,] GetSpawnedRooms() => spawnedRooms;

    private void PlaceOneRoom()
    {
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;
        HashSet<Vector2Int> vacantPlaces = new HashSet<Vector2Int>();
        for (int x = 0; x < spawnedRooms.GetLength(0); x++)
        {
            for (int y = 0; y < spawnedRooms.GetLength(1); y++)
            {
                if (spawnedRooms[x, y] == null) continue;
                
                if (x > 0 && spawnedRooms[x - 1, y] == null) vacantPlaces.Add(new Vector2Int(x - 1, y));
                if (y > 0 && spawnedRooms[x, y - 1] == null) vacantPlaces.Add(new Vector2Int(x, y - 1));
                if (x < maxX && spawnedRooms[x + 1, y] == null) vacantPlaces.Add(new Vector2Int(x + 1, y));
                if (y < maxY && spawnedRooms[x, y + 1] == null) vacantPlaces.Add(new Vector2Int(x, y + 1));

            }
        }
        Room newRoom = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
        
        for (int limit = 0; limit < 25;limit++)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (position.x + 1 <= maxX && spawnedRooms[position.x + 1, position.y]?.tag == "BiggestRoom" ||
                position.x - 1 >= 0 && spawnedRooms[position.x - 1, position.y]?.tag == "BiggestRoom")
            {
                Destroy(newRoom.gameObject);
                newRoom = Instantiate(roomPrefabs[0]);
            }

            if (ConnectToRooms(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * 29, (position.y - 5) * 24, 0);
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
            
        }
        else if (selectedDirections == Vector2Int.right)
        {
            room.doorR.SetActive(false);
            selectedRoom.doorL.SetActive(false);
            
        }
        else if (selectedDirections == Vector2Int.down)
        {
            room.doorD.SetActive(false);
            selectedRoom.doorU.SetActive(false);
            
        }
        else if (selectedDirections == Vector2Int.left)
        {
            room.doorL.SetActive(false);
            selectedRoom.doorR.SetActive(false);
            
        }
        spawnedRooms[x, y] = selectedRoom;
        spawnedRooms[pos.x, pos.y] = room;

        return true;
    }

    public void SetCorridors()
    {
        int maxX = spawnedRooms.GetLength(0);
        int maxY = spawnedRooms.GetLength(1);
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms[i, j] != null && spawnedRooms[i, j].doorR != null)
                {
                    if (i + 1 < maxX && !spawnedRooms[i, j].doorR.activeInHierarchy)
                    {
                        Vector3 pos = spawnedRooms[i, j].doorR.transform.position;
                        float lenght = spawnedRooms[i + 1, j].doorL.transform.position.x -
                                       spawnedRooms[i, j].doorR.transform.position.x - 18;
                        for (int k = 0; k <= lenght + 1; k++)
                        {
                            if(k == 0)
                                Instantiate(LeftHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                            else if(k == lenght + 1)
                                Instantiate(RightHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                            else
                                Instantiate(CenterHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                        }
                    }
                }

                if (spawnedRooms[i, j] != null && spawnedRooms[i, j].doorU != null)
                {
                    if (j + 1  <= maxX && !spawnedRooms[i, j].doorU.activeInHierarchy)
                    {
                        Vector3 pos = spawnedRooms[i, j].doorU.transform.position;
                        float lenght = spawnedRooms[i, j + 1].doorD.transform.position.y -
                                       spawnedRooms[i, j].doorU.transform.position.y - 17;
                        Instantiate(DownVert, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                        for (int k = 0; k <= lenght + 1; k++)
                        {
                            if(k == lenght + 1)
                                Instantiate(TopVert, new Vector3(pos.x, pos.y + k, pos.z), Quaternion.identity);
                            else
                                Instantiate(CenterVert, new Vector3(pos.x, pos.y + k, pos.z), Quaternion.identity);
                        }
                    }
                }

                
            }
        }
    }
    public void SaveGame()
    {
        SaveSystem.SaveWorldData(this);
    }
}

