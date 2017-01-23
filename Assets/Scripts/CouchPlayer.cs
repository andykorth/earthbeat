using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rewired;

public class CouchPlayer : MonoBehaviour {
    public bool dead;

    public GameObject projectile;
    public int playerNum;
	public GameObject spawnPosition1;
	public GameObject spawnPosition2;
	public GameObject explosion;
//    private GameObject actualShip;

	public MeshRenderer recolorableSection;
	public int recolorableMaterialIndex;
	public Transform rollTransform;

	public float verticalRotationSpeed = 2.0f;
	public float horizontalRotationSpeed = 4.0f;

	// The Rewired Player, used for input for this specific controller.
	private Player player;

    public float tilt = 2f;

    private Rigidbody rb;

    // We probably want to vary the speed, but for now it's constant:
    public float planeCurrentSpeed = 15.0f;
    private Quaternion baseRotation;
    private Camera cam;
    private float length;
    private Camera trackingCamera;
    private GameObject trackedObject;
    private bool canShoot;
    private GameObject canvas;
    private float SHOOT_TIMER = 0.25f;
    private float RELOAD_TIMER = 1.75f;
    public int shotChamber = 6;
    private readonly int MAX_SHOTS = 6;
    private readonly float OVERHEAT_TIMER = 2.2f;


    private IEnumerator respawnCoroutine;
    private IEnumerator reloadCoroutine;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private int INVINCIBILITY_TIMER = 2;
    private bool invincible;

    private float DASH_COOLDOWN_TIMER = 0.75f;
    private bool canDash = true;
    private bool dashed;
    private int DEFAULT_DASH_MODIFIER = 10;
    private int dashModifier = 10;
    private float DASH_TIMER = 0.25f;


    public void SetupController(int playerNum) {
        dead = false;
        invincible = true;
        this.playerNum = playerNum;
        canShoot = true;
        StartInvincibilityTimer();

        trackingCamera = GameObject.Find("Camera" + (playerNum + 1)).GetComponent<Camera>();
        initialPosition = trackingCamera.transform.localPosition;
        initialRotation = trackingCamera.transform.rotation;
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

	public void SetColor(Color c){
//		Material m = recolorableSection.materials [recolorableMaterialIndex];
//		m.color = c;
//		recolorableSection.materials[recolorableMaterialIndex] = m;
	}


    void FixedUpdate() {
        if (dead) return;
		// Physics / Motion updates are best suited for the Fixed Update loop
		float moveHorizontal = player.GetAxis("Horiz") * (horizontalRotationSpeed) * Time.deltaTime;
		float moveVertical = player.GetAxis("Vert") * (verticalRotationSpeed) * Time.deltaTime;
		float rightMoveVertical = player.GetAxis("RightVert") * verticalRotationSpeed * Time.deltaTime;

	    bool fire = player.GetButtonDown("Fire");

        if (canDash && player.GetButtonDown("Dash")) {
            dashed = true;
            canDash = false;
            StartDashTimer();
            StartDashCooldownTimer();
        }

        if (fire && shotChamber > 0 && canShoot) {
            Fire();
        }

		Quaternion lookRot = Quaternion.LookRotation (GameManager.Instance.VRPlayer.transform.position - this.transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 2f);
		

        Vector3 movement = new Vector3 (moveHorizontal, rightMoveVertical,  moveVertical);
	    movement = transform.rotation * movement;
	    rb.velocity = movement * planeCurrentSpeed * (dashed ? Mathf.Max(dashModifier--, 1) : 1);
        trackingCamera.transform.position = Vector3.Lerp(trackingCamera.transform.position, trackedObject.transform.position, Time.deltaTime * 7);
        trackingCamera.transform.rotation = trackedObject.transform.rotation;

		// Adjust the roll of the ship just for visual fun:
		roll = Util.ConstantLerp(roll, player.GetAxis("Horiz") * 30.0f, Time.deltaTime * 100.0f);
		rollTransform.localRotation = Quaternion.Euler(0f, 180f, roll);
    }
	private float roll = 0f;

	private bool useSpawnPoint1 = true;
    public void Fire() {
        shotChamber = shotChamber - 1;
        canShoot = false;
        ShootInterval();
        AudioManager.i.PlayLaser ();
		Transform spawn = useSpawnPoint1 ? spawnPosition1.transform : spawnPosition2.transform;
		useSpawnPoint1 = !useSpawnPoint1;

		Destroy (Instantiate (ParticleManager.i.muzzleSparks, spawn.position, spawn.rotation), 2f);

		Vector3 direction = spawn.forward; //Get a direction vector to fire the bullet at.
        //direction.Normalize(); // direction vector normalized to magnitude 1
		GameObject missile = Instantiate(projectile, spawn.position, spawn.rotation);
		missile.GetComponent<Projectile>().Fire(this.gameObject, direction);
        foreach (var collider in GetComponents<Collider>()) {
            Physics.IgnoreCollision(missile.GetComponentInChildren<Collider>(), collider);
        }
        Reload();

    }

	public void OnTriggerEnter(Collider other) {
		if (other.tag == "LandingZone") {

			if (dead || invincible) return;

			Debug.Log("Player " +this.playerNum+ " landed!");
			GameManager.Instance.DroneLanded ();
			AudioManager.i.PlayerLanded ();

			GameObject go = Instantiate(ParticleManager.i.plasmaExplosion, transform.position, transform.rotation);
			Destroy(go, 10);

			StartRespawnTimer ();
		}
	}

    public void OnCollisionEnter(Collision collision) {
        if (dead || invincible) return;
        GameObject go = Instantiate(explosion, transform.position, transform.rotation);
		AudioManager.i.PlayLargeExplosion ();
        Destroy(go, 4);
		rollTransform.gameObject.SetActive(false);
        StartRespawnTimer();

		GameManager.Instance.DroneDied ();
    }


    #region Timers
    private void StartRespawnTimer() {
        dead = true;
        respawnCoroutine = _Respawn();
        StartCoroutine(respawnCoroutine);
    }

    private IEnumerator _Respawn() {
        yield return new WaitForSeconds(2f);
        canvas.SetActive(true);
        trackingCamera.transform.position = initialPosition;
        trackingCamera.transform.rotation = initialRotation;
        GameManager.Instance.CouchPlayerDied(this.playerNum);
        Destroy(this.gameObject);
    }

    private void Reload() {
        if (shotChamber < MAX_SHOTS) {
            reloadCoroutine = _Reload();
            StartCoroutine(reloadCoroutine);
        }
    }

    private IEnumerator _Reload() {
        yield return new WaitForSeconds(shotChamber == 0 ? OVERHEAT_TIMER : RELOAD_TIMER);
        if (shotChamber < MAX_SHOTS) {
            shotChamber = shotChamber + 1;
        }

    }

    private void ShootInterval() {
        StartCoroutine(_ShootInterval());
    }

    private IEnumerator _ShootInterval() {
        yield return new WaitForSeconds(SHOOT_TIMER);
        canShoot = true;
    }

    private void StartInvincibilityTimer() {
        StartCoroutine(_StartInvincibilityTimer());
    }

    private IEnumerator _StartInvincibilityTimer() {
        yield return new WaitForSeconds(INVINCIBILITY_TIMER);
        invincible = false;
    }

    private void StartDashTimer() {
        StartCoroutine(_StartDashTimer());
    }

    private IEnumerator _StartDashTimer() {
        yield return new WaitForSeconds(DASH_TIMER);
        dashed = false;
        dashModifier = DEFAULT_DASH_MODIFIER;
    }

    private void StartDashCooldownTimer() {
        StartCoroutine(_StartDashCooldownTimer());
    }

    private IEnumerator _StartDashCooldownTimer() {
        yield return new WaitForSeconds(DASH_COOLDOWN_TIMER);
        canDash = true;
    }
    #endregion

}