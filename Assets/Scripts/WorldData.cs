
[System.Serializable]
public class WorldData
{
    public string[,] DungeonPositions;

    public WorldData(RoomPlacer rooms)
    {
        DungeonPositions = rooms.GetDungeonEls();
    }
}
