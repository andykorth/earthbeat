/*
	EditorDebug.cs
	© Mon May  3 10:55:21 CDT 2010 Graveck Interactive LLC
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;

public class EditorDebug {
	public static void Error(string error) {
		EditorUtility.DisplayDialog("Error", error, "OK","");
	}
}
