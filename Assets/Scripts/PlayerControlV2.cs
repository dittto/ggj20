using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlV2 : MonoBehaviour
{
    private int _isActiveFlag;
    private bool _isButtonHeld = false;
    private float _power;
    
    public Animator _directionAnimator;
    public Animator _arrowMaskAnimator;
    public Animator _arrowAnimator;
    public Transform _direction;
    public Transform _arrow;
    public Rigidbody _body;
    public ParticleSystem thrustParticle;

    public float _rotationSpeed;
    public float _moveSpeed;
    public KeyCode _keyCode;

    public Animator _playerAnimator;

    public void Awake()
    {
        _isActiveFlag = Animator.StringToHash("isActive");
        
        ShowDirection();

		_body = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        if (!_isButtonHeld) {
            UpdateDirection();
        }

        if (Input.GetKeyDown(_keyCode)) {
            _isButtonHeld = true;
        }

        if (_isButtonHeld) {
            SetPower();
            UpdateArrow();
        }

        if (Input.GetKeyUp(_keyCode)) {
            _isButtonHeld = false;
            MovePlayer();
            _power = 0;
            UpdateArrow();
            thrustParticle.Play();
        }
    }

    private void ShowDirection()
    {
        _directionAnimator.SetBool(_isActiveFlag, true);
        _arrowMaskAnimator.SetBool(_isActiveFlag, true);
        _arrowAnimator.SetBool(_isActiveFlag, true);
    }

    private void UpdateDirection()
    {
        _direction.eulerAngles = _direction.eulerAngles + new Vector3(0, 0, _rotationSpeed * -1);
    }

    private void MovePlayer()
    {
        var power = _power * _moveSpeed;

		Vector3 newForce = new Vector3(
				Mathf.Cos(Mathf.Deg2Rad * _direction.eulerAngles.z) * power,
				Mathf.Sin(Mathf.Deg2Rad * _direction.eulerAngles.z) * power,
				0
			);

		//_body.velocity += newForce;

		Debug.Log("New Force: " + newForce);
		Debug.Log("New Velocity: " + _body.velocity);

		_body.AddForce( newForce );
		//_body.AddRelativeForce( Mathf.Cos(Mathf.Deg2Rad * _direction.eulerAngles.z) * power, Mathf.Sin(Mathf.Deg2Rad * _direction.eulerAngles.z) * power, 0 );

		_playerAnimator.SetTrigger("Thrust");
    }

    private void SetPower()
    {   
        if (_power < 100) {
            _power += 5f;
        }

        if (_power > 100) {
            _power = 100;
        }
    }

    private void UpdateArrow()
    {
        var arrowMin = 1f;
        var arrowMax = 3f;

        _arrow.localPosition = new Vector3(((arrowMax - arrowMin) * (_power / 100f)) + arrowMin, _arrow.localPosition.y, _arrow.localPosition.z);
    }
}
