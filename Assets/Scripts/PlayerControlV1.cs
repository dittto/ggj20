using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlV1 : MonoBehaviour
{
    private int _isActiveFlag;
    private bool _isButtonHeld = false;
    
    public Animator _directionAnimator;
    public Transform _direction;
    public Rigidbody2D _body;

    public float _rotationSpeed;
    public float _moveSpeed;

    public void Awake()
    {
        _isActiveFlag = Animator.StringToHash("isActive");
    }

    public void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) {
            return;
        }

        if (keyboard.spaceKey.wasPressedThisFrame) {
            ShowDirection();
        }

        if (keyboard.spaceKey.wasReleasedThisFrame) {
            MovePlayer();
            HideDirection();
        }

        if (_isButtonHeld) {
            UpdateDirection();
        }
    }

    private void ShowDirection()
    {
        _directionAnimator.SetBool(_isActiveFlag, true);
        _isButtonHeld = true;
    }

    private void HideDirection()
    {
        _directionAnimator.SetBool(_isActiveFlag, false);
        _isButtonHeld = false;
    }

    private void UpdateDirection()
    {
        _direction.eulerAngles = _direction.eulerAngles + new Vector3(0, 0, _rotationSpeed * -1);
    }

    private void MovePlayer()
    {
        _body.AddForce(new Vector2(Mathf.Cos(Mathf.Deg2Rad * _direction.eulerAngles.z) * _moveSpeed, Mathf.Sin(Mathf.Deg2Rad * _direction.eulerAngles.z) * _moveSpeed));
    }
}
