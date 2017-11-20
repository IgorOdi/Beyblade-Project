using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour {

	public bool playerControlled;
	public Attributes.Type type;
	public Attributes atributos;
	public int actualStamina;
	public float actualSpeed;
	public int specialPoints;
	public IA ia;

	void Start() {

		gameObject.tag = "Beyblade";
		actualStamina = atributos.stamina;
		actualSpeed = atributos.speed;

		if (playerControlled) {

			ia = gameObject.AddComponent<PlayerControl> ();
		} else {
			switch (type) {

			case Attributes.Type.Attack:
				ia = gameObject.AddComponent<AttackIA> ();
				break;

			case Attributes.Type.Defense:
				ia = gameObject.AddComponent<DefenseIA> ();
				break;

			case Attributes.Type.Stamina:
				ia = gameObject.AddComponent<StaminaIA> ();
				break;

			case Attributes.Type.Balance:
				ia = gameObject.AddComponent<BalanceIA> ();
				break;
			}
		}

	}

	void Update() {

		if (actualStamina <= 0) {
			
			actualStamina = 0;
			ia.enabled = false;

			if (playerControlled)
				print ("Game Over");
		}

		if (ia != null) {

			ia.RotationSpeed ((float)actualStamina, atributos.stamina);
			ia.Movimento (actualSpeed, atributos.stamina, actualStamina);
		}
	}
}
