/*
	ToggleActiveRecursively.cs
	© Mon May 22 11:52:02 CDT 2006 Graveck Interactive
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;

public class ToggleActiveRecursively : ScriptableObject
{
	[MenuItem ("Selection/Toggle Active Recursively of Selected %i", false, 400)]
	static void DoToggle()
	{
		Object[] activeGOs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Editable | SelectionMode.TopLevel);
		
		foreach (GameObject activeGO in activeGOs)
			activeGO.SetActive(!activeGO.activeInHierarchy);
	}
}
