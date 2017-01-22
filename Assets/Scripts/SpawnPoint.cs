using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public Vector3 GetSpawnPoint() {
        Vector3 rndPosWithin;
        rndPosWithin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        return transform.TransformPoint(rndPosWithin * .5f);
    }
}
