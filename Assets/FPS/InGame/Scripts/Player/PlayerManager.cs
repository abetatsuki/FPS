
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace FPS.InGame.Scripts.Player
{
    /// <summary> プレイヤーマネージャー </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(NavMeshAgent))]
    [DefaultExecutionOrder(-100)]
    public class PlayerManager : MonoBehaviour
    {
        public void Init()
        {
            GetComponent();
            CreatePure();
            InitPure();
        }

        [SerializeField] private GameObject _camera;
        private PlayerInput _playerinput;
        private Rigidbody _rb;
        private InputBuffer _inputBuffer;
        private PlayerLook _playerLook;
        private PlayerMove _playerMove;

        private Vector2 _moveInput = Vector2.zero;
        private void Awake()
        {
            Init();
        }
        private void OnEnable()
        {
            InputEventRegister();
        }
        private void OnDisable()
        {
            InputEventUnRegister();
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

        private void CreatePure()
        {
            _playerLook = new PlayerLook(this.transform, _camera.transform);
            _playerMove = new PlayerMove(_rb, _camera.transform);
            _inputBuffer = new InputBuffer(_playerinput);
        }

        private void InitPure()
        {
            _inputBuffer.Init();
        }
        private void InputEventRegister()
        {
            _inputBuffer.MoveAction.Performed += _playerMove.InputMove;
            _inputBuffer.MoveAction.Canceled += _playerMove.InputMove;
            _inputBuffer.LookAction.Performed += _playerLook.InputOnLook;
        }
        private void InputEventUnRegister()
        {
            _inputBuffer.MoveAction.Performed -= _playerMove.InputMove;
            _inputBuffer.MoveAction.Canceled -= _playerMove.InputMove;
            _inputBuffer.LookAction.Performed -= _playerLook.InputOnLook;
        }


    }
}
