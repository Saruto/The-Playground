using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;

public class Lobby : MonoBehaviourPunCallbacks {
	// ------ Fields and Properties ------ //



	// Seralized Fields
	[SerializeField] GameObject PlayerListEntryPrefab = null;

	[SerializeField] Button ReadyButton = null;


	// ------ Methods ------ //

	// --- Start --- //
	void Start() {
		// Instantiate ourselves as soon as we enter the Lobby.
		// Will create this object for all clients and assign this client as its owner.
		GameObject playerListEntry = PhotonNetwork.Instantiate(PlayerListEntryPrefab.name, Vector3.zero, Quaternion.identity);
		ReadyButton.onClick.AddListener(playerListEntry.GetComponent<PlayerListEntry>().OnReady);
	}

	// --- Photon Callbacks --- //

}
