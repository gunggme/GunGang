using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gun : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private Transform _bulletFirePos;

    [SerializeField] private float _movePower = 600;
    [SerializeField] private float _maxTorqueBonus = 150;
    [SerializeField] private float _torque = 120;

    [SerializeField] private float _maxUpAssist = 30;
    [SerializeField] private float _maxY = 10;
    
    [SerializeField] private float _maxAngularVelocity = 10;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var assistPoint = Mathf.InverseLerp(0, _maxY, _rb.velocity.y);
            var assistAmount = Mathf.Lerp(_maxUpAssist, 0, assistPoint);
            var forceDir = -transform.forward * _movePower + Vector3.up * assistAmount;
            if (_rb.position.y > _maxY) forceDir.y = Mathf.Min(0, forceDir.y);
            _rb.AddForce(forceDir);

            var angularPoint = Mathf.InverseLerp(0, _maxAngularVelocity, Mathf.Abs(_rb.angularVelocity.z));
            var amount = Mathf.Lerp(0, _maxTorqueBonus, angularPoint);
            var torque = _torque + amount;
            
            var dir = Vector3.Dot(_bulletFirePos.right, Vector3.forward) < 0 ? Vector3.right : Vector3.left;
            _rb.AddTorque(dir * torque);
        }
    }
}
