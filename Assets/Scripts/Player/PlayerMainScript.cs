using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject _coinBag;
    PlayerAnimScript _anim;
    PlayerMoveScript _moveScript;

    public GameObject _coinBag;
    public bool _hasCoins;

    public Slider _healthBar;
    public Image _healthFilledImage;
    bool isDead = false;
    
    void Start()
    {
        _anim = GetComponent<PlayerAnimScript>();
        _moveScript = GetComponent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _healthBar.value = _moveScript.chargeJumpTimer;
    }

    public void pickupCoin()
    {
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
