using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private int damage = 2;
    public float speed = 5f;
    private GameObject sender;

    [SerializeField] private GameObject explosion;

    void Awake() {
        Destroy(this.gameObject, 8);
    }

    public int getDamage() {
        return damage;
    }

    public void Fire(GameObject sender, Vector3 startPosition) {
        this.sender = sender;
        GetComponent<Rigidbody>().velocity = startPosition * speed;

		// Eventually destroy bullets:
		Destroy(this.gameObject, 20.0f);
    }

//    public void Fire(Vector3 startPosition, float customSpeed) {
//        GetComponent<Rigidbody>().AddForce(startPosition * customSpeed);
//    }


    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetInstanceID() != sender.GetInstanceID()) {
            Destroy(this.gameObject);
            GameObject exposionInst = Instantiate(explosion, transform.position, transform.rotation);
            Destroy(exposionInst, 1);
        }

        //TODO: effects!
    }

	public void OnTriggerEnter(Collider other) {
		if (other.tag == "Polyp") {
			// We hit the big bad guy!
			Destroy(this.gameObject);
			GameObject exposionInst = Instantiate(explosion, transform.position, transform.rotation);
			Destroy(exposionInst, 1);

			GameManager.Instance.VRPlayerTookDamage ();

			HeartRateMonitor.ReceiveChangeOfRate(1);
		}
	}

}
