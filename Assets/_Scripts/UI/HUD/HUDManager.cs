using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	BeyManager beyManager;
	Beyblade[] beys;
	public Image[] staminaBar;
	public Image[] hudBey;
	public GameObject[] sparkHUD;

	public void DefineBeys() {

		beyManager = FindObjectOfType<BeyManager> ();

		beys = new Beyblade[beyManager.inGameBeys.Count];

		for (int i = 0; i < beyManager.inGameBeys.Count; i++)
			beys[i] = beyManager.inGameBeys [i].GetComponent<Beyblade> ();

		DefineHUDSprite ();
	}

	void Update() {

		for (int i = 0; i < beyManager.inGameBeys.Count; i++) {
			
			staminaBar[i].fillAmount = (float)beys[i].actualStamina / beys[i].atributos.stamina;

			bool activeSpark = beys[i].GetComponent<BeySpecial> ().canSpecial;
			sparkHUD[i].SetActive (activeSpark);
		}
	}

	void DefineHUDSprite() {

		for (int i = 0; i < beyManager.inGameBeys.Count; i++) {
			SpriteRenderer beyRend = beyManager.inGameBeys [i].GetComponentInChildren<SpriteRenderer> ();
			hudBey[i].sprite = beyRend.sprite;
		}
	}
}
