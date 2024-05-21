using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeMusicPhase : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void changeMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
