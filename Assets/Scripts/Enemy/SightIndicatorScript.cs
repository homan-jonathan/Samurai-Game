using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightIndicatorScript : MonoBehaviour
{
    public Image warningImage;
    public Color warningColor;
    public Color spottedColor;
    
    SphereCollider sphereCollider;
    EnemySightScript enemySightScript;
    // Start is called before the first frame update
    void Start()
    {
        enemySightScript = GetComponent<EnemySightScript>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemySightScript._seenPlayerRecently > 0)
        {
            warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
        }
        else
        {
            warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
        }
        sphereCollider.radius = enemySightScript.GetViewDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            warningImage.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            warningImage.enabled = false;
        }
    }
}
