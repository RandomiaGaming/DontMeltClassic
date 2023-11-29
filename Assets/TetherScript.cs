using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TetherScript : MonoBehaviour
{

    public Sprite Glowing;
    private SpriteRenderer sr;
    private Sprite Normal;
    private Transform Player;
    private bool CanTether = true;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = GetComponent<SpriteRenderer>();
        Normal = sr.sprite;
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, Player.position) < 4 && InputManager.Jump() && CanTether)
        {
            LaunchPlayer();
        }
        if (Vector3.Distance(transform.position, Player.position) > 4 && !CanTether)
        {
            CanTether = true;
        }
        if (CanTether && Vector3.Distance(transform.position, Player.position) < 4)
        {
            sr.sprite = Glowing;
        }
        else
        {
            sr.sprite = Normal;
        }
        Vector2 direction = transform.position - Player.position;
        direction = new Vector2(Mathf.Sign(direction.x), Mathf.Sign(direction.y));
        transform.localScale = direction;
    }
    public void LaunchPlayer()
    {
        CanTether = false;
        Player.gameObject.GetComponent<Rigidbody2D>().velocity = transform.localScale * 11.5f;
    }
}
