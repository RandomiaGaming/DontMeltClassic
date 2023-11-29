using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class QuitButton : MonoBehaviour
{

    void Update()
    {
        if (!LevelManager.levelManager.StageComplete.activeInHierarchy)
        {
            if (InputManager.Jump())
            {
                List<RaycastResult> results = new List<RaycastResult>();
                PointerEventData PointerData = new PointerEventData(EventSystem.current);
                PointerData.position = InputManager.Pos();
                EventSystem.current.RaycastAll(PointerData, results);
                foreach (RaycastResult result in results)
                {
                    if (result.gameObject == gameObject)
                    {
                        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                    }
                }
            }
            else if (InputManager.Back())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
