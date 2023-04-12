using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightIndicatorScript : MonoBehaviour
{
    public Image warningImage;
    public Color warningColor;
    public Color spottedColor;
    
    EnemySightScript _enemySightScript;
    SphereCollider _sphereCollider;
    public bool _playerInPossibleViewRange = false;
    // Start is called before the first frame update
    void Start()
    {
        _enemySightScript = GetComponentInParent<EnemySightScript>();
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = _enemySightScript.VIEW_DISTANCE * _enemySightScript.PLAYER_RUNNING_MULTIPLIER;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_enemySightScript._seenPlayerRecently > 0)
        {
            warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
        }
        else
        {
            warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
        }*/
        
        
        //_sphereCollider.radius = _enemySightScript.GetViewDistance();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            //warningImage.enabled = true;
            _playerInPossibleViewRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            _playerInPossibleViewRange = false;
        }
    }
}
