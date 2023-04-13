using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    Transform _transform;
    public CameraScript _playerCam;
    public PlayerMoveScript _player;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        /*_player = FindObjectOfType<PlayerMoveScript>();
        _playerCam = _player*/
        _playerCam = FindObjectOfType<CameraScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        _transform.LookAt(_playerCam.transform.position);
    }
}
