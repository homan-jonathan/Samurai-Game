using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    CameraScript _playerCam;
    Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _playerCam = FindObjectOfType<CameraScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _transform.LookAt(_playerCam.transform.position);
    }
}
