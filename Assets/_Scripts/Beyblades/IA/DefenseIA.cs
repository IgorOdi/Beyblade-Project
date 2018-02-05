using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseIA : IA {

	public override void Movement (float _speed, int _maxStamina, int _stamina) {

		base.Movement (_speed, _maxStamina, _stamina);
		_speed = _speed * (float)_stamina / _maxStamina * Time.deltaTime;

		float distance = Vector2.Distance (transform.position, center);

		if (distance > _stamina / 100) {

			transform.position = Vector2.MoveTowards (transform.position, center, _speed);
			gameObject.layer = 9;
		} else {

			gameObject.layer = 8;
		}
	}
}
