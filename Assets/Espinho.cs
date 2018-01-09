using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espinho : MonoBehaviour {

	 Vector3 tamanho;
	private float x;

	// Use this for initialization
	void Start () {

		Destroy (gameObject, 4);
		x = 0;
	}

	// Update is called once per frame
	void Update () {

		tamanho = new Vector3 (transform.localScale.x+x,transform.localScale.y+x,transform.localScale.z);

		x += 0.007f*Time.deltaTime;

		transform.localScale = tamanho;

		transform.Translate ( 0, 1f*Time.deltaTime , 0 );
		
	}

	void OnTriggerEnter2D(Collider2D hit){
	
		if (hit.gameObject.tag == "Beyblade" && hit.gameObject.name != "Bear Claws") {

			Destroy (gameObject);

			Beyblade beyblade = hit.gameObject.GetComponent<Beyblade>();

			beyblade.actualStamina -= 25;
		}
	
	}
}
