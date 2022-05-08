using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomClosing : MonoBehaviour
{
    public GameObject doorU;
    public GameObject doorR;
    public GameObject doorD;
    public GameObject doorL;
    public GameObject triggers;
    private bool isClosed;

    

    public void Start()
    {
        if (doorU.activeInHierarchy) doorU = null;
        if (doorR.activeInHierarchy) doorR = null;
        if (doorD.activeInHierarchy) doorD = null;
        if (doorL.activeInHierarchy) doorL = null;
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(doorU != null) doorU.SetActive(true);
            if(doorR != null) doorR.SetActive(true);
            if(doorD != null) doorD.SetActive(true);
            if(doorL != null) doorL.SetActive(true);
        }

        isClosed = true;
    }

    private void Update()
    {
        
        if (isClosed)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if(doorU != null) doorU.SetActive(false);
                if(doorR != null) doorR.SetActive(false);
                if(doorD != null) doorD.SetActive(false);
                if(doorL != null) doorL.SetActive(false);
                Destroy(triggers);
            }
        }
        
    }
}    
