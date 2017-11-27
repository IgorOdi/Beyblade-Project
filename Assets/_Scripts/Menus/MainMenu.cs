using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	[SerializeField]
	private Button[] button;
	[SerializeField]
	private GameObject[] telas;

	void Start() {

		button = new Button[3];

		for (int i = 0; i < button.Length; i++)
			button [i] = GetComponentsInChildren<Button> () [i];

		button [0].onClick.AddListener (Jogar);
		button [1].onClick.AddListener (Instru);
		button [2].onClick.AddListener (Creditos);
	}

	void Jogar() {

		for (int i = 0; i < telas.Length; i++)
			telas [i].SetActive (false);

		telas [1].SetActive (true);
	}

	void Instru() {

		for (int i = 0; i < telas.Length; i++)
			telas [i].SetActive (false);

		telas [2].SetActive (true);
	}

	void Creditos() {

		for (int i = 0; i < telas.Length; i++)
			telas [i].SetActive (false);

		telas [3].SetActive (true);
	}
}