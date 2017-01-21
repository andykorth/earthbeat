/*
	ProjectSearch.cs
	© Thu Oct 16 11:42:44 CDT 2008 Graveck Interactive LLC
	by Jonathan Czeck
*/
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;

public class ProjectSearch : ScriptableWizard {
	public string searchString = "";
	public bool previewSearch = false;
	public bool addToSelection = false;
	public bool ignoreCase = true;
	
	private List<UnityEngine.Object> foundObjects;
	private Regex regex;
	
	void FindRecursively(string baseAssetsPath) {
		string basePath = Environment.CurrentDirectory + "/Assets/" + baseAssetsPath;
		DirectoryInfo di = new DirectoryInfo(basePath);
		FileInfo[] fi = di.GetFiles();
		
		foreach (FileInfo fiTemp in fi) {
			if (regex.IsMatch(fiTemp.Name)) {
				UnityEngine.Object obj = AssetDatabase.LoadAssetAtPath(String.Format("{0}/{1}", baseAssetsPath, fiTemp.Name), typeof(UnityEngine.Object));
				if (obj)
					foundObjects.Add(obj);
			}
		}
		
		DirectoryInfo[] subdirectories = di.GetDirectories();
		
		foreach (DirectoryInfo subdirectory in subdirectories) {
			FindRecursively(String.Format("{0}/{1}", baseAssetsPath, subdirectory.Name));
		}
	}
	
	void UpdateResults() {
		foundObjects = new List<UnityEngine.Object>();
		
		if (searchString.Length < 1)
			return;
		
		if (ignoreCase)
			regex = new Regex(searchString, RegexOptions.IgnoreCase);
		else
			regex = new Regex(searchString);
				
		FindRecursively("");		
	}
	
	void OnWizardCreate() {
		UpdateResults();
		if (addToSelection) {
			foreach (UnityEngine.Object obj in Selection.objects) {
				foundObjects.Add(obj);
			}
		}
		Selection.objects = foundObjects.ToArray();
	}
	
	void OnWizardUpdate() {
		if (previewSearch) {
			UpdateResults();
			helpString = String.Format("{0} match{1} found.", foundObjects.Count, foundObjects.Count == 1 ? "" : "es");
		}
	}
	
	[MenuItem ("Assets/Find In Project... %#r")]
	static void OnMenuItemSelected () {
		ScriptableWizard.DisplayWizard("Find In Project...", typeof(ProjectSearch), "Search");
	}
}
