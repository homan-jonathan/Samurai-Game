using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveScript : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndx = 0;

    EnemySightScript enemySightScript;
    public Transform _playerTransform;

    public float GUARD_WALK_SPEED = 1f;
    public float GUARD_RUN_SPEED = 2f;

    public bool isWalking = false;
    public bool isRunning = false;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[waypointIndx].position);

        enemySightScript = GetComponent<EnemySightScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ReachedDestinationOrGaveUp())
        {
            agent.speed = GUARD_WALK_SPEED;
            SetNewWaypoint();
        }
        if (enemySightScript.CanSeePlayer())
        {
            agent.speed = GUARD_RUN_SPEED;
            PursuePlayer();
        }
    }

    void SetNewWaypoint()
    {
        waypointIndx = (waypointIndx + 1) % waypoints.Length;
        agent.SetDestination(waypoints[waypointIndx].position);
        isRunning = false;
        isWalking = true;
    }

    void PursuePlayer()
    {
        agent.SetDestination(_playerTransform.position);
        isRunning = true;
        isWalking = false;
    }

    //from DataGreed/UnityNavMeshCheck.cs github
    bool ReachedDestinationOrGaveUp()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return false;
    }

}
