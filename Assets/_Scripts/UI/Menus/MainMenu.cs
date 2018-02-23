using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	private Button[] button;
	[SerializeField]
	private GameObject[] screen;

	void Start() {

		button [0].onClick.AddListener (Campanha);
		button [1].onClick.AddListener (Exibicao);
		button [2].onClick.AddListener (Instru);
	}

	void Campanha() {

		for (int i = 0; i < screen.Length; i++)
			screen [i].SetActive (false);

		screen [1].SetActive (true);
	}

	void Exibicao() {

		for (int i = 0; i < screen.Length; i++)
			screen [i].SetActive (false);

		screen [2].SetActive (true);
	}

	void Instru() {

		for (int i = 0; i < screen.Length; i++)
			screen [i].SetActive (false);

		screen [3].SetActive (true);
	}

	void Creditos() {

		for (int i = 0; i < screen.Length; i++)
			screen [i].SetActive (false);

		screen [4].SetActive (true);
	}
}