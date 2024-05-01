using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gun : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float _forceAmount = 600;
    [SerializeField] private float _maxTorqueBonus = 150;
    [SerializeField] private float _torque = 120;

    [SerializeField] private float _maxUpAssist = 30;
    [SerializeField] private float _maxY = 10;
    
    [SerializeField] private float _maxAngularVelocity = 10;

    private Animator _gunAnimator;
    [SerializeField] private GameObject _sparkEffect;

    private void Awake()
    {
        _gunAnimator = transform.GetChild(0).GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_sparkEffect, _spawnPoint.position, _spawnPoint.rotation);
            _gunAnimator.SetTrigger("Fire");
            // Apply force - More up assist depending on y position
            var assistPoint = Mathf.InverseLerp(0, _maxY, _rb.position.y);
            var assistAmount = Mathf.Lerp(_maxUpAssist, 0, assistPoint);
            var forceDir = -transform.forward * _forceAmount + Vector3.up * assistAmount;
            if (_rb.position.y > _maxY) forceDir.y = Mathf.Min(0, forceDir.y);
            _rb.AddForce(forceDir);

            // Determine the additional torque to apply when swapping direction
            var angularPoint = Mathf.InverseLerp(0, _maxAngularVelocity, Mathf.Abs(_rb.angularVelocity.z));
            var amount = Mathf.Lerp(0, _maxTorqueBonus, angularPoint);
            var torque = _torque + amount;
            
            // Apply torque
            var dir = Vector3.Dot(_spawnPoint.forward, Vector3.right) < 0 ? Vector3.left : Vector3.right;
            _rb.AddTorque(dir * torque);
        }
    }
}
