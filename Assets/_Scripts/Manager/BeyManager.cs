using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeyManager : MonoBehaviour {

	public static BeyManager instance;
	public static string SelectedBey;
	public List<GameObject> inGameBeys;
	public GameObject[] indicator;
	public HUDManager hudManager;
	public EndGame endGame;
	private int beyNumber;
	private int winCondition;

	void Awake() {

		if (instance == null)
			instance = this;

		hudManager = GetComponent<HUDManager> ();
		DefinePlayerBey ();
	}

	void DefinePlayerBey() {

		foreach (GameObject bey in GameManager.instance.beyblades) {

			if (bey.name == SelectedBey)
				inGameBeys.Add (bey);
		}

		List<GameObject> validChoices = new List<GameObject> ();

		foreach (GameObject bey in GameManager.instance.beyblades) {

			if (GameManager.unlockList.Contains (bey.name) && bey.name != SelectedBey)
				validChoices.Add (bey);
		}

		DefineGameBeys (validChoices);
	}

	void DefineGameBeys(List<GameObject> _validChoices) {

		switch (GameManager.matchMode.singleplayerMode) {

		case SingleplayerMode.SoloMode:
			
			beyNumber = 2;
			winCondition = 1;
			break;

		case SingleplayerMode.DuoMode:

			beyNumber = 4;
			winCondition = 2;
			break;

		case SingleplayerMode.Deathmatch:
			beyNumber = GameManager.unlockList.Count >= 4 ? 4 : 3;
			winCondition = 1;
			break;
		}

		while (inGameBeys.Count < beyNumber) {

			int random = Random.Range (0, _validChoices.Count);
			GameObject beyToAdd = _validChoices [random];
			inGameBeys.Add (beyToAdd);
			_validChoices.Remove (beyToAdd);
		}

		ActiveBeys ();
	}
		
	void ActiveBeys() {

		for (int i = 0; i < inGameBeys.Count; i++) {

			Vector2 spawnPoint;

			if (inGameBeys [i].GetComponent<Beyblade> ().type == Attributes.Type.Attack) {
				
				spawnPoint = RandomPosition (3, 3);
			} else if (inGameBeys [i].GetComponent<Beyblade> ().type == Attributes.Type.Defense) {

				spawnPoint = RandomPosition (1.5f, 1.5f);
			} else {

				spawnPoint = new Vector2 (4, 0);
			}
				
			GameObject spawnedBey = Instantiate (inGameBeys[i], spawnPoint, Quaternion.identity);
			spawnedBey.name = spawnedBey.name.Replace ("(Clone)", "");
			inGameBeys.RemoveAt (i);
			inGameBeys.Insert (i, spawnedBey);
			Beyblade beyCode = spawnedBey.GetComponent<Beyblade> ();

			if (spawnedBey.name == SelectedBey) {

				beyCode.ia = spawnedBey.AddComponent<PlayerControl> ();
				beyCode.playerControlled = true;
				indicator [0].SetActive (true);
				indicator [0].transform.SetParent (spawnedBey.transform, false);

			} else {
				
				switch (beyCode.type) {

				case Attributes.Type.Attack:
					beyCode.ia = spawnedBey.AddComponent<AttackIA> ();
					beyCode.atributos.dmgCool = 0.15f;
					break;

				case Attributes.Type.Defense:
					beyCode.ia = spawnedBey.AddComponent<DefenseIA> ();
					beyCode.atributos.dmgCool = 0.5f;
					break;

				case Attributes.Type.Stamina:
					beyCode.ia = spawnedBey.AddComponent<StaminaIA> ();
					beyCode.atributos.dmgCool = 0.15f;
					break;

				case Attributes.Type.AtkDef:
					beyCode.ia = spawnedBey.AddComponent<AtkDefIA> ();
					beyCode.atributos.dmgCool = 0.35f;
					break;

				case Attributes.Type.AtkSta:
					beyCode.ia = spawnedBey.AddComponent<AtkStaIA> ();
					beyCode.atributos.dmgCool = 0.15f;
					break;

				case Attributes.Type.DefSta:
					beyCode.ia = spawnedBey.AddComponent<DefStaIA> ();
					beyCode.atributos.dmgCool = 0.35f;
					break;
				}
			}

			beyCode.atributos.staminaDecay = 4;
		}

		if (GameManager.matchMode.singleplayerMode == SingleplayerMode.DuoMode) {

			indicator [1].transform.SetParent (inGameBeys [1].transform, false);
			indicator [1].SetActive (true);
			Physics2D.IgnoreCollision (inGameBeys [0].GetComponent<Collider2D> (), inGameBeys [1].GetComponent<Collider2D> ());
			Physics2D.IgnoreCollision (inGameBeys [2].GetComponent<Collider2D> (), inGameBeys [3].GetComponent<Collider2D> ());
		}
			
		hudManager.DefineBeys ();
		GetComponent<SpecialSpawn> ().Spawn ();
	}

	public Vector2 RandomPosition(float x, float y) {

		float _x = Random.Range(-x, x);
		float _y = Random.Range (-y, y);

		return new Vector2 (_x, _y);
	}

	public void RemoveInGameBey(GameObject _beyToRemove) {

		inGameBeys.Remove (_beyToRemove);

		if (inGameBeys[0].name != SelectedBey)
			StartCoroutine(endGame.Finish ("Derrota"));
		else if (inGameBeys.Count <= winCondition)
			StartCoroutine(endGame.Finish ("Vitória"));
	}
}