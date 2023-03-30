using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    private Animator _anim;
    public GameObject _player;
    PlayerMoveScript _playerScript;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerScript = _player.GetComponent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isCrouched", _playerScript.IsCrouched());
        _anim.SetBool("isMoving", _playerScript.IsMoving());
        /*if(_playerScript.isCrouched)
        {
            _anim.SetBool("isCrouched", true);
        }
        else
        {
            _anim.SetBool("isCrouched", false);
        }*/

        /*if (_playerScript.isMoving)
        {
            _anim.SetBool("isMoving", true);
        }
        else
        {
            _anim.SetBool("isMoving", false);
        }*/
    }
}
