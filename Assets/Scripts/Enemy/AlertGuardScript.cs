using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertGuardScript : MonoBehaviour
{
    List<GameObject> _nearbyEnemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AlertNearbyGuards() {
        _nearbyEnemies.ForEach(e => { e.GetComponent<WMEnemySightScript>().PlayerIsVisible(); });
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
