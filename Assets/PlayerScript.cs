using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerState { Dead, Alive, Animating, HasJetPack, InCannon }

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public Sprite WithJetPack;
    public Sprite WithJetPackOn;
    private Sprite Normal;
    private PlayerState playerState = PlayerState.Alive;
    private Transform CurrentCannon = null;
    public GameObject DeathParticle;
    private Vector2 StartPos;
    void Start()
    {
        StartPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
        Normal = sr.sprite;
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.localScale = new Vector3(Mathf.Sign(rb.velocity.x), Mathf.Sign(rb.gravityScale), 1);
        if (playerState == PlayerState.InCannon)
        {
            if (CurrentCannon != null)
            {
                transform.position = CurrentCannon.transform.position;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                sr.enabled = false;
                if (InputManager.Jump())
                {
                    rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                    sr.enabled = true;
                    rb.velocity = CurrentCannon.transform.localScale * 20f;
                    CurrentCannon = null;
                    playerState = PlayerState.Alive;
                }
            }
            else
            {
                playerState = PlayerState.Alive;
            }
        }
        else if (playerState == PlayerState.HasJetPack)
        {
            rb.velocity = new Vector2(10, rb.velocity.y);
            sr.sprite = WithJetPack;
            if (InputManager.JumpHeld())
            {
                sr.sprite = WithJetPackOn;
                rb.velocity = new Vector2(10, 8 * Mathf.Sign(rb.gravityScale));
            }
            else if (!InputManager.JumpHeld() && Mathf.Sign(rb.velocity.y) == Mathf.Sign(rb.gravityScale))
            {
                rb.velocity = new Vector2(10, 0);
            }
        }
        else if (playerState == PlayerState.Alive)
        {
            sr.sprite = Normal;
        }
        else if (playerState == PlayerState.Dead)
        {
            Instantiate(DeathParticle, transform);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            sr.enabled = false;
            playerState = PlayerState.Animating;
        }
        else if (playerState == PlayerState.Animating && GameObject.FindGameObjectWithTag("DeathPartical") == null)
        {
            //Reset
            playerState = PlayerState.Alive;
            CurrentCannon = null;
            rb.gravityScale = 1;
            transform.position = StartPos;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.velocity = Vector2.zero;
            GetComponent<SpriteRenderer>().enabled = true;
            sr.sprite = Normal;
        }
    }

    public void KillPlayer()
    {
        if (playerState == PlayerState.Alive || playerState == PlayerState.HasJetPack || playerState == PlayerState.InCannon)
        {
            playerState = PlayerState.Dead;
        }
    }
    public void GiveJetPack()
    {
        if (playerState == PlayerState.Alive || playerState == PlayerState.InCannon)
        {
            CurrentCannon = null;
            playerState = PlayerState.HasJetPack;
        }
        else if (playerState == PlayerState.HasJetPack)
        {
            CurrentCannon = null;
            playerState = PlayerState.Alive;
        }
    }
    public void SetCannon(Transform tr)
    {
        if (playerState == PlayerState.Alive || playerState == PlayerState.HasJetPack)
        {
            CurrentCannon = tr;
            playerState = PlayerState.InCannon;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Lava")
        {
            KillPlayer();
        }
    }
}
