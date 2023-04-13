using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _coinBag;
    PlayerAnimScript _anim;

    public bool _hasCoins;

    bool isDead = false;
    void Start()
    {
        _anim = GetComponent<PlayerAnimScript>();
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.sword) && !isDead)
        {
            GetComponent<CharacterController>().detectCollisions = false;
            isDead = true;
            _anim.PlayDeathAnim();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
