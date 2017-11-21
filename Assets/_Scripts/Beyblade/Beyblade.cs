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

	[HideInInspector]
	public bool inGame;
	[HideInInspector]
	public Transform cuia;
	private float timeCounter;

	void Start() {

		gameObject.tag = "Beyblade";
		gameObject.layer = 8;
		actualStamina = atributos.stamina;
		actualSpeed = atributos.speed;
		cuia = GameObject.FindGameObjectWithTag ("Cuia").transform;

		if (playerControlled) {

			ia = gameObject.AddComponent<PlayerControl> ();
		} else {
			switch (type) {

			case Attributes.Type.Attack:
				ia = gameObject.AddComponent<AttackIA> ();
				atributos.dmgCool = 0.75f;
				break;

			case Attributes.Type.Defense:
				ia = gameObject.AddComponent<DefenseIA> ();
				atributos.dmgCool = 1.5f;
				break;

			case Attributes.Type.Stamina:
				ia = gameObject.AddComponent<StaminaIA> ();
				atributos.dmgCool = 0.5f;
				break;

			case Attributes.Type.Balance:
				ia = gameObject.AddComponent<BalanceIA> ();
				atributos.dmgCool = 0.75f;
				break;
			}
		}

	}

	void Update() {

		timeCounter += Time.deltaTime;

		if (timeCounter >= 2) {

			int staminaDecay = type == Attributes.Type.Defense ? 4 : 2;
			actualStamina -= staminaDecay;
			timeCounter = 0;
		}

		float distancia = Vector2.Distance (transform.position, cuia.position);

		if (actualStamina <= 10 || distancia > 5) {

			inGame = false;
			actualStamina = 0;
			ia.enabled = false;

			if (playerControlled)
				print ("Game Over");
			else
				OutGame ();
		}
	}

	void OutGame() {

		GetComponent<Collider2D> ().enabled = false;
		GetComponentInChildren<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 0.5f);
	}

	void FixedUpdate() {

		if (ia != null && actualStamina > 10) {

			ia.RotationSpeed ((float)actualStamina, atributos.stamina);
			ia.Movimento (actualSpeed, atributos.stamina, actualStamina);
		}
	}
}
