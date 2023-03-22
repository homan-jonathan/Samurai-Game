using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    CharacterController _charCon;
    public float _rotAmt = 0;
    Transform _transform;

    private Vector3 moveDirection = Vector3.zero;

    public float rotationSpeed = 720F;
    public float speed = 5.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;

    public bool isCrouched = false;
    public bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        _charCon = GetComponent<CharacterController>();
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveDirection = new Vector3(speed * Input.GetAxis("Horizontal"), 0, speed * Input.GetAxis("Vertical"));
        _transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(_transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        _charCon.Move(moveDirection * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.Q))
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

        if (_charCon.velocity != Vector3.forward || _charCon.velocity != Vector3.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
