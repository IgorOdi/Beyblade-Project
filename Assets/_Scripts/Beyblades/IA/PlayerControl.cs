using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : IA {

	void Update() {

		if (Input.GetMouseButtonDown(0) && beySpecial.canSpecial) beySpecial.SpecialFX ();
	}

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		_speed = _speed * 1.5f;
		rb.velocity = new Vector2 (Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
	}

//	public override void Movimento (float _speed, int _maxStamina, int _stamina) {
//
//		_speed = _speed * 3f;
//		rb.velocity = new Vector2 (Input.acceleration.x * _speed, Input.acceleration.y * _speed);
//	}
}