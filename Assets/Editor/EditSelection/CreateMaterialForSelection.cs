
using UnityEngine;
using UnityEditor;
using System.Collections;

public class CreateMaterialForSelection : ScriptableWizard {
	public Material material;
	
	[MenuItem ("Selection/Create Materials For Selection...", false, 1408)]
	static void DoStuff() {
		Object[] activeGOs = Selection.GetFiltered(typeof(Texture2D), SelectionMode.Assets);
		Debug.Log("Matching selection: " + activeGOs);
		
		foreach (Texture2D t in activeGOs){
			string path = AssetDatabase.GetAssetPath(t);
			path = path.Remove(path.LastIndexOf("/") + 1);
			
			string materialPath = path + t.name +"Mat.mat";
		
			Material material = AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material)) as Material;
			
			if (material == null) {
				material = new Material (Shader.Find("VertexLit"));
				material.mainTexture = t;
				
				Debug.Log("Creating material: " + materialPath);
				AssetDatabase.CreateAsset(material, materialPath);
				EditorUtility.SetDirty(material);
			}
		}
			
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
			
	}
}
