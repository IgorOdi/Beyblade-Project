using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour {

	BeyCollision beyCollision;

	void Awake() {

		beyCollision = GetComponentInParent<BeyCollision> ();
	}

	public void DeactiveSpark() {

		beyCollision.DeactivateSpark ();
	}
}
