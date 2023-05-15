using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardActivationScript : MonoBehaviour
{
    EnemyPointersScript _enemyPointersScript;
    // Start is called before the first frame update
    void Start()
    {
        _enemyPointersScript = FindObjectOfType<EnemyPointersScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tag.enemy) {
            other.GetComponent<GuardMainScript>().ActivateGuard();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == Tag.enemy)
        {
            other.GetComponent<GuardMainScript>().DeactivateGuard();
            _enemyPointersScript.ClearTarget(other.transform);
        }
    }
}
