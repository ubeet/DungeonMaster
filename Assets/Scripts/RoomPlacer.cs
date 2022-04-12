using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room startRoom;
    public int dungeonSize;
    public int roomStartX;
    public int roomStartY;
    public int roomsAmount;
    
    public Corridor corVert;
    public Corridor corHor;

    private Room[,] spawnedRooms;
    private string[,] dungeonEls;


    private void Start()
    {
        if (File.Exists(SaveSystem.worldPath))
        {
            WorldData data = SaveSystem.LoadWorldData();
            dungeonEls = data.DungeonPositions;
            int maxX = dungeonEls.GetLength(0);
            int maxY = dungeonEls.GetLength(1);
            for (int x = 0; x < dungeonEls.GetLength(0); x++)
            {
                for (int y = 0; y < dungeonEls.GetLength(1); y++)
                {
                    Vector3 roomPos = new Vector3((x-10) * 14, (y-10) * 12, 0);
                    if (dungeonEls[x, y] != null)
                    {
                        if (dungeonEls[x, y] == "Room")
                        {
                            Room room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)]);
                            if(x - 1 > 0 && dungeonEls[x - 1, y] == corHor.gameObject.tag) room.doorL.SetActive(false);
                            if(x + 1 < maxX && dungeonEls[x + 1, y] == corHor.gameObject.tag) room.doorR.SetActive(false);
                            if(y + 1 < maxY && dungeonEls[x, y + 1] == corVert.gameObject.tag) room.doorU.SetActive(false);
                            if(y - 1 > 0 && dungeonEls[x, y - 1] == corVert.gameObject.tag) room.doorD.SetActive(false);
                            room.transform.position = roomPos;
                        }else if (dungeonEls[x, y] == startRoom.gameObject.tag)
                        {
                            Room room = startRoom;
                            room.doorD.SetActive(false);
                            room.transform.position = roomPos;
                        }else if (dungeonEls[x, y] == corVert.gameObject.tag)
                        {
                            Corridor cor = Instantiate(corVert);
                            cor.transform.position = new Vector3((x-10) * 14, (y-11) * 12, 0);
                        }else if (dungeonEls[x, y] == corHor.gameObject.tag)
                        {
                            Corridor cor = Instantiate(corHor);
                            cor.transform.position = new Vector3((x - 11) * 14, (y - 10) * 12, 0);
                        }
                    }
                }
            }
            
        }
            
        else
        {
            spawnedRooms = new Room[dungeonSize,dungeonSize];
            dungeonEls = new string[spawnedRooms.GetLength(0) * 2 - 1, spawnedRooms.GetLength(1) * 2 - 1];
            dungeonEls[roomStartX*2, roomStartY*2] = startRoom.gameObject.tag;
            spawnedRooms[roomStartX, roomStartY] = startRoom;
            for (int i = 0; i < roomsAmount; i++)
            {
                PlaceOneRoom();
            }
        }

    }

    public string[,] GetDungeonEls()
    {
        return dungeonEls;
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
                dungeonEls[position.x*2, position.y*2] = newRoom.gameObject.tag;
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
            dungeonEls[x * 2, y * 2 - 1] = corVert.gameObject.tag;
        }
        else if (selectedDirections == Vector2Int.right)
        {
            room.doorR.SetActive(false);
            selectedRoom.doorL.SetActive(false);
            Corridor cor = Instantiate(corHor, new Vector3((pos.x - 5) * 28, (pos.y - 5) * 24, 0), Quaternion.identity);            
            dungeonEls[x * 2 - 1, y * 2] = corHor.gameObject.tag;
        }
        else if (selectedDirections == Vector2Int.down)
        {
            room.doorD.SetActive(false);
            selectedRoom.doorU.SetActive(false);
            Corridor cor = Instantiate(corVert, new Vector3((pos.x - 5) * 28, (pos.y - 5) * 24 - 24, 0), Quaternion.identity);
            dungeonEls[x * 2, y * 2 + 1] = corVert.gameObject.tag;
        }
        else if (selectedDirections == Vector2Int.left)
        {
            room.doorL.SetActive(false);
            selectedRoom.doorR.SetActive(false);
            Corridor cor = Instantiate(corHor, new Vector3((pos.x - 5) * 28 - 28, (pos.y - 5) * 24, 0), Quaternion.identity);            
            dungeonEls[x * 2 + 1, y * 2] = corHor.gameObject.tag;
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

