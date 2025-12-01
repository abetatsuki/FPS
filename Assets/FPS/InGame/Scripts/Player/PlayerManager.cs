
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.InGame.Scripts.Player
{
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    [DefaultExecutionOrder(-100)]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _camera;
        private PlayerInput _playerinput;
        private Rigidbody _rb;
        private InputBuffer _inputBuffer;
        private PlayerLook _playerLook;
        private PlayerMove _playerMove;
        private void Awake()
        {
            GetComponent();
            Init();
        }

        private void Update()
        {
            _playerLook.ChangeLookDirection();
            _playerMove.MoveRigidbody();
        }

        private void GetComponent()
        {
            _playerinput = GetComponent<PlayerInput>();
            _rb = GetComponent<Rigidbody>();
        }

        private void Init()
        {
            _playerLook = new PlayerLook(this.transform,_camera.transform);
            _playerMove = new PlayerMove(_rb,_camera.transform);
            
        }

        
    }
}
