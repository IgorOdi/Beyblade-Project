using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSpecial : BeySpecial {

	[SerializeField]
	private GameObject fire;

	public override void SpecialFX () {

		base.SpecialFX ();

		canSpecial = false;
		fire.SetActive (true);
		StartCoroutine (TempoFire());
	}

	IEnumerator TempoFire() {

		yield return new WaitForSeconds (4);
		fire.SetActive (false);
	}
}
