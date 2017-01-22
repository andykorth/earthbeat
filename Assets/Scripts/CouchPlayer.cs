﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class CouchPlayer : MonoBehaviour {
    public Projectile projectile;
    public int playerNum;
    public GameObject spawnPosition;

	public float verticalRotationSpeed = 2.0f;
	public float horizontalRotationSpeed = 2.0f;

	// The Rewired Player, used for input for this specific controller.
	private Player player;

    public float tilt = 2f;

    private Rigidbody rb;

    // We probably want to vary the speed, but for now it's constant:
    public float planeCurrentSpeed = 7.0f;
    private Quaternion baseRotation;
    private Camera cam;
    private float length;
    private Camera trackingCamera;
    private GameObject trackedObject;
    private float time;

    public void SetupController(int playerNum) {
        this.playerNum = playerNum;

        GameObject camObj = new GameObject("Camera_"+playerNum);
        trackingCamera = camObj.AddComponent<Camera>();
        trackedObject = this.transform.Find("TrackingCamera").gameObject;

		player = ReInput.players.GetPlayer(playerNum);
	    rb = this.GetComponent<Rigidbody>();


	    if (player == null || !player.isPlaying) {
			// no controller for this player, delete them.
			Debug.Log ("No controller found for playerID: " + playerNum + " Removing their fighter plane.");
			Destroy (this.gameObject);
			return;
		} else {
			Debug.Log ("PlayerID: " + playerNum + " ready to fly! Name: " + player.name);
		}

        SetCamera();
    }

    private void SetCamera() {
        this.trackingCamera.rect = GameManager.Instance.PlayerCamPositions[playerNum];
        //this.trackingCamera.targetDisplay = 2;
    }



    void FixedUpdate() {
		// Physics / Motion updates are best suited for the Fixed Update loop
		float moveHorizontal = player.GetAxis("Horiz") * (horizontalRotationSpeed) * Time.deltaTime;
		float moveVertical = player.GetAxis("Vert") * (verticalRotationSpeed) * Time.deltaTime;
		float rightMoveVertical = player.GetAxis("RightVert") * verticalRotationSpeed * Time.deltaTime;

	    bool fire = player.GetButtonDown("Fire");

		if (fire) {
            Fire();
        }

        Quaternion lookRot = Quaternion.LookRotation(GameManager.Instance.VRPlayer.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 2f);

	    Vector3 movement = new Vector3 (moveHorizontal, rightMoveVertical,  moveVertical);
	    movement = transform.rotation * movement;
	    rb.velocity = movement * planeCurrentSpeed;
	}

	public void Update(){
	    trackingCamera.transform.position = Vector3.Lerp(trackingCamera.transform.position, trackedObject.transform.position, Time.deltaTime * 7);
	    trackingCamera.transform.rotation = trackedObject.transform.rotation;
	    // Visual effects run in the update loop.
	}

    public void Fire() {
        Vector3 direction =  spawnPosition.transform.position - transform.position; //Get a direction vector to fire the bullet at.
        direction.Normalize(); // direction vector normalized to magnitude 1
        Instantiate(projectile, spawnPosition.transform.position, spawnPosition.transform.rotation).Fire(this.gameObject, direction);
    }
}