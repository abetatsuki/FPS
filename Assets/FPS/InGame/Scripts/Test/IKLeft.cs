using UnityEngine;

/// <summary>
/// 両手IKと左肘Hint、および上半身回転補正を行うクラス。
/// OnAnimatorIK はアニメーション更新の最後に実行され、IK値を反映する。
/// </summary>
public class IKController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform rightHandTarget;

    [SerializeField]
    private Transform leftHandTarget;

    [SerializeField]
    private Transform leftElbowHint;

    // Spine3：上半身を制御する骨
    private Transform spine3;

    private void Start()
    {
        // Spine3 のキャッシュ（HumanBodyBones.Spine によって階層が違う場合があるため、Spine3 を明示）
        spine3 = animator.GetBoneTransform(HumanBodyBones.UpperChest);
    }

    /// <summary>
    /// Unity の AnimatorIK 更新タイミングで呼ばれ、
    /// 手足IK・肘Hint・上半身回転補正を行う。
    /// </summary>
    /// <param name="layerIndex">レイヤー番号（使用しない）</param>
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null)
        {
            return;
        }

        ApplyRightHandIK();
        ApplyLeftHandIK();
       // AdjustUpperBodyRotation();
    }

    /// <summary>
    /// 右手をターゲットに追従させる IK 設定。
    /// </summary>
    private void ApplyRightHandIK()
    {
        if (rightHandTarget == null)
        {
            return;
        }

        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);
    }

    /// <summary>
    /// 左手と肘Hintを設定して左手が自然に銃を添えるようにする。
    /// </summary>
    private void ApplyLeftHandIK()
    {
        if (leftHandTarget == null)
        {
            return;
        }

        // 手の位置・回転
        animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

        animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
        animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

        // 肘Hint
        if (leftElbowHint != null)
        {
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, 1f);
            animator.SetIKHintPosition(AvatarIKHint.LeftElbow, leftElbowHint.position);
        }
    }

    /// <summary>
    /// 上半身（Spine3）を右手方向へ少しだけ向け、
    /// 銃の構え姿勢を自然に見せる補正。
    /// </summary>
    private void AdjustUpperBodyRotation()
    {
        if (spine3 == null || rightHandTarget == null)
        {
            return;
        }

        Vector3 direction = rightHandTarget.position - spine3.position;
        if (direction.sqrMagnitude < 0.0001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);

        // 上半身がガクッと向かないようにゆるめ（0.2〜0.4が自然）
        float weight = 0.3f;

        spine3.rotation = Quaternion.Slerp(spine3.rotation, targetRotation, weight);
    }
}
