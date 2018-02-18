using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackIA : IA {

	private float timeCounter, searchCounter, limitTimer;
	private int hits;
	private GameObject target;
	private Vector2 randomTarget;
	[SerializeField]
	private List<Beyblade> beybladeList;

	void Start() {

		beybladeList = new List<Beyblade> (BeyManager.instance.inGameBeys.Count);

		if (GameManager.matchMode.singleplayerMode == SingleplayerMode.DuoMode) {

			if (TeamManager.instance.Check (gameObject, TeamManager.instance.team [0])) {

				foreach (GameObject bey in TeamManager.instance.team[0].beys) {

					Beyblade elegibleBey = bey.GetComponent<Beyblade> ();
					beybladeList.Add (elegibleBey);
				}
			} else {

				foreach (GameObject bey in TeamManager.instance.team[1].beys) {

					Beyblade elegibleBey = bey.GetComponent<Beyblade> ();
					beybladeList.Add (elegibleBey);
				}
			}
		} else {

			foreach (GameObject bey in BeyManager.instance.inGameBeys) {

				if (bey.name != gameObject.name) {

					Beyblade elegibleBey = bey.GetComponent<Beyblade> ();
					beybladeList.Add (elegibleBey);
				}
			}
		}

		limitTimer = Random.Range (2, 4);
		randomTarget = Random.insideUnitCircle * 4;
	}

	void Update() {

		searchCounter += Time.deltaTime;

		if (searchCounter > limitTimer && searchCounter < limitTimer + 2) {
				
			target = null;
		} else if (searchCounter > limitTimer + 2) {

			randomTarget = Random.insideUnitCircle * 4;
			target = SetTarget ();
			searchCounter = 0;
		}

		for (int i = 0; i < beybladeList.Count; i++)
			if (beybladeList [i].actualStamina <= 10)
				beybladeList.Remove (beybladeList [i]);
	}

	public GameObject SetTarget() {
		
		limitTimer = Random.Range (2, 4);

		if (beybladeList.Count > 0)
			return beybladeList [Random.Range (0, beybladeList.Count)].gameObject;
		
		return null;
	}

	public override void Movement (float _speed, int _maxStamina, int _stamina) {

		base.Movement (_speed, _maxStamina, _stamina);

		if (target == null) {

			_speed = _speed * (float)_stamina / _maxStamina * Time.deltaTime;
			transform.position = Vector2.MoveTowards (transform.position, randomTarget, Time.deltaTime * _speed);
		} else {

			float y = target.transform.position.y - transform.position.y;
			float x = target.transform.position.x - transform.position.x;

			float rot = (Mathf.Atan2 (y, x) * Mathf.Rad2Deg);

			transform.rotation = Quaternion.Euler (0, 0, rot);
			transform.Translate(new Vector2(Time.deltaTime * _speed, 0));
		}
	}
}