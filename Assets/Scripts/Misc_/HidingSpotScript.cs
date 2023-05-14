using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    Material[] _mats;
    // Start is called before the first frame update
    void Start()
    {
        Renderer[] renderers = GetComponents<Renderer>();
        _mats = new Material[renderers.Length];
        for (int i =0; i < renderers.Length; i++) { 
            _mats[i] = renderers[i].material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterHidingSpot(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player) || other.gameObject.tag.Equals(Tag.hiddenPlayer))
        {
            foreach (var mat in _mats)
            {
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, .1f);
            }
            other.gameObject.tag = Tag.hiddenPlayer;
        }
    }

    public void ExitHidingSpot(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.hiddenPlayer) || other.gameObject.tag.Equals(Tag.player))
        {
            foreach (var mat in _mats)
            {
                mat.color = new Color(mat.color.r, mat.color.g, mat.color.b, 1f);
            }
            other.gameObject.tag = Tag.player;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        EnterHidingSpot(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ExitHidingSpot(other);
    }
}
