using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWidowSpecial : BeySpecial {

	private int contador = 0;
	private bool superataque;
	private bool acabou;


	public override void SpecialFX () {

		base.SpecialFX ();

		contador++;
		superataque = false;


		if (contador < 4) {
			
			bey.actualSpeed += 2; 
			bey.atributos.attack += 2;

		}

		if (contador % 3 == 0)
			superataque = true;
		
	

		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {

		yield return new WaitForSeconds (4);

		superataque = false;

	}


	void OnCollisionEnter2D(Collision2D hit) {

		if (hit.gameObject.tag == "Beyblade" && superataque) {

			hit.gameObject.GetComponent<Beyblade>().actualStamina = 0;

			superataque = false;
		}
	}


}
