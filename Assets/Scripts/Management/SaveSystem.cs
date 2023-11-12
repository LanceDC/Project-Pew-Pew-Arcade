using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SaveGameNumber(GameManager.GameNumber number)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/GameNumber.number";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, number);
        stream.Close();
    }

    public static GameManager.GameNumber LoadGameNumber()
    {
        string path = Application.persistentDataPath + "/GameNumber.number";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameManager.GameNumber number = formatter.Deserialize(stream) as GameManager.GameNumber;
            stream.Close();

            return number;
        }
        else
        {
            //Debug.LogError("Path not found : " + path);
            return null;
        }
    }

    public static void SavePlayer(GameManager manager, int place)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/PlayerSocre" + place + ".score";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(manager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(int place)
    {
        string path = Application.persistentDataPath + "/PlayerSocre" + place + ".score";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Path not found : " + path);
            return null;
        }
    }
}
