using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{


    public int Level;


    [System.Serializable]
    class SaveData
    {
        public int Level = 1;

    }

    public void Start()
    {

        if (GameObject.FindGameObjectsWithTag("GameController").Length > 1)
        {
            Destroy(gameObject);
        }else
        {
            DontDestroyOnLoad(this);
            Load();
            if (Level < 1)
            {
                Level = 1;
            }
        }
        
       
    }

    public  void Save()
    {
        BinaryFormatter binary = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");
        SaveData Data = new SaveData();
        Data.Level = Level;
        binary.Serialize(file, Data);
        file.Close();
    }

    public void Load()
    {
       
        if (File.Exists(Application.persistentDataPath + "/SaveGame.dat"))
        {
            BinaryFormatter binary = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SaveGame.dat", FileMode.Open);
            SaveData Data = (SaveData)binary.Deserialize(file);
            file.Close();
            Level = Data.Level;
        }
    }
 
}