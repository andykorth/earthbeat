/*
	TransformCopyPaste, Andy Korth
*/
using UnityEngine;
using UnityEditor;

public class TransformCopyPaste : ScriptableObject
{

	private class TransformSave
	{
		public Vector3 position;
		public Quaternion rotation;
		public Vector3 localScale;
		
		public TransformSave( Vector3 position, Quaternion rotation, Vector3 localScale)
		{
			this.position = position;
			this.rotation = rotation;
			this.localScale = localScale;
		}
	}
	
	private static TransformSave pastebuffer;
	
	[MenuItem ("Custom/Copy Transform %&c", false, 485)]
	static void Copy()
	{
		Transform t = Selection.activeTransform;
		pastebuffer = new TransformSave( t.position, t.rotation,  t.localScale);
					Debug.Log("Copied: " + t.ToString());

	}

	[MenuItem ("Custom/Paste Transform %&v", false, 486)]
	static void Paste()
	{
		if(pastebuffer == null){
			Debug.Log("Your clipboard is empty.");
			return;
		}
		
		int numberApplied = 0;
		
		foreach (Transform t in Selection.transforms)
		{
			
			if (t != null && pastebuffer != null)
			{
				t.position = pastebuffer.position;
				t.rotation = pastebuffer.rotation;
				t.localScale = pastebuffer.localScale;
				numberApplied++;
			}
		}
		
		Debug.Log("Pasted to " + numberApplied + " Transforms successfully out of " + Selection.transforms.Length + ".");
	}
}
