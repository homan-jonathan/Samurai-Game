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

    public float _playerScore;
    
    bool _isDead = false;
    bool _hasCoins = false;
    bool _hasSword = false;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<PlayerAnimScript>();
        _moveScript = GetComponent<PlayerMoveScript>();
        _takedownScript = GetComponent<PlayerTakedownScript>();
        _playerScore = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = _moveScript.GetChargeJumpTimer();
    }

    public void PickupCoin()
    {
        _playerScore += 100f;
        _anim.PlayPickupCoinsAnim();
        _hasCoins = true;
        _coinBagOnPlayer.SetActive(true);
    }
    public void PickupSword()
    {
        _playerScore += 1000f;
        _anim.PlayPickupCoinsAnim();
        _hasSword = true;
        _swordOnPlayer.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.weapon) && !_isDead)
        {
            GetComponent<CharacterController>().enabled = false;
            _playerScore = 0.0f;
            _anim.PlayDeathAnim();
            GAME_SCENE_MANAGER.HasLost();
            _isDead = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.name == "GuardSpottedRange")
        {
            if (Input.GetKeyDown(KeyBinding.interact()))
            {
                GuardShaderScript _thisEnemy = other.GetComponentInParent<GuardShaderScript>();
                _thisEnemy._isTagged = true;
                print("tag enemy");
            }
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
    public float PlayerScore()
    {
        return _playerScore;
    }
}
