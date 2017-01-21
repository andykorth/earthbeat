using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject couchPlayerPrefab;
    public GameObject spawnPoint;

    private CouchPlayer[] couchPlayers;
    private int lastJoysticks;

    void Start() {
        couchPlayers = new CouchPlayer[4];
    }

    void Update() {
        string[] newJoysticks = Input.GetJoystickNames();

        if (newJoysticks.Length > lastJoysticks) {
            lastJoysticks = newJoysticks.Length;
            for (var i = 0; i < newJoysticks.Length; i++) {
                if (couchPlayers[i] == null) {
                    CouchPlayer player = SetupNewCouchPlayer(i);
                    couchPlayers[i] = player;
                }
            }
        }
    }

    CouchPlayer SetupNewCouchPlayer(int num) {
        //TODO: random spawn point around the VRPlayer...?
        GameObject newPlayerObject = Instantiate(couchPlayerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        var couchPlayer = newPlayerObject.GetComponent<CouchPlayer>();
        couchPlayer.playerNum = num;
        return couchPlayer;
    }
}