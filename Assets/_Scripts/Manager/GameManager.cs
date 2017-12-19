using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	public List<GameObject> beyblades;
	public static List<string> unlockList = new List<string> {"Dragon Fury", "Turtle Shell", "Glacial Searpent"};
	public static MatchMode matchMode;

	void Awake() {

		if (instance == null)
			instance = this;

		StartGame ();
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad (gameObject);
	}

	public static void Unlock(string _beyName) {

		foreach (GameObject bey in GameManager.instance.beyblades)
			if (bey.name == _beyName)
				unlockList.Add (bey.name);
	}

	void StartGame() {

		SceneManager.LoadScene ("Menu");
	}
}