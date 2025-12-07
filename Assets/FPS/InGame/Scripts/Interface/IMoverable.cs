using UnityEngine;

public interface IMovarable
{
    void Move(Vector3 direction);
    public float CurrentSpeed { get; }
}
