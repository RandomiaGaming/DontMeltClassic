using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class LevelManager : MonoBehaviour
{
    public static int CurrentStage = 0;
    public static LevelManager levelManager;
    public GameObject StageComplete;

    private void Awake()
    {
        levelManager = this;
        if (File.Exists(Application.persistentDataPath + "/SaveGame.dat"))
        {
            string FileData = File.ReadAllText(Application.persistentDataPath + "/SaveGame.dat");
           CurrentStage = int.Parse(FileData);
        }else
        {
            FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");
            StreamWriter writer = new StreamWriter(file);
            writer.Write(CurrentStage.ToString());
            file.Close();
        }
        if (CurrentStage < transform.childCount)
        {
            if (!transform.GetChild(CurrentStage).gameObject.activeInHierarchy)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                transform.GetChild(CurrentStage).gameObject.SetActive(true);
            }
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }

    private void Update()
    {
        if (StageComplete.activeInHierarchy && InputManager.Jump())
        {
            StageComplete.SetActive(false);
            if (CurrentStage < transform.childCount)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                transform.GetChild(CurrentStage).gameObject.SetActive(true);
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }
    }
    public void NextStage()
    {
        StageComplete.SetActive(true);
        CurrentStage++;
        FileStream file = File.Create(Application.persistentDataPath + "/SaveGame.dat");
        StreamWriter writer = new StreamWriter(file);
        writer.Write(CurrentStage.ToString());
        writer.Dispose();
        file.Close();
    }
}
