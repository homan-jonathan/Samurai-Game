using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardGroupingScript : GuardMoveScript
{
    public Transform[] waypoints;
    public GameObject[] guards;

    int _attachedCount = 0;
    int _waypointIndx = 0;
    float delay = 0;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(waypoints[_waypointIndx].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (guards.Length == _attachedCount)
        {
            _agent.isStopped = false;
            if (ReachedDestinationOrGaveUp() && delay < 0)
            {
                _waypointIndx = (_waypointIndx + 1) % waypoints.Length;
                _agent.SetDestination(waypoints[_waypointIndx].position);
                delay = 1.5f;
            }
        }
        else {
            _agent.isStopped = true;
        }
        delay -= Time.deltaTime;
    }

    public void AttachGuard(Transform guardTransform) {
        guardTransform.rotation = transform.rotation;
        guardTransform.parent = transform;
        guardTransform.GetComponent<NavMeshAgent>().enabled = false;
        _attachedCount++;
    }

    public void RemoveGuard(Transform guardTransform)
    {
        guardTransform.parent = null;
        guardTransform.GetComponent<NavMeshAgent>().enabled = true;
        _attachedCount--;
    }
}
