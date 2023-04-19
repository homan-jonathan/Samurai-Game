using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySightScript : MonoBehaviour
{
    public Transform _headTransform;
    [Range(0, 360)]
    public float VIEW_ANGLE;
    [Range(0, 360)]
    public float WARNING_VIEW_ANGLE;
    public float VIEW_DISTANCE = 1;
    public float PLAYER_CROUCHING_MULTIPLIER = .25f;
    public float PLAYER_SPOTTED_DURATION = 1f;
    public float RESET_TIME = 2.5f;
    public Image warningImage;
    public Color warningColor;
    public Color spottedColor;

    Transform _playerTransform;
    PlayerMoveScript _playerMoveScript;
    SightIndicatorScript _sightIndicatorScript;
    GuardSoundsScript _guardSoundScript;
    bool _playerCloseToGaurd = false;
    float _seenPlayerRecently = 0;
    float _inCautionRange = 0;

    void Start()
    {
        _sightIndicatorScript = GetComponentInChildren<SightIndicatorScript>();
        _guardSoundScript = GetComponent<GuardSoundsScript>();
        _playerTransform = GetComponent<EnemyAnimScript>().GetPlayerReference().transform;
        _playerMoveScript = GetComponent<EnemyAnimScript>().GetPlayerReference().GetComponent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_seenPlayerRecently > 0)
        {
            _seenPlayerRecently -= Time.deltaTime;
        }
        if (_inCautionRange > 0)
        {
            _seenPlayerRecently -= Time.deltaTime;
        }
    }

    public bool CanSeePlayer()
    {
        if (!_sightIndicatorScript.IsPlayerInPossibleViewRange()) { //check if in collider, minimize raycast need
            return false;
        }

        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, 1);
        Vector3 directionToPlayer = (new Vector3(_playerTransform.position.x, positionInFrontofHead.y - .5f, _playerTransform.position.z) - positionInFrontofHead).normalized;

        //Updates Ray position
        Ray ray = new Ray(positionInFrontofHead, directionToPlayer);
        
        RaycastHit hit2;
        if (Physics.Raycast(ray, out hit2, CalculateViewDistance() * _sightIndicatorScript.GetVisibilityInicatorRange()))
        {
            if ((!(Vector3.Angle(transform.forward, directionToPlayer) < WARNING_VIEW_ANGLE/2) ||
                hit2.collider.tag != Tag.player) &&
                _seenPlayerRecently <= 0)
            {
                warningImage.enabled = false;
                _inCautionRange = 0;
            } //turn off image if, in angle, not the player, and wasnt seen recently

            if (hit2.collider.tag == Tag.player)
            { //if ray hit player
                if (Vector3.Angle(transform.forward, directionToPlayer) < WARNING_VIEW_ANGLE/2)
                { //inside the view arc(warning area)
                    warningImage.enabled = true;
                    warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
                    if (_inCautionRange <= 0) { 
                        _guardSoundScript.GuardSpottedNoise();
                    }
                    _inCautionRange = RESET_TIME;
                }

                if (Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2)
                { //inside of view Angle
                    if (hit2.distance <= CalculateViewDistance())
                    {
                        //seen player
                        warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
                        if (_seenPlayerRecently <= 0) {
                            _guardSoundScript.GuardAlertedNoise();
                        }
                        _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
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
