using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyGO; 
    
    //private GameObject winScreen;
    private RoomClosing room;
    private bool isBigRoom;
    
    private void Start()
    {
        isBigRoom = transform.parent.parent.CompareTag("BiggestRoom");
        Invoke(nameof(SpawnEnemies), 1);
    }
    

    private void SpawnEnemies()
    {
        room = transform.parent.GetChild(transform.parent.childCount - 1).GetChild(0).GetComponent<RoomClosing>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var enemy = Instantiate(enemyGO);
            enemy.transform.SetParent(child);
            enemy.transform.localRotation = Quaternion.Euler(0, 0, 0);
            enemy.transform.localPosition = new Vector3(0, 0, 0);
            enemy.transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
            enemy.transform.GetChild(0).GetComponent<Enemy>().AI = false;
        }
    }

    internal void AIEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Enemy>().AI = true;
    }
    internal void AIDisable()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Enemy>().AI = false;
    }
    
    internal void IconEnable()
    {
        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).GetChild(0).GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Player>().isDead) AIDisable();
        if (transform.childCount == 0)
        {
            var guiManager = GameObject.FindWithTag("GUI").GetComponent<GUIManager>();
            if (isBigRoom && !guiManager._win)
                guiManager.Win();
            room.RoomOpening();
        }
    }
}
