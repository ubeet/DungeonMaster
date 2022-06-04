using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSpawn : Interactable
{
    [SerializeField] private Item[] Items;
    private Transform childPoint;
    private bool isMedicine;
    private Item obj;
    private Text price;
    private Player playerObj;
    
    private void Start()
    {
        childPoint = transform.GetChild(1);
        obj = Instantiate(Items[Random.Range(0, Items.Length)]);
        price = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        price.text = obj.cost.ToString();
        isMedicine = obj.isMedicine;
        obj.isMedicine = false;
        obj.gameObject.transform.SetParent(childPoint);
        obj.gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        playerObj = other.gameObject.GetComponent<Player>();
        if (Input.GetKeyUp(KeyCode.E) && playerInRange && playerObj.currentMoney >= obj.cost)
        {
            if (isMedicine)
            {
                playerObj.TakeHealing(obj.tag.Equals("Medicine") ? playerObj.mHealing : playerObj.pHealing);
            }
            else
            {
                var gunCircle = other.gameObject.transform.GetChild(1);
                var newGun = Instantiate(obj);
                newGun.gameObject.transform.SetParent(gunCircle);
                newGun.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            }
            playerObj.TakeMoney(-obj.cost);
            Destroy(obj.gameObject);
        }

    }

    
}
