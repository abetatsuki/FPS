using System.Diagnostics;
using UnityEngine;

namespace FPS.InGame.Scripts.Player
{
    public class PlayerLook
    {
        public Vector2 PlayerLookInput => _playerLookInput;

        public void InputOnLook(Vector2 lookDirection)
        {
            _playerLookInput = lookDirection;
            SetDirection(lookDirection);
        }

        public void ChangeLookDirection(Transform player)
        {
            player.rotation = Quaternion.Euler(_yaw,_pitch , 0);
        }

        private void SetDirection(Vector2 lookDirection)
        {
            _yaw -= lookDirection.y;
            _pitch += lookDirection.x;
            _yaw = Mathf.Clamp(_yaw, -_maxDt, _maxDt);
            _pitch = Mathf.Clamp(_pitch, -_maxDt, _maxDt);
        }

        private Vector2 _playerLookInput;
        private float _yaw;
        private float _pitch;
        private float _maxDt = 45f;
    }
}
