using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BeySelector : MonoBehaviour {

	public BeybladeInfo selectedBey;
	[SerializeField]
	private Button confirmButton;

	public SelectedInfo info;

	void OnEnable() {

		confirmButton.onClick.AddListener (Iniciar);
	}

	public void Select() {

		selectedBey = EventSystem.current.currentSelectedGameObject.GetComponent<BeybladeInfo>();

		info.name = selectedBey.name;
		info.type = selectedBey.type;
		info.specialDesc = selectedBey.specialDesc;
		info.sprite = selectedBey.sprite;
		info.attributes = selectedBey.attributes;

		info.Atualiza (selectedBey.unlocked);
	}

	void Iniciar() {

		if (selectedBey.unlocked) {
			BeyManager.SelectedBey = selectedBey.gameObject.name;
			UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
		}
	}
}
