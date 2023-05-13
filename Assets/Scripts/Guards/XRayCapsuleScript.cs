using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayCapsuleScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerMoveScript _playerScript;
    MeshRenderer _meshRenderer;
    void Start()
    {
        _playerScript = FindObjectOfType<PlayerMoveScript>();
        _meshRenderer = GetComponent<MeshRenderer>();
        //_meshRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnBecameVisible()
    {
        //_meshRenderer.enabled = false;
    }
    private void OnBecameInvisible()
    {
        _meshRenderer.enabled = true;
    }
}
