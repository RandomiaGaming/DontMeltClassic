using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    
    void Update()
    {
        if (InputManager.Jump())
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
