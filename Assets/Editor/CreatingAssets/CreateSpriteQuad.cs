using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CreateSpriteQuad :ScriptableWizard {

	public Object meshPrefab = AssetDatabase.LoadMainAssetAtPath("Assets/Shared/Misc/Quad.fbx");

	[MenuItem ("Custom/Create Sprite Quad...", false, 405)]
	static void CreateWizard() {
		ScriptableWizard.DisplayWizard("Create Sprite Quad...", typeof(CreateSpriteQuad), "Make it so");
		
	}
	
	void OnWizardCreate() {
    	foreach(UnityEngine.Object s in Selection.GetFiltered(typeof(Texture), SelectionMode.Assets)){
			Texture texture = s as Texture;
			if(texture == null) continue;
			
			string materialPath = AssetDatabase.GetAssetPath(texture)+ ".mat";		
			Material m  = null;
			// Dont overwrite existing materials, as we would
			// lose existing material settings.
			if (!File.Exists(materialPath)){
			
				m = new Material(Shader.Find("Unlit/Transparent"));
				m.SetTexture("_MainTex", texture);
			    AssetDatabase.CreateAsset(m, materialPath);
			}else{
				m = (Material) AssetDatabase.LoadAssetAtPath(materialPath, typeof(Material) );	
			} 
			GameObject go = new GameObject(texture.name + "Sprite");
			MeshRenderer mr = go.AddComponent<MeshRenderer>();
			MeshFilter mf = go.AddComponent<MeshFilter>();
			mf.mesh = ((GameObject)meshPrefab).GetComponent<MeshFilter>().sharedMesh;
			mr.sharedMaterial = m;
			
			Selection.activeGameObject = go;
			Selection.activeTransform = go.transform;
			
			SizeToMaterial.AutoSizeToTexture();
			go.transform.localScale = 10f * go.transform.localScale;
			go.transform.rotation = Quaternion.Euler(0f,180f,0f);
		}
	}

}
