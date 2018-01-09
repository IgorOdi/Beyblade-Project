using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSpecial : BeySpecial {

	[SerializeField]
	private Transform[] local; 

	[SerializeField]
	private GameObject espinho;

	public override void SpecialFX () {

		base.SpecialFX ();

		for(int i = 0; i < local.Length; i++){

			Instantiate (espinho, transform.position, local [i].rotation);

		}

	}

}
