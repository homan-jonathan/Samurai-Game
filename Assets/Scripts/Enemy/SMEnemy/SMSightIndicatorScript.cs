using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SMSightIndicatorScript : MonoBehaviour
{
    public bool _playerInPossibleViewRange = false;
    public float EPISLON_VISIBILITY_RANGE = 2f;

    SMEnemySightScript _enemySightScript;
    SphereCollider _sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        _enemySightScript = GetComponentInParent<SMEnemySightScript>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sphereCollider.radius != _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE) {
            _sphereCollider.radius = _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE;
            _enemySightScript.warningImage.enabled = false;
        }
        //_sphereCollider.radius = _sightScript.VIEW_DISTANCE * _sightScript.PLAYER_RUNNING_MULTIPLIER;
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

    public bool IsPlayerInPossibleViewRange() {
        return _playerInPossibleViewRange;
    }

    public float GetVisibilityInicatorRange() {
        return EPISLON_VISIBILITY_RANGE;
    }
}
