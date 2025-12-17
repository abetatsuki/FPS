using UnityEngine;

public class IKWeightController : MonoBehaviour
{
    private Animator _animator;

    /// <summary>
    /// Animator を取得します。
    /// </summary>
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        SetHandIKWeight(0f);
    }

    /// <summary>
    /// 手の IK 重みを設定します。
    /// 値は 0〜1 の範囲で、IK による手の制御をどれだけ有効にするかが変わります。
    /// </summary>
    /// <param name="weight">適用する IK 重み（0〜1）</param>
    public void SetHandIKWeight(float weight)
    {
        float w = Mathf.Clamp01(weight);

        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, w);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, w);

        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, w);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightHand, w);
    }

    /// <summary>
    /// AnimatorIK のタイミングで IK weight を反映します。
    /// SetHandIKWeight で設定された値が毎フレーム適用されます。
    /// </summary>
    private void OnAnimatorIK(int layerIndex)
    {
        // SetHandIKWeight が直接 Animator に値を入れているため、ここでは処理を追加していません。
        // 必要になればここに調整コードを追加できます。
    }
}