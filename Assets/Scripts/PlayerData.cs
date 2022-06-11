[System.Serializable]

public class PlayerData
{
    public int health { get; set; }
    public int money { get; set; }
    public float[] position { get; set; }

    internal PlayerData(Player player)
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
