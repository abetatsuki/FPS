
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace FPS.InGame.Scripts.Player
{
    /// <summary> プレイヤーマネージャー </summary>
    [RequireComponent(typeof(PlayerInput))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    //[RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
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
        private Animator _animator;

        private float _currentSpeed;

        private float _speed = 0f;
        private void Awake()
        {
            Init();
        }
        private void Update()
        {
            _playerLook.ChangeLookDirection();
            _playerMove.MoveRigidbody();
            ParameterUpdate();
            SetAnimation();
        }
        private void OnEnable()
        {
            InputEventRegister();
            PureCheck();
        }
        private void OnDisable()
        {
            InputEventUnRegister();
        }



        private void GetComponent()
        {
            _playerinput = GetComponent<PlayerInput>();
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
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
        private void SetAnimation()
        {
            _animator.SetFloat("Speed", _currentSpeed);
        }
        private void ParameterUpdate()
        {
            _currentSpeed = _playerMove.CurrentSpeed;
        }

        private void PureCheck()
        {
            bool ok = true;

            if (_playerinput == null)
            {
                Debug.LogError("PlayerManager.PureCheck: PlayerInput is null.");
                ok = false;
            }
            if (_rb == null)
            {
                Debug.LogError("PlayerManager.PureCheck: Rigidbody is null.");
                ok = false;
            }
            if (_inputBuffer == null)
            {
                Debug.LogError("PlayerManager.PureCheck: InputBuffer is null.");
                ok = false;
            }
            if (_playerLook == null)
            {
                Debug.LogError("PlayerManager.PureCheck: PlayerLook is null.");
                ok = false;
            }
            if (_playerMove == null)
            {
                Debug.LogError("PlayerManager.PureCheck: PlayerMove is null.");
                ok = false;
            }
            if (_animator == null)
            {
                Debug.LogError("PlayerManager.PureCheck: Animator is null.");
                ok = false;
            }

            if (!ok)
            {
                // 致命的な欠損があれば無効化して以降の参照エラーを防ぐ
                enabled = false;
                return;
            }

        }

        


    }
}
