using System;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    
    public static readonly string PlayerPath = Application.persistentDataPath + "PlayerSave.data";
    public static readonly string WorldPath = Application.persistentDataPath + "WorldSave.data";

    public static void SavePlayerData(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(PlayerPath, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        bf.Serialize(stream, data);
        
        stream.Close();
    }
    
    public static void SaveWorldData(RoomPlacer rooms)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(WorldPath, FileMode.Create);
        
        WorldData data = new WorldData(rooms);
        try
        {
            bf.Serialize(stream, data);
        }
        catch (Exception e)
        {
            Debug.LogError("World save file not found in " + WorldPath);
            throw;
        }
        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayerData()
        {
            if (File.Exists(PlayerPath))
            {
                BinaryFormatter bf = new BinaryFormatter(); 
                FileStream stream = new FileStream(PlayerPath, FileMode.Open);
                
                PlayerData data = bf.Deserialize(stream) as PlayerData;
                
                stream.Close();
                return data;
            }
            
            Debug.LogError("Player save file not found in " + PlayerPath);
            return null; 
            
        }

    public static WorldData LoadWorldData()
    {
        if (File.Exists(WorldPath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(WorldPath, FileMode.Open);
            
            WorldData data = bf.Deserialize(stream) as WorldData;
            
            stream.Close();
            return data;
        }
        
        Debug.LogError("World save file not found in " + WorldPath);
        return null;
        
    }
    
    public static void DeleteData()
    {
        if (File.Exists(PlayerPath))
            File.Delete(PlayerPath);
        if (File.Exists(WorldPath))
            File.Delete(WorldPath);
    }
}
