using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Lobby : MonoBehaviourPunCallbacks {
	// ------ Fields and Properties ------ //

	// Seralized Fields
	[SerializeField] GameObject PlayerListEntryPrefab = null;


	// ------ Methods ------ //

	// --- Start --- //
	void Start() {
		// Instantiate ourselves as soon as we enter the Lobby.
		// Will create this object for all clients and assign this client as its owner.
		StartCoroutine(delayed_instantiation());

	}

	// Delays the instantiation by a frame.
	IEnumerator delayed_instantiation() {
		yield return new WaitForSeconds(1f);
		GameObject newEntry = PhotonNetwork.Instantiate(PlayerListEntryPrefab.name, Vector3.zero, Quaternion.identity);
	}


	// --- Photon Callbacks --- //

	// Called whenever any *other* player enters this room.
	public override void OnPlayerEnteredRoom(Player newPlayer) {
		Debug.Log("Player " + newPlayer.NickName + " joined");


	}

	// Called whenever any *other* player leaves a room.
	public override void OnPlayerLeftRoom(Player otherPlayer) {
		Debug.Log("Player " + otherPlayer.NickName + " left");


	}

}
