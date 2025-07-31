using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Portal : MonoBehaviour
{
    public PortalType Type;


    [HideInInspector]
    public bool Teleporting;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerScript Player = collision.gameObject.GetComponent<PlayerScript>();
            switch (Type)
            {
                case PortalType.GravityPortal:
                    Player.gameObject.GetComponent<Rigidbody2D>().gravityScale *= -1;
                    break;
                case PortalType.JetpackPortal:
                    Player.GiveJetPack();
                    break;
                case PortalType.RegularPortal:
                    if (!Teleporting)
                    {
                        GameObject OutPut = null;
                        foreach (GameObject p in GameObject.FindGameObjectsWithTag("Portal"))
                        {
                            if (p != gameObject && OutPut == null)
                            {
                                OutPut = p;
                            }else if (p != gameObject && p.GetComponent<Portal>().Type == PortalType.RegularPortal && Vector3.Distance(transform.position, OutPut.transform.position) > Vector3.Distance(transform.position, p.transform.position))
                            {
                                OutPut = p;
                            }
                        }
                        if (OutPut != null)
                        {
                            Player.transform.position = OutPut.transform.position;
                            OutPut.GetComponent<Portal>().Teleporting = true;
                        }
                    }
                    break;
                case PortalType.GoalPortal:
                    LevelManager.levelManager.NextStage();
                    break;
            }
        }
    }
    public enum PortalType { RegularPortal, JetpackPortal, GravityPortal, GoalPortal }

}
