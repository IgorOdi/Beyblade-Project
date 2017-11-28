using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyManager : MonoBehaviour {

	public static string SelectedBey;
	public List<GameObject> inGameBeys;
	public GameObject indicator;
	public HUDManager hudManager;

	void Awake() {

		hudManager = GetComponent<HUDManager> ();
		DefinePlayerBey ();
	}

	void DefinePlayerBey() {

		foreach (GameObject bey in GameManager.unlockedBeys) {

			if (bey.name == SelectedBey)
				inGameBeys.Add (bey);
		}
			
		List<int> validChoices = new List<int> ();

		for (int i = 0; i < GameManager.unlockedBeys.Count; i++) {
			validChoices.Add (i);
		}

		while (inGameBeys.Count < 4) {
			
			int randomizador = validChoices[Random.Range(0, validChoices.Count)];

			if (GameManager.unlockedBeys[randomizador].name != SelectedBey)
				inGameBeys.Add(GameManager.unlockedBeys[randomizador]);

			validChoices.Remove (randomizador);
		}
			
		ActiveBeys ();
	}

	void ActiveBeys() {

		for (int i = 0; i < inGameBeys.Count; i++) {

			Vector2 spawnPoint;

			if (inGameBeys [i].GetComponent<Beyblade> ().type == Attributes.Type.Attack) {
				
				spawnPoint = RandomPosition (-3, 3, false, 0);
			} else if (inGameBeys [i].GetComponent<Beyblade> ().type == Attributes.Type.Defense) {

				spawnPoint = RandomPosition (-1.5f, 1.5f, false, 0);
			} else {

				spawnPoint = RandomPosition (-3, 3, true, 1.5f);
			}

			GameObject spawnedBey = Instantiate (inGameBeys[i], spawnPoint, Quaternion.identity);
			string beyName = spawnedBey.name;
			beyName = beyName.Replace ("(Clone)", "");
			inGameBeys.RemoveAt (i);
			inGameBeys.Insert (i, spawnedBey);

			if (beyName == SelectedBey) {

				spawnedBey.GetComponent<Beyblade> ().playerControlled = true;
				indicator.transform.SetParent (spawnedBey.transform, false);
			}
		}
			
		hudManager.DefineBeys ();
		GetComponent<SpecialSpawn> ().Spawn ();
	}

	public Vector2 RandomPosition(float x, float y, bool reRoll, float limit) {

		float _x = Random.Range(-x, x);
		float _y = Random.Range (-y, y);

		if ((reRoll) && (x < limit && x > -limit) || (y < limit && y > -limit)) {

			return RandomPosition (-x, -y, reRoll, limit);
		} else {

			return new Vector2 (_x, _y);
		}
	}

	public void RemoveInGameBey(GameObject _beyToRemove) {

		inGameBeys.Remove (_beyToRemove);

		if (inGameBeys.Count <= 1)	FindObjectOfType<EndGame> ().Finish ("Vitória");
	}
}
