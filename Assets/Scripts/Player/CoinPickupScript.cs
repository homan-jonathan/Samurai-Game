using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

public class CoinPickupScript : MonoBehaviour
{

    public GameObject _player;
    PlayerMainScript _playerScript;

    
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = _player.GetComponent<PlayerMainScript>();
    }

    // Update is called once per frame
    void Update()
    {
            
    }

    private void OnTriggerStay(Collider other)
    {
        /*if(Input.GetKeyDown(KeyCode.E)) 
        {
            _playerScript.pickupCoin();
            Destroy(gameObject);
        }*/
        if (Input.GetKeyDown(KeyBinding.interact()))
        {
            _playerScript.pickupCoin();

            //Destroy(gameObject);
            gameObject.SetActive(false);
        }
        
    }
}
