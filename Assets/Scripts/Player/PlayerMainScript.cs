using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainScript : MonoBehaviour
{
    PlayerMoveScript _moveScript;

    public GameObject _coinBag;
    public bool _hasCoins;

    public bool isDead = false;

    public Slider _healthBar;
    public Image _healthFilledImage;

    void Start()
    {
        _moveScript = GetComponent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthBar.value = _moveScript.chargeJumpTimer;
    }

    public void pickupCoin()
    {
        //play animation for picking up item
        //do something with scoring
        _coinBag.SetActive(true);
        _hasCoins = true;
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            isDead = true;
        }
    }

    public bool IsDead()
    {
        return isDead;
    }

    
}
