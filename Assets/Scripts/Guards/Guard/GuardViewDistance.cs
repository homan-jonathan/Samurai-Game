using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardViewDistance : MonoBehaviour
{
    public bool _playerInPossibleViewRange = false;
    public float EPISLON_VISIBILITY_RANGE = 2f;

    GuardSightScript _enemySightScript;
    SphereCollider _sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        _enemySightScript = GetComponentInParent<GuardSightScript>();
        _sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_sphereCollider.radius != _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE)
        {
            _sphereCollider.radius = _enemySightScript.CalculateViewDistance() * EPISLON_VISIBILITY_RANGE;
        }
    }

    public bool IsPlayerInPossibleViewRange()
    {
        return _playerInPossibleViewRange;
    }

    public float GetMaxViewRange()
    {
        return EPISLON_VISIBILITY_RANGE;
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
