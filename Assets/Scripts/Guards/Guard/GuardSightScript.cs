using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSightScript : MonoBehaviour
{
    [Range(0, 360)]
    public float VIEW_ANGLE;
    public float VIEW_DISTANCE = 1;
    public float PLAYER_CROUCHING_MULTIPLIER = .25f;
    public float PLAYER_SPOTTED_DURATION = 1f;

    Transform _headTransform;
    Transform _playerTransform;
    PlayerMoveScript _playerMoveScript;
    GuardViewDistance _sightIndicatorScript;
    GuardAlertScript _alertGuardScript;
    GuardSoundScript _guardSoundScript;
    bool _playerCloseToGaurd = false;
    float _seenPlayerRecently = 0;

    void Start()
    {
        _headTransform = transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head");
        _sightIndicatorScript = GetComponentInChildren<GuardViewDistance>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
        _playerMoveScript = GetComponent<GuardMainScript>().GetPlayerReference().GetComponent<PlayerMoveScript>();
        _alertGuardScript = GetComponentInChildren<GuardAlertScript>();
        _guardSoundScript = GetComponent<GuardSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_seenPlayerRecently > 0)
        {
            _seenPlayerRecently -= Time.deltaTime;
        }

        if (_sightIndicatorScript.IsPlayerInPossibleViewRange()) {
            CanSeePlayer();
        }
    }

    void CanSeePlayer()
    {
        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, 1);
        Vector3 directionToPlayer = (new Vector3(_playerTransform.position.x, positionInFrontofHead.y - .5f, _playerTransform.position.z) - positionInFrontofHead).normalized;
        Ray ray = new Ray(positionInFrontofHead, directionToPlayer);

        //Can See the player
        bool hitPlayer = false;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag.Equals(Tag.player) || (hit.collider.tag.Equals(Tag.hiddenPlayer) && _seenPlayerRecently > 0))
            {
                hitPlayer = true;
            }
        }

        //In View Radius
        if (hitPlayer && Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2 && (_seenPlayerRecently > 0 || hit.distance <= CalculateViewDistance()))
        {
            _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
            _alertGuardScript.AlertNearbyGuards();
        }

        //Physically close to the player
        if (PlayerInCloseProximity())
        {
            _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
            _alertGuardScript.AlertNearbyGuards();
        }

    }

    public bool IsPlayerVisible() {
        return _seenPlayerRecently > 0;
    }

    public void SetPlayerIsVisible()
    {
        if (!IsPlayerVisible())
        {
            _guardSoundScript.GuardAlertedNoise();
        }
        _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
    }

    public float CalculateViewDistance()
    {
        float viewDistanceMultiplier = 1;

        if (_playerMoveScript.IsCrouched())
        {
            viewDistanceMultiplier = PLAYER_CROUCHING_MULTIPLIER;
        }

        return viewDistanceMultiplier * VIEW_DISTANCE;
    }

    bool PlayerInCloseProximity()
    {
        if (_seenPlayerRecently <= 0)
        {
            return false;
        }

        return _playerCloseToGaurd;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            _playerCloseToGaurd = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            _playerCloseToGaurd = false;
        }
    }
}
