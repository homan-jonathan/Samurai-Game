using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySightScript : MonoBehaviour
{   
    public Transform _headTransform;
    public Transform _playerTransform;
    public PlayerMoveScript _playerMoveScript;
    [Range(0, 360)]
    public float VIEW_ANGLE;
    public float VIEW_DISTANCE = 1;
    public float PLAYER_CROUCHING_MULTIPLIER = .25f;
    public float PLAYER_RUNNING_MULTIPLIER = 1.5f;
    public float PLAYER_DETECTION_RADIUS = 1.5f;
    public float PLAYER_SPOTTED_DURATION = 1f;

    public float _seenPlayerRecently = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_seenPlayerRecently > 0)
        {
            _seenPlayerRecently -= Time.deltaTime;
        }
    }

    public bool CanSeePlayer()
    {
        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, .25f);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

        if (PlayerInViewDistance(positionInFrontofHead, directionToPlayer)) {
            return true;
        }
        if (PlayerInCloseProximity()) {
            return true;
        }

        return false;
    }

    bool PlayerInViewDistance(Vector3 positionInFrontofHead, Vector3 directionToPlayer)
    {
        RaycastHit hit;
        if (Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2 && Physics.Raycast(positionInFrontofHead, directionToPlayer, out hit, VIEW_DISTANCE * CalculateViewDistance()))
        {
            if (hit.collider.tag == Tag.player)
            {
                _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                return true;
            }
        }

        return false;
    }

    float CalculateViewDistance() {
        float viewDistanceMultiplier = 1;

        if (_playerMoveScript.IsCrouched())
        {
            viewDistanceMultiplier = PLAYER_CROUCHING_MULTIPLIER;
        }
        else if (_playerMoveScript.IsRunning())
        {
            viewDistanceMultiplier = PLAYER_RUNNING_MULTIPLIER;
        }

        return viewDistanceMultiplier;
    }

    bool PlayerInCloseProximity() {
        if (_seenPlayerRecently <= 0) {
            return false;
        }

        RaycastHit[] hits = Physics.SphereCastAll(transform.position, PLAYER_DETECTION_RADIUS, Vector3.forward);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == Tag.player)
            {
                _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                return true;
            }
        }

        return false;
    }

    public float GetViewDistance() {
        return VIEW_DISTANCE * CalculateViewDistance();
    }
}
