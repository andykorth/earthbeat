using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayGameButton : MonoBehaviour {

	public void PlayNow(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainVR");
	}

	public void Update(){
		// Watch for JoinGame action in each Player
		for(int i = 0; i < ReInput.players.playerCount; i++) {
			if(ReInput.players.GetPlayer(i).GetButtonDown("JoinGame")) {
				PlayNow ();
			}
		}
	}
}
