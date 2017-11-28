using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : MonoBehaviour {

	public SpecialSpawn spawn;

	public IEnumerator Spawnou() {

		yield return new WaitForSeconds (2);
		GetComponent<Collider2D> ().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.tag == "Beyblade") {

			other.GetComponent<BeySpecial> ().canSpecial = true;
			GetComponent<Collider2D> ().enabled = false;
			transform.position = spawn.transform.position;
			spawn.Spawn ();
			gameObject.SetActive (false);
		}
	}
}
