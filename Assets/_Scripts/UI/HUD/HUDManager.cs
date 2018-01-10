using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour {

	private Beyblade[] beys;
	private BeySpecial[] beySpecials;
	private int beyCount;
	public Image[] staminaBar;
	public Image[] hudBey;
	public GameObject[] sparkHUD;

	public void DefineBeys() {

		beyCount = BeyManager.instance.inGameBeys.Count;

		beys = new Beyblade[beyCount];
		beySpecials = new BeySpecial[beyCount];

		for (int i = 0; i < beyCount; i++) {
			
			staminaBar [i].transform.parent.gameObject.SetActive (true);
			beys [i] = BeyManager.instance.inGameBeys [i].GetComponent<Beyblade> ();
			beySpecials [i] = beys [i].GetComponent<BeySpecial> ();
		}

		DefineHUDSprite ();
	}

	void DefineHUDSprite() {

		for (int i = 0; i < beyCount; i++) {
			SpriteRenderer beyRend = BeyManager.instance.inGameBeys [i].GetComponentInChildren<SpriteRenderer> ();
			hudBey[i].sprite = beyRend.sprite;
		}
	}

	void Update() {

		for (int i = 0; i < BeyManager.instance.inGameBeys.Count; i++) {

			staminaBar[i].fillAmount = (float)beys[i].actualStamina / beys[i].atributos.stamina;
			sparkHUD[i].SetActive (beySpecials [i].canSpecial);
		}
	}
}
