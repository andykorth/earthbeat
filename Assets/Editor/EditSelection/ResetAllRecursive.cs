
using UnityEngine;
using UnityEditor;

public class ResetAllRecursive : ScriptableObject
{
	/*
	[MenuItem ("Selection/Recursively Revert All To Prefab", false, 401)]
	static void DoToggle()
	{
	
		Object[] activeGOs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Editable);
		
		foreach (GameObject activeGO in activeGOs)
			foreach (Transform t in activeGO.transform)
				EditorUtility.ResetGameObjectToPrefabState(t.gameObject);
	}
	*/
}
