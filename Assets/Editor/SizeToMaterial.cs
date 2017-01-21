
using UnityEngine;
using UnityEditor;
using System.Collections;

public class SizeToMaterial : ScriptableWizard {

	[MenuItem ("Selection/Autosize To Texture %;")]
	public static void AutoSizeToTexture()
	{
		foreach (Transform transform in Selection.transforms)
		{
			Texture targetTexture = transform.gameObject.GetComponent<Renderer>().sharedMaterial.mainTexture;
			float width = targetTexture.width;
			float height = targetTexture.height;
			transform.localScale = new Vector3(width / height, 1.0f, 1.0f);
		}
	}


	[MenuItem ("Selection/Size To Texture")]
	static void Do2()
	{
		ScriptableWizard.DisplayWizard("This changes the selected go's transform's local size to match the aspect ratio of targeted texture ", typeof(SizeToMaterial), "Resize!");
	}
	
	public Texture targetTexture;
	
	void OnWizardUpdate() {
		if (targetTexture == null){
			errorString = "Put a texture in the hole above.";
		}
	}
	
	void OnWizardCreate(){
		foreach (Transform transform in Selection.transforms)
		{
			float width = targetTexture.width;
			float height = targetTexture.height;
			transform.localScale = new Vector3(width / height, 1.0f, 1.0f);
		}
	}

		
}

