using UnityEngine;
using System.Collections;

public class DrawGizmo : MonoBehaviour {

	public string gizmoName;
	
	void OnDrawGizmos(){
		Gizmos.color = new Color(1, 1, 1, 1); 
		Gizmos.DrawIcon(transform.position, gizmoName + ".psd"); 
	}
}
