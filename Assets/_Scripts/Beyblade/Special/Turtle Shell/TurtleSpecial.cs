using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpecial : BeySpecial {

	[SerializeField]
	private GameObject shield;

	public override void SpecialFX () {

		base.SpecialFX ();
		shield.SetActive (true);
		gameObject.tag = "Shield";
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime ()
	{
		base.SpecialTime ();

		yield return new WaitForSeconds (5);
		gameObject.tag = "Beyblade";
		shield.SetActive (false);
	}
		
}
