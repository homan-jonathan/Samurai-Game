using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMainScript : MonoBehaviour
{
    public GameObject PLAYER;
    GuardShaderScript _shaderScript;
    //public XRayCapsuleScript _xRayCapsule;

    // Start is called before the first frame update
    void Start()
    {
        //_xRayCapsule = GetComponentInChildren<XRayCapsuleScript>();
        //_xRayCapsule.gameObject.SetActive(false);
        _shaderScript = GetComponentInChildren<GuardShaderScript>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "TagGuardCollider")
        {
            if (Input.GetKeyDown(KeyBinding.interact())){
                _shaderScript._isTagged = true;
                print("tag enemy");
            }
        }
    }

    public GameObject GetPlayerReference()
    {
        return PLAYER;
    }
}
