using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomClosing : MonoBehaviour
{
    private GameObject triggers;
    private GameObject wallN;
    private GameObject wallE;
    private GameObject wallS;
    private GameObject wallW;
    private EnemySpawn spawn;

    public void Start()
    {
        var thisRoom = transform.parent.parent;
        
        spawn = thisRoom.GetChild(thisRoom.childCount - 6).GetComponent<EnemySpawn>();
        wallN = thisRoom.GetChild(thisRoom.childCount - 5).gameObject;
        wallE = thisRoom.GetChild(thisRoom.childCount - 4).gameObject;
        wallS = thisRoom.GetChild(thisRoom.childCount - 3).gameObject;
        wallW = thisRoom.GetChild(thisRoom.childCount - 2).gameObject; 
        
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
            if(wallN != null) wallN.SetActive(true);
            if(wallE != null) wallE.SetActive(true);
            if(wallS != null) wallS.SetActive(true);
            if(wallW != null) wallW.SetActive(true);
            spawn.IconEnable();
            spawn.AIEnable();
        }
    }

    internal void RoomOpening()
    {
        if(wallN != null) wallN.SetActive(false);
        if(wallE != null) wallE.SetActive(false);
        if(wallS != null) wallS.SetActive(false);
        if(wallW != null) wallW.SetActive(false);
        triggers.SetActive(false);
    }
}    
