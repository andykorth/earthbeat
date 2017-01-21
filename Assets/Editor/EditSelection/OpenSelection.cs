
using UnityEngine;
using UnityEditor;

public class OpenSelection : ScriptableObject
{
	[MenuItem ("Selection/Open Selection ^o", false, 1)]
	static void DoOpenSelection()
	{
		foreach (Object o in Selection.objects){		
			AssetDatabase.OpenAsset(o);
		}
	}
	
	[MenuItem ("Selection/Open Selection ^o", true, 1)]
	static bool ValidateGroupObjectsCommand() {
		foreach (Object o in Selection.objects){		
			if(AssetDatabase.Contains(o)){
				return true;
			}
		}
		return false;
	}
	
}
