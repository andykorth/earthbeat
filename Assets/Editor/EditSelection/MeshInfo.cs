using UnityEngine;
using UnityEditor;
using System.Collections;

public class MeshInfo : ScriptableObject
{	
	[MenuItem ("Selection/Mesh Info/Vertex and Triangle Count %#i")]
	public static void ShowCount()
	{
		int triangles = 0;
		int vertices = 0;
		int meshCount = 0;
		
		foreach (GameObject go in Selection.GetFiltered(typeof(GameObject), SelectionMode.TopLevel))
		{
			Component[] meshes = go.GetComponentsInChildren(typeof(MeshFilter));

			foreach (MeshFilter mesh in meshes)
			{
				if (mesh.sharedMesh)
				{
					vertices += mesh.sharedMesh.vertexCount;
					triangles += mesh.sharedMesh.triangles.Length / 3;
					meshCount++;

/*					if (mesh.sharedMesh.uv2 != null && mesh.sharedMesh.uv2.Length > 0)
						EditorUtility.DisplayDialog("Double UVs", "Double UVs detected in " + mesh.name, "OK", "");
*/
				}
			}
			
			Component[] skinnedMeshes = go.GetComponentsInChildren(typeof(SkinnedMeshRenderer));
			
			foreach (SkinnedMeshRenderer mesh in skinnedMeshes) {
				if (mesh.sharedMesh) {
					vertices += mesh.sharedMesh.vertexCount;
					triangles += mesh.sharedMesh.triangles.Length / 3;
					meshCount++;
				}
			}
		}

		EditorUtility.DisplayDialog("Vertex and Triangle Count", vertices
			+ " vertices in selection.  " + triangles + " triangles in selection.  "
			+ meshCount + " meshes in selection." + (meshCount > 0 ? ("  Average of " + vertices / meshCount
			+ " vertices and " + triangles / meshCount + " triangles per mesh.") : ""), "OK", "");
		
	}
	
	[MenuItem ("Selection/Mesh Info/Vertex and Triangle Count %#i", true)]
	public static bool ValidateShowCount()
	{
		return Selection.activeGameObject;
	}
	
	[MenuItem ("Selection/Mesh Info/UV Info for Materials", true)]
	public static bool ValidateUVInfo()
	{
		if (!Selection.activeGameObject)
			return false;
		
		MeshFilter meshFilter = Selection.activeGameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
		
		return (meshFilter != null);
	}
	
	[MenuItem ("Selection/Mesh Info/UV Info")]
	public static void UVInfoGeneric() {
		MeshFilter meshFilter = Selection.activeGameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
		
		if (meshFilter)
		{
			Mesh mesh = meshFilter.sharedMesh;
			Vector2[] uv = mesh.uv;
			Vector2[] uv2 = mesh.uv2;
			
			EditorUtility.DisplayDialog("UV Info", uv.Length + "uvs and " + uv2.Length + "uv2s.", "OK", "");
		}
	}
	
	[MenuItem ("Selection/Mesh Info/UV Info for Materials")]
	public static void UVInfo()
	{
		MeshFilter meshFilter = Selection.activeGameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
		
		if (meshFilter)
		{
			Mesh mesh = meshFilter.sharedMesh;
			Vector2[] uv = mesh.uv;
			
			for (int i=0; i < mesh.subMeshCount; i++)
			{
				int[] triangles = mesh.GetTriangles(i);
				
				System.Console.WriteLine("SUBMESH " + i);
				
				for (int currentVertex = 0; currentVertex < triangles.Length; currentVertex++)
				{
					int vertex = triangles[currentVertex];
					
					System.Console.WriteLine("  UV " + uv[vertex].x + " " + uv[vertex].y);
				}
				
			}
		}
		
	}
	
	[MenuItem ("Selection/Mesh Info/Report Bounds ^#b")]
	public static void ReportBounds()
	{
		Object[] renderers = Selection.activeGameObject.GetComponentsInChildren(typeof(Renderer));
		Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
		bool inited = false;
		
		foreach (Renderer renderer in renderers)
		{
			if (inited)
			{
				bounds.Encapsulate(renderer.bounds.min);
				bounds.Encapsulate(renderer.bounds.max);
			}
			else
			{
				bounds = renderer.bounds;
				inited = true;
			}
		}
		
		EditorUtility.DisplayDialog("Bounds", Selection.activeGameObject.name + "\nSize.x = " + bounds.size.x + "\nSize.y = " + bounds.size.y + "\nSize.z = " + bounds.size.z, "OK", "");
	}
}
