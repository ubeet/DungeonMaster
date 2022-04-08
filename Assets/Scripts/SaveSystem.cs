using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static string path = Application.persistentDataPath + "Save.data";
    public static string worldPath = Application.persistentDataPath + "WorldSave.data";
    
    public static void SavePlayerData(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        bf.Serialize(stream, data);
        stream.Close();
    }
    
    public static void SaveWorldData(RoomPlacer rooms)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(worldPath, FileMode.Create);
    
            WorldData data = new WorldData(rooms);
            
            bf.Serialize(stream, data);
            stream.Close();
        }
    
    public static PlayerData LoadPlayerData()
        {
            if (File.Exists(path))
            {
                BinaryFormatter bf = new BinaryFormatter(); 
                FileStream stream = new FileStream(path, FileMode.Open);
                PlayerData data = bf.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Player save file not found in " + path);
                return null; 
            }
        }
    
    
    
    public static WorldData LoadWorldData()
    {
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter(); 
            FileStream stream = new FileStream(worldPath, FileMode.Open);
            WorldData data = bf.Deserialize(stream) as WorldData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("World save file not found in " + worldPath);
            return null; 
        }
    }
    

    public static void DeleteData()
    {
        if (File.Exists(path))
            File.Delete(path);
        if (File.Exists(worldPath))
            File.Delete(worldPath);
    }
}
