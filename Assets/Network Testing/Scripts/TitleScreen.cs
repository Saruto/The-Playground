using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class TitleScreen : MonoBehaviourPunCallbacks {
    // ------ Fields and Properties ------ //

	// Seralized Fields
	[SerializeField] InputField PlayerNameInput = null;


	// ------ Methods ------ //

	// --- Awake --- //
	void Awake() {
		PhotonNetwork.AutomaticallySyncScene = true;
	}


	// --- Button Functions --- //
	// Called when the user presses the Connect Button
	public void OnConnectButton() {
		PhotonNetwork.NickName = PlayerNameInput.text;
		// If we're not connected, connect to Photon
		if(!PhotonNetwork.IsConnected) {
			Debug.Log("Connecting...");
			PhotonNetwork.GameVersion = "1";
			PhotonNetwork.ConnectUsingSettings();
		}
	}



	// --- Photon Callbacks --- //

	// Called when this client gets connected to the master server.
	public override void OnConnectedToMaster() {
		Debug.Log("Connected to Master");
		// Join a random room as soon as we get connected to the master server.
		PhotonNetwork.JoinRandomRoom();
	}


	// Called once we join a room.
	// Note: Apparently does not get called if AutomaticallySyncScene is on and the joined room is not on te TitleScreen. 
	public override void OnJoinedRoom() {
		Debug.Log("Joined Room");

		// Load the lobby scene *if* we created the room.
		// Note that this implies that we're the master client.
		if(PhotonNetwork.CurrentRoom.PlayerCount == 1) {
			PhotonNetwork.LoadLevel("Lobby");
		}
	}

	// Called if we failed to join a room
	public override void OnJoinRandomFailed(short returnCode, string message) {
		// Just create a room
		Debug.Log("OnJoinRandomFailed called - Creating Room...");
		PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = 2 });
	}



}
