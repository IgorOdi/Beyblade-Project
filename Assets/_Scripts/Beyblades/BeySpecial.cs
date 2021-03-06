﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BeySpecial : MonoBehaviour {

	protected Beyblade bey;
	protected IA ia;
	public bool canSpecial;

	public virtual void Start() {

		bey = GetComponent<Beyblade> ();
		ia = GetComponent<IA> ();
	}

	public virtual void SpecialFX() {

		GetComponentInChildren<BeySoundManager> ().PlaySpecialSound ();
		canSpecial = false;
	}

	public virtual IEnumerator SpecialTime() {

		yield return null;
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "PowerUP")
			canSpecial = true;
	}
}