 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaurdCanvasScript : MonoBehaviour
{
    [Range(0, 360)]
    public float WARNING_VIEW_ANGLE;

    Image warningImage;
    public Color warningColor;
    public Color spottedColor;

    float _inCautionRange = 0;
    public float RESET_TIME = 2.5f;

    Transform _headTransform;
    Transform _playerTransform;
    Transform _transform;
    GuardViewDistance _guardViewDistanceScript;
    GuardSightScript _guardSightScript;
    GuardSoundScript _guardSoundScript;
    // Start is called before the first frame update
    void Start()
    {
        _headTransform = GetComponentInParent<GuardMainScript>().transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Neck/Head");
        _guardViewDistanceScript = GetComponentInParent<GuardMainScript>().GetComponentInChildren<GuardViewDistance>();
        _guardSightScript = GetComponentInParent<GuardSightScript>();
        _guardSoundScript = GetComponentInParent<GuardSoundScript>();
        _playerTransform = GetComponentInParent<GuardMainScript>().GetPlayerReference().transform;
        _transform = GetComponentInParent<GuardMainScript>().transform;
        warningImage = GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_inCautionRange > 0)
        {
            _inCautionRange -= Time.deltaTime;
        }

        UpdateVisionImage();
    }

    void UpdateVisionImage()
    {
        if (!_guardViewDistanceScript.IsPlayerInPossibleViewRange() && !_guardSightScript.IsPlayerVisible())
        {
            warningImage.enabled = false;
        }
        else
        {
            Vector3 positionInFrontofHead = _headTransform.position + _transform.rotation * new Vector3(0, 0, 1);
            Vector3 directionToPlayer = (new Vector3(_playerTransform.position.x, positionInFrontofHead.y - .5f, _playerTransform.position.z) - positionInFrontofHead).normalized;
            Ray ray = new Ray(positionInFrontofHead, directionToPlayer);

            //Can See the player
            bool hitPlayer = false;
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag.Equals(Tag.player) || (hit.collider.tag.Equals(Tag.hiddenPlayer) && _guardSightScript.IsPlayerVisible()))
                {
                    hitPlayer = true;
                }
            }

            //In View Radius
            if (_guardSightScript.IsPlayerVisible())
            {
                warningImage.enabled = true;
                warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
            } //In caution view radius
            else if (hitPlayer && Vector3.Angle(_transform.forward, directionToPlayer) < WARNING_VIEW_ANGLE / 2)
            {
                warningImage.enabled = true;
                warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
                if (_inCautionRange <= 0)
                {
                    _guardSoundScript.GuardSpottedNoise();
                }
                _inCautionRange = RESET_TIME;
            }
            else
            {
                warningImage.enabled = false;
            }
        }
    }
}
