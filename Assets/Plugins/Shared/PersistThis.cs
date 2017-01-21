using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistThis : Script {

	public void Awake() {
		DontDestroyOnLoad(this.gameObject);
	}		

}