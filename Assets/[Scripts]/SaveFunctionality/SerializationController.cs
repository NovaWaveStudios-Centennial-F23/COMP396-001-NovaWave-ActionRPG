using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class SerializationController
{
    public static bool Save(string saveName, object data)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/mysticrealms"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/mysticrealms");
        }

        string path = Application.persistentDataPath + "/mysticrealms/" + saveName + ".xml";

        StreamWriter writer = new StreamWriter(path);
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

        serializer.Serialize(writer, data);
        writer.Close();

        return true;
    }

    public static object Load(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(SaveData)); 
        
        if (!File.Exists(path))
        {
            return null;
        }

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object data = serializer.Deserialize(file);
            file.Close();
            return data;
        }
        catch
        {
            file.Close();
            throw new NullReferenceException("Cannot load file");
        }
    }

    public static void Delete(string path)
    {
        if (!File.Exists(path))
        {
            Debug.Log("Save Does not exist");
        }
        else
        {
            File.Delete(path);
        }
    }
}
