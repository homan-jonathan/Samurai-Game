using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{
    public LayerMask mask;
    Transform _transform;
    CharacterController _charCon;
 
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _charCon = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (RaycastHitsEnemy())
        {
            print("facing enemy");
        }
    }

    private bool RaycastHitsEnemy()
    {
        RaycastHit hit;

        Vector3 startPoint = transform.position + _charCon.center;

        if (Physics.SphereCast(startPoint, _charCon.height/4, transform.forward, out hit, Mathf.Infinity))
        {
            var obj = hit.collider.gameObject;

            if (obj.tag == Tag.playerRaycastTarget)
            {
                return true;
            }
        }
        return false;
    }
}
