using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundsScript : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip playerHit;
    public AudioClip zoneChange;
    public AudioClip pickedUpCoins;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerHitNoise()
    {
        audioSource.PlayOneShot(playerHit);
    }

    public void ZoneChangeNoise()
    {
        audioSource.PlayOneShot(zoneChange);
    }

    public void PickedUpCoinsNoise()
    {
        audioSource.PlayOneShot(pickedUpCoins);
    }
}
