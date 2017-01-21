using UnityEngine;
using UnityEditor;
using System.Collections;

public class PrefabReplace : ScriptableWizard
{

	[MenuItem ("Selection/Replace with Prefabs...", false, 200)]
	static void Do()
	{
		ScriptableWizard.DisplayWizard("Prefab Replacer ", typeof(PrefabReplace), "Replace!");
	}
	
	[MenuItem ("Selection/Replace with Prefabs...", true, 200)]
	public static bool ValidateShowCount()
	{
		return Selection.activeGameObject;
	}
	
	public GameObject replaceWith;
	
	void OnWizardUpdate() {
		if (replaceWith == null){
			errorString = "Drag prefab into the box thing";
		}
	}
	
	void OnWizardCreate(){
		foreach (Transform transform in Selection.transforms)
		{
			GameObject clone = (GameObject) PrefabUtility.InstantiatePrefab(replaceWith);
			clone.transform.position = transform.position;
			clone.transform.rotation = transform.rotation;
			clone.transform.localScale = transform.localScale;
			clone.transform.parent = transform.parent;

			DestroyImmediate(transform.gameObject);
		}

		
	}
	
}

