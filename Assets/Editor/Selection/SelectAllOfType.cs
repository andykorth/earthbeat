using UnityEngine;
using UnityEditor;
using System.Collections;

public class SelectAllOfType : ScriptableWizard
{	
	public string className = "ExampleScript";
	
	[MenuItem ("Custom/Selection/Select All With Component...", false, 501)]
	public static void SelectAllOfTypeMenuIem()
	{
		ScriptableWizard.DisplayWizard("Select All of Type...", typeof(SelectAllOfType), "Select", "");
	}
	
	void OnWizardCreate()
	{
		Object[] objs = FindObjectsOfType(typeof(GameObject));
		ArrayList newSelectionBuilder = new ArrayList();
		
		foreach (GameObject go in objs)
		{
			Component comp = go.GetComponent(className);
			if (comp)
				newSelectionBuilder.Add(go);
		}
		
		GameObject[] newSelection = new GameObject[newSelectionBuilder.Count];
		
		for (int i=0; i < newSelectionBuilder.Count; i++)
			newSelection[i] = newSelectionBuilder[i] as GameObject;
		
		Selection.objects = newSelection;
	}

}
