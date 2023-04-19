using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    public Transform cameraTransform;

    CharacterController _charCon;
    PlayerAnimScript _animScript;
    PlayerMainScript _mainScript;
    CameraScript _cameraScript;
    Transform _transform;

    public float CROUCH_MOVESPEED = 2.0F;
    public float WALK_MOVESPEED = 4.0F;
    public float RUN_MOVESPEED = 6.0F;
    public float JUMP_HEIGHT = 7.0F;
    public float CHARGED_JUMP_HEIGHT = 15.0f;
    public float GRAVITY = 20.0F;
    public float ROTATION_SPEED = 720F;

    Vector3 _moveDirection = Vector3.zero;
    float _speed;
    float _ySpeed = 0;

    //movement logic
    bool _ableToJump = true;
    bool _isJumping = false;
    bool _isGrounded = true;
    bool _isFalling = false;

    //logic for charging jump
    bool _hasChargedJump = false;
    float _chargeJumpTimer = 0.0f;
    public float NEEDED_TO_JUMP = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _cameraScript = cameraTransform.GetComponent<CameraScript>();
        _animScript = GetComponent<PlayerAnimScript>();
        _mainScript = GetComponent<PlayerMainScript>();
        _transform = transform;

        _speed = WALK_MOVESPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainScript.IsDead()) {
            return;
        }

        if (IsCrouched())
        {
            ChargeJump();
            _speed = CROUCH_MOVESPEED;
        } else if (IsRunning()) 
        {
            _speed = RUN_MOVESPEED;
            _ableToJump = true;
        } 
        else if (IsWalking())
        {
            _speed = WALK_MOVESPEED;
            _ableToJump = true;
        }

        float timePassed = 0.0f;

        if (_charCon.isGrounded)
        {
            timePassed = 0.0f;
            _ySpeed = -1;
            _isGrounded = true;
            _isJumping = false;
            _isFalling = false;

            if (Input.GetKeyDown(KeyBinding.jump()) && _ableToJump)
            {
                Jump();
                _animScript.PlayJumpAnim();
            }
        }
        else
        {
            _isGrounded = false;
            timePassed += Time.deltaTime;
            _ySpeed -= Time.deltaTime * GRAVITY;

            if (timePassed >= 0.25f)
            {
                _isFalling = true;
            }
        }
        

        switch (_cameraScript._mode)
        {
            case CameraScript.Mode.OrbitCam:
                float rotationRelativeToCamera1 = cameraTransform.rotation.eulerAngles.y;
                _moveDirection = Quaternion.Euler(0, rotationRelativeToCamera1, 0) * new Vector3(_speed * Input.GetAxis("Horizontal"), 0, _speed * Input.GetAxis("Vertical"));

                if (_moveDirection != Vector3.zero)
                {
                    Quaternion toRotation1 = Quaternion.LookRotation(_moveDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation1, ROTATION_SPEED * Time.deltaTime);
                }
                break;
            case CameraScript.Mode.FollowCam:
                float rotationRelativeToCamera2 = _cameraScript._rotAmtX;
                _moveDirection = Quaternion.Euler(0, rotationRelativeToCamera2, 0) * new Vector3(0, 0, _speed * Input.GetAxis("Vertical"));

                Quaternion toRotation2 = Quaternion.Euler(0, rotationRelativeToCamera2 + _speed * Input.GetAxis("Horizontal"), 0);
                transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation2, ROTATION_SPEED * Time.deltaTime);

                break;
        }

        _transform.Translate(_moveDirection * _speed * Time.deltaTime + new Vector3(0, _ySpeed, 0) * Time.deltaTime, Space.World);

        _charCon.Move(_moveDirection * Time.deltaTime + new Vector3(0, _ySpeed, 0) * Time.deltaTime);
    }

    void ChargeJump() {
        if (!IsWalking() && !IsRunning() && !IsJumping() && !IsFalling())
        {
            _chargeJumpTimer += Time.deltaTime;
        }
        else
        {
            _chargeJumpTimer += Time.deltaTime * 0.10f;
        }

        if (_chargeJumpTimer >= NEEDED_TO_JUMP)
        {
            _hasChargedJump = true;
        }
        else
        {
            _hasChargedJump = false;
        }
    }
    void Jump() {
        if (_hasChargedJump)
        {
            _ySpeed = CHARGED_JUMP_HEIGHT;
            _hasChargedJump = false;
            _chargeJumpTimer = 0.0f;
        }
        else
        {
            _ySpeed = JUMP_HEIGHT;
        }
        _isJumping = true;
    }

    public bool IsCrouched() {
        if (Input.GetKey(KeyBinding.crouch()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsWalking()
    {
        if (_moveDirection != Vector3.zero && !IsRunning())
        {   
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRunning() {
        if (_moveDirection != Vector3.zero && Input.GetKey(KeyBinding.sprint()))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    public bool IsJumping()
    {
        return _isJumping;
    }
    public bool IsGrounded()
    {
        return _isGrounded;
    }
    public bool IsFalling()
    {
        return _isFalling;
    }

    public float GetChargeJumpTimer() { return _chargeJumpTimer; }
}
