using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// version number
	public int Version = 0;

	// Characters
	public Character Character1;
	public Character Character2;
	
	// display fields
	[SerializeField] Text Character1Text;
	[SerializeField] Text Character2Text;

	// Singleton
	public static GameManager Instance;

	// ------------------------------------------ Methods ------------------------------------------ //

	//  --------- Awake ---------  //
	void Awake() {
		if(Instance == null) {
			Instance = this;
		} else {
			Destroy(gameObject);
		}
	}

	//  --------- Start ---------  //
	void Start () {
		// Make Default Characters
		Character1 = new Character("Neptunia", 20, 10);
		Character2 = new Character("Noire", 15, 25);
	}
	
	//  --------- Update ---------  //
	void Update () {
		// --- Display --- //
		DisplayInfo(Character1Text, Character1);
		DisplayInfo(Character2Text, Character2);

		// --- Input --- //
		// If the Player preses a numbered key, save the data to that slot.
		if(Input.GetKeyDown(KeyCode.Alpha1)) {
			SaveLoad.Save(1);
		}
		if(Input.GetKeyDown(KeyCode.Alpha2)) {
			SaveLoad.Save(2);
		}
		if(Input.GetKeyDown(KeyCode.Alpha3)) {
			SaveLoad.Save(3);
		}

		// If the player presses a F key, load the data in that slot.
		if(Input.GetKeyDown(KeyCode.F1)) {
			SaveLoad.Load(1);
		}
		if(Input.GetKeyDown(KeyCode.F2)) {
			SaveLoad.Load(2);
		}
		if(Input.GetKeyDown(KeyCode.F3)) {
			SaveLoad.Load(3);
		}

	}

	// Displays a character's info.
	void DisplayInfo(Text text, Character cara) {
		text.text = "Name: " + cara.Name + "\nHealth: " + cara.Health + "\nMagic: " + 
			cara.Magic + "\nNumber Of Skills: " + cara.skills.Count;
	}
}
