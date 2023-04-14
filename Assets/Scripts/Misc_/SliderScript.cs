using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    Transform _transform;
    public CameraScript _playerCam;
    public PlayerMoveScript _player;

    public Canvas _canvas;

    public bool showSlider;
    void Start()
    {
        _transform = transform;
        _player = FindObjectOfType<PlayerMoveScript>();
        _playerCam = FindObjectOfType<CameraScript>();
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //print(_player.IsCrouched());
        if (_player.IsCrouched())
        {
            //gameObject.SetActive(true);
            _canvas.enabled = true;
        }
        else
        {
            //gameObject.SetActive(false);
            _canvas.enabled = false;
        }
    }
    void LateUpdate()
    {
        _transform.LookAt(_playerCam.transform.position);
    }
}
