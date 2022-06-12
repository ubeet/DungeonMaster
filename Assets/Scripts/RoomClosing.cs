using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomClosing : MonoBehaviour
{
    private GameObject wallN, doorN;
    private GameObject wallE, doorE;
    private GameObject wallS, doorS;
    private GameObject wallW, doorW;
    private GameObject triggers;
    private EnemySpawn spawn;

    private void Start()
    {
        var thisRoom = transform.parent.parent;
        
        spawn = thisRoom.GetChild(thisRoom.childCount - 10).GetComponent<EnemySpawn>();
        wallN = thisRoom.GetChild(thisRoom.childCount - 9).gameObject;
        wallE = thisRoom.GetChild(thisRoom.childCount - 8).gameObject;
        wallS = thisRoom.GetChild(thisRoom.childCount - 7).gameObject;
        wallW = thisRoom.GetChild(thisRoom.childCount - 6).gameObject; 
        doorN = thisRoom.GetChild(thisRoom.childCount - 5).gameObject;
        doorE = thisRoom.GetChild(thisRoom.childCount - 4).gameObject;
        doorS = thisRoom.GetChild(thisRoom.childCount - 3).gameObject;
        doorW = thisRoom.GetChild(thisRoom.childCount - 2).gameObject;

        triggers = transform.parent.gameObject;
        if (wallN.activeInHierarchy) wallN = null;
        if (wallE.activeInHierarchy) wallE = null;
        if (wallS.activeInHierarchy) wallS = null;
        if (wallW.activeInHierarchy) wallW = null;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(wallN != null) doorN.SetActive(true);
            if(wallE != null) doorE.SetActive(true);
            if(wallS != null) doorS.SetActive(true);
            if(wallW != null) doorW.SetActive(true);
            
            spawn.IconEnable();
            spawn.AIEnable();
        }
    }

    internal void RoomOpening()
    {
        if(wallN != null) doorN.SetActive(false);
        if(wallE != null) doorE.SetActive(false);
        if(wallS != null) doorS.SetActive(false);
        if(wallW != null) doorW.SetActive(false);
        
        triggers.SetActive(false);
    }
}    
