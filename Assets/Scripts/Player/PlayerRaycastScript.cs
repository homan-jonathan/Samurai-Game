using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerRaycastScript : MonoBehaviour
{
    public LayerMask mask;
    Transform _transform;
    CharacterController _charCon;

    bool _tagEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _charCon = GetComponent<CharacterController>();
    }

    private void Update()
    {
        /*if (RaycastHitsEnemy())
        {
            print("tag enemy");
            _tagEnemy = true;
        }
        else
        {
            _tagEnemy = false;
        }*/
        CheckEnemyRaycast();
    }

    private void CheckEnemyRaycast()
    {
        RaycastHit hit;
        
        Vector3 startPoint = transform.position + _charCon.center;

        if (Physics.SphereCast(startPoint, _charCon.height/4, transform.forward, out hit, Mathf.Infinity))
        {
            var obj = hit.collider.gameObject;

            if (obj.name == "PlayerRaycastTarget" && Input.GetKeyDown(KeyBinding.interact()))
            {
                GuardShaderScript _script = obj.GetComponentInParent<GuardShaderScript>();
                _script._isTagged = true;
                print("tag enemy");
                //return true;
            }
        }
        //return false;
    }
}
