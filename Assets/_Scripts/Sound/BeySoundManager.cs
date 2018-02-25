using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeySoundManager : MonoBehaviour {

	[SerializeField]
	private AudioSource source;
	public AudioClip[] collisionSounds;
	public AudioClip specialSound;

	public void PlayCollisionSound() {

		int randomizador = Random.Range (0, collisionSounds.Length);
		source.volume = 0.1f;
		source.clip = collisionSounds [randomizador];
		source.Play ();
	}

	public void PlaySpecialSound() {

		source.volume = 0.5f;
		source.clip = specialSound;
		source.Play ();
	}
}
