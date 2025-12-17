using UnityEngine;

public class AnimationCon
{
    public AnimationCon(Animator anim)
    {
       _anim = anim;
    }
    public void SetAnimation(float speed)
    {
      _anim.SetFloat("Speed",speed,0.1f,Time.deltaTime);
    }
    private Animator _anim;
}
