using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SMEnemyMoveScript : GuardMoveScript
{
    public float GUARD_RUN_SPEED = 2f;

    GuardSightScript _sightScript;
    Transform _playerTransform;
    GuardAnimatorScript _anim;
    GuardMoveScript _baseMoveScript;
    
    Vector3 _wayPoint;
    Quaternion _wayPointDirection;
    bool _isRunning = false;
    bool _reachedWayPoint = true;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _wayPoint = transform.position;
        _wayPointDirection = transform.rotation;

        _sightScript = GetComponent<GuardSightScript>();
        _anim = GetComponent<GuardAnimatorScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
        _baseMoveScript = GetComponent<GuardMoveScript>();

        _agent.speed = GUARD_RUN_SPEED;
    }

    // Update is called once per frame
    void Update()
    {
        if (_anim.AttackAnimationIsPlaying()) //If attacking then stop moving
        {
            _agent.isStopped = true;
            return;
        }
        else if (_baseMoveScript._stopMovement)
        {
            _isRunning = false;
            _agent.isStopped = true;
            _anim.PlayDeathAnim();
        }
        else
        {
            _agent.isStopped = false;
        }

        if (Vector3.Distance(transform.position, _wayPoint) <= .5f)
        {
            transform.rotation = _wayPointDirection;
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

    public bool IsRunning() { return _isRunning; }

}
