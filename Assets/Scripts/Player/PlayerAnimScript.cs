using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    Animator _anim;
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
        if(_playerScript.isCrouched)
        {
            _anim.SetBool("Crouched", true);
        }
        else
        {
            _anim.SetBool("Crouched", false);
        }

        if(_playerScript.isMoving)
        {
            _anim.SetBool("Moving", true);
        }
        else
        {
            _anim.SetBool("Moving", false);
        }
    }
}
