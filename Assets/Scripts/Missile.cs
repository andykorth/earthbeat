using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	void FixedUpdate () {
        transform.RotateAround (transform.position, transform.parent.forward, 5f);
	}
}
