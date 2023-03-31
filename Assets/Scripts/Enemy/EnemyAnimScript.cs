using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimScript : MonoBehaviour
{
    private Animator _anim;
    public GameObject _enemy;
    EnemyCharchterMoveScript _enemyScript;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _enemyScript = _enemy.GetComponent<EnemyCharchterMoveScript>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("isWalking", _enemyScript.isWalking);
        _anim.SetBool("isRunning", _enemyScript.isRunning);
    }
}
