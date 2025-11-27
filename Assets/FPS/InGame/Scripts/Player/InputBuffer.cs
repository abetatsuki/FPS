using System;
using TMPro;
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

        private PlayerInput _playerInput;
        private const string LOOKACTION = "Look";
        private InputAction _lookAction;

        private void SetInputAction()
        {
            _lookAction = _playerInput.actions[LOOKACTION];
        }

        private void SetEvent()
        {
            _lookAction.performed += OnLookInput;
            _lookAction.canceled += OnLookInput;
        }

        private void Dispose()
        {
            if (_playerInput == null) return;
            _lookAction.performed -= OnLookInput;
            _lookAction.canceled -= OnLookInput;
            _lookAction.Disable();
        }

        private void OnLookInput(InputAction.CallbackContext context)
        {
            OnLook?.Invoke(context.ReadValue<Vector2>());
        }
    } 
}
