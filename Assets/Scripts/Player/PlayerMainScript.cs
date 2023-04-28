using UnityEngine;
using UnityEngine.UI;

public class PlayerMainScript : MonoBehaviour
{
    public GameSceneManagerScript GAME_SCENE_MANAGER;
    public Slider healthBar;

    PlayerAnimScript _anim;
    PlayerMoveScript _moveScript;
    PlayerTakedownScript _takedownScript;

    public GameObject _coinBagOnPlayer;
    public GameObject _swordOnPlayer;
    
    bool _isDead = false;
    bool _hasCoins = false;
    bool _hasSword = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<PlayerAnimScript>();
        _moveScript = GetComponent<PlayerMoveScript>();
        _takedownScript = GetComponent<PlayerTakedownScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = _moveScript.GetChargeJumpTimer();
    }

    public void PickupCoin()
    {
        _anim.PlayPickupCoinsAnim();
        _hasCoins = true;
        _coinBagOnPlayer.SetActive(true);
    }
    public void PickupSword()
    {
        _anim.PlayPickupCoinsAnim();
        _hasSword = true;
        _swordOnPlayer.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.sword) && !_isDead)
        {
            GetComponent<CharacterController>().enabled = false;
            _anim.PlayDeathAnim();
            GAME_SCENE_MANAGER.HasLost();
            _isDead = true;
        }
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public bool HasCoins() { 
        return _hasCoins;
    }
    public bool HasSword()
    {
        return _hasSword;
    }
}
