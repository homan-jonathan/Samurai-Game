using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class PickupScript : MonoBehaviour
{
    public PlayerMainScript _playerScript;
    public GameSceneManagerScript _gameSceneManagerScript;
    public InteractTextScript _popupTextScript;

    Text _interactPopupText;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = FindObjectOfType<PlayerMainScript>();
        _popupTextScript = FindObjectOfType<InteractTextScript>();
        _interactPopupText = _popupTextScript.GetComponentInChildren<Text>();
        _interactPopupText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == Tag.player)
        {
            _popupTextScript.SetText();
            if (Input.GetKeyDown(KeyBinding.interact()))
            {
                _playerScript.PickupCoin();
                gameObject.SetActive(false);
                _popupTextScript.RemoveText();
            }
        }
        
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == Tag.player)
        {
            _popupTextScript.RemoveText();
        }

    }
}
