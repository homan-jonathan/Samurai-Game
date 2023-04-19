using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNPCScript : MonoBehaviour
{
    Transform _transform;
    Transform _playerTransform;
    PlayerMoveScript _playerScript;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        _playerScript = FindObjectOfType<PlayerMoveScript>();
        _playerTransform = _playerScript.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.LookAt(_playerTransform);
        _transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
