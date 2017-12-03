using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode {

	Singleplayer,
	Multiplayer
}

public enum SingleplayerMode {

	SoloMode,
	DuoMode,
	Deathmatch
}

public class MatchMode : MonoBehaviour {

	public GameMode gameMode;
	public SingleplayerMode singleplayerMode;

	public Button[] gameModeButton;
	public Button[] singleplayerModeButton;
	public GameObject[] screen;
	public bool duoModeEnabled;

	void OnEnable() {

		GameManager.matchMode = this;

		gameModeButton [0].onClick.AddListener (delegate {

			SelectMode (GameMode.Singleplayer);
		});

		gameModeButton [1].onClick.AddListener (delegate {

			SelectMode (GameMode.Multiplayer);
		});

		singleplayerModeButton [0].onClick.AddListener (delegate {
			
			SingleplayerSelectMode(SingleplayerMode.SoloMode);
		});

		singleplayerModeButton [1].onClick.AddListener (delegate {

			SingleplayerSelectMode(SingleplayerMode.DuoMode);
		});

		singleplayerModeButton [2].onClick.AddListener (delegate {

			SingleplayerSelectMode(SingleplayerMode.Deathmatch);
		});
	}

	public void SelectMode(GameMode _gameMode) {

		if (_gameMode == GameMode.Singleplayer) {

			screen [0].SetActive (false);
			screen [1].SetActive (true);
		} else if (_gameMode == GameMode.Multiplayer) {

			Debug.LogWarning ("Modo de Jogo Indisponível");
		}

		gameMode = _gameMode;
	}

	public void SingleplayerSelectMode(SingleplayerMode _singleplayerMode) {

		duoModeEnabled = GameManager.unlockList.Count >= 4 ? true : false;

		if (_singleplayerMode == SingleplayerMode.DuoMode && !duoModeEnabled) {
			
			Debug.LogWarning ("Modo de Jogo Indisponível");
		} else {

			for (int i = 0; i < screen.Length; i++)
				screen [i].SetActive (false);	

			screen [2].SetActive (true);
			singleplayerMode = _singleplayerMode;
		}
	}

	void OnDisable() {

		for (int i = 0; i < screen.Length; i++)
			screen [i].SetActive (false);	

		screen [0].SetActive (true);
	}
}