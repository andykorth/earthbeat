using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : Script {
	private AudioSource[] audioSources;
	private int currentTrack = 0;
	private const float CROSSFADETIME = 5f;

	// Use this for initialization
	void Start () {
		audioSources = transform.GetComponentsInChildren<AudioSource> ();

		currentTrack = Random.Range (0, audioSources.Length);

		// Play first track immediately.
		audioSources [currentTrack].Play ();
		audioSources [currentTrack].volume = 1.0f;
	}

	public void PlayNextTrack() {
		int current = currentTrack;
		int nextTrack = currentTrack + 1;

		audioSources[nextTrack].Play();

		var c = AddAnimation (CROSSFADETIME, (a) => {
			audioSources[current].volume = 1-a;
			audioSources[nextTrack].volume = a;
		});
		AddDelayed (c, () => audioSources [current].Stop ());
	}


}
