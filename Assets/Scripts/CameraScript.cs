using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform _playerTransform;
    public Transform _headTransform;
    Transform _transform;

    enum Mode { FollowCam, OrbitCam }
    Mode _mode;
    public float _rotAmt = 0;
    public float ROTATION_SPEED = 1;

    Vector3 _cameraVelocity = Vector3.zero;
    Vector3 _offset;
    // Start is called before the first frame update
    void Start()
    {
        _mode = Mode.OrbitCam;
        _transform = transform;
        _offset = _transform.position - _playerTransform.position;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosn = _playerTransform.position
             + Quaternion.Euler(0, _rotAmt, 0) * _offset;
        _transform.position = Vector3.SmoothDamp(_transform.position, newPosn, ref _cameraVelocity, .1f);

        if (Input.GetKeyDown(KeyCode.C)) {
            if (_mode == Mode.FollowCam)
            {
                _mode = Mode.OrbitCam;
            }
            else {
                _mode = Mode.FollowCam;
            }
        }
    }

    private void LateUpdate()
    {
        switch (_mode) {
            case Mode.FollowCam:
                FollowCam();
                break;
            case Mode.OrbitCam:
                OrbitCam();
                break;
        }
    }

    void FollowCam() {
        _rotAmt = _playerTransform.rotation.eulerAngles.y;

        _transform.LookAt(_headTransform);
    }

    void OrbitCam() {
        if (Input.GetMouseButton(1)) { //right mouse button
            _rotAmt += Input.GetAxis("Mouse X") * ROTATION_SPEED;
        }

        _transform.LookAt(_headTransform);
    }
}
