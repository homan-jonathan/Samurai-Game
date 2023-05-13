using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMainScript : MonoBehaviour
{
    public GameObject PLAYER;
    public XRayCapsuleScript _xRayCapsule;

    // Start is called before the first frame update
    void Start()
    {
        _xRayCapsule = GetComponentInChildren<XRayCapsuleScript>();
        //_xRayCapsule.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public GameObject GetPlayerReference()
    {
        return PLAYER;
    }
}
