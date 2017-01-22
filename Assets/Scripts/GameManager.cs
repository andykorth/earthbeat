using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GameObject couchPlayerPrefab;
    public int maxPlayers = 4;
    private List<PlayerMap> playerMap; // Maps Rewired Player ids to game player ids

	public List<Color> playerColors;

    [SerializeField]
    private GameObject _VRPlayer;
    public GameObject VRPlayer {
        get { return _VRPlayer; }
    }

	public int VRMaxPlayerHitPoints = 500;
	private int VRCurrentPlayerHitPoints = 500;
	public int droneDeaths = 0;
	public int droneLandings = 0;

    #region Singleton Code
    private static GameManager _instance = null;

    public static GameManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType (typeof(GameManager)) as GameManager;
                if (_instance == null) {
                    GameObject obj = new GameObject ("GameManager", typeof(GameManager));
					Debug.Log ("Could not find an GameManager object in the scene. Object was auto-generated. " + obj);
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
		VRCurrentPlayerHitPoints = VRMaxPlayerHitPoints;
    }

    void Update() {

        // Watch for JoinGame action in each Player
        for(int i = 0; i < ReInput.players.playerCount; i++) {
            if(ReInput.players.GetPlayer(i).GetButtonDown("JoinGame")) {
                AssignNextPlayer(i);
            }
        }
    }

	public void DroneDied(){
		droneDeaths += 1;
		DroneUIManager.i.UpdateUI (droneDeaths, droneLandings);
	}

	public void DroneLanded(){
		droneLandings += 1;
		DroneUIManager.i.UpdateUI (droneDeaths, droneLandings);
	}

	public void VRPlayerTookDamage(){
		VRCurrentPlayerHitPoints -= 1;
		Debug.Log ("Hit VR Player: " + VRCurrentPlayerHitPoints);
		DroneUIManager.i.bossBar.SetProgress (100f * (VRCurrentPlayerHitPoints / (float)VRMaxPlayerHitPoints));
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

        // Add the Rewired Player as the next open game player slot
        playerMap.Add(new PlayerMap(rewiredPlayerId, rewiredPlayerId));
        SetupNewCouchPlayer(rewiredPlayerId);

        Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);

        // Disable the Assignment map category in Player so no more JoinGame Actions return
        rewiredPlayer.controllers.maps.SetMapsEnabled(false, "Assignment");

        // Enable UI control for this Player now that he has joined
        rewiredPlayer.controllers.maps.SetMapsEnabled(true, "UI");

        //Debug.Log("Added Rewired Player id " + rewiredPlayerId + " to game player " + gamePlayerId);
        Debug.Log("Wecome player " + rewiredPlayerId);
    }


    CouchPlayer SetupNewCouchPlayer(int num) {
        //TODO: random spawn point around the VRPlayer...?
        GameObject spawnPoint = GameObject.Find(string.Format("Player{0}_Spawn", num+1));
        SpawnPoint sp = spawnPoint.GetComponent<SpawnPoint>();
        Vector3 pos = sp.GetSpawnPoint();
        GameObject newPlayerObject = Instantiate(couchPlayerPrefab, pos, spawnPoint.transform.rotation);
        var couchPlayer = newPlayerObject.GetComponent<CouchPlayer>();
        couchPlayer.SetupController(num);
		couchPlayer.SetColor (playerColors [num]);

		AudioManager.i.PlayerStart ();

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

    public void CouchPlayerDied(int playerNum) {
        //playerMap.Add(new PlayerMap(rewiredPlayerId, rewiredPlayerId));
        var pm = playerMap.Find(p => p.gamePlayerId == playerNum);
        var index = playerMap.IndexOf(pm);
        if (index > -1) {
            playerMap.RemoveAt(index);
            Debug.Log("Removed player " + playerNum);
        }
        else {
            Debug.LogError("Couldn't find player in playerMap! This should not happen.");
        }

    }
}
