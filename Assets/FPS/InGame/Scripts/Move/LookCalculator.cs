using UnityEngine;

public static class LookCalculator
{
    public static (float yaw, float pitch) CalculatorLook(Vector2 direction, float speed, float deltaTime,float maxDt,float yaw , float pitch)
    {
       
        yaw += direction.x * speed * deltaTime;
        pitch -= direction.y * speed * deltaTime;
        pitch  = Mathf.Clamp(pitch, -maxDt, maxDt);
        return (yaw, pitch);

    }
}
