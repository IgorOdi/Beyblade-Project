using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : IA {

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		rb.velocity = new Vector2 (Input.GetAxis("Horizontal") * _speed, Input.GetAxis("Vertical") * _speed);
	}

	public void Move (float _speed) {

		transform.Translate (Input.acceleration.x * _speed, Input.acceleration.y * _speed, 0);
	}
}
