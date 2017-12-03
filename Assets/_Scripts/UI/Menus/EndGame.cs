using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	[SerializeField]
	private Image tela;
	[SerializeField]
	private GameObject KO;
	[SerializeField]
	private Sprite[] telas;
	[SerializeField]
	private Button[] btn;
	[SerializeField]
	private AudioSource BGM;

	public IEnumerator Finish(string gameStatus) {

		Time.timeScale = 0.25f;

		yield return new WaitForSeconds (0.125f);

		KO.SetActive (true);

		yield return new WaitForSeconds (0.5f);

		KO.SetActive (false);

		tela.gameObject.SetActive (true);
		btn [0].gameObject.SetActive (true);
		btn [1].gameObject.SetActive (true);

		btn [0].onClick.AddListener (PlayAgain);
		btn [1].onClick.AddListener (BackToMenu);

		int index = gameStatus == "Vitória" ? 0 : 1;
		tela.sprite = telas [index];

		BGM.Stop ();
	}

	void PlayAgain() {

		Time.timeScale = 1;
		SceneManager.LoadScene ("Game");
	}

	void BackToMenu() {

		Time.timeScale = 1;
		SceneManager.LoadScene ("Menu");
	}
}
