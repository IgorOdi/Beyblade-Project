using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D hit)
    {

        if (hit.tag == "Beyblade") {

            hit.gameObject.GetComponent<Beyblade>().actualStamina +=  50 * (int)Time.deltaTime;
        }
    }
}
