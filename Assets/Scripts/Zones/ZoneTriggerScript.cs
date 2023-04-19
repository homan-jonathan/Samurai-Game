using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTriggerScript : MonoBehaviour
{
    public string entertingString;
    public string exitingString;
    public GameCanvasScript gameCanvasScript;

    bool flipVar = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player)) {
            if (flipVar)
            {
                StartCoroutine(gameCanvasScript.DisplayZoneText(entertingString));
                FindObjectOfType<PlayerSoundsScript>().ZoneChangeNoise();
            }
            else
            {
                StartCoroutine(gameCanvasScript.DisplayZoneText(exitingString));
            }
            flipVar = !flipVar;
        }
    }
}
