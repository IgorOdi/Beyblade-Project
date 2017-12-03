using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSpecial : BeySpecial {

	[SerializeField]
	private GameObject aura;

	public override void SpecialFX () {

		base.SpecialFX ();

		canSpecial = false;
		aura.SetActive (true);
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime ()
	{
		base.SpecialTime ();

		yield return new WaitForSeconds (2);

		var clone = Instantiate (gameObject, transform.position, Quaternion.identity);

		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());

		if (GetComponent<PlayerControl> () != null) {

			for (int i = 0; i < clone.transform.childCount; i++)
				if (clone.transform.GetChild(i).name.Contains("PlayerIndicator"))
					Destroy(clone.transform.GetChild(i).gameObject);

			clone.GetComponent<Beyblade> ().playerControlled = false;
			clone.GetComponent<BullSpecial> ().aura.SetActive (false);
			Destroy (clone.GetComponent<PlayerControl> ());
		}

		aura.SetActive (false);

		Destroy (clone.gameObject, 10);
	}

//	IEnumerator TempoMultiplicar() {
//
//		yield return new WaitForSeconds (2);
//
//		var clone = Instantiate (gameObject, transform.position, Quaternion.identity);
//
//		Physics2D.IgnoreCollision(GetComponent<Collider2D>(), clone.GetComponent<Collider2D>());
//
//		if (GetComponent<PlayerControl> () != null) {
//
//			for (int i = 0; i < clone.transform.childCount; i++)
//				if (clone.transform.GetChild(i).name.Contains("PlayerIndicator"))
//					Destroy(clone.transform.GetChild(i).gameObject);
//
//			clone.GetComponent<Beyblade> ().playerControlled = false;
//			clone.GetComponent<BullSpecial> ().aura.SetActive (false);
//			Destroy (clone.GetComponent<PlayerControl> ());
//		}
//
//		aura.SetActive (false);
//
//		Destroy (clone.gameObject, 15);
//	}
}
