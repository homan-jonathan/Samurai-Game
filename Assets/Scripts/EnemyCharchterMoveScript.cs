using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharchterMoveScript : MonoBehaviour
{

    NavMeshAgent agent;
    public Transform[] waypoints;
    int waypointIndx = 0;

    public Transform _headTransform;
    public Transform _playerTransform;
    public float MAX_VIEW_DISTANCE = 1;
    [Range(0, 360)]
    public float viewAngle;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[waypointIndx].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (ReachedDestinationOrGaveUp())
        {
            SetNewWaypoint();
        }
        if (CanSeePlayer()) {
            PursuePlayer();
        }
    }

    void SetNewWaypoint() {
        waypointIndx = (waypointIndx + 1) % waypoints.Length;
        agent.SetDestination(waypoints[waypointIndx].position);
    }

    void PursuePlayer() {
        agent.SetDestination(_playerTransform.position);
    }

    bool CanSeePlayer() {
        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, .25f);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

        RaycastHit hit;
        if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle/2 && Physics.Raycast(positionInFrontofHead, directionToPlayer, out hit, MAX_VIEW_DISTANCE)) {
            if (hit.collider.tag == Tag.player)
            {
                return true;
            }
        }

        return false;
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
