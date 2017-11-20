using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyCollision : MonoBehaviour {

	public Rigidbody2D rb;
	public bool colidiu;
	public float timer;

	public class Combat {

		public Beyblade attacker;
		public Beyblade defender;

		public Combat(GameObject bey, GameObject otherBey) {

			float beyVel = bey.GetComponent<Rigidbody2D>().velocity.magnitude;
			float otherBeyVel = otherBey.GetComponent<Rigidbody2D>().velocity.magnitude;

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

	void Update() {

		if (colidiu) {

			timer += Time.deltaTime;

			if (timer > 2) {

				colidiu = false;
				timer = 0;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {

		if (other.gameObject.tag == "Beyblade") {

			colidiu = true;

			Combat combat = new Combat (gameObject, other.gameObject);
			combat.attacker.actualStamina -= 2;
			combat.defender.actualStamina -= Damage (combat.attacker.atributos.attack, combat.defender.atributos.defense);
		}
	}

	public int Damage(int attack, int defense) {

		int dmg = attack - defense;

		if (dmg < 0)
			dmg = 1;

		return dmg;
	}
}
