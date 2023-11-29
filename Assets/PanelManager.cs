using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public GameObject[] Panels;
    public GameObject current;

    private void Start()
    {
        SetPanel(current);
    }

    public void SetPanel(GameObject Panel)
    {
        current = Panel;
        foreach (GameObject go in Panels)
        {
            if (go == current)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
