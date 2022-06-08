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
        obj = Instantiate(Items[Random.Range(0, Items.Length)], childPoint, true);
        price = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        price.text = obj.cost.ToString();
        isMedicine = obj.isBuff;
        obj.isBuff = false;
        obj.isInInventory = false;
        obj.gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        if (playerInRange)
        {
            playerObj = other.gameObject.GetComponent<Player>();
            if (Input.GetKeyUp(KeyCode.E) && playerObj.currentMoney >= obj.cost)
            {
    
                if (isMedicine)
                    playerObj.TakeHealing(obj.tag.Equals("Medicine") ? playerObj.mHealing : playerObj.pHealing);
                else
                {
                    var gunCircle = other.gameObject.transform.GetChild(1);
                    var newGun = Instantiate(obj, gunCircle, true);
                    newGun.isInInventory = true;
                    newGun.gameObject.transform.localPosition = new Vector3(0.3f, 0, 0);
                    newGun.gameObject.transform.localScale = new Vector3(2.02f, 2.02f, 1);
                    var e = Quaternion.Euler(0f, 0f, 180f);
                    newGun.gameObject.transform.localRotation = e;
                    newGun.gameObject.SetActive(false);
                }
                playerObj.TakeMoney(-obj.cost);
                Destroy(obj.gameObject);
            }
        }
    }

    
}
