using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	[SerializeField]
	private Image tela;
	[SerializeField]
	private Sprite[] telas;
	[SerializeField]
	private Button[] btn;

	public void Finish(string gameStatus) {

		tela.gameObject.SetActive (true);
		btn [0].gameObject.SetActive (true);
		btn [1].gameObject.SetActive (true);

		btn [0].onClick.AddListener (PlayAgain);
		btn [1].onClick.AddListener (BackToMenu);

		int index = gameStatus == "Vitória" ? 0 : 1;
		tela.sprite = telas [index];
	}

	void PlayAgain() {

	}

	void BackToMenu() {

		SceneManager.LoadScene ("Menu");
		Time.timeScale = 1;
	}
}
