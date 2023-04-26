using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffSetScript : MonoBehaviour
{
    public Transform target;
    Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y + _offset.y, target.position.z);
    }
}
