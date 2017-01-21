using UnityEngine;
using UnityEditor;

public class PlaceSelectionOnSurface : ScriptableObject
{
	static bool selectedStuff(){
		Object[] objs = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep | SelectionMode.Editable | SelectionMode.ExcludePrefab);		
		return objs != null && objs.Length > 0;
	}

	[MenuItem ("Selection/Place Selection On Surface", false, 120)]
	static void DoPlace ()
	{
		if (Terrain.activeTerrain != null) {
			foreach (Transform transform in Selection.transforms)
			{
				Vector3 position = transform.position;
				float height = Terrain.activeTerrain.SampleHeight(position);
				if (position.y < height)
					position.y = height;
				transform.position = position;
			}
		}
		
		foreach (Transform transform in Selection.transforms)
		{
			RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down);
			float closestHitDistance = Mathf.Infinity;
			Vector3 closestHitPoint = transform.position;
			
			foreach (RaycastHit hit in hits) {
				if (hit.distance < closestHitDistance && !hit.transform.IsChildOf(transform) && hit.transform != transform) {
					closestHitDistance = hit.distance;
					closestHitPoint = hit.point;
				}
			}
			
			transform.position = closestHitPoint;
		}
	}
	
	[MenuItem ("Selection/Place Selection On Back Surface #d", false, 130)]
	static void DoBackPlace ()
	{
		foreach (Transform transform in Selection.transforms)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, -transform.forward, out hit))
			{
				transform.position = hit.point;
			}
		}
	}
	
	[MenuItem ("Selection/Round Rotation #r", false, 110)]
	static void DoRoundRotation () {
		foreach (Transform transform in Selection.transforms)
		{
			Vector3 angles = transform.eulerAngles;
			angles.x = Mathf.Round(angles.x / 45f) * 45f;
			angles.y = Mathf.Round(angles.y / 45f) * 45f;
			angles.z = Mathf.Round(angles.z / 45f) * 45f;
			transform.rotation = Quaternion.Euler(angles);
		}
	}
	
	[MenuItem ("Selection/Place Selection On Back Surface #d", true, 130)]
	[MenuItem ("Selection/Place Selection On Surface", true, 120)]
	[MenuItem ("Selection/Round Position  #f", true, 100)]
	[MenuItem ("Selection/Round Rotation #r", true, 110)]
	static bool Validate() {
		return selectedStuff();
	}

	[MenuItem ("Selection/Round Position  #f", false, 100)]
	static void DoRoundPosition () {
		foreach (Transform transform in Selection.transforms)
		{
			Vector3 position = transform.localPosition;
			position.x = Mathf.Round(2f*position.x)/2f;
			position.y = Mathf.Round(2f*position.y)/2f;
			position.z = Mathf.Round(2f*position.z)/2f;
			transform.localPosition = position;
		}
	}
	
	[MenuItem ("Selection/Scale to Vector.onef", false, 111)]
	static void DoRoundPositionLol () {
		foreach (Transform transform in Selection.transforms)
		{
			transform.localScale = Vector3.one;
		}
	}
	
	[MenuItem ("Selection/Rotate 180 around Y", false, 112)]
	static void DoRoundPositionLol2 () {
		foreach (Transform transform in Selection.transforms)
		{
			transform.localRotation *= Quaternion.Euler(0, 180, 0);
		}
	}
	
	[MenuItem ("Selection/Move to Z=0  #z", false, 101)]
	static void DoMovePosZ () {
		foreach (Transform transform in Selection.transforms)
		{
			Vector3 position = transform.localPosition;
			position.z = 0;
			transform.localPosition = position;
		}
	}

}
