using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject _player;
    Transform _playerTransform;
    Transform _transform;

    enum Mode { FollowCam, OrbitCam }
    Mode _mode;
    float _rotAmt = 0;

    Vector3 _cameraVelocity = Vector3.zero;
    Vector3 _offset = new Vector3(0, 3, -3);
    // Start is called before the first frame update
    void Start()
    {
        _mode = Mode.OrbitCam;
        _playerTransform = _player.transform;
        _transform = transform;
        _transform.position = _playerTransform.position + _offset;
        _transform.LookAt(_playerTransform);
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

    }

    void OrbitCam() {
        if (Input.GetKey(KeyCode.E))
        {
            _rotAmt++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _rotAmt--;
        }

        _transform.LookAt(_playerTransform);
    }
}
