using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
	void Update () {
	    transform.RotateAround(transform.parent.forward, Time.deltaTime * 4);
	}
}
