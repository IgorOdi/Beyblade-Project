using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIA : IA {

	private float timeCounter, searchCounter, limitTimer;
	private GameObject target;
	private Vector2 randomTarget;
	private List<Beyblade> beybladeList;

	void Start() {

		beybladeList = new List<Beyblade> (GameObject.FindGameObjectsWithTag("Beyblade").Length);

		foreach (GameObject bey in GameObject.FindGameObjectsWithTag("Beyblade")) {

			if (bey.name != gameObject.name) {
				Beyblade elegibleBey = bey.GetComponent<Beyblade> ();
				beybladeList.Add (elegibleBey);
			}
		}

		limitTimer = 2.5f;
		randomTarget = SetRandomTarget ();
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

		for (int i = 0; i < beybladeList.Count; i++)
			if (beybladeList [i].actualStamina <= 10)
				beybladeList.Remove (beybladeList [i]);
	}

	public Vector2 SetRandomTarget() {

		float posX = Random.Range (1, 4);
		float posY = Random.Range (1, 4);
		Vector2 targetPos = new Vector2 (posX, posY);

		return targetPos;
	}

	public GameObject SetTarget() {

		Beyblade targ = beybladeList [Random.Range (0, beybladeList.Count)];

		if (targ.type == Attributes.Type.Defense) {

			int randomizador = Random.Range (0, 6);

			if (randomizador < 2)
				return targ.gameObject;
			else {
				
				targ = beybladeList [Random.Range (0, beybladeList.Count)];
				return targ.gameObject;
			}
		} else {

			if (targ.type == Attributes.Type.Attack) {

				int randomizador = Random.Range (0, 6);

				if (randomizador < 2)
					return targ.gameObject;
				else {

					targ = beybladeList [Random.Range (0, beybladeList.Count)];
					return targ.gameObject;
				}
			} else {

				return targ.gameObject;
			}
		}
	}

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		if (target == null) {

			transform.position = Vector2.Lerp (transform.position, randomTarget, Time.deltaTime);
		} else {

			float y = target.transform.position.y - transform.position.y;
			float x = target.transform.position.x - transform.position.x;

			float rot = (Mathf.Atan2 (y, x) * Mathf.Rad2Deg);

			transform.rotation = Quaternion.Euler (0, 0, rot);
			transform.Translate(new Vector2(0.05f * _speed, 0));
		}
	}

	void OnCollsionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade") {

			target = null;
			searchCounter = 0;
		}
	}
}