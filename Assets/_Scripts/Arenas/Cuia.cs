using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuia : MonoBehaviour {

	public int numEdges;
	public float raio;

	void Awake() {

		EdgeCollider2D edgeCollider = gameObject.AddComponent<EdgeCollider2D> ();

		Vector2[] pontos = new Vector2[numEdges];

		for (int i = 0; i < numEdges; i++) {

			float angulo = 2 * Mathf.PI * i / numEdges;
			float x = raio * Mathf.Cos (angulo);
			float y = raio * Mathf.Sin (angulo);

			pontos [i] = new Vector2 (x, y);
		}

		edgeCollider.points = pontos;
	}
}
