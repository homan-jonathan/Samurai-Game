using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTakedownScript : MonoBehaviour
{
    public GameSceneManagerScript _gameSceneManagerScript;
    public InteractTextScript _popupTextScript;
    Text _interactPopupText;

    PlayerAnimScript _animScript;
    // Start is called before the first frame update
    void Start()
    {
        _popupTextScript = FindObjectOfType<InteractTextScript>();
        _interactPopupText = _popupTextScript.GetComponentInChildren<Text>();
        _interactPopupText.text = "";
        _animScript = GetComponent<PlayerAnimScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == Tag.enemy)
        {
            GuardSightScript _thisGuardSight = other.gameObject.GetComponent<GuardSightScript>();
            GuardMoveScript _thisGuardMove = other.gameObject.GetComponent<GuardMoveScript>();
            if (!_thisGuardSight.IsPlayerVisible() && !_thisGuardMove._stopMovement)
            {
                _popupTextScript.SetText();
            }
            else
            {
                _popupTextScript.RemoveText();
            }
            if (Input.GetKeyDown(KeyBinding.interact()) && !_thisGuardSight.IsPlayerVisible())
            {
                _animScript.PlayStabAnim();
                _popupTextScript.RemoveText();
                _thisGuardMove.Die();
            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == Tag.enemy)
        {
            _popupTextScript.RemoveText();
        }

    }
}
