using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private int damage = 2;
    public float speed = 5f;
    private GameObject sender;

    void Awake() {
        Destroy(this.gameObject, 10);
    }

    public int getDamage() {
        return damage;
    }

    public void Fire(GameObject sender, Vector3 startPosition) {
        this.sender = sender;
        GetComponent<Rigidbody>().velocity = startPosition * speed;
    }

    public void Fire(Vector3 startPosition, float customSpeed) {
        GetComponent<Rigidbody>().AddForce(startPosition * customSpeed);
    }

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject != sender) {
            Destroy(this.gameObject);
        }

        //TODO: effects!
    }

}
