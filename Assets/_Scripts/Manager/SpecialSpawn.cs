﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawn : MonoBehaviour {

	public SpecialPower power;

	public void Spawn() {

		StartCoroutine (TempoSpawn ());
	}

	public IEnumerator TempoSpawn() {

		power.spawn = this;

		int randomizador = Random.Range (15, 20);
		yield return new WaitForSeconds (randomizador);

		float x = Random.Range (-3, 3);
		float y = Random.Range (-3, 3);
		Vector2 spawnPoint = new Vector2 (x, y);

		power.gameObject.SetActive (true);
		power.transform.position = spawnPoint;
		StartCoroutine (power.Spawnou ());
	}
}
