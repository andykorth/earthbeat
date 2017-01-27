using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class WaterMovement : MonoBehaviour {
    private MeshRenderer _renderer;
    void Start() {
        _renderer = GetComponent<MeshRenderer>();
    }
	void Update () {
	    var offset = _renderer.material.mainTextureOffset;
	    _renderer.material.mainTextureOffset = new Vector2(
	        Mathf.Lerp(offset.x, offset.x + 0.001f/2, Time.time),
	        0f
	    );

	    var y = Mathf.Lerp(transform.position.y, transform.position.y + Mathf.Sin(Time.time)/10, Time.deltaTime/2);
	    var tidePos = new Vector3(transform.position.x, y, transform.position.z);
	    transform.position = tidePos;
	}
}
