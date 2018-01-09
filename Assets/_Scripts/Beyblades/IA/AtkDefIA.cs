using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkDefIA : IA {

	private float timeCounter, searchCounter, limitTimer;
	private int hits;
	private GameObject target;
	private Vector2 randomTarget;
	private List<Beyblade> beybladeList;
	private Vector2 point;

	void Start() {

		BeyManager manager = FindObjectOfType<BeyManager> ();

		beybladeList = new List<Beyblade> (manager.inGameBeys.Count);

		foreach (GameObject bey in manager.inGameBeys) {

			if (bey.name != gameObject.name) {
				Beyblade elegibleBey = bey.GetComponent<Beyblade> ();
				beybladeList.Add (elegibleBey);
			}
		}

		limitTimer = Random.Range (4, 8);
		randomTarget = SetRandomTarget ();
	}

	void Update() {

		searchCounter += Time.deltaTime;

		if (searchCounter > limitTimer && searchCounter < limitTimer + 2) {

			target = null;
		} else if (searchCounter > limitTimer + 2) {

			randomTarget = SetRandomTarget ();
			target = SetTarget ();
			searchCounter = 0;
		}

		for (int i = 0; i < beybladeList.Count; i++)
			if (beybladeList [i].actualStamina <= 10)
				beybladeList.Remove (beybladeList [i]);
	}

	public Vector2 SetRandomTarget() {

		float posX = Random.Range (-4, 4);
		float posY = Random.Range (-4, 4);
		Vector2 targetPos = new Vector2 (posX, posY);

		return targetPos;
	}

	public GameObject SetTarget() {

		if (beybladeList.Count > 0) {

			Beyblade targ = beybladeList [Random.Range (0, beybladeList.Count)];
			limitTimer = Random.Range (8, 12);

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
		} else
			return null;
	}

	public override void Movimento (float _speed, int _maxStamina, int _stamina) {

		base.Movimento (_speed, _maxStamina, _stamina);

		if (target == null) {

			transform.position = Vector2.Lerp (transform.position, randomTarget, Time.deltaTime * _speed);
		} else {

			float y = target.transform.position.y - transform.position.y;
			float x = target.transform.position.x - transform.position.x;

			float rot = (Mathf.Atan2 (y, x) * Mathf.Rad2Deg);

			transform.rotation = Quaternion.Euler (0, 0, rot);
			transform.Translate(new Vector2(Time.deltaTime * _speed, 0));
		}
	}
}