using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController _charCon;
    public float _rotAmt = 0;
    public Transform _camera;
    Transform _transform;

    private Vector3 moveDirection = Vector3.zero;

    public const float CROUCH_MOVESPEED = 2.0F;
    public const float WALK_MOVESPEED = 4.0F;
    public const float RUN_MOVESPEED = 6.0F;

    public float rotationSpeed = 720F;
    public float speed;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public bool isCrouched = false;
    public bool isMoving = false;
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
        else if (IsMoving())
        {
            speed = WALK_MOVESPEED;
        }

        float rotationRelativeToCamera = _camera.rotation.eulerAngles.y;
        moveDirection = Quaternion.Euler(0, rotationRelativeToCamera, 0) * new Vector3(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));
        _transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        _charCon.Move(moveDirection * Time.deltaTime);
        
    }

    public bool IsCrouched() {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsMoving()
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

    bool IsRunning() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
