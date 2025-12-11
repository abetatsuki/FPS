using UnityEngine;

public interface IJumprable
{
    void Jump(float jump);
    void OnCollisionEnter(Collision collider);
}
