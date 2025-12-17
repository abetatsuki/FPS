using UnityEngine;

public class IKRight : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform rightHandTarget;
    [SerializeField] private Transform leftHandTarget;

    // 左肩の前倒し量（0.2〜0.35）
    [SerializeField] private float leftShoulderWeight = 0.25f;

    // OnAnimatorIK は Unity が IK を処理するタイミングで呼ばれる
    private void OnAnimatorIK(int layerIndex)
    {
        if (animator == null)
        {
            return;
        }
        var leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);

        // --- 右手 IK（武器を持つ手なのでそのまま） ---
        HandleHandIK(AvatarIKGoal.RightHand, rightHandTarget);

        // --- 左手 IK（ここを追加することで左手が動く） ---
        HandleHandIK(AvatarIKGoal.LeftHand, leftHandTarget);

        // --- 左肩のねじれ補正 ---
        AdjustLeftShoulder();
    }

    // HandleHandIK は「手のIKをターゲット位置に合わせる処理」
    private void HandleHandIK(AvatarIKGoal goal, Transform target)
    {
        if (target == null)
        {
            animator.SetIKPositionWeight(goal, 0f);
            animator.SetIKRotationWeight(goal, 0f);
            return;
        }

        animator.SetIKPositionWeight(goal, 1f);
        animator.SetIKRotationWeight(goal, 1f);
        animator.SetIKPosition(goal, target.position);
        animator.SetIKRotation(goal, target.rotation);
    }

    // AdjustLeftShoulder は「左肩が胸にねじれ込むのを防ぐ処理」
    // 前方向(Pitch)へ少し倒すだけで激的に自然になる。
    private void AdjustLeftShoulder()
    {
        Transform leftShoulder = animator.GetBoneTransform(HumanBodyBones.LeftShoulder);

        if (leftShoulder == null)
        {
            return;
        }

        // 左肩を前に倒す回転（15度）
        Quaternion forward = Quaternion.Euler(15f, 0f, 0f);

        leftShoulder.rotation = Quaternion.Slerp(
            leftShoulder.rotation,
            leftShoulder.rotation * forward,
            leftShoulderWeight
        );
    }
}