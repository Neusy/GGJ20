using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] songs;
    private uint currentClip;
    private AudioSource src;

    void Setup() {
        currentClip = 0;
    }

    // Start is called before the first frame update
    void Awake()
    {
        src = GetComponent<AudioSource>();
        src.clip = songs[currentClip];
        src.Play();
    }

    void FixedUpdate() {
        if (!src.isPlaying) {
            currentClip += 1;
            if (currentClip == songs.Length)
                currentClip = 1;   // Skip the "blind" beats
            src.clip = songs[currentClip];
            src.Play();
        }
    }
}
