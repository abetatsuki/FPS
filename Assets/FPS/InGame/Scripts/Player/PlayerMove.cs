
using UnityEngine;

namespace FPS.InGame.Scripts.Player
{
    public class PlayerMove
    {
        public PlayerMove(Rigidbody rigidbody, Transform camera)
        {
            _rb = rigidbody;
            _cameraTransform = camera;
        }
        public float CurrentSpeed => _currentSpeed;
        public void InputMove(Vector2 input)
        {
            _moveInput = new Vector3(input.x, 0, input.y);
        }

        public void MoveRigidbody()
        {
            Vector3 _velocity = _rb.linearVelocity;

            Vector3 _cameraForward = _cameraTransform.forward;
            Vector3 _cameraRight = _cameraTransform.right;

            _cameraForward.y = 0;
            _cameraRight.y = 0;
            _cameraForward.Normalize();
            _cameraRight.Normalize();

            Vector3 _moveDir = (_cameraForward * _moveInput.z) +
                       (_cameraRight * _moveInput.x);

            Vector3 _horizontalVelocity = _moveDir * _moveSpeed;

            _rb.linearVelocity = new Vector3(_horizontalVelocity.x,
                _velocity.y, _horizontalVelocity.z);

            _currentSpeed = _horizontalVelocity.magnitude;
        }

        private Rigidbody _rb;
        private Transform _cameraTransform;

        private Vector3 _moveInput = Vector2.zero;

        private float _moveSpeed = 3f;
        private float _currentSpeed = 0f;
    }
}