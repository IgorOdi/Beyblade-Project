using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpecial : BeySpecial {

	[SerializeField]
	private GameObject aura;

	public override void SpecialFX () {

		canSpecial = false;
		aura.SetActive (true);
		StartCoroutine (TempoMultiplicar ());
	}

	IEnumerator TempoMultiplicar() {

		yield return new WaitForSeconds (2);

		var clone = Instantiate (gameObject, transform.position, Quaternion.identity);

		if (GetComponent<PlayerControl> () != null) {

			clone.GetComponent<Beyblade> ().playerControlled = false;
			clone.GetComponent<BullSpecial> ().aura.SetActive (false);
			Destroy (clone.GetComponent<PlayerControl> ());
		}

		aura.SetActive (false);

		Destroy (clone.gameObject, 15);
	}
}
