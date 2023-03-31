using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _coinBag;

    public bool _hasCoins;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pickupCoin()
    {
        //play animation for picking up item
        //do something with scoring
        _coinBag.SetActive(true);
        _hasCoins = true;
    }
}
