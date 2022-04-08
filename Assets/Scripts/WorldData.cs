using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WorldData
{
    public string[,] DungeonPositions;

    public WorldData(RoomPlacer rooms)
    {
        DungeonPositions = rooms.GetDungeonEls();
    }
}
