using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatEffect : MonoBehaviour
{

	private float scaleMax = 160;
	private Material material;
	private float r;
	private float g;
	private float b;

	void Start()
	{
		material = gameObject.GetComponent<MeshRenderer>().material;
		r = material.GetColor(Shader.PropertyToID("_TintColor")).r;
		g = material.GetColor(Shader.PropertyToID("_TintColor")).g;
		b = material.GetColor(Shader.PropertyToID("_TintColor")).b;
	}

	// Update is called once per frame
	void Update()
	{
		transform.localScale = transform.localScale + Vector3.one*10f;
		material.SetColor(Shader.PropertyToID("_TintColor"), new Color(r, g, b, 1f - transform.localScale.x / scaleMax));

		if (transform.localScale.x > scaleMax)
			Destroy(gameObject);
	}
}
