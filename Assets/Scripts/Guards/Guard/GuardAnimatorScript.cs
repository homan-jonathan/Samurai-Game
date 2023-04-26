using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimatorScript : MonoBehaviour
{
    protected Animator _anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PlayDeathAnim()
    {
        _anim.SetTrigger("isDead");
        print("DIE");
    }
    public virtual void PlayAttackAnim() { }

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
