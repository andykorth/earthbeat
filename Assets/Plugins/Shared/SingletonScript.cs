/*           
**  SingletonScript.cs
**      
**  Copyright (c) 2010-2013 Andy Korth and Howling Moon Software
**    
**  Source may be used under for whatever by whoever
**  
** This was based on an idea by Neil Carter's lovely Script.cs, details at http://nether.homeip.net:8080/unity/
* 
*/

using UnityEngine;
using System.Collections;

// Makes crappy unity warnings go away.
public class SingletonScript {};

public class SingletonScript <T> : Script where T:Object  {

	protected static T instance;

	public static T i {
		get {
			if(instance != null) { 
				return instance;
			} else {
				T[] instances = FindObjectsOfType<T>();
				if(instances.Length > 1){
					Debug.LogError("More than one " + typeof(T) + " is in the scene!");					
					return null;
				}else if(instances.Length == 0){
					//Debug.LogError("There is no " + typeof(T) + " in this scene!");				
					return null;
				}
				
				instance = instances[0];
				(instance as SingletonScript<T>).SingletonCreated();
				return instance;
			}
		}
	}
	
	public virtual void SingletonCreated(){
	
	}
	
	public static bool Exists_VERY_EXPENSIVE(){
		return FindObjectsOfType<T>().Length >= 1;
	}
		
	public static bool Existed(){
		return i != null;
	}

	public virtual void OnLevelWasLoaded(){
		// reset the instance, since now that we've reloaded a level the old one was probably destroyed.
		instance = null;
	}

	public static void ForceReload(){
		instance = null;
	}
	

}
