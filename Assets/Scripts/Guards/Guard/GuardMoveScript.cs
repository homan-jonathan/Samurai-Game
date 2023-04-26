using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardMoveScript : MonoBehaviour
{
    protected NavMeshAgent _agent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    //from DataGreed/UnityNavMeshCheck.cs github
    protected bool ReachedDestinationOrGaveUp()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
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
}
