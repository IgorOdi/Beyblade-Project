using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MammothSpecial : BeySpecial {

	public override void SpecialFX () {

		base.SpecialFX ();

		bey.transform.localScale = new Vector3 (1.5f,1.5f,1);
		bey.actualStamina += 50;

		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {

		yield return new WaitForSeconds (6);
		bey.transform.localScale = Vector3.one;


	}

}
