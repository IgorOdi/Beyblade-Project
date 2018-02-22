using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpecial : BeySpecial {

	[SerializeField]
	private GameObject aura;
	private bool canImpactAttack;

	private int baseAttack;

	public override void Start () {
		
		base.Start ();
		baseAttack = bey.atributos.attack / 2;
	}

	public override void SpecialFX () {

		base.SpecialFX ();

		aura.SetActive (true);
		bey.atributos.attack += baseAttack;
		canImpactAttack = true;
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {

		yield return new WaitForSeconds (1);
		aura.SetActive (false);
	}

	private void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade" && canImpactAttack) {

			Beyblade otherBey = other.gameObject.GetComponent<Beyblade> ();
			otherBey.actualStamina -= (bey.atributos.attack * 10) - otherBey.atributos.defense;

			float x = other.transform.position.x - transform.position.x;
			float y = other.transform.position.y - transform.position.y;
			float rot = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
			other.transform.rotation = Quaternion.Euler (0, 0, rot);

			canImpactAttack = false;
			bey.atributos.attack -= baseAttack;

			StartCoroutine (Impact (other.transform));
		}
	}

	private IEnumerator Impact(Transform _bey) {

		for (int i = 0; i < 6; i++) {

			_bey.Translate (0.05f, 0, 0);
			yield return new WaitForSeconds (0.01f);
		}
	}
}
