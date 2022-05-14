using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RoomData
{
    public float[] position;
    public bool doorU;
    public bool doorR;
    public bool doorD;
    public bool doorL;
    public string roomTag;
    public bool triggers;

    public RoomData(Room room)
    {
        position = new float[3];
        
        position[0] = room.gameObject.transform.position.x;
        position[1] = room.gameObject.transform.position.y;
        position[2] = room.gameObject.transform.position.z;
        
        roomTag = room.tag;
        
        if (room.triggers != null) triggers = room.triggers.activeInHierarchy;

        if (room.doorU != null) doorU = room.doorU.activeInHierarchy;
        if (room.doorR != null) doorR = room.doorR.activeInHierarchy;
        if (room.doorD != null) doorD = room.doorD.activeInHierarchy;
        if (room.doorL != null) doorL = room.doorL.activeInHierarchy;

    }

}
