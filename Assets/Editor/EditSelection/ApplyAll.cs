using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ApplyAll : ScriptableObject
{

	[MenuItem ("Selection/Apply All", false, 205)]
	static void Do()
	{
		foreach (GameObject instance in Selection.gameObjects) {
			PrefabUtility.ReplacePrefab(instance, PrefabUtility.GetPrefabParent(instance), ReplacePrefabOptions.ConnectToPrefab);
		}
	}

	[MenuItem ("Selection/Revert All", false, 206)]
	static void klnklkldkfdjkjkdfjkdfDo()
	{
		foreach (GameObject instance in Selection.gameObjects) {
			PrefabUtility.RevertPrefabInstance(instance);
		}
	}


	[MenuItem ("Selection/Apply All", true, 205)]
	public static bool ValidateShowCount()
	{
		return Selection.activeGameObject;
	}


	[MenuItem ("Selection/Sort Children", false, 208)]
	public static void asdf()
	{
		GameObject parent = Selection.activeGameObject;
		List<Transform> children = new List<Transform> ();
		foreach (Transform c in parent.transform) {
			children.Add (c);
		}
		IOrderedEnumerable<Transform> cs = children.OrderBy (a => a.name);

		for (int i = 0; i < cs.Count(); i++) {
			cs.ElementAt(i).SetAsLastSibling ();
		}

	}

}

