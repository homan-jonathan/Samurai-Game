using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController _charCon;
    public float _rotAmt = 0;
    public Transform _cameraTransform;
    public CameraScript _cameraScript;
    Transform _transform;

    private Vector3 moveDirection = Vector3.zero;

    public const float CROUCH_MOVESPEED = 2.0F;
    public const float WALK_MOVESPEED = 4.0F;
    public const float RUN_MOVESPEED = 6.0F;
    public float JUMP_HEIGHT = 8.0F;
    public float GRAVITY = 20.0F;
    float _ySpeed = 0;

    public float rotationSpeed = 720F;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _transform = transform;
        speed = WALK_MOVESPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsCrouched())
        {
            speed = CROUCH_MOVESPEED;
        } else if (IsRunning()) 
        {
            speed = RUN_MOVESPEED;
        } 
        else if (IsWalking())
        {
            speed = WALK_MOVESPEED;
        }


        //ref passed by reference and allows modification of said thing
        if (_charCon.isGrounded)
        {
            _ySpeed = -1;
            if (Input.GetKeyDown(KeyBinding.jump()))
            {
                _ySpeed = JUMP_HEIGHT;
            }
        }
        else
        {
            _ySpeed -= Time.deltaTime * GRAVITY;
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
}
