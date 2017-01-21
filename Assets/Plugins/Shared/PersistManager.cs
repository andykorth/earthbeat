using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistManager : SingletonScript<PersistManager> {

	public string versionString;
	public string buildDateString;

	public GameObject[] requiredManagerPrefabs;	 
	public GameObject[] requiredPerSceneManagerPrefabs;	 

	public static bool madeThem = false;

	static List<GameObject> instances;

	public void Awake() {
		Application.targetFrameRate = 60;

		if(!madeThem){
			instances = new List<GameObject>();
			foreach (GameObject prefab in requiredManagerPrefabs) {
				GameObject instance = Instantiate(prefab);

				instance.name = "+" + prefab.name;
				instances.Add(instance);
				DontDestroyOnLoad(instance);
			}					
			madeThem = true;
		}

		foreach (GameObject prefab in requiredPerSceneManagerPrefabs) {
			GameObject instance = Instantiate(prefab);
			instance.name = "-" + prefab.name;
		}				
	}		

	public static void ClearAll () {
		madeThem = false;
		foreach(GameObject ins in instances) {
			Destroy(ins);
		}
	}
}