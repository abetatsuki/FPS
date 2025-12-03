using UnityEngine;

namespace Assets.FPS.InGame.Scripts.Interface
{
    public interface IInputrable
    {
        Vector3 MoveInput { get; }
        Vector3 LookInput { get; }
    }
}