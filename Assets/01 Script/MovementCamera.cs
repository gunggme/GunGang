using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _distance;
    [SerializeField] private float _height;
    
    [SerializeField] private float _damping;
    
    private void FixedUpdate()
    {
        Vector3 moveVec = (Vector3.left * _distance) + (Vector3.up * _height) + _target.position;

        transform.position = Vector3.Lerp(transform.position, moveVec, _damping * Time.deltaTime);
    }
}
