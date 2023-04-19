using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickupScript : MonoBehaviour
{
    public PlayerMainScript _playerScript;

    Text _interactPopupText;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = FindObjectOfType<PlayerMainScript>();
        _interactPopupText = GetComponentInChildren<Text>();
        _interactPopupText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyBinding.interact()))
        {
            _playerScript.PickupCoin();
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == Tag.player)
        {
            _interactPopupText.text = "Press " + KeyBinding.interact() + " to interact";
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == Tag.player)
        {
            _interactPopupText.text = "";
        }

    }
}
