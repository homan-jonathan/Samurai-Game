using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GGuardMoveScript : GuardMoveScript
{
    public float GUARD_WALK_SPEED = 1f;
    public float GUARD_RUN_SPEED = 2f;

    GuardSightScript _sightScript;
    Transform _playerTransform;
    GGuardAnimScript _anim;

    public Transform waypoint;
    public GuardGroupingScript _guardGroup;
    bool _hasReachedWaypoint = false;
    bool _isWalking = false;
    bool _isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(waypoint.position);

        _sightScript = GetComponent<GuardSightScript>();
        _anim = GetComponent<GGuardAnimScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_sightScript.IsPlayerVisible())
        {
            if (_anim.AnimationIsPlaying(AnimationState.swingSword)) //If attacking then stop moving
            {
                _agent.isStopped = true;
                return;
            }

            PursuePlayer(); 
        }
        else 
        {
            if (!_hasReachedWaypoint)
            {
                SetWaypoint();
                if (Vector3.Distance(transform.position, waypoint.position) <= _agent.stoppingDistance && !_hasReachedWaypoint) {
                    _hasReachedWaypoint = true;
                    _agent.isStopped = true;
                    _guardGroup.AttachGuard(transform);
                }
            }
        }
    }

    void SetWaypoint()
    {
        _agent.SetDestination(waypoint.position);
        _isRunning = false;
        _isWalking = true;
    }

    void PursuePlayer()
    {
        if (_hasReachedWaypoint)
        {
            _guardGroup.RemoveGuard(transform);
        }
        _agent.isStopped = false;
        _agent.speed = GUARD_RUN_SPEED;
        _agent.SetDestination(_playerTransform.position);
        _hasReachedWaypoint = false;
        _isRunning = true;
        _isWalking = false;
    }

    public bool IsWalking() { return _isWalking; }
    public bool IsRunning() { return _isRunning; }

}
