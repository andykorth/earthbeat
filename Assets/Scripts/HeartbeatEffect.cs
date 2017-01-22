using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatEffect : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.localScale = transform.localScale + Vector3.one;

		if (transform.localScale.x > 70f)
			Destroy(gameObject);
	}
}
