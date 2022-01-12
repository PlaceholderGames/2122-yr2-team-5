using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jumpSound, walkingSound, collectingSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        jumpSound = Resources.Load<AudioClip>("jump");
        walkingSound = Resources.Load<AudioClip>("walking");
        collectingSound = Resources.Load<AudioClip>("Collecting");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;
            case "walking":
                audioSrc.PlayOneShot(walkingSound);
                break;
            case "collecting":
                audioSrc.PlayOneShot(collectingSound);
                break;
        }
    }
}
