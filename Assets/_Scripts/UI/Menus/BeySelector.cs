using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeySelector : MonoBehaviour {

	public List<GameObject> beys;
	private int selectedBey;
	[SerializeField]
	private Button[] botoes;

	void Start() {

		botoes [0].onClick.AddListener (PreviousBey);
		botoes [1].onClick.AddListener (NextBey);
		botoes [2].onClick.AddListener (Iniciar);
	}

	void PreviousBey() {

		for (int i = 0; i < beys.Count; i++)
			beys [i].SetActive (false);

		if (selectedBey > 0) selectedBey--;
		else selectedBey = beys.Count-1;
		beys [selectedBey].SetActive (true);
	}

	void NextBey() {

		for (int i = 0; i < beys.Count; i++)
			beys [i].SetActive (false);

		if (selectedBey < beys.Count-1) selectedBey++;
		else selectedBey = 0;
		beys [selectedBey].SetActive (true);
	}

	void Iniciar() {

		BeyManager.SelectedBey = beys [selectedBey].name;
		UnityEngine.SceneManagement.SceneManager.LoadScene ("Game");
	}
}
