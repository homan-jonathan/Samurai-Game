using UnityEngine;
using UnityEngine.UI;

public class PlayerMainScript : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerAnimScript _anim;
    PlayerMoveScript _moveScript;
    public GameSceneManagerScript gameSceneManager;

    SliderScript _sliderScript;

    public GameObject _coinBag;
    public bool hasCoins;

    public Slider healthBar;
    public Image healthFilledImage;
    bool isDead = false;
    
    void Start()
    {
        _anim = GetComponent<PlayerAnimScript>();
        _moveScript = GetComponent<PlayerMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = _moveScript.chargeJumpTimer;
    }

    public void pickupCoin()
    {
        _coinBag.SetActive(true);
        hasCoins = true;
        _anim.PlayPickupCoinsAnim();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals(Tag.sword) && !isDead)
        {
            GetComponent<CharacterController>().detectCollisions = false;
            isDead = true;
            _anim.PlayDeathAnim();
            gameSceneManager.HasLost();
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}
