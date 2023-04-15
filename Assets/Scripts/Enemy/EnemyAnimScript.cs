using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimScript : MonoBehaviour
{
    private Animator _anim;
    public GameObject _enemy;
    EnemyMoveScript _enemyScript;
    GuardSoundsScript _guardSounds;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _enemyScript = _enemy.GetComponent<EnemyMoveScript>();
        _guardSounds = GetComponent<GuardSoundsScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isWalking", _enemyScript.isWalking);
        _anim.SetBool("isRunning", _enemyScript.isRunning);
    }

    public void PlayAttackAnim() {
        _anim.SetTrigger("swingSword");
        _guardSounds.SwordSlashNoise();
    }

    //from edu4hd0
    bool AnimatorIsPlaying()
    {
        return _anim.GetCurrentAnimatorStateInfo(0).length >
               _anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    //from edu4hd0
    public bool AnimationIsPlaying(string stateName)
    {
        return AnimatorIsPlaying() && _anim.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }
}
