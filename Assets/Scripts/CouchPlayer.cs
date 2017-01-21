using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CouchPlayer : MonoBehaviour {
    public Projectile projectile;
    public int playerNum;

    void Update() {
        Debug.Log(Input.GetAxis("Horizontal" + playerNum));
        Debug.Log(Input.GetAxis("Vertical" + playerNum));
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");


        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2") || Input.GetButtonDown("Fire3")) {
            Fire();
        }

        if (moveHorizontal > 0) {

        }

        if (moveVertical > 0) {

        }

    }

    public IEnumerator _TestFire() {
        while (true) {
            Fire();
            yield return new WaitForSeconds(3);
        }
    }

    public void Fire() {
        Instantiate(projectile, transform.position, transform.rotation).Fire(transform.forward);
    }
}