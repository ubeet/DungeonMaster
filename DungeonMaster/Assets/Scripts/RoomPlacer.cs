using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomPlacer : MonoBehaviour
{
    public Room[] roomPrefabs;
    public Room startRoom;
    public GameObject corVert;
    public GameObject corHor;

    private Room[,] spawnedRooms;
    
    
    private void Start()
    {
        spawnedRooms = new Room[11, 11];
        spawnedRooms[5, 5] = startRoom;
        for (int i = 0; i < 6; i++)
        {
            PlaceOneRoom();
        }
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
        
        Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
        ConnectToRooms(newRoom, position);
        newRoom.transform.position = new Vector3((position.x - 5) * 36, (position.y - 5) * 27, 0);
        spawnedRooms[position.x, position.y] = newRoom;
        /*int limit = 500;
        while (limit-- > 0)
        {
            Vector2Int position = vacantPlaces.ElementAt(Random.Range(0, vacantPlaces.Count));
            if (ConnectToRooms(newRoom, position))
            {
                newRoom.transform.position = new Vector3((position.x - 5) * 36, (position.y - 5) * 27, 0);
                spawnedRooms[position.x, position.y] = newRoom;
                break;
            }
        }
        */
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
        Room selectedRoom = spawnedRooms[pos.x + selectedDirections.x, pos.y + selectedDirections.y];
        if (selectedDirections == Vector2Int.up)
        {
            room.doorU.SetActive(false);
            selectedRoom.doorD.SetActive(false);
            Instantiate(corVert, new Vector3((pos.x - 5) * 36, (pos.y - 5) * 27, 0), Quaternion.identity);
            //GameObject corridorU = Instantiate(corVert);
            //corridorU.transform.position = room.transform.position;
        }
        else if (selectedDirections == Vector2Int.right)
        {
            room.doorR.SetActive(false);
            selectedRoom.doorL.SetActive(false);
            Instantiate(corHor, new Vector3((pos.x - 5) * 36, (pos.y - 5) * 27, 0), Quaternion.identity);            //GameObject corridorL = Instantiate(corHor);
            //GameObject corridorR = Instantiate(corHor);
            //corridorR.transform.position = room.transform.position;
        }
        else if (selectedDirections == Vector2Int.down)
        {
            room.doorD.SetActive(false);
            selectedRoom.doorU.SetActive(false);
            Instantiate(corVert, new Vector3((pos.x - 5) * 36, (pos.y - 5) * 27 - 27, 0), Quaternion.identity);
            //GameObject corridorD = Instantiate(corVert);
            //corridorD.transform.position = new Vector3(room.transform.position.x - 36, room.transform.position.y, room.transform.position.z);
        }
        else if (selectedDirections == Vector2Int.left)
        {
            room.doorL.SetActive(false);
            selectedRoom.doorR.SetActive(false);
            Instantiate(corHor, new Vector3((pos.x - 5) * 36 - 36, (pos.y - 5) * 27, 0), Quaternion.identity);            //GameObject corridorL = Instantiate(corHor);
            //corridorL.transform.position = new Vector3(room.transform.position.x, room.transform.position.y - 27, room.transform.position.z);
        }

        return true;
    }
}
