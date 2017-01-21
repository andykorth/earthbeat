using UnityEngine;
using System.Collections;
using UnityEditor;

public class AddMeshCollider : MonoBehaviour
{
	[MenuItem("Selection/Add MeshCollider ^m")]
	static void AddMeshCollider1()
	{
		Selection.activeTransform.gameObject.AddComponent(typeof(MeshCollider));
	}
	
	[MenuItem("Selection/Add MeshCollider ^m", true)]
	static bool ValidateAddMeshCollider()
	{
		return (Selection.activeTransform != null);
	}
}
