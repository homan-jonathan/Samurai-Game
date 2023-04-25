using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WMEnemyMoveScript : MonoBehaviour
{
    public float GUARD_WALK_SPEED = 1f;
    public float GUARD_RUN_SPEED = 2f;

    GuardSightScript _sightScript;
    Transform _playerTransform;
    NavMeshAgent _agent;
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
        _playerTransform = _anim.GetPlayerReference().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.AnimationIsPlaying(AnimationState.swingSword)) //If attacking then stop moving
        {
            _agent.isStopped = true;
            return;
        }
        else {
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

    //from DataGreed/UnityNavMeshCheck.cs github
    bool ReachedDestinationOrGaveUp()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance) {
            return true;
        }
        /*if (!_agent.pathPending)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }*/

        return false;
    }

    public bool IsWalking() { return _isWalking; }
    public bool IsRunning() { return _isRunning; }

}
