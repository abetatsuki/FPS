using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class EquipWeapon : MonoBehaviour
{
    [Header("Ray Settings")] [SerializeField] [Range(0, 2)]
    private float _rayLength;

    [SerializeField] private Vector3 _rayOffset;
    [SerializeField] private LayerMask _weaponMask;
    [SerializeField] private Transform _equipPos;
    [SerializeField] private Transform _aimingPos;
    private RaycastHit _topRayHitInfo;

    private Weapon _currentWeapon;
    private bool _isAiming;

    [Header("Right Hand Target")] [SerializeField]
    private TwoBoneIKConstraint _rightHandIK;
    [SerializeField] private Transform rightHandTarget;
    
    [Header("Left Hand Target")] [SerializeField]
    private TwoBoneIKConstraint _leftHandIK;
    [SerializeField] private Transform leftHandTarget;

    [SerializeField] private Transform _IkRightHandPos;
    [SerializeField] private Transform _IkLeftHandPos;

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Equip();
        }

        if (Input.GetButtonDown("Fire1"))
        {
            _isAiming = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            _isAiming = false;
        }

        if (Input.GetButtonDown("Fire2"))
        {
            _isAiming = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            _isAiming = false;
        }

        if (_currentWeapon)
        {
            if (!_isAiming)
            {
                _currentWeapon.transform.parent = _equipPos.transform;
                _currentWeapon.transform.position = _equipPos.position;
                _currentWeapon.transform.rotation = _equipPos.rotation;

                _leftHandIK.weight = 0f;
            }
            else
            {
                _currentWeapon.transform.parent = _aimingPos.transform;
                _currentWeapon.transform.position = _aimingPos.position;
                _currentWeapon.transform.rotation = _aimingPos.rotation;

                _leftHandIK.weight = 1f;
                leftHandTarget.position = _IkLeftHandPos.position;
                leftHandTarget.rotation = _IkLeftHandPos.rotation;
            }
                _rightHandIK.weight = 1f;
                rightHandTarget.position = _IkRightHandPos.position;
                rightHandTarget.rotation = _IkRightHandPos.rotation;
        }
}

    private void FixedUpdate()
    {
        RayCastsHandler();
    }

    private void RayCastsHandler()
    {
        Ray topRay = new Ray(transform.position + _rayOffset, transform.forward);
        Debug.DrawRay(transform.position + _rayOffset, transform.forward * _rayLength, Color.red);
        Physics.Raycast(topRay, out _topRayHitInfo, _rayLength, _weaponMask);
    }

    private void Equip()
    {
        if (_topRayHitInfo.collider != null)
        {
            _currentWeapon = _topRayHitInfo.transform.gameObject.GetComponent<Weapon>();
        }

        if (!_currentWeapon) return;
        

        _currentWeapon.IsRotation = false;
    }
}