
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

        public void InputMove(Vector2 input)
        {
           _moveInput = new Vector3(input.x, 0, input.y);
        }

        public void MoveRigidbody()
        {
            _velocity = _rb.linearVelocity;
            
            _cameraForward =  _cameraTransform.forward;
            _cameraRight   =  _cameraTransform.right;

            _cameraForward.y = 0;
            _cameraRight.y = 0;
            _cameraForward.Normalize();
            _cameraRight.Normalize();
            
            _moveDir = (_cameraForward * _moveInput.z) +
                       (_cameraRight * _moveInput.x);
            
            _horizontalVelocity = _moveDir * _moveSpeed;

            _rb.linearVelocity = new Vector3(_horizontalVelocity.x,
                _velocity.y, _horizontalVelocity.z);
        }
        
        private Rigidbody _rb;
        private Transform _cameraTransform;
        
        private Vector3 _moveInput = Vector2.zero;
        private Vector3 _velocity = Vector3.zero;
        private Vector3 _horizontalVelocity = Vector3.zero;
        
        private Vector3  _cameraForward = Vector3.zero;
        private Vector3  _cameraRight = Vector3.zero;
        private Vector3  _moveDir = Vector3.zero;
        
        private float _moveSpeed = 3f;
    }
}