using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRangeAttack : MonoBehaviour
{
    public float THROW_DISTANCE = 1;
    [Range(0, 360)]
    public float ATTACK_ANGLE = 0;

    GuardAnimatorScript _enemyAnimScript;
    Transform _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAnimScript = GetComponent<GuardAnimatorScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
        StartCoroutine(AttemptAttack());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AttemptAttack()
    {
        while (true)
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) <= THROW_DISTANCE &&
                    Vector3.Angle(transform.forward, _playerTransform.position - transform.position) < ATTACK_ANGLE / 2 &&
                    !_enemyAnimScript.AnimationIsPlaying(AnimationState.throwNinjaStar))
            {
                _enemyAnimScript.PlayAttackAnim();
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
