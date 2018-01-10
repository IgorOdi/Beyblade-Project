using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSpecial : BeySpecial {

	public override void SpecialFX () {

		base.SpecialFX ();

		float custoDeVida = (float)bey.actualStamina * 0.2f;

		bey.actualStamina -= (int)custoDeVida;

		bey.atributos.attack *= 2;

		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {

		yield return new WaitForSeconds (4);

		bey.atributos.attack /= 2;

	}
}