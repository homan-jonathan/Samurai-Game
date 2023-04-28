using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    MeshCollider _collider;
    GuardMoveScript _moveScript;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<MeshCollider>();
        _moveScript = GetComponentInParent<GuardMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _collider.enabled = !_moveScript._stopMovement;
    }
}
