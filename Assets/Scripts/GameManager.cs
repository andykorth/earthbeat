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

    [SerializeField]
    private GameObject _VRPlayer;

    public GameObject VRPlayer {
        get { return _VRPlayer; }
    }

    public enum PlayerNames {
        PLAYER_1,
        PLAYER_2,
        PLAYER_3,
        PLAYER_4
    }

    private Rect[] _PlayerCamPositions;
    public Rect[] PlayerCamPositions {
        get { return _PlayerCamPositions; }
    }


    #region Singleton Code
    private static GameManager _instance = null;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType (typeof(GameManager)) as GameManager;
                if (_instance == null) {
                    GameObject obj = new GameObject ("GameManager", typeof(GameManager));
                    print ("Could not find an GameManager object in the scene. Object was auto-generated.");
                }
            }

            return _instance;
        }
    }

    void OnApplicationQuit() {
        _instance = null;
    }
    #endregion

    void Awake() {
        playerMap = new List<PlayerMap>();
        _PlayerCamPositions = new Rect[4];
        _PlayerCamPositions[0] = new Rect(0f, 0f, 0.5f, 0.5f);
        _PlayerCamPositions[1] = new Rect(0.5f, 0f, 0.5f, 0.5f);
        _PlayerCamPositions[2] = new Rect(0f, 0.5f, 0.5f, 0.5f);
        _PlayerCamPositions[3] = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
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
