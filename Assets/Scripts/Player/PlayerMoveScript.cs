using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController _charCon;
    public float _rotAmt = 0;
    Transform _transform;

    private Vector3 moveDirection = Vector3.zero;

    public const float CROUCH_MOVESPEED = 2.0F;
    public const float WALK_MOVESPEED = 4.0F;
    //public const float RUN_MOVESPEED = 6.0F;

    public float rotationSpeed = 720F;
    public float speed;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public bool isCrouched = false;
    public bool isMoving = false;

    private Vector3 lastPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _transform = transform;
        lastPos = _transform.position;
        speed = WALK_MOVESPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (isCrouched)
        {
            speed = CROUCH_MOVESPEED;
        } else
        {
            speed = WALK_MOVESPEED;
        }
        moveDirection = new Vector3(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));
        _transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        _charCon.Move(moveDirection * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouched)
            {
                isCrouched = true;
            }
            else
            {
                isCrouched = false;
            }
        } 

        /*if (moveDirection != Vector3.forward || moveDirection != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }*/
        /*if(_transform.position != lastPos)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPos = _transform.position;*/
        if(moveDirection != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
