
using UnityEngine;
using UnityEditor;

public class ChangeMeshImportSettings : ScriptableObject
{
	
	public delegate void ChangeMesh(ModelImporter import);
	
	public static void OptimizeMeshSelection(ChangeMesh c){
		
		
		AssetDatabase.StartAssetEditing();
			

		foreach(Object obj in Textures()){
			string path = AssetDatabase.GetAssetPath(obj);
			ModelImporter import = AssetImporter.GetAtPath(path) as ModelImporter;
			
			if(import == null) continue;
			
			Debug.Log("Settings for: " + path);
			
			c(import);
			
			EditorUtility.SetDirty(obj);
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		
		EditorUtility.DisplayCancelableProgressBar("Optimizing stuff...", "Still gotta refresh, but hit revert when it asks.", 0.99f);
			
		AssetDatabase.StopAssetEditing();
		//AssetDatabase.Refresh();
		
		EditorUtility.ClearProgressBar();
		
	}
	
	[MenuItem ("Custom/Optimization/Mesh Keyframe Reduction Off", false, 2004)]
	public static void DoThingy1() {
		OptimizeMeshSelection(import => import.animationCompression = ModelImporterAnimationCompression.Off);
	}
		
	[MenuItem ("Custom/Optimization/Mesh Keyframe Reduction to 0.0", false, 2005)]
	public static void DoThingy2() {
		OptimizeMeshSelection(import => {
			import.animationCompression = ModelImporterAnimationCompression.KeyframeReduction;
			import.animationPositionError = import.animationRotationError = import.animationScaleError = 0.0f;
		});
	}
		
	[MenuItem ("Custom/Optimization/Mesh Keyframe Reduction to 0.5", false, 2007)]
	public static void DoThingy3() {
		OptimizeMeshSelection(import => {
			import.animationCompression = ModelImporterAnimationCompression.KeyframeReduction;
			import.animationPositionError = import.animationRotationError = import.animationScaleError = 0.5f;
		});
	}
			
	[MenuItem ("Custom/Optimization/Mesh Keyframe Reduction to 0.01", false, 2006)]
	public static void DoThing345dfsgy3() {
		OptimizeMeshSelection(import => {
			import.animationCompression = ModelImporterAnimationCompression.KeyframeReduction;
			import.animationPositionError = import.animationRotationError = import.animationScaleError = 0.01f;
		});
	}

	
	private static Object[] Textures(){
		return Selection.objects;
	}
}
