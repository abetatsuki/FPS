
using UnityEngine;

namespace FPS.InGame.Scripts.Player
{
    public class PlayerLook
    {
        public PlayerLook(Transform player,Transform camera)
        {
            _player = player;
            _camera = camera;
        }
        public Vector2 PlayerLookInput => _playerLookInput;

        public void InputOnLook(Vector2 lookDirection)
        {
            _playerLookInput = lookDirection;
            SetDirection(lookDirection);
        }

        public void ChangeLookDirection()
        {
            _player.rotation =Quaternion.Euler(0,_yaw,0);
           _camera.rotation = Quaternion.Euler(_pitch,_yaw, 0);
        }

        private void SetDirection(Vector2 lookDirection)
        {
            _pitch -= lookDirection.y *_lookSpeed * Time.deltaTime;
            _yaw += lookDirection.x * _lookSpeed * Time.deltaTime;
            
            _pitch = Mathf.Clamp(_pitch,- _maxDt, _maxDt);
            
        }

        private Transform _player;
        private Transform _camera;
        private Vector2 _playerLookInput;
        private float _lookSpeed = 10f;
        private float _pitch;
        private float _yaw;
        private float _maxDt = 45f;
    }
}