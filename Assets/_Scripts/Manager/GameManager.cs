using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public static List<GameObject> beyblades;
	public static List<GameObject> unlockedBeys;
	public static List<string> unlockList = new List<string> {"Dragon Fury", "Dark Spell", "Bull Slap", "Turtle Shell"};

	void Awake() {

		if (instance == null)
			instance = this;

		LoadBeybladeList ();
		DontDestroyOnLoad (gameObject);
	}

	void LoadBeybladeList() {

		beyblades = new List<GameObject> ();

		Object[] beys = Resources.LoadAll ("Beyblades", typeof(GameObject));
		foreach (GameObject bey in beys) {

			beyblades.Add (bey);
		}

		UnlockBeyblades ();
	}

	void UnlockBeyblades() {

		unlockedBeys = new List<GameObject> ();

		foreach (GameObject bey in GameManager.beyblades) {

			for (int i = 0; i < unlockList.Count; i++) {

				if (bey.name == unlockList [i]) {

					unlockedBeys.Add (bey);
				}
			}

		}

		StartGame ();
	}

	public void Unlock(string _beyName) {

		foreach (GameObject bey in GameManager.beyblades) {

			if (bey.name == _beyName) {

				unlockedBeys.Add (bey);
			}
		}
	}

	void StartGame() {

		SceneManager.LoadScene ("Menu");
	}
}