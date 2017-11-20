using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyCollision : MonoBehaviour {

	public Rigidbody2D rb;

	public float timer;

	public class Combat {

		public Beyblade attacker, defender;
		public float beyVel, otherBeyVel;

		public Combat(GameObject bey, GameObject otherBey) {

			beyVel = bey.GetComponent<Rigidbody2D>().velocity.magnitude;
			otherBeyVel = otherBey.GetComponent<Rigidbody2D>().velocity.magnitude;

			if (beyVel > otherBeyVel) {

				attacker = bey.GetComponent<Beyblade>();
				defender = otherBey.GetComponent<Beyblade>();
			} else {

				attacker = otherBey.GetComponent<Beyblade>();
				defender = bey.GetComponent<Beyblade>();
			}
		}
	}

	void Start() {

		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade") {

			Combat combat = new Combat (gameObject, other.gameObject);
			combat.attacker.actualStamina -= 2;
			combat.defender.actualStamina -= Damage (combat.attacker.atributos.attack, combat.defender.atributos.defense);

			float x = other.transform.position.x - transform.position.x;
			float y = other.transform.position.y - transform.position.y;

			float rot = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (0, 0, rot);

			float attackerMultiplier = 10;
			float defenderMultiplier = 10;

			if (combat.defender.type == Attributes.Type.Defense)
				defenderMultiplier /= 10;

			float defenderImpact = (Random.Range (0.1f, 0.4f) / combat.defender.GetComponent<Rigidbody2D>().mass) * defenderMultiplier;
			float attackerImpact = (Random.Range (0.1f, 0.4f) / combat.attacker.GetComponent<Rigidbody2D> ().mass) * attackerMultiplier;

			print("Attacker: " + attackerImpact);
			print ("Defender: " + defenderImpact);

			combat.attacker.transform.Translate(new Vector2(-attackerImpact, 0));
			combat.defender.transform.Translate(new Vector2(-defenderImpact, 0));
		}
	}

	public int Damage(int attack, int defense) {

		int dmg = attack - defense;

		if (dmg < 0)
			dmg = 1;

		return dmg;
	}
}
