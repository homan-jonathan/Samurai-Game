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
    public float PLAYER_RUNNING_MULTIPLIER = 1.5f;
    //public float PLAYER_DETECTION_RADIUS = 1.5f;
    public float PLAYER_SPOTTED_DURATION = 1f;
    public float RAYCAST_COOLDOWN_TIME = .1f;

    SightIndicatorScript _sightIndicatorScript;
    public float _seenPlayerRecently = 0;
    float _timeSinceLastRayCast = 0;
    //bool _playerInPossibleViewRange = false;
    bool _playerCloseToGaurd = false;

    public Ray ray = new Ray();

    public Image warningImage;
    public Color warningColor;
    public Color spottedColor;
    // Start is called before the first frame update
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
        /*_timeSinceLastRayCast -= Time.deltaTime; //Check if last raycast was recent, if it was than act like guard can still see player
        if (_timeSinceLastRayCast > 0)
        {
            return true;
        }*/

        if (!_sightIndicatorScript._playerInPossibleViewRange) { //check if in collider, minimize raycast need
            return false;
        }

        Vector3 positionInFrontofHead = _headTransform.position + transform.rotation * new Vector3(0, 0, .25f);
        Vector3 directionToPlayer = (_playerTransform.position - transform.position).normalized;


        //Updates Ray position
        ray.origin = positionInFrontofHead;
        ray.direction = directionToPlayer;



        /*if (_seenPlayerRecently > 0)
        {
            warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
        }
        else
        {
            //hit player but is unseeable to guard
            warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
        }*/


        //Sees if it is hitting player
        RaycastHit hit;
        if (Vector3.Angle(transform.forward, directionToPlayer) < 90 &&
            Physics.Raycast(ray, out hit, VIEW_DISTANCE * PLAYER_RUNNING_MULTIPLIER))
        {
            if (hit.collider.tag == Tag.player)
            {
                warningImage.enabled = true;
                if (hit.distance <= CalculateViewDistance())
                {
                    //seen player
                    warningImage.color = new Color(spottedColor.r, spottedColor.g, spottedColor.b);
                }
                else
                {
                    //hit player but is unseeable to guard
                    warningImage.color = new Color(warningColor.r, warningColor.g, warningColor.b);
                }


                /*if (_seenPlayerRecently < 0)
                {
                    warningImage.enabled = false;
                }*/
            }
        }

        if (!PlayerInCloseProximity() && 
            Vector3.Angle(transform.forward, directionToPlayer) < 90 &&
            Physics.Raycast(ray, out hit, VIEW_DISTANCE * PLAYER_RUNNING_MULTIPLIER))
        {
            warningImage.enabled = false; 
        }

        if (PlayerInViewDistance(directionToPlayer) || PlayerInCloseProximity())
        { //If can see player
            //_timeSinceLastRayCast = RAYCAST_COOLDOWN_TIME;
            return true;
        }



        return false;
    }

    bool PlayerInViewDistance(Vector3 directionToPlayer)
    {
        RaycastHit hit;
        if (Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2 &&
            Physics.Raycast(ray, out hit, CalculateViewDistance()))
        {
            if (hit.collider.tag == Tag.player) //If ray hit player in the ViewDistance and proper angle
            {
                _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                return true;
            }
        }
        /*if (Vector3.Angle(transform.forward, directionToPlayer) < VIEW_ANGLE / 2 && 
            Physics.Raycast(positionInFrontofHead, directionToPlayer, out hit, VIEW_DISTANCE * CalculateViewDistance()))
        {
            if (hit.collider.tag == Tag.player)
            {
                _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                return true;
            }
        }*/

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

        return viewDistanceMultiplier * VIEW_DISTANCE;
    }

    bool PlayerInCloseProximity() {
        if (_seenPlayerRecently <= 0) {
            return false;
        }

        /*RaycastHit[] hits = Physics.SphereCastAll(transform.position, PLAYER_DETECTION_RADIUS, Vector3.forward);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == Tag.player)
            {
                _seenPlayerRecently = PLAYER_SPOTTED_DURATION;
                return true;
            }
        }*/

        return _playerCloseToGaurd;
    }

    /*public float GetViewDistance() {
        return VIEW_DISTANCE * CalculateViewDistance();
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            _playerCloseToGaurd = true;
            //_playerInPossibleViewRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.player))
        {
            _playerCloseToGaurd = false;
            //_playerInPossibleViewRange = false;
        }
    }
}
