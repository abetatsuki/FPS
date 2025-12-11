using Assets.FPS.InGame.Scripts.Interface;
using FPS.InGame.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;
using UniRx;
[RequireComponent(typeof(PlayerInput))]
public class PlayerCon : MonoBehaviour
{
    private IMovarable _mover;
    private IInputrable _inputrable;
    private ILookrable _lookrable;
    private IJumprable _jumprable;
    private AnimationCon _animCon;
    private Collider _collider;
 
    private readonly IntReactiveProperty _health = new IntReactiveProperty();

    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _speed = 10f;
    private void Awake()
    {
        _mover = new RigidBodyMover(GetComponent<Rigidbody>(), _speed,_cameraRoot);
        _inputrable = new PlayerInputReader(GetComponent<PlayerInput>());
        _lookrable = new Look(GetComponent<Transform>(),_cameraRoot.transform);
        _animCon = new AnimationCon(GetComponent<Animator>());
        _jumprable = new Jumper(GetComponent<Collider>());

    }

    private void Update()
    {
        Player();
    }
    private void Player()
    {
        _lookrable.ChangeLook(_inputrable.LookInput);
        _mover.Move(_inputrable.MoveInput);
        _animCon.SetAnimation(_mover.CurrentSpeed);
        _jumprable.Jump(_inputrable.JumpInput);
    }
    private void OnCollisionEnter(Collision collision)
    {
        _jumprable.OnCollisionEnter(collision);
    }
}
