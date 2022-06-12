using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    private static GameObject gb;
    
    public int weaponSwitch { get; set; } = 0;
    
    private void Start()
    {
        SwitchWeapon();
    }

    private void Update()
    {
        int curWeapon = weaponSwitch;
        
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (weaponSwitch >= transform.childCount - 1) weaponSwitch = 0;
            else weaponSwitch++;
        }
        
        if(curWeapon != weaponSwitch) SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == weaponSwitch) weapon.gameObject.SetActive(true);
            else weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
