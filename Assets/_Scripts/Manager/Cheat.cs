using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour {

	public Toggle[] togglesToCheat;
	private BeySelector selector;
	public GameObject mudCrab;

	void Awake() {

		togglesToCheat = new Toggle[3];

		for (int i = 0; i < togglesToCheat.Length; i++) {
			
			togglesToCheat [i] = GetComponentsInChildren<Toggle> () [i];
			togglesToCheat [i].onValueChanged.AddListener (delegate {

				VerifyCheatEnabled ();
			});
		}
	}

	void VerifyCheatEnabled() {

		if (FindObjectOfType<BeySelector>() != null)
			selector = FindObjectOfType<BeySelector> ();

		if (togglesToCheat [0].isOn && togglesToCheat [1].isOn && togglesToCheat [2].isOn) {
			selector.beys.Add (mudCrab);
			GameManager.instance.Unlock ("Mud Crab");
		}
	}
}
