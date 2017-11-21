using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeybladeManager : MonoBehaviour {

	public List<GameObject> Beyblades;

	void Start() {

		foreach (GameObject bey in Beyblades) {

			print (bey);
		}
	}
}
