using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardRangeAttack : MonoBehaviour
{
    public float THROW_DISTANCE = 1;
    [Range(0, 360)]
    public float ATTACK_ANGLE = 0;
    public GameObject shuriken;
    public float speed;

    GuardAnimatorScript _enemyAnimScript;
    Transform _playerTransform;
    Transform _spawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        _enemyAnimScript = GetComponent<GuardAnimatorScript>();
        _playerTransform = GetComponent<GuardMainScript>().GetPlayerReference().transform;
        _spawnLocation = transform.Find("Root/Hips/Spine_01/Spine_02/Spine_03/Clavicle_R/Shoulder_R/Elbow_R/Hand_R/SM_Wep_Shuriken_01");
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
                    !_enemyAnimScript.AnimationIsPlaying(AnimationState.throwshuriken))
            {
                _enemyAnimScript.PlayAttackAnim();
                GameObject ninjaStar = Instantiate(shuriken, _spawnLocation.position, Quaternion.identity);
                ninjaStar.GetComponent<Rigidbody>().AddForce(Quaternion.Euler(0, -90, 0) * new Vector3((_playerTransform.position - _spawnLocation.position).x, 0, (_playerTransform.position - _spawnLocation.position).z) * Time.deltaTime * speed);
                print(_spawnLocation);
                print((_spawnLocation.position - _playerTransform.position) * Time.deltaTime * speed);
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
