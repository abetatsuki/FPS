using UnityEngine;

public class Jumper : IJumprable
{
    public Jumper(Collider collider)
    {
        _Collider = collider;
        _rb = _Collider.attachedRigidbody;
    }
    public void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
    }

    public void Jump(float value)
    {
        if (_isGround) return;
        if (value < _threshold) return;
        _rb.AddForce(Vector3.up * _jumpPower, ForceMode.VelocityChange);
    }

    private float _threshold = 0.5f;
    private float _jumpPower = 1f;
    private bool _isGround = false;
    private Rigidbody _rb;
    private Collider _Collider;
}
