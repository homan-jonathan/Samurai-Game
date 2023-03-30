using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform _playerTransform;
    Transform _transform;

    public float _rotAmtX = 0;
    public float _rotAmtY = 0;
    public float ROTATION_SPEED = 1;
    public float VERTICAL_CAMERA_MOVEMENT = 0;
    public float Y_LOOKAT_OFFSET = 1;
    public float Y_RETURN_SPEED = .25f;
    public float SMOOTH_TIME = 1f;

    enum Mode { FollowCam, OrbitCam }
    Mode _mode;

    //Vector3 _cameraVelocity = Vector3.zero;
    Vector3 _offset;
    Vector3 _lookAtLocation;
    float _rotAmtSpeedX = 0;
    // Start is called before the first frame update
    void Start()
    {
        _mode = Mode.OrbitCam;
        _transform = transform;
        _offset = _transform.position - _playerTransform.position;
        _lookAtLocation = _playerTransform.position + new Vector3(0, Y_LOOKAT_OFFSET, 0);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Update Look at Target
        _lookAtLocation = new Vector3(_playerTransform.position.x, _lookAtLocation.y, _playerTransform.position.z);
        if (Mathf.Abs(_playerTransform.position.y - _lookAtLocation.y) > VERTICAL_CAMERA_MOVEMENT)
        {
            _lookAtLocation.y = _playerTransform.position.y + Y_LOOKAT_OFFSET;
        }

        //Move the camera
        Vector3 newPosn = _playerTransform.position
             + Quaternion.Euler(_rotAmtY, _rotAmtX, 0) * _offset;
        _transform.position = newPosn;//Vector3.SmoothDamp(_transform.position, newPosn, ref _cameraVelocity, SMOOTH_TIME * Time.deltaTime);
        //Trend Y rotation to 0
        if (Mathf.Abs(_rotAmtY) > Y_RETURN_SPEED * 2 - .1 && !Input.GetMouseButton(1)) {
            if (_rotAmtY > 0)
            {
                _rotAmtY -= Y_RETURN_SPEED;
            }
            else {
                _rotAmtY += Y_RETURN_SPEED;
            }
        }

        //Use the specified camera mode
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
        _rotAmtX = _playerTransform.rotation.eulerAngles.y; //NEEDS FIXED/IMPROVED
        if (Input.GetMouseButton(1))
        {
            UpdateYRotation();
        }

        _transform.LookAt(_lookAtLocation);
    }

    void OrbitCam() {
        if (Input.GetMouseButton(1)) { //right mouse button
            _rotAmtX += Input.GetAxis("Mouse X") * ROTATION_SPEED;
            UpdateYRotation();
        }

        _transform.LookAt(_lookAtLocation);
    }

    void UpdateYRotation() {
        _rotAmtY += Input.GetAxis("Mouse Y") * ROTATION_SPEED / 4;
        _rotAmtY = Mathf.Clamp(_rotAmtY, -20, 20);
    }
}
