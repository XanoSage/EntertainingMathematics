using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controll : MonoBehaviour
{
	[SerializeField]
	private Transform _target;

	[SerializeField]
	private float _maxSpeed = 2f;

	[SerializeField]
	private float _accelerate = 2.25f;

	private Vector3 _moveDirection = Vector3.zero;
	private Vector3 _prevDirection = Vector3.zero;
	private Vector3 _nextPosition = Vector3.zero;

	private float _currentSpeed = 0f;

	private bool _anyMotion = false;

	// Use this for initialization
	void Start ()
	{
		if (_target == null)
			_target = transform;	
	}
	
	// Update is called once per frame
	void Update ()
	{
		DetectMotion();
		UpdateSpeed();
		UpdateMotion();
	}

	private void MoveToLeft()
	{
		_moveDirection += Vector3.left;
		_anyMotion = true;
	}

	private void MoveToRight()
	{
		_moveDirection += Vector3.right;
		_anyMotion = true;
	}

	private void MoveToUp()
	{
		_moveDirection += Vector3.up;
		_anyMotion = true;
	}

	private void MoveToDown()
	{
		_moveDirection += Vector3.down;
		_anyMotion = true;
	}

	private void UpdateMotion()
	{
		_nextPosition = _target.position + _currentSpeed * _moveDirection;
		_target.position = Vector3.Lerp(_target.position, _nextPosition, Time.deltaTime);
	}

	private void DetectMotion()
	{
		_anyMotion = false;
		_moveDirection = Vector3.zero;

		if (Input.GetKey(KeyCode.D))
		{
			MoveToRight();
		}

		if (Input.GetKey(KeyCode.A))
		{
			MoveToLeft();
		}

		if (Input.GetKey(KeyCode.W))
		{
			MoveToUp();
		}

		if (Input.GetKey(KeyCode.S))
		{
			MoveToDown();
		}

		if (_moveDirection.Equals(Vector3.zero))
			_moveDirection = _prevDirection;
		else
			_prevDirection = _moveDirection;
	}

	private void UpdateSpeed()
	{
		if (_anyMotion)
		{
			if (_currentSpeed < _maxSpeed)
				_currentSpeed +=  _accelerate * Time.deltaTime;
			else if (_currentSpeed >= _maxSpeed)
				_currentSpeed = _maxSpeed;
		}
		else
		{
			if (_currentSpeed > 0f)
				_currentSpeed -= _accelerate * Time.deltaTime;
			else if (_currentSpeed <= 0f)
				_currentSpeed = 0f;
		}
	}
}
