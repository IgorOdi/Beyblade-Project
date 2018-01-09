using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonflySpecial : BeySpecial {

	public override void SpecialFX () {

		base.SpecialFX ();

		bey.actualSpeed += 4; 
		bey.gameObject.tag = "Shield";

		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {
		
		yield return new WaitForSeconds (4);

		bey.actualSpeed -= 4;
		bey.gameObject.tag = "Beyblade";

	}

}