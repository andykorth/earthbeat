
using UnityEngine;
using UnityEditor;

public class CompressAsOgg : ScriptableObject
{
//	[MenuItem ("Custom/Optimization/Set Ogg - Mono 96kbit DoL 3D", false, 1120)]
//	public static void DoThingy1() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			AudioImporterSampleSettings s = new AudioImporterSampleSettings ();
//			s.compressionFormat = AudioCompressionFormat.MP3;
//			s.quality = 0.3f;
//
//			audio.forceToMono = true;
//			audio.compressionBitrate = 96*1000;
////			audio.lo = AudioClipLoadType.DecompressOnLoad;
//		//	audio.decompressOnLoad = true;
////			audio.
//			
//			//EditorUtility.SetDirty(audio);
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Set Ogg - Mono 64kbit", false, 1121)]
//	public static void DoThingy3() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.format = AudioImporterFormat.Compressed;
//			audio.forceToMono = true;
//			audio.compressionBitrate = 64*1000;
//			audio.loadType = AudioClipLoadType.DecompressOnLoad;
//	//		audio.decompressOnLoad = true;
//			audio.threeD = true;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Set Ogg - Stereo 96kbit", false, 1122)]
//	public static void DoThingy2() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.format = AudioImporterFormat.Compressed;
//			audio.forceToMono = true;
//			audio.compressionBitrate = 96*1000;
//			audio.loadType = AudioClipLoadType.CompressedInMemory;
////			audio.decompressOnLoad = false;
//			audio.threeD = false;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Set Ogg - Mono 64kbit Voiceover", false, 1123)]
//	public static void DoThingy4() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.format = AudioImporterFormat.Compressed;
//			audio.forceToMono = true;
//			audio.compressionBitrate = 64*1000;
//			audio.loadType = AudioClipLoadType.DecompressOnLoad;
//	//		audio.decompressOnLoad = true;
//			audio.threeD = false;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Hardware Decoding Off", false, 1125)]
//	public static void DoThingy54444() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.hardware = false;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Hardware Decoding On", false, 1126)]
//	public static void DoThingy54444a() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//
//			audio.hardware = true;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/3D Off, wav, load into memory", false, 1127)]
//	public static void DoTddddhingy54444() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.threeD = false;
//			audio.loadType = AudioClipLoadType.DecompressOnLoad;
//			audio.format = AudioImporterFormat.Native;
//			audio.forceToMono = true;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	[MenuItem ("Custom/Optimization/3D Off, 63 Qual, Compressed", false, 1128)]
//	public static void DoTddddhingy544aaa44() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.threeD = false;
//			audio.loadType = AudioClipLoadType.CompressedInMemory;
//			audio.format = AudioImporterFormat.Compressed;
//			audio.forceToMono = true;
//			audio.compressionBitrate = 63 * 1000;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//
//	
//	[MenuItem ("Custom/Optimization/Audio CompressedInMemory", false, 1507)]
//	public static void DoasdfsThingy54444a() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//
//			audio.loadType = AudioClipLoadType.CompressedInMemory;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	[MenuItem ("Custom/Optimization/Audio DecompressOnLoad", false, 1508)]
//	public static void dddddfasdfac() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			audio.loadType = AudioClipLoadType.DecompressOnLoad;
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//		[MenuItem ("Custom/Optimization/Audio StreamFromDisc", false, 1509)]
//	public static void dsddddddfasdfac() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//
//			audio.loadType = AudioClipLoadType.Streaming;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	
//	[MenuItem ("Custom/Optimization/Set MPEG - iOS Stream - Qual 70", false, 1124)]
//	public static void DoThingy5() {
//		foreach(Object obj in AudioClips()){
//			string path = AssetDatabase.GetAssetPath(obj);
//			AudioImporter audio = (AudioImporter)AssetImporter.GetAtPath(path);
//			
//			audio.format = AudioImporterFormat.Compressed;
//			audio.forceToMono = true;
//			audio.loadType = AudioClipLoadType.Streaming;
//			audio.threeD = false;
//			audio.compressionBitrate = 70 * 1000;
//			audio.hardware = true;
//			
//			AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//		}
//		
//		AssetDatabase.Refresh();
//	}
//	private static Object[] AudioClips(){
//		return Selection.GetFiltered(typeof(AudioClip), SelectionMode.DeepAssets);
//	}
}
