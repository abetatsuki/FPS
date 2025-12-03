using Unity.VisualScripting;
using UnityEngine;

public static class MovementCalculator
{ 
    public static Vector3 Calculate(Vector3 direction, float speed, float deltaTime,Transform camera)
    {
        Vector3 forward = camera.forward;
        Vector3 right = camera.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 moveDir = (forward * direction.z) + (right * direction.x);
        Vector3 _velocity = moveDir * speed;
        return _velocity;
    }
}
