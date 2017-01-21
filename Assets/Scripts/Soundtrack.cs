using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
	private AudioSource[] audioSources;
	private int currentTrack = 0;

	// Use this for initialization
	void Start () {
		audioSources = transform.GetComponentsInChildren<AudioSource> ();
		audioSources [currentTrack].Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
