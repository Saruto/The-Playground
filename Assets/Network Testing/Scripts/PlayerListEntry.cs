using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerListEntry : MonoBehaviourPun {
    // --- Awake --- //
	void Awake() {
		// Simply finds the Players List gameobject and makes it our parent.
		transform.SetParent(GameObject.Find("Players Layout").transform);
		transform.localScale = new Vector3(1, 1, 1);

		// Also sets the values for the player that owns it.
		GetComponentInChildren<Text>().text = photonView.Owner.NickName;
	}
}
