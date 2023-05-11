using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMEnemyAnimScript : GuardAnimatorScript
{
    SMEnemyMoveScript _enemyScript;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _enemyScript = GetComponent<SMEnemyMoveScript>();
        _guardSounds = GetComponent<GuardSoundScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isRunning", _enemyScript.IsRunning());
    }
}
