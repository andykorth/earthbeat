﻿using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject couchPlayerPrefab;
    public GameObject spawnPoint;

    private CouchPlayer[] couchPlayers;
    private int lastJoysticks;

    public int maxPlayers = 4;

    private List<PlayerMap> playerMap; // Maps Rewired Player ids to game player ids
    private int gamePlayerIdCounter = 0;

    void Awake() {
        playerMap = new List<PlayerMap>();
    }

    void Update() {

        // Watch for JoinGame action in each Player
        for(int i = 0; i < ReInput.players.playerCount; i++) {
            if(ReInput.players.GetPlayer(i).GetButtonDown("JoinGame")) {
                AssignNextPlayer(i);
            }
        }
    }

    void AssignNextPlayer(int rewiredPlayerId) {
        if(playerMap.Count >= maxPlayers) {
            Debug.LogError("Max player limit already reached!");
            return;
        }
        if (playerMap.Find(p => p.rewiredPlayerId == rewiredPlayerId) != null) {
            //already exists
            return;
        }

        int gamePlayerId = GetNextGamePlayerId();

        // Add the Rewired Player as the next open game player slot
        playerMap.Add(new PlayerMap(rewiredPlayerId, gamePlayerId));
        SetupNewCouchPlayer(gamePlayerId);

        Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);

        // Disable the Assignment map category in Player so no more JoinGame Actions return
        rewiredPlayer.controllers.maps.SetMapsEnabled(false, "Assignment");

        // Enable UI control for this Player now that he has joined
        rewiredPlayer.controllers.maps.SetMapsEnabled(true, "UI");

        Debug.Log("Added Rewired Player id " + rewiredPlayerId + " to game player " + gamePlayerId);
    }

    private int GetNextGamePlayerId() {
        return gamePlayerIdCounter++;
    }

    CouchPlayer SetupNewCouchPlayer(int num) {
        //TODO: random spawn point around the VRPlayer...?
        GameObject newPlayerObject = Instantiate(couchPlayerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        var couchPlayer = newPlayerObject.GetComponent<CouchPlayer>();
        couchPlayer.SetupController(num);
        return couchPlayer;
    }

    // This class is used to map the Rewired Player Id to your game player id
    private class PlayerMap {
        public int rewiredPlayerId;
        public int gamePlayerId;

        public PlayerMap(int rewiredPlayerId, int gamePlayerId) {
            this.rewiredPlayerId = rewiredPlayerId;
            this.gamePlayerId = gamePlayerId;
        }
    }
}
