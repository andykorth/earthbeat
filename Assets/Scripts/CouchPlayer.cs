﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class CouchPlayer : MonoBehaviour {
    public Projectile projectile;
    public int playerNum;
    public GameObject spawnPosition;
    public GameObject explosion;

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
    private bool canShoot;
    private GameObject canvas;
    private float SHOOT_TIMER = 0.15f;
    private float RELOAD_TIMER = 1f;
    public int shotChamber = 6;
    private readonly int MAX_SHOTS = 4;
    private readonly int OVERHEAT_TIMER = 3;

    public void SetupController(int playerNum) {
        this.playerNum = playerNum;
        canShoot = true;
        trackingCamera = GameObject.Find("Camera" + (playerNum + 1)).GetComponent<Camera>();
        canvas = trackingCamera.transform.Find("BGCanvas-PressStartToJoin").gameObject;
        canvas.SetActive(false);
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

    }

    private void Reload() {
        if (shotChamber < MAX_SHOTS) StartCoroutine(_Reload());
    }

    private IEnumerator _Reload() {
        yield return new WaitForSeconds(shotChamber == 0 ? OVERHEAT_TIMER : RELOAD_TIMER);
        shotChamber++;
    }

    private void ShootInterval() {
        StartCoroutine(_ShootInterval());
    }

    private IEnumerator _ShootInterval() {
        yield return new WaitForSeconds(SHOOT_TIMER);
        canShoot = true;
    }


    void FixedUpdate() {
		// Physics / Motion updates are best suited for the Fixed Update loop
		float moveHorizontal = player.GetAxis("Horiz") * (horizontalRotationSpeed) * Time.deltaTime;
		float moveVertical = player.GetAxis("Vert") * (verticalRotationSpeed) * Time.deltaTime;
		float rightMoveVertical = player.GetAxis("RightVert") * verticalRotationSpeed * Time.deltaTime;

	    bool fire = player.GetButtonDown("Fire");

		if (fire && shotChamber > 0 && canShoot) {
            Fire();
        }

        Quaternion lookRot = Quaternion.LookRotation(GameManager.Instance.VRPlayer.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 2f);

	    Vector3 movement = new Vector3 (moveHorizontal, rightMoveVertical,  moveVertical);
	    movement = transform.rotation * movement;
	    rb.velocity = movement * planeCurrentSpeed;

        trackingCamera.transform.position = Vector3.Lerp(trackingCamera.transform.position, trackedObject.transform.position, Time.deltaTime * 7);
        trackingCamera.transform.rotation = trackedObject.transform.rotation;
    }

    public void Fire() {
        shotChamber = shotChamber - 1;
        canShoot = false;
        ShootInterval();
        AudioManager.i.PlayLaser ();
        Vector3 direction = -spawnPosition.transform.forward;//spawnPosition.transform.position - transform.position; //Get a direction vector to fire the bullet at.
        //direction.Normalize(); // direction vector normalized to magnitude 1
        Instantiate(projectile, spawnPosition.transform.position, spawnPosition.transform.rotation).Fire(this.gameObject, direction);
        Reload();
    }

    public void OnCollisionEnter(Collision collision) {
        Destroy(this.gameObject);
        GameObject go = Instantiate(explosion, transform.position, transform.rotation);
        Destroy(go, 4);
        StartRespawnTimer();
    }

    private void StartRespawnTimer() {
        StartCoroutine(_Respawn());
    }

    private IEnumerator _Respawn() {
        yield return new WaitForSeconds(2f);
        GameManager.Instance.CouchPlayerDied(this);
        canvas.SetActive(true);
    }

}