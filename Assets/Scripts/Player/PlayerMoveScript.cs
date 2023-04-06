using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController _charCon;
    public float _rotAmt = 0;
    public Transform _cameraTransform;
    public CameraScript _cameraScript;
    Transform _transform;

    PlayerAnimScript _animScript;

    private Vector3 moveDirection = Vector3.zero;

    public const float CROUCH_MOVESPEED = 2.0F;
    public const float WALK_MOVESPEED = 4.0F;
    public const float RUN_MOVESPEED = 6.0F;
    public float JUMP_HEIGHT = 8.0F;
    public float GRAVITY = 20.0F;
    float _ySpeed = 0;

    public float rotationSpeed = 720F;
    public float speed;

    private bool ableToJump = true;

    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isFalling = false;

    // Start is called before the first frame update
    void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _transform = transform;
        speed = WALK_MOVESPEED;

        _animScript= GetComponent<PlayerAnimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot();
        if (IsCrouched())
        {
            ableToJump = false; 
            speed = CROUCH_MOVESPEED;
        } else if (IsRunning()) 
        {
            speed = RUN_MOVESPEED;
            ableToJump = true;
        } 
        else if (IsWalking())
        {
            speed = WALK_MOVESPEED;
            ableToJump = true;
        }


        //ref passed by reference and allows modification of said thing
        if (_charCon.isGrounded)
        {
            _ySpeed = -1;
            isGrounded = true;
            isJumping = false;
            isFalling = false;

            if (Input.GetKeyDown(KeyBinding.jump()) && ableToJump)
            {
                _ySpeed = JUMP_HEIGHT;
                isJumping = true;
                //_animScript.PlayJumpAnimation();
            }
        }
        else
        {
            _ySpeed -= Time.deltaTime * GRAVITY;
            isGrounded = false;

            if((isJumping && _ySpeed < 0) || _ySpeed < -2)
            {
                isFalling = true;
            }
        }

        switch (_cameraScript._mode)
        {
            case CameraScript.Mode.OrbitCam:
                float rotationRelativeToCamera1 = _cameraTransform.rotation.eulerAngles.y;
                moveDirection = Quaternion.Euler(0, rotationRelativeToCamera1, 0) * new Vector3(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));

                if (moveDirection != Vector3.zero)
                {
                    Quaternion toRotation1 = Quaternion.LookRotation(moveDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation1, rotationSpeed * Time.deltaTime);
                }
                break;
            case CameraScript.Mode.FollowCam:
                float rotationRelativeToCamera2 = _cameraScript._rotAmtX;
                moveDirection = Quaternion.Euler(0, rotationRelativeToCamera2, 0) * new Vector3(0, 0, speed * Input.GetAxis("Vertical"));

                Quaternion toRotation2 = Quaternion.Euler(0, rotationRelativeToCamera2 + speed * Input.GetAxis("Horizontal"), 0);
                transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation2, rotationSpeed * Time.deltaTime);

                break;
        }

        _transform.Translate(moveDirection * speed * Time.deltaTime + new Vector3(0, _ySpeed, 0) * Time.deltaTime, Space.World);

        _charCon.Move(moveDirection * Time.deltaTime + new Vector3(0, _ySpeed, 0) * Time.deltaTime);
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
        if (moveDirection != Vector3.zero)
        {   
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsRunning() {
        if (Input.GetKey(KeyBinding.sprint()))
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
        return isJumping;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }
    public bool IsFalling()
    {
        return isFalling;
    }
}
