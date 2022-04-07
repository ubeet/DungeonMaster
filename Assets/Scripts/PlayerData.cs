using UnityEngine;

[System.Serializable]

public class PlayerData
{
    public int health;
    public int money;
    public float[] position;

    public PlayerData(Player player)
    {
        health = player.currentHealth;
        money = player.currentMoney;
        position = new float[3];
        var position1 = player.transform.position;
        position[0] = position1.x;
        position[1] = position1.y;
        position[2] = position1.z;
    }
}
