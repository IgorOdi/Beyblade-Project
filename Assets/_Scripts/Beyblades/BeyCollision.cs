using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyCollision : MonoBehaviour {

	[HideInInspector]
	public Rigidbody2D rb;
	private bool canDamage;
	[SerializeField]
	private GameObject spark;
	private BeySoundManager beySound;

	private int damageMultiplier;
	private int defenderDamageMultiplier;

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
		beySound = GetComponentInChildren<BeySoundManager> ();
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (gameObject.tag == "Beyblade" && other.gameObject.tag == "Beyblade") {

			Combat combat = new Combat (gameObject, other.gameObject);
			bool otherCanDamage = other.gameObject.GetComponent<BeyCollision> ().canDamage;

			if (canDamage && otherCanDamage) {
				#region Damage

				if (combat.attacker.type == Attributes.Type.Attack)
					damageMultiplier = combat.attacker.atributos.attack * 4;
				else if (combat.attacker.type == Attributes.Type.AtkDef || combat.attacker.type == Attributes.Type.AtkSta)
					damageMultiplier = combat.attacker.atributos.attack * 2;
				else
					damageMultiplier = 1;
				
				damageMultiplier *= combat.defender.type == Attributes.Type.Stamina || combat.defender.type == Attributes.Type.AtkSta ? 2 : 1;

				if (combat.defender.type == Attributes.Type.Defense)
					defenderDamageMultiplier = 4;
				else if (combat.defender.type == Attributes.Type.AtkDef || combat.defender.type == Attributes.Type.AtkSta)
					defenderDamageMultiplier = 2;
				else
					defenderDamageMultiplier = 1;

				defenderDamageMultiplier *= combat.attacker.type == Attributes.Type.Stamina || combat.defender.type == Attributes.Type.AtkSta ? 4 : 1;

				combat.attacker.actualStamina -= DefenderDamage (combat.defender.atributos.defense, defenderDamageMultiplier);
				combat.defender.actualStamina -= AttackerDamage (combat.attacker.atributos.attack, combat.defender.atributos.defense, damageMultiplier);

				canDamage = false;
				StartCoroutine(DamageCooldown());
				#endregion
			}

			#region Impact

			float x = other.transform.position.x - transform.position.x;
			float y = other.transform.position.y - transform.position.y;

			float rot = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;

			transform.rotation = Quaternion.Euler (0, 0, rot);

			float attackerMultiplier = combat.attacker.type == Attributes.Type.Attack ? 8 : 5;
			float defenderMultiplier = combat.defender.type == Attributes.Type.Defense ? 8 : 2;

			float defenderImpact = (Random.Range (0.6f, 1.6f) / (combat.defender.GetComponent<Rigidbody2D>().mass) * defenderMultiplier);
			float attackerImpact = (Random.Range (0.6f, 1.6f) / (combat.attacker.GetComponent<Rigidbody2D> ().mass) * attackerMultiplier);

			StartCoroutine(Impact(combat.attacker.gameObject, attackerImpact/2));
			StartCoroutine(Impact(combat.defender.gameObject, defenderImpact));

			#endregion

			#region VFX

			spark.SetActive(true);

			bool attackerSpark = attackerImpact > 0.5f ? true : false;
			bool defenderSpark = defenderImpact > 0.5f ? true : false;
			combat.attacker.gameObject.GetComponent<BeyCollision>().spark.GetComponent<Animator>().SetBool("Strong", attackerSpark);
			combat.defender.gameObject.GetComponent<BeyCollision>().spark.GetComponent<Animator>().SetBool("Strong", defenderSpark);

			foreach(ContactPoint2D hit in other.contacts) {

				Vector2 faiscaPosition = hit.point;
				spark.transform.position = faiscaPosition;
			}

			#endregion

			#region SFX

			beySound.PlayCollisionSound();

			#endregion
		}
	}

	public void DeactivateSpark() {

		spark.SetActive (false);
	}

	public int DefenderDamage(int defense, int defenseMultiplier) {

		int dmg = (defense * defenseMultiplier) / 2;
		if (dmg < 0) dmg = 1;
		return dmg;
	}

	public int AttackerDamage(int attack, int defense, int damageMultiplier) {

		int dmg = ((attack - defense) + damageMultiplier)/2;
		if (dmg <= 0) dmg = 1;
		return dmg;
	}

	IEnumerator Impact(GameObject _bey, float _impact) {

		int i = 0;

		while (i < 6) {

			_bey.transform.Translate (-_impact / 6, 0, 0);
			i++;
			yield return new WaitForSeconds (0.01f);
		}
	}

	IEnumerator DamageCooldown() {

		float dmgCool = GetComponent<Beyblade> ().atributos.dmgCool;
		yield return new WaitForSeconds (dmgCool);
		canDamage = true;
	}
}