using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAnimatorScript : MonoBehaviour
{
    protected Animator _anim;
    protected string _attackAnim;
    protected GuardSoundScript _guardSounds;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public virtual void PlayDeathAnim()
    {
        _anim.SetTrigger("isDead");
    }
    public virtual void PlayAttackAnim() {
        _anim.SetTrigger("attack");
        _guardSounds.AttackNoise();
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

    public bool AttackAnimationIsPlaying()
    {
        return AnimatorIsPlaying() && _anim.GetCurrentAnimatorStateInfo(0).IsName(_attackAnim);
    }
}
