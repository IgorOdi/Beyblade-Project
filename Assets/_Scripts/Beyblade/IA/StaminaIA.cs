using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaIA : IA {

	private Vector2 center = new Vector2 (0, 0);
	private float timeCounter;

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		float distance = 0;
		distance = Vector2.Distance (transform.position, center);

		timeCounter += Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));

		float x = Mathf.Cos (timeCounter) * distance;
		float y = Mathf.Sin (timeCounter) * distance;

		transform.position = new Vector2 (x, y);
	}
}
