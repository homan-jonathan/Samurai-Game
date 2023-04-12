using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySightScript : MonoBehaviour
{
    public Transform _headTransform;
    public Transform _playerTransform;
    public PlayerMoveScript _playerMoveScript;
    [Range(0, 360)]
    public float VIEW_ANGLE;
    public float VIEW_DISTANCE = 1;
    public float PLAYER_CROUCHING_MULTIPLIER = .25f;
    //public float PLAYER_RUNNING_MULTIPLIER = 1.5f;
    public float PLAYER_SPOTTED_DURATION = 1f;

    SightIndicatorScript _sightIndicatorScript;
    public float _seenPlayerRecently = 0;
    bool _playerCloseToGaurd = false;

    public Ray ray = new Ray();

    public Image warningImage;
    public Color warningColor;
    public Color spottedColor;
    void Start()
    {
        _sightIndicatorScript = GetComponentInChildren<SightIndicatorScript>();
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
        if (!_sightIndicatorScript._playerInPossibleViewRange) { //check if in collider, minimize raycast need
            return false;
        }

        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, .25f);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;

        //Updates Ray position
        ray.origin = positionInFrontofHead;
        ray.direction = directionToPlayer;

        RaycastHit hit2;
        if (Physics.Raycast(ray, out hit2, CalculateViewDistance() * _sightIndicatorScript.EPISLON_VISIBILITY_RANGE/* * PLAYER_RUNNING_MULTIPLIER*/))
        {
            if (!(Vector3.Angle(transform.forward, directionToPlayer) < 90) &&
                hit2.collider.tag != Tag.player &&
                _seenPlayerRecently <= 0)
            {
                warningImage.enabled = false;
            } //turn off image if, in angle, not the player, and wasnt seen recently

            if (hit2.collider.tag == Tag.player)
            { //if ray hit player
                if (Vector3.Angle(transform.forward, directionToPlayer) < 90)
                { //inside the view arc(warning area)
                    warningImage.enabled = true;
                    warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
                }

                if (Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2)
                { //inside of view Angle
                    if (hit2.distance <= CalculateViewDistance())
                    {
                        //seen player
                        _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                        warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
                        return true;
                    }
                }
            }
        }

        if (PlayerInCloseProximity())
        {
            _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
            warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
            return true;
        }

        return false;
    }

    public float CalculateViewDistance() {
        float viewDistanceMultiplier = 1;

        if (_playerMoveScript.IsCrouched())
        {
            viewDistanceMultiplier = PLAYER_CROUCHING_MULTIPLIER;
        }
        /*else if (_playerMoveScript.IsRunning())
        {
            viewDistanceMultiplier = PLAYER_RUNNING_MULTIPLIER;
        }*/

        return viewDistanceMultiplier * VIEW_DISTANCE;
    }

    bool PlayerInCloseProximity() {
        if (_seenPlayerRecently <= 0) {
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
