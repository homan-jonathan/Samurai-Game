using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMainScript : MonoBehaviour
{
    public bool isDead = false;
    public bool isActive = false;
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

        public void ActivateGuard()
    {
        isActive = true;
    }

    public void DeactivateGuard()
    {
        isActive = false;
    }

    public void killGuard() {
        isDead = true;
    }

    public GameObject GetPlayerReference()
    {
        return PLAYER;
    }
}
