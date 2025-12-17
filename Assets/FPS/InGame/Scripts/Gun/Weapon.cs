using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    private Rigidbody _rb;
    public bool IsRotation { get; set; }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_rb)
        {
            _rb.isKinematic = true;
        }

        IsRotation = true;
    }

    private void Update()
    {
        if(!IsRotation) return;
        transform.Rotate(Vector3.up * _rotationSpeed * (1 - Mathf.Exp(-_rotationSpeed * Time.deltaTime)));
    }
}