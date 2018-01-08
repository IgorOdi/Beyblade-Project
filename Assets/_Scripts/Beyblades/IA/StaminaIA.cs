using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaIA : IA {

	private float timeCounter;
	float distance;

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		base.Movimento (_speed, _maxStamina, _stamina);

		distance = Vector2.Distance (transform.position, center);

		_speed = _speed / 1.5f;

		timeCounter += Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));

		float x = Mathf.Cos (timeCounter) * distance;
		float y = Mathf.Sin (timeCounter) * distance;

		transform.position = new Vector2 (x, y);
	}
}
