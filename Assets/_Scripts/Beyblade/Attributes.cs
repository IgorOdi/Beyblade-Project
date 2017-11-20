using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attributes {

	public enum Type
	{
		Attack,
		Defense,
		Stamina,
		Balance
	}

	public int attack;
	public int defense;
	public int stamina;
	public float speed;
}
