[System.Serializable]
public class WorldData
{
    public float[,,] position;
    public string[,] roomTag;
    public bool[,] triggers;
    public bool[,] isOpen;
    public bool[,] wallN;
    public bool[,] wallE;
    public bool[,] wallS;
    public bool[,] wallW;

    public WorldData(RoomPlacer rooms)
    {
        Room[,] room = rooms.GetSpawnedRooms();
        
        position = new float[room.GetLength(0), room.GetLength(1), 3];
        roomTag = new string[room.GetLength(0), room.GetLength(1)];
        triggers = new bool[room.GetLength(0), room.GetLength(1)];
        isOpen = new bool[room.GetLength(0), room.GetLength(1)];
        wallN = new bool[room.GetLength(0), room.GetLength(1)];
        wallW = new bool[room.GetLength(0), room.GetLength(1)];
        wallS = new bool[room.GetLength(0), room.GetLength(1)];
        wallE = new bool[room.GetLength(0), room.GetLength(1)];

        for (int i = 0; i < room.GetLength(0); i++)
        {
            for (int j = 0; j < room.GetLength(1); j++)
            {
                if (room[i, j] != null)
                {
                    RoomData roomData = new RoomData(room[i, j]);
                    for (int k = 0; k < position.GetLength(2); k++)
                        position[i, j, k] = roomData.position[k];
                    
                    triggers[i, j] = roomData.triggers;
                    roomTag[i, j] = roomData.roomTag;
                    isOpen[i, j] = roomData.isOpen;
                    wallN[i, j] = roomData.wallN;
                    wallE[i, j] = roomData.wallE;
                    wallS[i, j] = roomData.wallS;
                    wallW[i, j] = roomData.wallW;
                }
                
            }
        }
    }
    
}
