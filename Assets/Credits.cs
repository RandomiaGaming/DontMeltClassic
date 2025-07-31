using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Credits : MonoBehaviour
{
    private void Start()
    {
        LevelManager.CurrentStage = 0;
        File.Delete(Application.persistentDataPath + "/SaveGame.dat");
    }
    void Update()
    {
        if (InputManager.Jump())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
}
