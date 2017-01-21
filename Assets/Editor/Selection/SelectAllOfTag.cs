using UnityEngine;
using UnityEditor;
using System.Collections;

public class SelectAllOfTag : ScriptableWizard
{	
	public string tagName = "ExampleTag";
	public bool reportNumberSelected = false;
	
	[MenuItem ("Custom/Selection/Select All of Tag...", false, 502)]
	public static void SelectAllOfTypeMenuIem()
	{
		ScriptableWizard.DisplayWizard("Select All of Tag...", typeof(SelectAllOfTag), "Select", "");
	}
	
	void OnWizardCreate()
	{
		GameObject[] gos = GameObject.FindGameObjectsWithTag(tagName);
		Selection.objects = gos;
		
		if (reportNumberSelected) {
			EditorUtility.DisplayDialog("Report of tag " + tagName, gos.Length + " objects selected.", "OK");
		}
	}
}
