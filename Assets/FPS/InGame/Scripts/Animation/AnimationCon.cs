using UnityEngine;

public class AnimationCon
{
    public AnimationCon(Animator anim)
    {
       _anim = anim;
    }
    public void SetAnimation(float speed)
    {
      _anim.SetFloat("Speed",speed);
    }
    private Animator _anim;
}
