using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private int damage = 2;
    public float speed = 50f;


    void Awake() {
        Destroy(this.gameObject, 10);
    }

    public int getDamage() {
        return damage;
    }

    public void Fire(Vector3 startPosition) {
        GetComponent<Rigidbody>().velocity = startPosition * speed;

    }

    public void Fire(Vector3 startPosition, float customSpeed) {
        GetComponent<Rigidbody>().AddForce(startPosition * customSpeed);
    }

    public void OnCollisionEnter() {
        Destroy(this.gameObject);
        //TODO: effects!
    }

}
