using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    Transform _transform;
    CameraScript _playerCam;
    PlayerMoveScript _player;
    Canvas _canvas;
    void Start()
    {
        _transform = transform;
        _player = FindObjectOfType<PlayerMoveScript>();
        _playerCam = FindObjectOfType<CameraScript>();
        _canvas = GetComponentInParent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsCrouched())
        {
            _canvas.enabled = true;
        }
        else
        {
            _canvas.enabled = false;
        }
    }
    void LateUpdate()
    {
        _transform.LookAt(_playerCam.transform.position);
    }
}
