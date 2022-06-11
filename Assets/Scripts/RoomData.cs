using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class RoomData
{
    public float[] position { get; set; }
    public bool wallN { get; set; }
    public bool wallE { get; set; }
    public bool wallS { get; set; }
    public bool wallW { get; set; }
    public string roomTag { get; set; }
    public bool triggers { get; set; }
    public bool isOpen { get; set; }

    internal RoomData(Room room)
    {
        position = new float[3];
        
        position[0] = room.gameObject.transform.position.x;
        position[1] = room.gameObject.transform.position.y;
        position[2] = room.gameObject.transform.position.z;
        
        roomTag = room.tag;
        
        if (room.triggers != null) triggers = room.triggers.activeInHierarchy;
        if (room.chest != null) isOpen = room.chest.GetComponent<Chest>().isOpen;

        if (room.wallN != null) wallN = room.wallN.activeInHierarchy;
        if (room.wallE != null) wallE = room.wallE.activeInHierarchy;
        if (room.wallS != null) wallS = room.wallS.activeInHierarchy;
        if (room.wallW != null) wallW = room.wallW.activeInHierarchy;

    }

}
