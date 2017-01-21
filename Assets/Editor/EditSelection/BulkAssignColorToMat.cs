/*
	BulkAssignMaterialWizard.cs
	© Fri Dec 11 16:27:14 CST 2009 Graveck Interactive LLC
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class BulkAssignColorToMat : ScriptableWizard {
	public Color changeToColor;
	
	[MenuItem ("Selection/Bulk Color Assign To Material...", false, 404)]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard("Bulk Color Assign...", typeof(BulkAssignColorToMat), "Apply");
	}
	
	void OnWizardCreate() {
		Object[] mats = Selection.GetFiltered(typeof(Material), SelectionMode.Deep);
		
		foreach (Material m in mats) {
			m.color = changeToColor;
		}
	}
}
