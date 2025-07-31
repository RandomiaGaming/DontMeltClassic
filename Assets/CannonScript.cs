using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonScript : MonoBehaviour
{
    private float Timer = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerScript>().SetCannon(transform);
        }
    }

    private void Update()
    {
        if (Timer >= 1)
        {
            if ((Vector2)transform.localScale == new Vector2(1, 1))
            {
                transform.localScale = new Vector2(1, -1);
            }
            else if ((Vector2)transform.localScale == new Vector2(1, -1))
            {
                transform.localScale = new Vector2(-1, -1);
            }
            else if ((Vector2)transform.localScale == new Vector2(-1, -1))
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
            Timer = 0;
        }
        else
        {
            Timer += Time.deltaTime;
        }
    }
}

