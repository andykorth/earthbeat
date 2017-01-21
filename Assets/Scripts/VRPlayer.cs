using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayer : MonoBehaviour {
    public int health = 20;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision) {
        var projectile = collision.collider.transform.gameObject.GetComponent<Projectile>();
        if (projectile != null) {
            health = health - projectile.getDamage();
            ProcessDamage();
        }
    }

    void ProcessDamage() {
        if (health == 0) {
            //TODO: What to do when the VR player "dies"?
        }
        //TODO: damage animation on the VRplayer
        //TODO: any other UI update to indicate damage
    }


}
