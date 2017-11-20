using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IA : MonoBehaviour {

	public Rigidbody2D rb;
	public Animator beyAnimator;

	void Awake() {

		rb = GetComponentInChildren<Rigidbody2D> ();
		beyAnimator = GetComponentInChildren<Animator> ();
	}

	public virtual void Movimento(float _speed, int _maxStamina, int _stamina) {

	}

	public void RotationSpeed(float _rotSpeed, float _maxSpeed) {

		beyAnimator.speed = _rotSpeed/_maxSpeed;
	}
}
