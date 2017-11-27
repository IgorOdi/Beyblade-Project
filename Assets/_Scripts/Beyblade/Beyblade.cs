using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beyblade : MonoBehaviour {

	public bool playerControlled;
	public Attributes.Type type;
	public Attributes atributos;
	public int actualStamina;
	public float actualSpeed;
	public IA ia;
	public BeySpecial special;


	[HideInInspector]
	public Transform cuia;
	private float timeCounter;

	void Start() {

		gameObject.tag = "Beyblade";
		gameObject.layer = 8;
		actualStamina = atributos.stamina;
		actualSpeed = atributos.speed;
		cuia = GameObject.FindGameObjectWithTag ("Cuia").transform;
		special = GetComponent<BeySpecial> ();

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

		float distancia = Vector2.Distance (transform.position, cuia.position);

		timeCounter += Time.deltaTime;

		int staminaDecay = type == Attributes.Type.Defense ? 3 : 2;
		int decayMultiply = (float)actualStamina / atributos.stamina > 0.2f ? 1 : 10;

		if (timeCounter > 2) {
			
			actualStamina -= staminaDecay * decayMultiply;
			timeCounter = 0;
		}

		if (actualStamina <= 0  || distancia > 5) {

			OutGame ();
		}
	}

	void OutGame() {

		ia.enabled = false;
		GetComponent<Collider2D> ().enabled = false;
		GetComponentInChildren<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 0.5f);

		BeyManager beyManager = FindObjectOfType<BeyManager> ();
		beyManager.RemoveInGameBey (gameObject);

		if (playerControlled) { 
			
			Time.timeScale = 0;
			FindObjectOfType<EndGame> ().Finish ("Derrota");
		}
	}

	void FixedUpdate() {

		if (ia != null && actualStamina > 0) {

			ia.RotationSpeed ((float)actualStamina, atributos.stamina);
			ia.Movimento (actualSpeed, atributos.stamina, actualStamina);
		}
	}
}
