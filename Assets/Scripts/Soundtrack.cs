using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour {
	private AudioSource[] audioSources;
	private int currentTrack = 0;
	private const float CROSSFADETIME = 5f;

	// Use this for initialization
	void Start () {
		audioSources = transform.GetComponentsInChildren<AudioSource> ();
		audioSources [currentTrack].Play ();
		StartCoroutine (PlayNextTrack ());
	}

	IEnumerator PlayNextTrack() {
		while (true) {
			yield return new WaitForSeconds (audioSources [currentTrack].clip.length - CROSSFADETIME);

			//float fadeTime = Time.time;

			//while (

			currentTrack = (currentTrack + 1) % audioSources.Length;
			audioSources [currentTrack].Play ();
		}
	}



}
