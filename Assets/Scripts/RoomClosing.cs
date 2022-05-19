using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RoomClosing : MonoBehaviour
{
    public GameObject wallN;
    public GameObject wallE;
    public GameObject wallS;
    public GameObject wallW;
    public GameObject triggers;
    private bool isClosed;

    public void Start()
    {
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
        }

        isClosed = true;
    }

    private void Update()
    {
        if (isClosed)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if(wallN != null) wallN.SetActive(false);
                if(wallE != null) wallE.SetActive(false);
                if(wallS != null) wallS.SetActive(false);
                if(wallW != null) wallW.SetActive(false);
                triggers.SetActive(false);
            }
        }
    }
}    
