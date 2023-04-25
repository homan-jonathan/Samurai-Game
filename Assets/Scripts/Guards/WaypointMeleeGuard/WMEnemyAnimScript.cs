using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMEnemyAnimScript : GuardAnimatorScript
{
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

    public override void PlayAttackAnim() {
        _anim.SetTrigger("swingSword");
        _guardSounds.SwordSlashNoise();
    }
}
