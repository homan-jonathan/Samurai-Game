using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRaycastScript : MonoBehaviour
{
    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask))
        {
            var obj = hit.collider.gameObject;
            if(obj.tag == Tag.enemy)
            {
                Debug.Log($"looking at {obj.name}", this);
            }

        }
    }
}
