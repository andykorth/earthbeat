using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayGameButton : MonoBehaviour {

	public void PlayNow(){
		UnityEngine.SceneManagement.SceneManager.LoadScene ("MainVR");
	}
}
