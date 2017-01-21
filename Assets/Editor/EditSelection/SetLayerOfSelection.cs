/*
	SetLayerOfSelection.cs
	© Wed Jun  7 15:24:15 CDT 2006 Graveck Interactive
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;

public class SetLayerOfSelection : ScriptableWizard
{
	public int layer;
	
	[MenuItem ("Selection/Set Layer of Selection %l", false, 402)]
	static void DoSet()
	{
		ScriptableWizard.DisplayWizard("Set Layer of Selection", typeof(SetLayerOfSelection), "Set");
		
	}
	
	void OnWizardUpdate()
	{
		helpString = "Set the layer of the objects in the selection to...";
	}
	
	void OnWizardCreate()
	{
		Object[] objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.ExcludePrefab | SelectionMode.Editable);
		
		foreach (GameObject go in objs)
		{
			go.layer = layer;
		}
	}	
}
