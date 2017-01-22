using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonScript<AudioManager> {

	public AudioClip[] lasers;
	public AudioClip[] slaps;
	public AudioClip[] swoops;

	public void PlayLaser(){
		PlayRandom (lasers);
	}

	public void PlayRandom(AudioClip[] all){
		if (all == null) {
			return;
		}
		AudioClip c = all[Random.Range(0, all.Length)];
		PlayClip (c);	
	}



	public static void PlayClip(AudioClip clip){
		if(clip == null) return;
		GameObject newClip = new GameObject(clip.name + " Instantiation");
		newClip.AddComponent(typeof(AudioSource));
		newClip.GetComponent<AudioSource>().clip = clip;

		newClip.GetComponent<AudioSource>().Play();
		Object.Destroy(newClip, clip.length + 0.5f);
	}

	public static void PlayClip(AudioClip clip, float pitchBend){
		GameObject newClip = new GameObject(clip.name + " Instantiation" + pitchBend);
		newClip.AddComponent(typeof(AudioSource));
		newClip.GetComponent<AudioSource>().clip = clip;
		newClip.GetComponent<AudioSource>().pitch = pitchBend;

		newClip.GetComponent<AudioSource>().Play();
		Object.Destroy(newClip, clip.length + 0.5f);
	}
}
