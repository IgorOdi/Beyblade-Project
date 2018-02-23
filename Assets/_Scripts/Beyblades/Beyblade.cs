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
	private bool inGame;
	private int decayMultiply;
	private float distance;

	void Start() {

		inGame = true;
		gameObject.tag = "Beyblade";
		gameObject.layer = 8;
		actualStamina = atributos.stamina;
		actualSpeed = atributos.speed;
		cuia = GameObject.FindGameObjectWithTag ("Cuia").transform;
		special = GetComponent<BeySpecial> ();
	}
		
	void FixedUpdate() {

		if (ia != null || actualStamina > 0) {

			ia.RotationSpeed ((float)actualStamina, atributos.stamina);
			ia.Movement (actualSpeed, atributos.stamina, actualStamina);
		}
	}

	void Update() {

		timeCounter += Time.deltaTime;
		distance = Vector2.Distance (transform.position, cuia.position);
		decayMultiply = (float)actualStamina / atributos.stamina > 0.2f ? 1 : 2;

		if (timeCounter > 2) {
			
			actualStamina -= atributos.staminaDecay * decayMultiply;
			timeCounter = 0;
		}

		if ((actualStamina <= 0  || distance > 5) && inGame) OutGame ();
	}

	void OutGame() {

		inGame = false;
		ia.enabled = false;
		GetComponent<Collider2D> ().enabled = false;
		GetComponentInChildren<SpriteRenderer> ().color = new Vector4 (1, 1, 1, 0.5f);

		BeyManager.instance.RemoveInGameBey (gameObject);
		Destroy (gameObject, 2f);
	}
}
