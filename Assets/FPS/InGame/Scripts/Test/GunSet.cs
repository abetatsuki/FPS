using System;
using UnityEngine;

public class GunSet : MonoBehaviour
{
   [SerializeField] private GameObject _gun;
   [SerializeField] private Transform _fightPos;
   [SerializeField] private bool _isFight = false;
   private Transform _runPos;

   private void Start()
   {
      _runPos = _gun.transform;
   }

   private void Update()
   {
      
      if(_isFight)
      {
         _gun.transform.localPosition = _fightPos.localPosition;
         _gun.transform.localRotation = _fightPos.localRotation;
      }
      else
      {
         _gun.transform.localPosition = _runPos.localPosition;
         _gun.transform.localRotation = _runPos.localRotation;
      }
   }
}
