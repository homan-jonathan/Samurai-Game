using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{
    EnemyAnimScript enemyAnimScript;
    public float SWING_DISTANCE = 1;
    public Transform playerTransform;
    [Range(0,360)]
    public float SWING_ANGLE = 0;
    bool hasSwung = false;
    // Start is called before the first frame update
    void Start()
    {
        enemyAnimScript = GetComponent<EnemyAnimScript>();
        StartCoroutine(AttemptAttack());
    }

    // Update is called once per frame
    void Update()
    {
        
        /*if (!enemyAnimScript.AnimationIsPlaying(AnimationState.swingSword)) {
            hasSwung = false;
        }*/
    }

    IEnumerator AttemptAttack() {
        while (true)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) <= SWING_DISTANCE &&
                    Vector3.Angle(transform.forward, playerTransform.position - transform.position) < SWING_ANGLE / 2 &&
                    !enemyAnimScript.AnimationIsPlaying(AnimationState.swingSword) &&
                    !hasSwung
                    )
            {
                //hasSwung = true;
                enemyAnimScript.PlayAttackAnim();
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
