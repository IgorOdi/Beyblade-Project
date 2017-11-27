using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyManager : MonoBehaviour {

	public static string SelectedBey;
	public List<GameObject> inGameBeys;
	public GameObject indicator;

	void Awake() {

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

			float x = Random.Range (-3, 3);
			float y = Random.Range (-3, 3);
			Vector2 spawnPoint = new Vector2 (x, y);

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

		StartCoroutine(GetComponent<SpecialSpawn> ().TempoSpawn ());
	}

	public void RemoveInGameBey(GameObject _beyToRemove) {

		inGameBeys.Remove (_beyToRemove);

		if (inGameBeys.Count <= 1)	FindObjectOfType<EndGame> ().Finish ("Vitória");
	}
}
