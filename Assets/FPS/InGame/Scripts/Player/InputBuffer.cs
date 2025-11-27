using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.InGame.Scripts.Player
{
    public class InputBuffer
    {
        public InputBuffer(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            SetInputAction();
            SetEvent();
        }

        public event Action<Vector2> OnLook;
        public event Action<Vector2> OnMove;

        private PlayerInput _playerInput;
        
        private const string LOOKACTION = "Look";
        private const string MOVEACTION = "Move";
        
        private InputAction _lookAction;
        private InputAction _moveAction;

        private void SetInputAction()
        {
            _lookAction = _playerInput.actions[LOOKACTION];
            _moveAction = _playerInput.actions[MOVEACTION];
        }

        private void SetEvent()
        {
            _lookAction.performed += OnLookInput;
            _lookAction.canceled += OnLookInput;
            _moveAction.performed += OnMoveInput;
            _moveAction.canceled += OnMoveInput;
        }

        private void Dispose()
        {
            if (_playerInput == null) return;
            
            _lookAction.performed -= OnLookInput;
            _lookAction.canceled -= OnLookInput;
            _moveAction.performed -= OnMoveInput;
            _moveAction.canceled -= OnMoveInput;
            
            _lookAction.Disable();
            _moveAction.Disable();
        }

        private void OnLookInput(InputAction.CallbackContext context)
        {
            OnLook?.Invoke(context.ReadValue<Vector2>());
        }

        private void OnMoveInput(InputAction.CallbackContext context)
        {
            OnMove?.Invoke(context.ReadValue<Vector2>());
        }
    } 
}
