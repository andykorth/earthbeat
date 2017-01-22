using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneUIManager : SingletonScript<DroneUIManager> {

	public Text kills, landings;

	public void UpdateUI(int killCount, int landingCount){
		kills.text = "Deaths: " + killCount;
		landings.text = "Landings: " + landingCount;
	}
}
