using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveCharacter(Character character)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/character.rofl";
        FileStream stream = new FileStream(path, FileMode.Create);

        CharacterData data = new CharacterData(character);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static CharacterData LoadCharacter()
    {
        string path = Application.persistentDataPath + "/character.rofl";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            CharacterData data =  formatter.Deserialize(stream) as CharacterData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    
}
