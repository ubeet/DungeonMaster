using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemyGO;
    private RoomClosing room;
    private void Start()
    {
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
        {
            transform.GetChild(i).GetChild(0).GetChild(0).GetComponent<Enemy>().AI = true;
        }
    }
    
    private void Update()
    {
        Debug.Log(transform.childCount);
        if(transform.childCount == 0) room.RoomOpening();
    }
}
