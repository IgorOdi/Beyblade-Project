using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabSpecial : BeySpecial {

	[SerializeField]
	private GameObject attractor;

	public override void SpecialFX () {
		
		base.SpecialFX ();

		attractor.SetActive (true);
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {
		
		yield return new WaitForSeconds (3);
		attractor.SetActive (false);
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (attractor.activeSelf) {

			Beyblade otherBey = other.gameObject.GetComponent<Beyblade> ();

			int random = Random.Range (0, 2);

			switch (otherBey.type) {

			case Attributes.Type.Attack:

				bey.atributos.attack += 1;
				break;

			case Attributes.Type.Defense:

				bey.atributos.defense += 1;
				break;

			case Attributes.Type.Stamina:

				bey.atributos.stamina += 1;
				break;

			case Attributes.Type.AtkDef:

				if (random == 0) bey.atributos.attack += 1;
				else bey.atributos.defense += 1;
				break;

			case Attributes.Type.AtkSta:
				
				if (random == 0) bey.atributos.attack += 1;
				else bey.atributos.stamina += 1;
				break;

			case Attributes.Type.DefSta:

				if (random == 0) bey.atributos.defense += 1;
				else bey.atributos.stamina += 1;
				break;
			}

			attractor.SetActive (false);
			canSpecial = false;
			StopCoroutine ("SpecialTime");
		}
	}
}
