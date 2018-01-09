#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BeybladeCreator : EditorWindow {

	public GameObject BeybladeBase;
	public string _name;
	public Attributes.Type _type;
	public int[] stats = new int[4];
	private string[] statsName = {"Attack: ", "Defense: ", "Stamina: ", "Speed: "};
	public Color _color;
	public Object _beySprite;

	[MenuItem("Window/Beyblade Creator")]

	public static void ShowWindow() {

		EditorWindow.GetWindow (typeof(BeybladeCreator));
	}

	void OnGUI() {

		_name = EditorGUILayout.TextField("Name: ", _name);

		_type = (Attributes.Type)EditorGUILayout.EnumPopup ("Type", _type);

		for (int i = 0; i < stats.Length; i++)
			stats [i] = EditorGUILayout.IntField (statsName[i], stats [i]);

		_color = EditorGUILayout.ColorField ("Trail Color: ", _color);

		_beySprite = EditorGUILayout.ObjectField ("Sprite", _beySprite, typeof(Sprite), null);

		if (GUILayout.Button ("Create"))
			CreateBey ();
	}

	public void CreateBey() {
		
		GameObject obj = PrefabUtility.InstantiatePrefab (BeybladeBase) as GameObject;
		obj.name = _name;
		obj.GetComponentInChildren<SpriteRenderer> ().sprite = (Sprite)_beySprite;
		obj.GetComponentInChildren<TrailRenderer> ().startColor = _color;

		Beyblade bey = obj.GetComponent<Beyblade> ();
		bey.type = _type;
		bey.atributos.attack = stats [0];
		bey.atributos.defense = stats [1];
		bey.atributos.stamina = stats [2];
		bey.atributos.speed = (float)stats [3];
	}
}
#endif