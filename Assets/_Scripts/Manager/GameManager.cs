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

		Screen.sleepTimeout = SleepTimeout.NeverSleep;
		DontDestroyOnLoad (gameObject);

		Unlock ("Bull Slap");
		Unlock ("Bear Claws");
		Unlock ("Metalcrusher Mammoth");
//		Unlock ("Dragonfly Dawn");
//		Unlock ("Otter Dive");
//		Unlock ("Mud Crab");
//		Unlock ("Speedy Falcon");
//		Unlock ("Thunder Tiger");
//		Unlock ("Black Widow");

		StartGame ();
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