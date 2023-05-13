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

        Vector3 startPoint = transform.position + (_charCon.center + new Vector3(0, 1f, 0));

        if (Physics.SphereCast(startPoint, _charCon.height / 2, transform.forward, out hit, 10000))
        {
            var obj = hit.collider.gameObject;

            if (obj.tag == Tag.enemy)
            {
                return true;
            }
        }
        return false;
    }
}
