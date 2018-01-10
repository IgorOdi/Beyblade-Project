using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedInfo : MonoBehaviour {

	public new string name;
	public string specialDesc;
	public Image sprite;
	public Attributes.Type type;
	public Attributes attributes;

	public Text nameText;
	public Text typeText;
	public Text specialDescText;
	public Image spriteImage;
	public Image[] attributesImage;

	[SerializeField]
	private float[] valorToDivide;
	private float[] valor = new float[4];

	private float t;

	public void Atualiza(bool _unlocked) {

		nameText.text = _unlocked ? name : "???";
		typeText.text = _unlocked ? TypeString() : "???";
		specialDescText.text = _unlocked ? specialDesc : "???";
		spriteImage.sprite = sprite.sprite;
		spriteImage.color = _unlocked ? Color.white : Color.black;

		valor [0] = _unlocked ? attributes.attack : 0;
		valor [1] = _unlocked ? attributes.defense : 0;
		valor [2] = _unlocked ? attributes.stamina : 0;
		valor [3] = _unlocked ? attributes.speed : 0;

		t = 0;
		StartCoroutine (AttributesLerp ());
	}

	private IEnumerator AttributesLerp() {

		while (t < 1) {

			for (int i = 0; i < 4; i++) {

				t += Time.deltaTime;

				attributesImage [i].fillAmount = Mathf.Lerp (attributesImage [i].fillAmount, valor [i] / valorToDivide [i], t);

				yield return new WaitForSeconds (0.01f);
			}
		}
	}

	private string TypeString() {

		switch (type) {

		case Attributes.Type.AtkDef:
			return "Attack and Defense";

		case Attributes.Type.AtkSta:
			return "Attack and Stamina";

		case Attributes.Type.DefSta:
			return "Defense and Stamina";
		}

		return type.ToString ();
	}
}