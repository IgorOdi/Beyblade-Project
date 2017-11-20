using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseIA : IA {

	private Vector3 startPosition;
	private Vector3 center;
	private float timer;

	void Start() {

		center = new Vector2 (0, 0);
	}

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		float distance = Vector2.Distance (transform.position, center);

		if (distance > 0.01f * _stamina) {

			transform.position = Vector2.Lerp (transform.position, center, 2 * Time.deltaTime);
			gameObject.layer = 9;
		} else {

			gameObject.layer = 8;
		}
	}
}
