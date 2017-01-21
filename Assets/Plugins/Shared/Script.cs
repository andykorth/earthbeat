#pragma warning disable 0649

using UnityEngine;
using System.Collections;


public static class ComponentParent {
	public static GameObject parent(this Component c){
		return c.transform.parent.gameObject;
	}
}


public class Script : MonoBehaviour
{
	
	// magic coroutine maker
	public delegate void AnimationDelegate(float alpha);

	public Coroutine AddAnimation(float duration, AnimationDelegate d){
		return StartCoroutine(AnimationHelper(duration, d, null));
	}

	public Coroutine AddAnimation(float duration, AnimationDelegate d, float wait ){
		return StartCoroutine(AnimationHelper(duration, d, new WaitForSeconds(wait)));
	}

	public Coroutine AddAnimation(float duration, AnimationDelegate d, Coroutine wait ){
		return StartCoroutine(AnimationHelper(duration, d, wait));
	}

	private IEnumerator AnimationHelper(float duration, AnimationDelegate d, YieldInstruction wait){
		if(wait != null) yield return wait;

		float startTime = Time.time;
		for(float elapsed = 0.0f; elapsed < duration; elapsed = Time.time - startTime){
			d(elapsed / duration);
			yield return null;
		}
		d(1.0f);
	}

	public Coroutine AddDelayed(float wait, System.Action delayed ){
		return StartCoroutine(DelayedHelper(new WaitForSeconds(wait), delayed));
	}

	public Coroutine AddDelayed(Coroutine wait, System.Action delayed ){
		return StartCoroutine(DelayedHelper(wait, delayed));
	}

	private IEnumerator DelayedHelper(YieldInstruction wait, System.Action delayed){
		yield return wait;

		delayed();
	}
		
	public Transform FindNameRecursive( string name){
		if(this.gameObject.name == name){
			return this.transform;
		}
		return FindNameRecursive(this.transform, name);
	}
	
	public Transform FindNameRecursive(Transform t, string name){
		//Debug.Log("gdfgffg: " + t.gameObject.name);
		if(!t.gameObject.activeInHierarchy){
			return null;
		}
		foreach(Transform t2 in t){
			//Debug.Log("Checking: " + t2.gameObject.name);
			if(string.Compare(t2.gameObject.name, name, true) == 0){
				//Debug.Log("found it!");
				return t2;
			}
			Transform x = FindNameRecursive(t2, name);
			if(x != null){
				return x;
			}
		}
		return null;
	}
		
}