using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Item : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] internal bool isBuff;
    [SerializeField] internal int cost;
    [SerializeField] internal bool isInInventory;
}
