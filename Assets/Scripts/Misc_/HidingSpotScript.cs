using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    Material _mat;
    // Start is called before the first frame update
    void Start()
    {
        _mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player) || other.gameObject.tag.Equals(Tag.hiddenPlayer)) {
            _mat.color = new Color(_mat.color.r, _mat.color.g, _mat.color.b, .3f);
            other.gameObject.tag = Tag.hiddenPlayer;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.hiddenPlayer) || other.gameObject.tag.Equals(Tag.player))
        {
            _mat.color = new Color(_mat.color.r, _mat.color.g, _mat.color.b, 1f);
            other.gameObject.tag = Tag.player;
        }
    }
}
