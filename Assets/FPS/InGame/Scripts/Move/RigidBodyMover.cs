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
        var rb = direction;
        Vector3 delta = MovementCalculator.Calculate(direction, _speed, Time.deltaTime, _camera);
        _rb.linearVelocity = Vector3.Lerp(_rb.linearVelocity,delta,_acceleration * Time.deltaTime);
        _currentSpeed = rb.x + rb.z;
    }
    private float _currentSpeed = 0f;
    private float _speed;
    private float _acceleration = 2f;
    private Transform _camera;
    private Rigidbody _rb;
}
