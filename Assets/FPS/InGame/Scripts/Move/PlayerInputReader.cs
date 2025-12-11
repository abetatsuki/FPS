using Assets.FPS.InGame.Scripts.Interface;
using FPS.InGame.Scripts.Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : IInputrable
{
    private InputBuffer _inputBuffer;
    private PlayerInput _playerInput;
    public Vector3 MoveInput { get; private set; }
    public Vector3 LookInput { get; private set; }
    public float JumpInput { get; private set; }

    public PlayerInputReader(PlayerInput playerInput)
    {
        _playerInput = playerInput;
        Init();
    }
    private void Init()
    {
        _inputBuffer = new InputBuffer(_playerInput);
        _inputBuffer.Init();
        _inputBuffer.MoveAction.Performed += InputMove;
        _inputBuffer.MoveAction.Canceled += InputMove;
        _inputBuffer.LookAction.Performed += InputLook;
        _inputBuffer.LookAction.Canceled += InputLook;
        _inputBuffer.JumpAction.Performed += InputJump;
        _inputBuffer.JumpAction.Canceled += InputJump;


    }
    public void InputMove(Vector2 input)
    {
        MoveInput = new Vector3(input.x, 0, input.y);
    }

    public void InputLook(Vector2 input)
    {
        LookInput = input;
    }
    public void InputJump(float jump)
    {
        JumpInput = jump;
    }
}
