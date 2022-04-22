[System.Serializable]
public class WorldData
{
    public float[,,] position;
    public bool[,] doorU;
    public bool[,] doorR;
    public bool[,] doorD;
    public bool[,] doorL;
    public string[,] roomTag;

    public WorldData(RoomPlacer rooms)
    {
        Room[,] room = rooms.GetSpawnedRooms();
        
        position = new float[room.GetLength(0), room.GetLength(1), 3];
        doorU = new bool[room.GetLength(0), room.GetLength(1)];
        doorL = new bool[room.GetLength(0), room.GetLength(1)];
        doorD = new bool[room.GetLength(0), room.GetLength(1)];
        doorR = new bool[room.GetLength(0), room.GetLength(1)];
        roomTag = new string[room.GetLength(0), room.GetLength(1)];
        
        for (int i = 0; i < room.GetLength(0); i++)
        {
            for (int j = 0; j < room.GetLength(1); j++)
            {
                if (room[i, j] != null)
                {
                    RoomData roomData = new RoomData(room[i, j]);
                    for (int k = 0; k < position.GetLength(2); k++)
                    {
                        position[i, j, k] = roomData.position[k];
                    }
                    doorD[i, j] = roomData.doorD;
                    doorU[i, j] = roomData.doorU;
                    doorL[i, j] = roomData.doorL;
                    doorR[i, j] = roomData.doorR;
                    roomTag[i, j] = roomData.roomTag;
                }
                
            }
        }
    }
    
}
