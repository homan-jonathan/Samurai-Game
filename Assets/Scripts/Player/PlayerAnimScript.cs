using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimScript : MonoBehaviour
{
    Animator _anim;
    PlayerMoveScript _playerMoveScript;
    PlayerSoundsScript _playerSoundsScript;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _playerMoveScript = GetComponent<PlayerMoveScript>();
        _playerSoundsScript = GetComponent<PlayerSoundsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isCrouched", _playerMoveScript.IsCrouched());
        _anim.SetBool("isWalking", _playerMoveScript.IsWalking());
        _anim.SetBool("isRunning", _playerMoveScript.IsRunning());
        _anim.SetBool("isGrounded", _playerMoveScript.IsGrounded());
        _anim.SetBool("isFalling", _playerMoveScript.IsFalling());
    }

    public void PlayDeathAnim() {
        _anim.SetTrigger("isDead");
        _playerSoundsScript.PlayerHitNoise();
    }

    public void PlayJumpAnim()
    {
        _anim.SetTrigger("hasJumped");
    }

    public void PlayPickupCoinsAnim()
    {
        _anim.SetTrigger("pickupCoins");
        _playerSoundsScript.PickedUpCoinsNoise();
    }
}
