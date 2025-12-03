using Assets.FPS.InGame.Scripts.Interface;
using UnityEngine;


public class Look : ILookrable
{
    public Look(Transform me, Transform camera)
    {
        _camera = camera;
        _me = me;
    }

    public void ChangeLook(Vector2 LookInput)
    {
        (_yaw,_pitch) = LookCalculator.CalculatorLook(LookInput, _speed, Time.deltaTime, _maxDt,_yaw,_pitch);
        _me.rotation = Quaternion.Euler(0f, _yaw, 0f);
        _camera.localRotation = Quaternion.Euler(_pitch, _yaw, 0f);
    }

    private float _speed = 5f;
    private float _maxDt = 45f; //SOData‰»‚µ‚æ‚¤
    private float _yaw = 0f;
    private float _pitch = 0f;
    private Transform _me;
    private Transform _camera;

}
