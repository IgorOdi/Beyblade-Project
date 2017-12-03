using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour {

	public GameObject[] screen;

	public void BackFromSelectScreen() {

		screen [0].SetActive (false);
		screen [1].SetActive (true);
		screen [2].SetActive (true);
	}
}