using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMGuardSoundsScript : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip swordSlash;
    public AudioClip guardAlerted;
    public AudioClip guardSpotted;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwordSlashNoise() {
        audioSource.PlayOneShot(swordSlash);
    }

    public void GuardAlertedNoise() {
        audioSource.PlayOneShot(guardAlerted);
    }

    public void GuardSpottedNoise()
    {
        audioSource.PlayOneShot(guardSpotted);
    }
}
