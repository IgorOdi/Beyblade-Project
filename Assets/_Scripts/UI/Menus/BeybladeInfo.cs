using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeybladeInfo: MonoBehaviour {

	[HideInInspector]
	public string name;
	[HideInInspector]
	public Image sprite;
	public Attributes.Type type;
	public Attributes attributes;
	[TextArea(1, 10)]
	public string specialDesc;
	public bool unlocked;

	void OnEnable() {

		name = gameObject.name;
		sprite = GetComponentsInChildren<Image> ()[1];

		foreach (string bey in GameManager.unlockList)
			if (name == bey)
				unlocked = true;

		sprite.color = unlocked ? Color.white : Color.black;
	}
}
