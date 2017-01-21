/*
	BulkAssignMaterialWizard.cs
	© Fri Dec 11 16:27:14 CST 2009 Graveck Interactive LLC
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class BulkAssignShaderToMat : ScriptableWizard {
	public Shader changeToShader;
	public string shaderName;
	
	[MenuItem ("Selection/Bulk Shader Assign To Material...", false, 404)]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard("Bulk Material Assign...", typeof(BulkAssignShaderToMat), "Apply");
	}
	
	void OnWizardCreate() {
		if(changeToShader == null){
			changeToShader = Shader.Find(shaderName);
			Debug.Log("Shader from name \"" + shaderName + "\" is: " + changeToShader.name);
		}
		
		Object[] mats = Selection.GetFiltered(typeof(Material), SelectionMode.Deep);
		
		
		foreach (Material m in mats) {
			m.shader = changeToShader;
		}
	}
	
		
}
