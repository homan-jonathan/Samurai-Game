using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEngine.UI;

public class CoinPickupScript : MonoBehaviour
{

    public GameObject _player;
    public GameObject _interactPopupText;
    public Text _text;

    PlayerMainScript _playerScript;

    
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = _player.GetComponent<PlayerMainScript>();
        _interactPopupText.SetActive(false);

        _text = _interactPopupText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "Press " + KeyBinding.interact() + " to interact";
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (Input.GetKeyDown(KeyBinding.interact()))
        {
            _playerScript.pickupCoin();

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == Tag.player)
        {
            _interactPopupText.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == Tag.player)
        {
            _interactPopupText.SetActive(false);
        }

    }
}
