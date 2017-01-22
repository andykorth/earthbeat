using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonScript<AudioManager> {

	public AudioClip[] lasers;
	public AudioClip[] slaps;
	public AudioClip[] swoops;
	public AudioClip[] monsters;

	public void PlayLaser(){
		PlayRandom (lasers, Random.value * 0.2f + 0.9f, 0.5f);
	}
	public void PlayMonster(){
		PlayRandom (monsters);
	}
	public void PlaySlap(){
		PlayRandom (slaps);
	}

	public void PlayRandom(AudioClip[] all){
		if (all == null) {
			return;
		}
		AudioClip c = all[Random.Range(0, all.Length)];
		PlayClip (c);	
	}

	private float nextSound = 0;
	public void Update(){
		// Random monster sounds

		if (nextSound <= 0) {
			PlayMonster ();
			nextSound = Random.value * 4.0f + 10.0f;
		}
		nextSound -= Time.deltaTime;
	}


	public static void PlayClip(AudioClip clip){
		if(clip == null) return;
		GameObject newClip = new GameObject(clip.name + " Instantiation");
		newClip.AddComponent(typeof(AudioSource));
		newClip.GetComponent<AudioSource>().clip = clip;

		newClip.GetComponent<AudioSource>().Play();
		Object.Destroy(newClip, clip.length + 0.5f);
	}

	public static void PlayClip(AudioClip clip, float pitchBend, float vol){
		GameObject newClip = new GameObject(clip.name + " Instantiation" + pitchBend);
		newClip.AddComponent(typeof(AudioSource));
		newClip.GetComponent<AudioSource>().clip = clip;
		newClip.GetComponent<AudioSource>().pitch = pitchBend;
		newClip.GetComponent<AudioSource> ().volume = vol;

		newClip.GetComponent<AudioSource>().Play();
		Object.Destroy(newClip, clip.length + 0.5f);
	}
}
