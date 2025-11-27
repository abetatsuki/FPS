using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.InGame.Scripts.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [DefaultExecutionOrder(-100)]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private PlayerInput _playerinput;
        private InputBuffer _inputBuffer;
        private PlayerLook _playerLook;
        private void Awake()
        {
            GetComponent();
            Init();
            SetEvent();
        }

        private void Update()
        {
            _playerLook.ChangeLookDirection(_camera.transform);
        }

        private void GetComponent()
        {
            _playerinput = GetComponent<PlayerInput>();
        }

        private void Init()
        {
            _inputBuffer = new InputBuffer(_playerinput);
            _playerLook = new PlayerLook();
        }

        private void SetEvent()
        {
            _inputBuffer.OnLook += _playerLook.InputOnLook;
        }

        private void DisposeEvent()
        {
            _inputBuffer.OnLook -= _playerLook.InputOnLook;
        }
    }
}
