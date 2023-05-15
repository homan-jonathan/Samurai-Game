using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAlertScript : MonoBehaviour
{
    List<GameObject> _nearbyEnemies = new List<GameObject>();
    public float EPISLON_ALERT_RANGE = .5f;

    GuardSightScript _enemySightScript;
    SphereCollider _sphereCollider;
    GuardMainScript _guardMainScript;
    // Start is called before the first frame update
    void Start()
    {
        _enemySightScript = GetComponentInParent<GuardSightScript>();
        _sphereCollider = GetComponent<SphereCollider>();
        _guardMainScript = GetComponentInParent<GuardMainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_guardMainScript.isActive)
        {
            return;
        }

        if (_sphereCollider.radius != _enemySightScript.CalculateViewDistance() * EPISLON_ALERT_RANGE)
        {
            _sphereCollider.radius = _enemySightScript.CalculateViewDistance() * EPISLON_ALERT_RANGE;
        }
    }

    public void AlertNearbyGuards()
    {
        _nearbyEnemies.ForEach(e => { e.GetComponent<GuardSightScript>().SetPlayerIsVisible(); });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.enemy))
        {
            _nearbyEnemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.enemy))
        {
            _nearbyEnemies.Remove(other.gameObject);
        }
    }
}
