using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class RoomData
{
    public float[] position;
    public bool wallN;
    public bool wallE;
    public bool wallS;
    public bool wallW;
    public string roomTag;
    public bool triggers;
    public bool isOpen;

    public RoomData(Room room)
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
