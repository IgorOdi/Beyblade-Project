using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour {

	public Toggle[] togglesToCheat;
	[SerializeField]
	private BeySelector selector;

	void Awake() {

		togglesToCheat = new Toggle[3];

		for (int i = 0; i < togglesToCheat.Length; i++) {
			
			togglesToCheat [i] = GetComponentsInChildren<Toggle> () [i];
			togglesToCheat [i].onValueChanged.AddListener (delegate { VerifyCheatEnabled (); });
		}
	}

	void VerifyCheatEnabled() {

		if (togglesToCheat [0].isOn && togglesToCheat [1].isOn && togglesToCheat [2].isOn)
			GameManager.Unlock ("Mud Crab");
	}
}
