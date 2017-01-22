using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRController : MonoBehaviour {
	public Rigidbody stickyHand;

	private Vector3 lastPosition;
	private Vector3[] velocity = new Vector3[FRAMECOUNT];
	private Vector3[] acceleration = new Vector3[FRAMECOUNT];

	private bool canFire;
	private float nextFireTime;

	private SteamVR_TrackedController trackedController;

	private const int FRAMECOUNT = 90;
	private float FIRESTRENGTH = 30f;

	private void Awake() {
		trackedController = gameObject.AddComponent<SteamVR_TrackedController> ();
	}
	
	private void Start() {
		lastPosition = transform.position;

		stickyHand.position = transform.position;

		Rigidbody rb = gameObject.AddComponent<Rigidbody> ();
		rb.isKinematic = true;
		rb.useGravity = false;

		SpringJoint sj = gameObject.AddComponent<SpringJoint> ();
		sj.autoConfigureConnectedAnchor = false;
		sj.connectedBody = stickyHand;
		sj.spring = 10f;
		sj.damper = 1f;
	}

	private void FixedUpdate() {
		Vector3 currentVelocity = (transform.position - lastPosition) / Time.fixedDeltaTime;

		for (int i = 1; i < FRAMECOUNT; i++) { // shift the velocity and acceleration from each frame down
			velocity [i] = velocity [i - 1];
			acceleration [i] = acceleration [i - 1];
		}

		velocity[0] = currentVelocity; // set the velocity and acceleration values for this frame
		acceleration[0] = velocity[0] - velocity[1];

		Vector3 velocitySum = Vector3.zero, accelerationSum = Vector3.zero;
		for (int i = 0; i < FRAMECOUNT; i++) {
			velocitySum += velocity [i]; // loop through and sum up the velocities for the past n frames
			accelerationSum += acceleration [i];
		}

		float avgVelocity = velocitySum.magnitude / FRAMECOUNT, avgAcceleration = accelerationSum.magnitude / FRAMECOUNT;

		if (canFire && avgAcceleration > 1.5f) {
			nextFireTime = Time.time + 0.5f;
			OnFire (FIRESTRENGTH * velocitySum / FRAMECOUNT);
			canFire = false;
		}

		if (avgVelocity > 2f && Time.time > nextFireTime) { // check to see if the VR player can fire
			canFire = true;
		}

		lastPosition = transform.position; // save the position for the next frame
	}

	private void OnFire(Vector3 projectileSpeed) {
		stickyHand.velocity = projectileSpeed;
	}
}
