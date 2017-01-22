using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateDespawner : MonoBehaviour {

	private float despawnTime = 5f;
	//private Transform[] children;

	void Start()
	{
		//children = gameObject.GetComponentsInChildren<Transform>(true);
	}

	// Update is called once per frame
	void Update ()
	{
		despawnTime -= Time.deltaTime;
		if (despawnTime < 0)
		{
			gameObject.transform.localScale -= Vector3.one * .015f;
			if (gameObject.transform.localScale.x <= 0)
			{
				Destroy(gameObject);
			}
		}
		
		// Ineffective attempt at shrinking individual pieces
		/*if (despawnTime < 0)
		{
			foreach (Transform child in children)
			{
				child.localScale -= Vector3.one * .01f;
				if (child.localScale.x <= 0)
				{
					Destroy(child.gameObject);
				}
			}

			if (children.Length == 0)
				Destroy(gameObject);
		}*/
	}
}
