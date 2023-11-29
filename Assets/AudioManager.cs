using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource Source;
    public AudioClip[] Songs;
    private System.Random rnd = new System.Random();
    private void Start()
    {
        Source = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!Source.isPlaying)
        {
            Source.clip = Songs[rnd.Next(0, Songs.Length - 1)];
            Source.Play();
        }
    }
}
