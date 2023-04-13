using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    private Animator _anim;
    public GameObject _player;
    PlayerMoveScript _playerMoveScript;
    PlayerMainScript _playerMainScript;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerMoveScript = _player.GetComponent<PlayerMoveScript>();
        _playerMainScript= _player.GetComponent<PlayerMainScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isCrouched", _playerMoveScript.IsCrouched());
        _anim.SetBool("isWalking", _playerMoveScript.IsWalking());
        _anim.SetBool("isRunning", _playerMoveScript.IsRunning());

        //_anim.SetBool("isJumping", _playerMoveScript.IsJumping());
        _anim.SetBool("isGrounded", _playerMoveScript.IsGrounded());
        _anim.SetBool("isFalling", _playerMoveScript.IsFalling());

        //_anim.SetBool("isDead", _playerMainScript.IsDead());


        
    }

    public void PlayDeathAnim() {
        //_anim.Play("Dead");
        _anim.SetTrigger("isDead");
        
    }

    public void PlayJumpAnim()
    {
        _anim.SetTrigger("hasJumped");
    }

    /*public void PlayJumpAnimation()
    {
        _anim.SetTrigger("HasJumped");
        _anim.ResetTrigger("HasJumped");
    }*/
}
