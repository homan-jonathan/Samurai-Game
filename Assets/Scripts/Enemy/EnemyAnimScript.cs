using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimScript : MonoBehaviour
{
    public GameObject PLAYER;
    
    Animator _anim;
    EnemyMoveScript _enemyScript;
    GuardSoundsScript _guardSounds;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _enemyScript = GetComponent<EnemyMoveScript>();
        _guardSounds = GetComponent<GuardSoundsScript>();
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

    public GameObject GetPlayerReference() {
        return PLAYER;
    }
}
