using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static string path = Application.persistentDataPath + "Save.data";
    
    public static void SaveData(Player player)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        bf.Serialize(stream, data);
        stream.Close();
    }
    
    public static PlayerData LoadData()
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
            Debug.LogError("Save file not found in " + path);
            return null; 
        }
    }

    public static void DeleteData()
    {
        if (File.Exists(path))
            File.Delete(path);
    }
}
