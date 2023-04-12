using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SightIndicatorScript : MonoBehaviour
{
    EnemySightScript _enemySightScript;
    SphereCollider _sphereCollider;
    public bool _playerInPossibleViewRange = false;
    public float EPISLON_VISIBILITY_RANGE = 2f;
    // Start is called before the first frame update
    void Start()
    {
        _enemySightScript = GetComponentInParent<EnemySightScript>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sphereCollider.radius != _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE) {
            _sphereCollider.radius = _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE;
            _enemySightScript.warningImage.enabled = false;
        }
        //_sphereCollider.radius = _enemySightScript.VIEW_DISTANCE * _enemySightScript.PLAYER_RUNNING_MULTIPLIER;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
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
