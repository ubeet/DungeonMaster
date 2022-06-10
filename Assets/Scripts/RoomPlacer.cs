using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room startRoom;
    public Room shopRoom;
    public Room chestRoom;
    public Room finalRoom;
    public int dungeonSize;
    public int roomStartX;
    public int roomStartY;
    public int roomsAmount;
    public int chestAmount;
    public GameObject LeftHor;
    public GameObject RightHor;
    public GameObject CenterHor;
    public GameObject TopVert;
    public GameObject DownVert;
    public GameObject CenterVert;

    private Room[,] spawnedRooms;
    
    private void Start()
    {
        spawnedRooms = new Room[dungeonSize, dungeonSize];
        if (File.Exists(SaveSystem.WorldPath))
        {
            var qu = Quaternion.Euler(-90f, 0f, 0f);
            WorldData data = SaveSystem.LoadWorldData();

            for (int i = 0; i < spawnedRooms.GetLength(0); i++)
            {
                for (int j = 0; j < spawnedRooms.GetLength(1); j++)
                {
                    if (data.roomTag[i, j] != null)
                    {
                        Room room = null;
                        if (startRoom.CompareTag(data.roomTag[i, j]))
                        {
                            room = Instantiate(startRoom,
                                new Vector3(data.position[i, j, 0], data.position[i, j, 1],
                                    data.position[i, j, 2]), Quaternion.identity);
                            room.gameObject.transform.localRotation = qu;
                        }
                        if (shopRoom.CompareTag(data.roomTag[i, j]))
                        {
                            room = Instantiate(shopRoom,
                                new Vector3(data.position[i, j, 0], data.position[i, j, 1], 
                                    data.position[i, j, 2]), Quaternion.identity);
                            room.gameObject.transform.localRotation = qu;
                        }
                        if (finalRoom.CompareTag(data.roomTag[i, j]))
                        {
                            room = Instantiate(finalRoom,
                                new Vector3(data.position[i, j, 0], data.position[i, j, 1],
                                    data.position[i, j, 2]), Quaternion.identity);
                            room.gameObject.transform.localRotation = qu;
                        }
                        if (chestRoom.CompareTag(data.roomTag[i, j]))
                        {
                            room = Instantiate(chestRoom,
                                new Vector3(data.position[i, j, 0], data.position[i, j, 1],
                                    data.position[i, j, 2]), Quaternion.identity);
                            room.gameObject.transform.localRotation = qu;
                            //if (data.isOpen[i, j])
                            //{
                            //    room.transform.GetChild(0).GetComponent<Chest>().isOpen = true;
                            //    room.transform.GetChild(0).GetComponent<Chest>().OpenChest();
                            //}
                        }
                        foreach (var el in roomPrefabs)
                        {
                            if (el.CompareTag(data.roomTag[i, j]))
                            {
                                room = Instantiate(el, new Vector3(data.position[i, j, 0],
                                    data.position[i, j, 1], data.position[i, j, 2]), Quaternion.identity);
                                room.gameObject.transform.localRotation = qu;
                                break;
                            }
                        }
                        if (room != null)
                        {
                            if(!data.wallN[i, j])
                                if (room.wallN != null)
                                    room.wallN.SetActive(false);

                            if(!data.wallE[i, j])
                                if (room.wallE != null)
                                    room.wallE.SetActive(false);
                                
                            if(!data.wallS[i, j])
                                if (room.wallS != null)
                                    room.wallS.SetActive(false);
                                
                            if(!data.wallW[i, j])
                                if (room.wallW != null)
                                    room.wallW.SetActive(false);
                            
                            if (!data.triggers[i, j])
                                if (room.triggers != null)
                                    room.triggers.SetActive(false);
                            var roomChild = room.transform.GetChild(0);
                            if (!roomChild.GetChild(roomChild.childCount - 1).gameObject.activeInHierarchy)
                            {
                                Debug.Log(":asdjasd");
                                Destroy(room.transform.GetChild(0).GetChild(roomChild.childCount - 6).gameObject);
                            }
                        }
                        spawnedRooms[i, j] = room;
                    }
                }
            }
        }
        else
        {
            var start = Instantiate(startRoom);
            start.transform.position = new Vector3((roomStartX - 5) * 29, (roomStartY - 5) * 24, 0);
            spawnedRooms[roomStartX, roomStartY] = start;
            for (int i = 0; i < roomsAmount; i++) PlaceOneRoom();
            for (int i = 0; i < chestAmount; i++) PlaceOneRoom(chestRoom);
            PlaceOneRoom(shopRoom);
            PlaceOneRoom(finalRoom);
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
        
        for (int limit = 0; limit < 25; limit++)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            /*if (position.x + 1 <= maxX && spawnedRooms[position.x + 1, position.y]?.tag == "BiggestRoom" ||
                position.x - 1 >= 0 && spawnedRooms[position.x - 1, position.y]?.tag == "BiggestRoom")
            {
                Destroy(newRoom.gameObject);
                newRoom = Instantiate(roomPrefabs[0]);
            }*/
            
            if (ConnectToRooms(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * 29, (position.y - 5) * 24, 0);
                spawnedRooms[position.x, position.y] = newRoom;
                newRoom.GetComponent<NavMeshSurface2d>().BuildNavMesh();
                break;
            }
        }
    }
    
    private void PlaceOneRoom(Room room)
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
        Room newRoom = Instantiate(room);
        
        for (int limit = 0; limit < 25;limit++)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            /*if (position.x + 1 <= maxX && spawnedRooms[position.x + 1, position.y]?.tag == "BiggestRoom" ||
                position.x - 1 >= 0 && spawnedRooms[position.x - 1, position.y]?.tag == "BiggestRoom")
            {
                Destroy(newRoom.gameObject);
                newRoom = Instantiate(roomPrefabs[0]);
            }*/
            
            if (ConnectToRooms(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * 29, (position.y - 5) * 24, 0);
                spawnedRooms[position.x, position.y] = newRoom;
                newRoom.GetComponent<NavMeshSurface2d>().BuildNavMesh();
                break;
            }
        }
    }

    private bool ConnectToRooms(Room room, Vector2Int pos)
    {
        
        int maxX = spawnedRooms.GetLength(0) - 1;
        int maxY = spawnedRooms.GetLength(1) - 1;
        
        Debug.LogWarning(room.wallN != null);
        Debug.LogWarning(pos.y < maxY);
        Debug.LogWarning(spawnedRooms[pos.x, pos.y + 1]?.wallS != null);
        
        List<Vector2Int> neighbours = new List<Vector2Int>();
        
        if(room.wallN != null && pos.y < maxY && spawnedRooms[pos.x, pos.y + 1]?.wallS != null)  neighbours.Add(Vector2Int.up);
        if(room.wallE != null && pos.x < maxX && spawnedRooms[pos.x + 1, pos.y]?.wallW != null)  neighbours.Add(Vector2Int.right);
        if(room.wallS != null && pos.y > 0 && spawnedRooms[pos.x, pos.y - 1]?.wallN != null)  neighbours.Add(Vector2Int.down);
        if(room.wallW != null && pos.x > 0 && spawnedRooms[pos.x - 1, pos.y]?.wallE != null)  neighbours.Add(Vector2Int.left);

        if (neighbours.Count == 0) return false;
        
        Vector2Int selectedDirections = neighbours[Random.Range(0, neighbours.Count)];
        int x = pos.x + selectedDirections.x;
        int y = pos.y + selectedDirections.y;
        
        Room selectedRoom = spawnedRooms[x, y];
        if (selectedDirections == Vector2Int.up)
        {
            
            room.wallN.SetActive(false);
            selectedRoom.wallS.SetActive(false);
        }
        else if (selectedDirections == Vector2Int.right)
        {
            room.wallE.SetActive(false);
            selectedRoom.wallW.SetActive(false);
        }
        else if (selectedDirections == Vector2Int.down)
        {
            room.wallS.SetActive(false);
            selectedRoom.wallN.SetActive(false);
        }
        else if (selectedDirections == Vector2Int.left)
        {
            room.wallW.SetActive(false);
            selectedRoom.wallE.SetActive(false);
        }
        spawnedRooms[x, y] = selectedRoom;
        spawnedRooms[pos.x, pos.y] = room;

        return true;
    }

    private void SetCorridors()
    {
        int maxX = spawnedRooms.GetLength(0);
        int maxY = spawnedRooms.GetLength(1);
        for (int i = 0; i < spawnedRooms.GetLength(0); i++)
        {
            for (int j = 0; j < spawnedRooms.GetLength(1); j++)
            {
                if (spawnedRooms[i, j] != null && spawnedRooms[i, j].wallE != null)
                {
                    if (i + 1 < maxX && !spawnedRooms[i, j].wallE.activeInHierarchy)
                    {
                        Vector3 pos = spawnedRooms[i, j].wallE.transform.position;
                        float length = spawnedRooms[i + 1, j].wallW.transform.position.x -
                                       spawnedRooms[i, j].wallE.transform.position.x - 18;
                        for (int k = 0; k <= length + 1; k++)
                        {
                            if(k == 0)
                                Instantiate(LeftHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                            else if(k == length + 1)
                                Instantiate(RightHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                            else
                                Instantiate(CenterHor, new Vector3(pos.x + k, pos.y, pos.z), Quaternion.identity);
                        }
                    }
                }

                if (spawnedRooms[i, j] != null && spawnedRooms[i, j].wallN != null)
                {
                    if (j + 1  <= maxX && !spawnedRooms[i, j].wallN.activeInHierarchy)
                    {
                        Vector3 pos = spawnedRooms[i, j].wallN.transform.position;
                        float length = spawnedRooms[i, j + 1].wallS.transform.position.y -
                                       spawnedRooms[i, j].wallN.transform.position.y - 17;
                        Instantiate(DownVert, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);
                        for (int k = 0; k <= length + 1; k++)
                        {
                            if(k == length + 1)
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

