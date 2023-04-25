using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMEnemyAnimScript : MonoBehaviour
{
    Animator _anim;
    WMEnemyMoveScript _enemyScript;
    GuardSoundScript _guardSounds;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _enemyScript = GetComponent<WMEnemyMoveScript>();
        _guardSounds = GetComponent<GuardSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isWalking", _enemyScript.IsWalking());
        _anim.SetBool("isRunning", _enemyScript.IsRunning());
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
