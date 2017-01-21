using UnityEngine;
using UnityEditor;
using System.Collections;

public class GroupObjects : ScriptableObject {

	[MenuItem("Selection/Group Selected GameObjects %g", false, 300)]
	static void GroupObjectsCommand() {
		GameObject mainSelection = Selection.activeGameObject;
		Transform selectionsParent = mainSelection.transform.parent;
		
		Object[] objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Editable | SelectionMode.ExcludePrefab);
		
		GameObject newParent = new GameObject(mainSelection.name + " Group");

		Vector3 sum = Vector3.zero;
		foreach (GameObject go in objs) {
			sum += go.transform.position;
		}
		newParent.transform.position = sum / objs.Length;

		foreach (GameObject go in objs){
			go.transform.SetParent(newParent.transform, true);
		}

		newParent.transform.SetParent(selectionsParent, true);
		Selection.activeGameObject = newParent;
	}
	
	[MenuItem("Selection/Group Selected GameObjects %g", true, 300)]
	static bool ValidateGroupObjectsCommand() {
		Object[] objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep | SelectionMode.Editable | SelectionMode.ExcludePrefab);
		
		return objs != null && objs.Length > 0;
	}

	
	[MenuItem ("GameObject/Create Child GameObject %#m", false, 301)]
	static void doItMore()
	{
		GameObject child = new GameObject();
		child.transform.parent = ((GameObject)Selection.activeObject).transform;
		
		child.transform.localPosition = Vector3.zero;
		child.transform.rotation = Quaternion.identity;
		
		Selection.activeObject = child;
	}


}
