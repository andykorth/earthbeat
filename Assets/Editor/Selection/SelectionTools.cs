/*
SelectionTools, compiled by Andy Korth. Most of it originally by Jonathan Czeck
Graveck Interactive
*/
using UnityEngine;
using UnityEditor;
using System.Collections;

public class SelectionTools : ScriptableObject
{

//	[MenuItem ("Custom/Selection/Deselect _a", false, 499)]
//	static void DoDeselect()
//	{
//		Selection.activeObject = null;
//	}
	
	[MenuItem ("Custom/Selection/Select All Cameras", false, 503)]
	static void SelectCameras()
	{
		Camera[] cameras = FindObjectsOfType(typeof(Camera)) as Camera[];
		GameObject[] gos = new GameObject[cameras.Length];
		
		for (int i=0; i < cameras.Length; i++)
		{
			gos[i] = (cameras[i]).gameObject;
		}
		
		Selection.objects = gos;
	}

	[MenuItem ("Custom/Selection/Select All AudioListeners", false, 505)]
	static void SelectAudioListeners()
	{
		AudioListener[] cameras = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
		GameObject[] gos = new GameObject[cameras.Length];
		
		for (int i=0; i < cameras.Length; i++)
		{
			gos[i] = (cameras[i]).gameObject;
		}
		
		Selection.objects = gos;
	}

	[MenuItem ("Custom/Selection/Select All Lights", false, 506)]
	static void SelectLights()
	{
		Light[] lights = FindObjectsOfType(typeof(Light)) as Light[];
		GameObject[] gos = new GameObject[lights.Length];
		
		for (int i=0; i < lights.Length; i++)
		{
			gos[i] = (lights[i]).gameObject;
		}
		
		Selection.objects = gos;
	}

	
	[MenuItem ("Custom/Selection/Select Main Camera ^c", false, 504)]
	static void Select1()
	{
		if (Camera.main != null)
			Selection.activeObject = Camera.main.gameObject;
	}
	
//	[MenuItem ("Custom/Selection/Select Manager Object _m", false, 509)]
//	static void Select2()
//	{
//		GameObject obj = GameObject.Find("*Managers");
//		
//		if (obj != null)
//			Selection.activeObject = obj;
//	}
//	
//	[MenuItem ("Custom/Selection/Select Parent _g", false, 500)]
//	static void DoSelectParent()
//	{
//		Selection.activeObject = ((GameObject)Selection.activeObject).transform.parent.gameObject;
//	}
//		
//	
//	[MenuItem ("Selection/Report Selection Length... _c", false, 550)]
//	public static void ReportSelectionLength() {
//		EditorUtility.DisplayDialog("Selection Length", Selection.objects.Length + "", "OK");
//	}
	
		
	[MenuItem ("Selection/Print Prefab Parent", false, 551)]
	public static void ReportSelectionLengaaaaaath() {
		EditorUtility.DisplayDialog("GetPrefabParent: ", PrefabUtility.GetPrefabParent(Selection.activeObject) + "", "OK");
	}
	
	[MenuItem ("Selection/Print selection", false, 551)]
	public static void asdfasdfnuinvui9uho9() {
		string s = "";
		foreach(UnityEngine.Object go in Selection.objects){
			s += go.name + "\n";	
		}
		Debug.Log("Selected: " + s);
	}
	
	[MenuItem ("Custom/Selection/Debug Log Current Selected Asset's Path", false, 551)]
	static void DoPrintLog()
	{
		Debug.Log(AssetDatabase.GetAssetPath(Selection.activeObject));
	}
	
	
	[MenuItem ("Custom/Selection/Select Textures Used In Selection", false, 508)]
	static void DoSelect()
	{
		Object[] objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep);
		
		ArrayList texturesList = new ArrayList();
		
		foreach (GameObject go in objs)
		{
			// look at each possible built in component's properties for textures
			// There are limitations preventing this from working properly with the
			// LineRenderer component and components with Flare assets.
			
			if (go.GetComponent<Camera>() != null)
			{
				if (go.GetComponent<Camera>().targetTexture != null)
					texturesList.Add(go.GetComponent<Camera>().targetTexture);
			}
			
			if (go.GetComponent<Renderer>() != null)
			{
				foreach (Material material in go.GetComponent<Renderer>().sharedMaterials)
				{
					AddTexturesInMaterialToList(ref texturesList, material);
				}
			}
			
			GUITexture myGUIText = go.GetComponent(typeof(GUITexture)) as GUITexture;
			
			if (myGUIText)
			{
				if (myGUIText.texture != null)
					texturesList.Add(myGUIText.texture);
			}
			
			if (go.GetComponent<GUIText>() != null)
			{
				if (go.GetComponent<GUIText>().material != null)
					AddTexturesInMaterialToList(ref texturesList, go.GetComponent<GUIText>().material);
				
				if (go.GetComponent<GUIText>().font != null)
				{
					if (go.GetComponent<GUIText>().font.material != null)
						AddTexturesInMaterialToList(ref texturesList, go.GetComponent<GUIText>().font.material);
				}
			}
			
			LensFlare myFlare = go.GetComponent(typeof(LensFlare)) as LensFlare;
			
			if (myFlare)
			{
				// unfortunately we can't get at a Flare's texture as of 1.5,
				// so we select the Flare asset instead.
				if (myFlare.flare != null)
					texturesList.Add(myFlare.flare);
			}
			
			if (go.GetComponent<Light>() != null)
			{
				if (go.GetComponent<Light>().cookie != null)
					texturesList.Add(go.GetComponent<Light>().cookie);
				
				// unfortunately we can't get at a Flare's texture as of 1.5,
				// so we select the Flare asset instead.
				if (go.GetComponent<Light>().flare != null)
					texturesList.Add(go.GetComponent<Light>().flare);
			}
			
			Projector myProjector = go.GetComponent(typeof(Projector)) as Projector;
			
			if (myProjector)
			{
				if (myProjector.material != null)
					AddTexturesInMaterialToList(ref texturesList, myProjector.material);
			}
			
			Skybox mySkybox = go.GetComponent(typeof(Skybox)) as Skybox;
			
			if (mySkybox)
			{
				if (mySkybox.material != null)
					AddTexturesInMaterialToList(ref texturesList, mySkybox.material);
			}
			
			// Unfortunately, we can't get at a LineRenderer's materials at all! Whoops!
			
			TextMesh myTextMesh = go.GetComponent(typeof(TextMesh)) as TextMesh;
			
			if (myTextMesh)
			{
				if (myTextMesh.font != null)
				{
					if (myTextMesh.font.material != null)
						AddTexturesInMaterialToList(ref texturesList, myTextMesh.font.material);
				}
			}
		}
		
		Object[] newSelection = new Object[texturesList.Count];
		
		for (int i=0; i< texturesList.Count; i++)
			newSelection[i] = texturesList[i] as Object;
		
		Selection.objects = newSelection;
	}
	
	static void AddTexturesInMaterialToList(ref ArrayList list, Material material)
	{
		// look for textures found in the built in shaders
		string[] builtInShadersTextureNames =
			{"_MainTex", "_BumpMap", "_Detail", "_ColorControl", "_ColorControlCube",
				"_ReflectionTex", "_RefractionTex", "_Fresnel", "_LightMap",
				"_Cube", "_FrontTex", "_BackTex", "_LeftTex", "_RightTex",
				"_UpTex", "_DownTex", "_Tex"};
		
		foreach (string textureName in builtInShadersTextureNames)
		{
			if (material.GetTexture(textureName) != null)
				list.Add(material.GetTexture(textureName));
		}
	}
	
	
	
}
