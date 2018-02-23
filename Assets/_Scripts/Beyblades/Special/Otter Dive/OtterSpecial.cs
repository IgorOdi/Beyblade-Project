using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtterSpecial : BeySpecial {

	[SerializeField]
	private SpriteRenderer sr;

	private const float halfAlpha = 0.5f;
	private const float fullAlpha = 1;
	private const int beyLayer = 8;
	private const int explosionDamage = 30;

	public override void SpecialFX () {
		
		base.SpecialFX ();

		sr.color = ChangeAlpha (halfAlpha);
		Physics2D.IgnoreLayerCollision (beyLayer, beyLayer, true);
		StartCoroutine (SpecialTime ());
	}

	public override IEnumerator SpecialTime () {
		
		yield return new WaitForSeconds (3);
		sr.color = ChangeAlpha (fullAlpha);
		Physics2D.IgnoreLayerCollision (beyLayer, beyLayer, false);
		CallExplosion ();
	}

	private Color ChangeAlpha(float _alpha) {

		Vector4 color = sr.color;
		color.w = _alpha;

		return color;
	}

	private void CallExplosion() {

		Collider2D[] beys = Physics2D.OverlapCircleAll (transform.position, 3, LayerMask.GetMask("Beyblade"));

		for (int i = 0; i < beys.Length; i++) {

			Beyblade _bey = beys [i].gameObject.GetComponent<Beyblade> ();
			_bey.actualStamina -= explosionDamage;
		}
	}
}