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

    public float CROUCH_MOVESPEED = 2.0F;
    public float WALK_MOVESPEED = 4.0F;
    public float RUN_MOVESPEED = 6.0F;
    public float JUMP_HEIGHT = 7.0F;
    public float CHARGED_JUMP_HEIGHT = 15.0f;
    public float GRAVITY = 20.0F;
    float _ySpeed = 0;

    public float rotationSpeed = 720F;
    public float speed;

    private bool ableToJump = true;

    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isFalling = false;

    private bool hasChargedJump = false;
    private float chargeJumpTimer = 0.0f;
    public float NEEDED_TO_JUMP = 3.0f;
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
        float max = 0, min = -40f;
        float angle = 0;

        if (IsCrouched())
        {
            angle -= speed * Time.deltaTime;
            angle = Mathf.Clamp(angle, min, max);
            transform.localRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);

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

        float timePassed = 0.0f;
        //ref passed by reference and allows modification of said thing
        if (_charCon.isGrounded)
        {
            timePassed = 0.0f;
            _ySpeed = -1;
            isGrounded = true;
            isJumping = false;
            isFalling = false;

            if (Input.GetKeyDown(KeyBinding.jump()) && ableToJump)
            {
                if(hasChargedJump)
                {
                    _ySpeed = CHARGED_JUMP_HEIGHT;
                    hasChargedJump = false;
                }
                else
                {
                    _ySpeed = JUMP_HEIGHT;
                }
                isJumping = true;
            }
        }
        else
        {
            isGrounded = false;
            timePassed += Time.deltaTime;
            _ySpeed -= Time.deltaTime * GRAVITY;

            if(timePassed >= 0.25f)
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
            //_transform.Rotate(0,40f,0);
            //_transform.rotation.Set(0, 40, 0, 0);
            chargeJumpTimer += Time.deltaTime;
            if (chargeJumpTimer >= NEEDED_TO_JUMP)
            {
                hasChargedJump = true;
            }
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
