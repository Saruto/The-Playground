using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

// SaveLoad is a class that holds static methods relating to the serialization and deserialization of various objects relating
// to a particular player's save data. It uses a containter ADT, SaveData, to store all the relevant objects and serializes it
// into the persistant data path location on the player's computer.
public static class SaveLoad{
	// ----------------------------------- Fields and Properties ----------------------------------- //

	// The path of the directory that contains all of the save data files.
	static readonly string SaveDataPath = Application.persistentDataPath + "/Save Data";

	// ------------------------------------------ Methods ------------------------------------------ //

	#region Save Methods
	// Serializes and saves data in a specified slot.
	public static void Save(int slot){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream mainFile = null;
		try{
			// Create main save folder if it's not there already
			if(!Directory.Exists(SaveDataPath)){
				Directory.CreateDirectory(SaveDataPath);
			}

			// --- Main File --- //
			mainFile = File.Create(SaveDataPath + "/Slot " + slot + ".dat");
			SaveData save = new SaveData();
			save.version = GameManager.Instance.Version;
			save.cara1 = GameManager.Instance.Character1;
			save.cara2 = GameManager.Instance.Character2;


			// Serialize and save to the desired file path
			bf.Serialize(mainFile, save);

			// Confirmation message. States where data is saved.
			Debug.Log("Data saved successfully! Location: " + SaveDataPath + "/Slot " + slot + ".dat");
		}
		catch(Exception ex){
			Debug.LogError("Game Manager: Failed to serialize save data (Reason: " + ex.ToString() + ")");
		}
		finally{
			mainFile.Close();
		}
	}
	#endregion

	#region Load Methods
	// Loads data from a serialized file. Can be adjusted to instead read from a specific file from a list of available files (multiple save slots).
	public static void Load(int slot){
		if(File.Exists(SaveDataPath + "/Slot " + slot + ".dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream mainFile = null;
			try{
				mainFile = File.Open(SaveDataPath + "/Slot " + slot + ".dat", FileMode.Open);

				SaveData save = (SaveData)bf.Deserialize(mainFile);
				
				// Compare Version Numbers
				if(GameManager.Instance.Version != save.version){
					Debug.LogWarning(string.Format("Warning! The current version of the game ({0}) does not equal the save data's version ({1})!", GameManager.Instance.Version, save.version));
				}
				
				GameManager.Instance.Character1 = save.cara1;
				GameManager.Instance.Character2 = save.cara2;

				// Confirmation message. 
				Debug.Log("Data loaded successfully!");
			}
			catch(Exception ex){
				Debug.LogError("Game Manager: Failed to deserialize save data (Reason: " + ex.ToString() + ")");
				throw ex;
			}
			finally{
				mainFile.Close();
			}
		}
		else{
			throw new Exception("Game Manager: Failed to find save data at specified path.");
		}
	}
	#endregion

	// ------------------------------------- Internal Classes -------------------------------------- //

	// A container ADT that stores all nessassary information about a player's save data.
	[Serializable]
	public class SaveData{
		// ----------------------------------- Fields and Properties ----------------------------------- //
		public int version;
		public Character cara1;
		public Character cara2;
	}
}


// An example "Character" object that we would save.
[Serializable]
public class Character {
	public string Name;
	public int Health;
	public int Magic;
	//float privateFloat = 3;
	//string privateStr = "i'm private";
	public List<Skill> skills = new List<Skill>();
	public Character(string name, int health, int magic) {
		Name = name;
		Health = health;
		Magic = magic;
	}
}
[Serializable]
public class Skill {
	public string Name;
	public int DamageDone;
	public Skill(string name, int damage) {
		Name = name;
		DamageDone = damage;
	}
}
