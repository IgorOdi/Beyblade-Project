using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkStaIA : IA {

	private float timeCounter, distance;
	private int sideMultiplier = 1;
	private int hitCounter;

	void Start() {

		StartCoroutine (ChangeDirection ());
		distance = Vector2.Distance (transform.position, center);
	}

	public override void Movement (float _speed, int _maxStamina, int _stamina) {

		base.Movement (_speed, _maxStamina, _stamina);

		_speed = _speed / 1.5f;

		if (sideMultiplier == 1) {

			distance = Mathf.Lerp (distance, 1, Time.deltaTime * _speed);
			timeCounter += Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));
		} else {
			
			distance = Mathf.Lerp (distance, 4, Time.deltaTime * _speed);
			timeCounter -= Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));
		}

		float x = Mathf.Cos (timeCounter) * distance;
		float y = Mathf.Sin (timeCounter) * distance;

		transform.position = new Vector2 (x, y);
	}

	IEnumerator ChangeDirection() {

		yield return new WaitForSeconds (6);
		sideMultiplier *= -1;
		StartCoroutine (ChangeDirection ());
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade") {

			hitCounter++;

			if (hitCounter >= 4) {
				sideMultiplier *= -1;
				hitCounter = 0;
			}
		}
	}
}