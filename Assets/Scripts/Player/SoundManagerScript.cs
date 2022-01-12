using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, walkingSound, collectingSound;
    public static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        walkingSound = Resources.Load<AudioClip>("walking");
        collectingSound = Resources.Load<AudioClip>("collecting");

        audioSrc = GetComponent<AudioSource>();
    }

    public static void PlaySound(Sounds sound)
    {
        switch (sound)
        {
            case Sounds.JUMP:
                audioSrc.PlayOneShot(jumpSound);
                break;
            case Sounds.WALKING:
                audioSrc.PlayOneShot(walkingSound);
                break;
            case Sounds.COLLECTING:
                audioSrc.PlayOneShot(collectingSound);
                break;
        }
    }
}

public enum Sounds
{
    JUMP = 0,
    WALKING = 1,
    COLLECTING = 2
}
