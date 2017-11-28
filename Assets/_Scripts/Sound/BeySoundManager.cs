﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeySoundManager : MonoBehaviour {

	private AudioSource source;
	public AudioClip[] collisionSounds;
	public AudioClip specialSound;

	void Awake() {

		source = GetComponent<AudioSource> ();
	}

	public void PlayCollisionSound() {

		int randomizador = Random.Range (0, collisionSounds.Length);
		source.volume = 0.1f;
		source.clip = collisionSounds [randomizador];
		source.Play ();
	}

	public void PlaySpecialSound() {

		AudioSource.PlayClipAtPoint (specialSound, transform.position, 1);
	}
}
