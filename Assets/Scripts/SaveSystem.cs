using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SaveSystem
{
    public static void Save<T>(string key, T saveData)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + $"/{key}.dat");

        bf.Serialize(file, saveData);
        file.Close();
    }

    public static T Load<T>(string key) where T : new()
    {
        if (File.Exists(Application.persistentDataPath + $"/{key}.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + $"/{key}.dat", FileMode.Open);
            T data = (T)bf.Deserialize(file);
            file.Close();
            return data;
        }

        else
        {
            return new T();
        }
    }
}
