using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLineOfSightScript : MonoBehaviour
{
    public Transform _headTransform;
    public Transform _playerTransform;
    public PlayerMoveScript _playerMoveScript;
    [Range(0, 360)]
    public float VIEW_ANGLE;
    public float MAX_VIEW_DISTANCE = 1;
    public float PLAYER_CROUCHING_MULTIPLIER = .25f;
    public float PLAYER_RUNNING_MULTIPLIER = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CanSeePlayer()
    {
        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, .25f);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

        float viewDistanceMultiplier = 1;
        if (_playerMoveScript.IsCrouched())
        {
            viewDistanceMultiplier = PLAYER_CROUCHING_MULTIPLIER;
        }
        else if (_playerMoveScript.IsRunning())
        {
            viewDistanceMultiplier = PLAYER_RUNNING_MULTIPLIER;
        }



        bool inViewAngle = Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2;
        RaycastHit hit;
        bool hitPlayer = Physics.Raycast(positionInFrontofHead, directionToPlayer, out hit, MAX_VIEW_DISTANCE * viewDistanceMultiplier);

        if (inViewAngle && hitPlayer)
        {
            if (hit.collider.tag == Tag.player)
            {
                return true;
            }
        }

        return false;
    }

    bool InCloseProximity() {
        bool isInproximity = false;



        return isInproximity;
    }
}
