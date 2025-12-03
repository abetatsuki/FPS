using Assets.FPS.InGame.Scripts.Interface;
using FPS.InGame.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerInput))]
public class PlayerCon : MonoBehaviour
{
    private IMovarable _mover;
    private IInputrable _inputrable;
    private ILookrable _lookrable;
 
    [SerializeField] private Transform _cameraRoot;
    [SerializeField] private float _speed = 10f;
    private void Awake()
    {
        _mover = new RigidBodyMover(GetComponent<Rigidbody>(), _speed,_cameraRoot);
        _inputrable = new PlayerInputReader(GetComponent<PlayerInput>());
        _lookrable = new Look(GetComponent<Transform>(),_cameraRoot.transform);
    }

    private void Update()
    {
        _lookrable.ChangeLook(_inputrable.LookInput);
        _mover.Move(_inputrable.MoveInput);
    }

}
