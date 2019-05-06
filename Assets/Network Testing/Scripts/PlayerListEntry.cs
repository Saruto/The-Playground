using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListEntry : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback {
	// ------ Fields and Properties ------ //
	bool isReady = false;

	[SerializeField] Image ReadyStatusImage = default;

	[SerializeField] Sprite NotReadySprite = default;
	[SerializeField] Sprite IsReadySprite = default;

	// ------ Methods ------ //
    // --- Awake --- //
	void Awake() {
		// Simply finds the Players List gameobject and makes it our parent.
		transform.SetParent(GameObject.Find("Players Layout").transform);
		transform.localScale = new Vector3(1, 1, 1);

		// Also sets the values for the player that owns it.
		GetComponentInChildren<Text>().text = photonView.Owner.NickName;
	}

	void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info) {
        info.Sender.TagObject = gameObject;
    }


	// Button callbacks
	public void OnReady() {
		photonView.RPC("UpdateLobbyListing", RpcTarget.All, !isReady);
	}

	// --- Photon Callbacks --- //
	// Called whenever any *other* player enters this room.
	public override void OnPlayerEnteredRoom(Player newPlayer) {
		Debug.Log("Player " + newPlayer.NickName + " joined");
		photonView.RPC("UpdateLobbyListing", newPlayer, isReady);
	}

	// Called whenever any *other* player leaves a room.
	public override void OnPlayerLeftRoom(Player otherPlayer) {
		Debug.Log("Player " + otherPlayer.NickName + " left");
	}

	// Updates the state of the lobby listing of another player in the room.
	[PunRPC]
	void UpdateLobbyListing(bool senderReady, PhotonMessageInfo info) {
		GameObject senderListEntry = (GameObject)info.Sender.TagObject;
		senderListEntry.GetComponent<PlayerListEntry>().isReady = senderReady;
		ReadyStatusImage.sprite = senderReady ? IsReadySprite : NotReadySprite;
	}

}
