using UnityEngine;

public class RigidBodyMover : IMovarable
{
    public RigidBodyMover(Rigidbody rb, float speed, Transform camera)
    {
        _rb = rb;
        _speed = speed;
        _camera = camera;
    }
    public float CurrentSpeed => _currentSpeed;
    public void Move(Vector3 direction)
    {
        Vector3 delta = MovementCalculator.Calculate(direction, _speed, Time.deltaTime, _camera);
        _rb.linearVelocity = delta;
        _currentSpeed = _rb.linearVelocity.magnitude;
    }
    private float _currentSpeed = 0f;
    private float _speed;
    private Transform _camera;
    private Rigidbody _rb;
}
