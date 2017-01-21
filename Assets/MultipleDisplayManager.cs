using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleDisplayManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int displayCount = 0;
		foreach (Display d in Display.displays) {
			displayCount += 1;
			d.Activate ();
		}
		Debug.Log ("Active displays: " + displayCount);
		if (displayCount <= 1) {
			Debug.LogError ("EarthBeat requires a vive display and a secondary display. It won't work well!");
		}
	}
	

}
