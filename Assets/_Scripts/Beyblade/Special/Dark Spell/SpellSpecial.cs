using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSpecial : BeySpecial {

	[SerializeField]
	private GameObject[] onionSkin;
	private bool isRewinding = false;
	private List<RewindInfo> info;

	public override void Start() {

		base.Start ();
		info = new List<RewindInfo> ();
	}
		
	public override void SpecialFX () {

		isRewinding = true;

		for (int i = 0; i<onionSkin.Length; i++)
			onionSkin[i].SetActive (true);
	}

	void FixedUpdate() {

		if (isRewinding) StartCoroutine (TempoRewinding ());
		else Record ();
	}

	void Record() {

		if (info.Count > (Mathf.Round (3f / Time.fixedDeltaTime)) - 1)
			info.RemoveAt (info.Count - 1);

		RewindInfo rewindInfo = new RewindInfo (transform.position, bey.actualStamina);
		info.Insert (0, rewindInfo);
	}


	IEnumerator TempoRewinding() {

		while (info.Count > 0 && isRewinding) {

			RewindInfo rewindInfo = info [0];
			transform.position = rewindInfo.position;
			bey.actualStamina = rewindInfo.stamina;
			info.RemoveAt (0);

			for (int i = 0; i < onionSkin.Length; i++) {
				yield return new WaitForSeconds (0.025f);
				onionSkin [i].transform.position = rewindInfo.position;
			}

			yield return new WaitForSeconds (0.5f);
		}
			
		isRewinding = false;
		canSpecial = false;

		for (int i = 0; i<onionSkin.Length; i++)
			onionSkin[i].SetActive (false);

		yield return null;
	}
}