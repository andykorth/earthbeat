using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatEffect : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.localScale = transform.localScale + Vector3.one * 15f;

		if (transform.localScale.x > 400f)
			Destroy(gameObject);
	}
}
