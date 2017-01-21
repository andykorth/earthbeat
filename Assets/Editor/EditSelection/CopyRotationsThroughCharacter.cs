using UnityEngine;
using UnityEditor;
using System.Collections;

public class CopyRotationsThroughCharacter : ScriptableWizard
{

	[MenuItem ("Selection/Copy Rotations Through Characters...", false, 200)]
	static void Do()
	{
		ScriptableWizard.DisplayWizard("Rotation Copier ", typeof(CopyRotationsThroughCharacter), "Do ze copy!");
	}
	
	[MenuItem ("Selection/Copy Rotations Through Characters...", true, 200)]
	public static bool ValidateShowCount()
	{
		return Selection.activeGameObject;
	}
	
	public GameObject copyFrom;
	
	void OnWizardUpdate() {
		if (copyFrom == null){
			errorString = "Drag source object parent into the magic box";
		}
	}
	
	void OnWizardCreate(){		
		foreach (Transform dest in Selection.transforms) {
			Recurse(dest, copyFrom.transform);
		}
	}
	
	void Recurse(Transform destP, Transform sourceP){
		foreach (Transform dest in destP) {
			foreach(Transform source in sourceP){
				if(dest.gameObject.name == source.gameObject.name){
					Debug.Log("Copying from " + dest.gameObject.name + " the rotations.");
					dest.localRotation = source.localRotation;
					Recurse(dest, source);
				}
			}
		}

		
	}
	
}

