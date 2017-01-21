
using UnityEngine;
using UnityEditor;

public class MaterialEditingStuff : ScriptableObject
{
	
	public delegate void EditObject(Object obj, string path);
	
	public static void ChangeAsset(EditObject c){
		
		foreach(Object obj in Materials()){
			string path = AssetDatabase.GetAssetPath(obj);
			
			c(obj, path);
			
			EditorUtility.SetDirty(obj);
			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
		}
		
		AssetDatabase.Refresh();
		AssetDatabase.SaveAssets();
	}

	
	[MenuItem ("Custom/Optimization/Custom Coded Mat Rename", false, 1800)]
	public static void DoThingy88() {
		EditObject e = delegate(Object obj, string path){
			string s = obj.name.Replace("REC_L00_A01_WaveHand_bp_v009_jr03-", "");
			string result = AssetDatabase.RenameAsset(path, s);
			
			Debug.Log("Renaming to: " + s + " returns:    " + result);
			
		};
		
		ChangeAsset(e);
		//
		

	}
	
	
	
	private static Object[] Materials(){
		return Selection.GetFiltered(typeof(Material), SelectionMode.DeepAssets);
	}
}
