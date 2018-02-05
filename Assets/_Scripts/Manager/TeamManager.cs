using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour {

	public static TeamManager instance;
	public Team[] team = new Team[2];
	private int teamLimit = 2;

	void Start() {

		if (instance == null)
			instance = this;

		DefineTeams ();
	}

	void DefineTeams() {

		for (int i = 0; i < 4; i++) {

			if (team [0].beys.Count < teamLimit)
				team [0].beys.Add (BeyManager.instance.inGameBeys [i]);
			else
				team [1].beys.Add (BeyManager.instance.inGameBeys [i]);
		}

		BeyManager.instance.indicator [1].transform.SetParent (BeyManager.instance.inGameBeys [1].transform, false);
		BeyManager.instance.indicator [1].SetActive (true);

		for (int i = 0; i < teamLimit/2; i++)
			Physics2D.IgnoreCollision (team [i].beys[0].GetComponent<Collider2D> (), team [i].beys[1].GetComponent<Collider2D> ());
	}

	public bool Check(GameObject beyblade, Team team) {

		foreach (GameObject g in team.beys) {

			if (g.name == beyblade.name)
				return true;
		}

		return false;
	}
}
