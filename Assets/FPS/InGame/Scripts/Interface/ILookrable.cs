using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.FPS.InGame.Scripts.Interface
{
    public interface ILookrable
    {
        void ChangeLook(Vector2 direction);
    }
}