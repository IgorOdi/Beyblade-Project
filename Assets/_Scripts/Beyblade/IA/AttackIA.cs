using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIA : IA {

	private float timeCounter, searchCounter;
	private float limitTimer;
	private int passageCounter;
	private GameObject target;
	private Vector2 randomTarget;
	[SerializeField]
	private GameObject[] beybladeList;

	void Start() {

		beybladeList = new GameObject[GameObject.FindGameObjectsWithTag ("Beyblade").Length];

		for (int i = 0; i < beybladeList.Length; i++)
			beybladeList[i] = GameObject.FindGameObjectsWithTag("Beyblade")[i];

		randomTarget = SetRandomTarget ();
		limitTimer = 4;
	}

	void Update() {

		searchCounter += Time.deltaTime;

		if (searchCounter > 2 && searchCounter < limitTimer) {
		
			target = null;
		} else if (searchCounter > limitTimer) {

			randomTarget = SetRandomTarget ();
			target = SetTarget ();
			searchCounter = 0;
		}
	}

	public Vector2 SetRandomTarget() {

		int randomizador = Random.Range (0, 6);

		if (randomizador >= 1) {

			limitTimer = 10;
			return new Vector2 (4, 0);
		} else {

			return new Vector2 ((int)Random.Range (-4, 4), (int)Random.Range (-4, 4));
		}
	}

	public GameObject SetTarget() {

		GameObject targ = beybladeList[Random.Range(1, beybladeList.Length)];

		if (targ.gameObject.GetComponent<Beyblade> ().type == Attributes.Type.Defense) {

			int randomizador = Random.Range (0, 6);

			if (randomizador < 2)
				return targ;
			else {

				targ = beybladeList [Random.Range (1, beybladeList.Length)];
				return targ;
			}
		} else
			return targ;
	}

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		if (target == null) {

			transform.position = Vector2.Lerp (transform.position, randomTarget, Time.deltaTime);

			if (transform.position == new Vector3 ((int)4, (int)0, 0)) {

				timeCounter += Time.deltaTime * (_speed * ((float)_stamina / _maxStamina));

				float x = Mathf.Cos (timeCounter) * 4;
				float y = Mathf.Sin (timeCounter) * 4;

				transform.position = new Vector2 (x, y);
			}

		} else {

			float y = target.transform.position.y - transform.position.y;
			float x = target.transform.position.x - transform.position.x;

			float rot = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (0, 0, rot);
			transform.Translate(new Vector2(0.15f, 0));
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade")
			randomTarget = SetRandomTarget ();
	}
}
