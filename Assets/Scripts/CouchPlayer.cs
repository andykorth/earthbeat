﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class CouchPlayer : MonoBehaviour {
    public Projectile projectile;
    public int playerNum;

	public float verticalRotationSpeed = 1.0f;
	public float horizontalRotationSpeed = 1.0f;

	// The Rewired Player, used for input for this specific controller.
	private Player player;


	public void SetupController(int playerNum) {
	    this.playerNum = playerNum;
		player = ReInput.players.GetPlayer(playerNum);


		if (player == null || !player.isPlaying) {
			// no controller for this player, delete them.
			Debug.Log ("No controller found for playerID: " + playerNum + " Removing their fighter plane.");
			Destroy (this.gameObject);
			return;
		} else {
			Debug.Log ("PlayerID: " + playerNum + " ready to fly! Name: " + player.name);
		}
	}


	void FixedUpdate() {
		// Physics / Motion updates are best suited for the Fixed Update loop


        float moveHorizontal = player.GetAxis("Horiz");
		float moveVertical = player.GetAxis("Vert");
		//Debug.Log("Player " + playerNum + " Horizontal " + moveHorizontal);
		//Debug.Log("Player " + playerNum + " Vertical " + moveVertical);

		bool fire = player.GetButtonDown("Fire");

		if (fire) {
            Fire();
        }

		this.transform.Rotate (new Vector3 (verticalRotationSpeed * moveVertical, 0f, horizontalRotationSpeed * moveHorizontal) * Time.deltaTime );

		// We probably want to vary the speed, but for now it's constant:
		float planeCurrentSpeed = 5.0f;
		// Also, if we want physics interaction, we should be setting a rigid body's velocity, rather than modifying the
		// position directly
		transform.position += this.transform.forward * planeCurrentSpeed * Time.deltaTime;

    }

	public void Update(){
		// Visual effects run in the update loop.
	}

    public void Fire() {
        Instantiate(projectile, transform.position, transform.rotation).Fire(transform.forward);
    }
}