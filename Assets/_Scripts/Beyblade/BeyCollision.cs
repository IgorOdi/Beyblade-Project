using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyCollision : MonoBehaviour {

	public Rigidbody2D rb;
	public bool canDamage;

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

		canDamage = true;
		rb = GetComponent<Rigidbody2D> ();
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade") {

			Combat combat = new Combat (gameObject, other.gameObject);
			bool otherCanDamage = other.gameObject.GetComponent<BeyCollision> ().canDamage;

			if (canDamage && otherCanDamage) {
				#region Damage

				int damageMultiplier = combat.attacker.type == Attributes.Type.Attack ? combat.attacker.atributos.attack * 4 : 1;

				combat.attacker.actualStamina -= 3 * combat.defender.atributos.defense;
				combat.defender.actualStamina -= Damage (combat.attacker.atributos.attack, combat.defender.atributos.defense) * damageMultiplier;

				canDamage = false;
				StartCoroutine(DamageCooldown());
				#endregion
			}

			#region Impact

			float x = other.transform.position.x - transform.position.x;
			float y = other.transform.position.y - transform.position.y;

			float rot = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (0, 0, rot);

			float attackerMultiplier = combat.attacker.type == Attributes.Type.Attack ? 10 : 5;
			float defenderMultiplier = combat.defender.type == Attributes.Type.Defense ? 10 : 2;

			float defenderImpact = (Random.Range (0.2f, 0.6f) / (combat.defender.GetComponent<Rigidbody2D>().mass) * defenderMultiplier);
			float attackerImpact = (Random.Range (0.2f, 0.6f) / (combat.attacker.GetComponent<Rigidbody2D> ().mass) * attackerMultiplier);

			combat.attacker.transform.Translate(new Vector2(-attackerImpact/2, 0));
			combat.defender.transform.Translate(new Vector2(-defenderImpact, 0));

			#endregion
		}
	}

	public int Damage(int attack, int defense) {

		int dmg = attack - defense;

		if (dmg <= 0)
			dmg = 1;
		
		return dmg;
	}

	IEnumerator DamageCooldown() {

		float dmgCool = GetComponent<Beyblade> ().atributos.dmgCool;
		yield return new WaitForSeconds (dmgCool);
		canDamage = true;
	}
}