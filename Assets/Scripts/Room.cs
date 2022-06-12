using UnityEngine;

public class Room : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] internal GameObject triggers;
    [SerializeField] internal GameObject wallN;
    [SerializeField] internal GameObject wallE;
    [SerializeField] internal GameObject wallS;
    [SerializeField] internal GameObject wallW;
    [SerializeField] internal GameObject chest;
}
