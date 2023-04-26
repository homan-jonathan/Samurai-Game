using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WMEnemyMoveScript : GuardMoveScript
{
    public float GUARD_WALK_SPEED = 1f;
    public float GUARD_RUN_SPEED = 2f;

    GuardMoveScript _baseMoveScript;
    GuardSightScript _sightScript;
    Transform _playerTransform;
    WMEnemyAnimScript _anim;

    public Transform[] waypoints;
    int _waypointIndx = 0;
    bool _isWalking = false;
    bool _isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(waypoints[_waypointIndx].position);

        _sightScript = GetComponent<GuardSightScript>();
        _anim = GetComponent<WMEnemyAnimScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
        _baseMoveScript = GetComponent<GuardMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.AnimationIsPlaying(AnimationState.swingSword)) //If attacking then stop moving
        {
            _agent.isStopped = true;
            return;
        }
        else if (_baseMoveScript._stopMovement){
            _isWalking = false;
            _isRunning = false;
            _agent.isStopped = true;
            _anim.PlayDeathAnim();
        }
        else
        {
            _agent.isStopped = false;
        }

        if (ReachedDestinationOrGaveUp())
        {
            _agent.speed = GUARD_WALK_SPEED;
            SetNewWaypoint();
        }
        if (_sightScript.IsPlayerVisible())
        {
            _agent.speed = GUARD_RUN_SPEED;
            PursuePlayer();
        }
    }

    void SetNewWaypoint()
    {
        _waypointIndx = (_waypointIndx + 1) % waypoints.Length;
        _agent.SetDestination(waypoints[_waypointIndx].position);
        _isRunning = false;
        _isWalking = true;
    }

    void PursuePlayer()
    {
        _agent.SetDestination(_playerTransform.position);
        _isRunning = true;
        _isWalking = false;
    }

    public bool IsWalking() { return _isWalking; }
    public bool IsRunning() { return _isRunning; }

}
