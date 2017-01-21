/*
	BulkAssignMaterialWizard.cs
	© Fri Dec 11 16:27:14 CST 2009 Graveck Interactive LLC
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class BulkAssignMaterialWizard : ScriptableWizard {
	public Material material;
	
	[MenuItem ("Selection/Bulk Material Assign...", false, 405)]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard("Bulk Material Assign...", typeof(BulkAssignMaterialWizard), "Apply");
	}
	
	void OnWizardCreate() {
		Object[] meshRenderers = Selection.GetFiltered(typeof(MeshRenderer), SelectionMode.Deep);
		
		foreach (MeshRenderer meshRenderer in meshRenderers) {
			Material[] materials = new Material[meshRenderer.sharedMaterials.Length];
			for (int i=0; i < materials.Length; i++) {
				materials[i] = material;
			}
			meshRenderer.sharedMaterials = materials;
		}
	}
}
