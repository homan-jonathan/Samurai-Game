using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WMEnemyAttackScript : MonoBehaviour
{
    public float SWING_DISTANCE = 1;
    [Range(0,360)]
    public float SWING_ANGLE = 0;

    WMEnemyAnimScript _enemyAnimScript;
    Transform _playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAnimScript = GetComponent<WMEnemyAnimScript>();
        _playerTransform = _enemyAnimScript.GetPlayerReference().transform;
        StartCoroutine(AttemptAttack());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator AttemptAttack() {
        while (true)
        {
            if (Vector3.Distance(_playerTransform.position, transform.position) <= SWING_DISTANCE &&
                    Vector3.Angle(transform.forward, _playerTransform.position - transform.position) < SWING_ANGLE / 2 &&
                    !_enemyAnimScript.AnimationIsPlaying(AnimationState.swingSword))
            {
                _enemyAnimScript.PlayAttackAnim();
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
