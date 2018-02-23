using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleSpecial : BeySpecial {

	[SerializeField]
	private GameObject shield;

	private int baseDef;

	private int ExplosionDamage {

		get {

			return bey.atributos.defense * 3;
		}
	}

	public override void Start () {
		
		base.Start ();
		baseDef = bey.atributos.defense;
	}

	public override void SpecialFX () {

		base.SpecialFX ();
		shield.SetActive (true);

		bey.actualSpeed += 2;
		bey.atributos.defense += 2;
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime ()
	{
		base.SpecialTime ();

		yield return new WaitForSeconds (3);

		bey.actualSpeed = bey.atributos.speed;
		bey.atributos.defense = baseDef;
		CallExplosion ();
		shield.SetActive (false);
	}

	private void CallExplosion() {

		Collider2D[] beys = Physics2D.OverlapCircleAll (transform.position, 3, LayerMask.GetMask("Beyblade"));

		for (int i = 0; i < beys.Length; i++) {

			Beyblade _bey = beys [i].gameObject.GetComponent<Beyblade> ();
			if (_bey != this.bey) _bey.actualStamina -= ExplosionDamage;
		}
	}
		
}
