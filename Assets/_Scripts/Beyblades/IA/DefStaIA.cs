using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefStaIA : IA {

	private float timeCounter;
	private float distance;

	public override void Movement (float _speed, int _maxStamina, int _stamina) {
		
		base.Movement (_speed, _maxStamina, _stamina);

		gameObject.layer = distance > _stamina / 100 ? 9 : 8;

		distance = Vector2.Distance (transform.position, center);

		_speed = _speed / 1.5f;

		timeCounter += Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));

		float x = Mathf.Cos (timeCounter) * distance;
		float y = Mathf.Sin (timeCounter) * distance;

		transform.position = new Vector2 (x, y);
	}
}