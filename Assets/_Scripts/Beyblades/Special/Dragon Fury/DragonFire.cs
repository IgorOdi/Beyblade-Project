using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFire : MonoBehaviour {

	float damageTimer;

	void OnEnable() {

		damageTimer = 0;
	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.tag == "Beyblade") {

			var beyblade = other.gameObject.GetComponent<Beyblade> ();

			damageTimer += Time.deltaTime;

			if (damageTimer > 1) {
				
				beyblade.actualStamina -= 25;
				damageTimer = 0;
			}
		}
	}
}
