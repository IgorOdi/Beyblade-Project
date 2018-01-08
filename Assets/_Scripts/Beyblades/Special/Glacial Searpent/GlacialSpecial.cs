using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlacialSpecial : BeySpecial {

	public List<Beyblade> beyList = new List<Beyblade>();

	public override void SpecialFX () {
		
		base.SpecialFX ();

		GetBeyblades ();
		ChangeBeySpeed (beyList, true);
		StartCoroutine (SpecialTime ());
	}

	void GetBeyblades() {

		for (int i = 0; i < BeyManager.instance.inGameBeys.Count; i++)
			beyList.Add(BeyManager.instance.inGameBeys [i].GetComponent<Beyblade> ());
	}

	void ChangeBeySpeed(List<Beyblade> _b, bool slow) {

		for (int i = 0; i < _b.Count; i++) {

			if (_b [i].gameObject.name != "Glacial Searpent") {
				if (slow)
					_b [i].actualSpeed /= 10;
				else
					_b [i].actualSpeed /= 0.1f;
			} else {

				if (slow)
					_b [i].atributos.attack *= 6;
				else
					_b [i].atributos.attack /= 6;
			}
		}

		beyList.Clear ();
	}
		
	public override IEnumerator SpecialTime () {
		
		yield return new WaitForSeconds (4);
		GetBeyblades ();
		ChangeBeySpeed (beyList, false);
	}
}
