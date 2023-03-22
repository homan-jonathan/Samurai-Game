using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject _player;
    Transform _playerTransform;
    Transform _transform;
    PlayerMoveScript _playerScript;

    Vector3 _cameraVelocity = Vector3.zero;
    Vector3 _offset = new Vector3(0, 3, -3);
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = _player.transform;
        _playerScript = _player.GetComponent<PlayerMoveScript>();
        _transform = transform;
        _transform.position = _playerTransform.position + _offset;
        _transform.LookAt(_playerTransform);
    }

    // Update is called once per frame
    void Update()
    {
        /* Vector3 newPosn = _playerTransform.position
             + Quaternion.Euler(0, _playerScript._rotAmt, 0) * _offset;
         _transform.position = Vector3.SmoothDamp(_transform.position, newPosn, ref _cameraVelocity, .1f);

         _transform.position = _playerTransform.position + Quaternion.Euler(0, _playerScript._rotAmt, 0) * _offset;
         _transform.LookAt(_playerTransform);

         if (Input.GetKey(KeyCode.E))
         {
             _transform.RotateAround(_playerTransform.position, Vector3.up, 1);
         }
         if (Input.GetKey(KeyCode.Q))
         {
             _transform.RotateAround(_playerTransform.position, Vector3.up, -1);
         }

         _offset = _transform.position - _playerTransform.position;
         _transform.position = _playerTransform.position + _offset;
         _transform.RotateAround(_playerTransform.position, Vector3.up, _playerScript._rotAmt);*/
        Vector3 newPosn = _playerTransform.position
             + Quaternion.Euler(0, _playerScript._rotAmt, 0) * _offset;
                 _transform.position = Vector3.SmoothDamp(_transform.position, newPosn, ref _cameraVelocity, .1f);

        _transform.LookAt(_playerTransform);

    }
}
