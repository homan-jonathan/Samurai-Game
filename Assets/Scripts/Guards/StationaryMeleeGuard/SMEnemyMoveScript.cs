using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMEnemyMoveScript : MonoBehaviour
{
    public float GUARD_RUN_SPEED = 2f;

    GuardSightScript _sightScript;
    Transform _playerTransform;
    NavMeshAgent _agent;
    SMEnemyAnimScript _anim;

    Vector3 _wayPoint;
    bool _isRunning = false;
    bool _reachedWayPoint = true;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _wayPoint = transform.position;

        _sightScript = GetComponent<GuardSightScript>();
        _anim = GetComponent<SMEnemyAnimScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;

        _agent.speed = GUARD_RUN_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.AnimationIsPlaying(AnimationState.swingSword)) //If attacking then stop moving
        {
            _agent.isStopped = true;
            return;
        }
        else
        {
            _agent.isStopped = false;
        }

        if (Vector3.Distance(transform.position, _wayPoint) <= .5f) {
            _reachedWayPoint = true;
            _isRunning = false;
        }

        if (ReachedDestinationOrGaveUp())
        {
            SetNewWaypoint();
        }
        if (_sightScript.IsPlayerVisible())
        {
            PursuePlayer();
        }
    }

    void SetNewWaypoint()
    {
        _agent.SetDestination(_wayPoint);
    }

    void PursuePlayer()
    {
        _agent.SetDestination(_playerTransform.position);
        _isRunning = true;
        _reachedWayPoint = false;
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

    public bool IsRunning() { return _isRunning; }

}
