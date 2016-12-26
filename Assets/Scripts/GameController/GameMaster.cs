using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

	private Image Healthbar; // Player Health Bar reference 

	public AudioSource source; 	// Audio Source
	public GameObject player; // Player Gameobject
	public GameObject deathSoundObject;
	public Transform spawnPoint; // SpawnPoint Transform 
	public GameObject respawnParticlePrefab; // Particle effect at respawn

	private Transform originalSpawnPoint;
	private GameObject CameraObject;
	private FollowPlayer followPlayer;
	private AudioSource deathSound;
	private Health playerHealth;
	private PlayerManager playerManager;
	private bool playerDidDie = false; // Temporary bool to alert Gamemaster script the player has died
	private bool isRespawning = false; // In the process of executing the RespawnPlayer() method
	private int fallBoundary = -115; // Boundary below the ground level where the player dies

	void Awake(){
		// Search for the HealthBar component in the health bar gameobject
		Healthbar = GameObject.Find ("Main Camera").transform.FindChild ("Canvas").FindChild ("Healthbar").GetComponent<Image> ();
		// Respawn Audio
		source = GetComponent<AudioSource> ();
		playerHealth = player.GetComponent<Health> ();
		playerManager = player.GetComponent<PlayerManager> ();
		deathSound = deathSoundObject.GetComponent<AudioSource> ();

		SaveInitialLevelConditions ();

		if (GameObject.FindGameObjectWithTag ("MainCamera") != null) {
			CameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
			followPlayer = CameraObject.GetComponent<FollowPlayer> ();
		}
	}

	void Start(){
		LoadInitialLevelConditions ();
	}

	void Update(){
		Healthbar.fillAmount = player.GetComponent<Health>().GetHealth() / 100;

		// Ensure player is not null
		if (player != null) {
			// If the player Health has reached 0, Kill the Player
			if ((playerHealth.GetHealth () <= 0 && !playerDidDie) 
				|| (player.transform.position.y <= fallBoundary && !playerDidDie)) {
				playerDidDie = true;
			}
			if (playerDidDie && !isRespawning) {
				isRespawning = true;
				StartCoroutine (KillPlayer ());
			}
		}
	}

	public bool IsPlayerRespawning(){
		return isRespawning;
	}
		
	public void RespawnPlayer (){
		// Reset the player health back to the max
		playerHealth.SetHealth (playerHealth.maxHealth);
		// Respawn Particle Effect
		// Play respawn Audio
		source.Play();
		// Change player position
		Vector3 newPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
		player.transform.position = newPosition;
		// Enable all the player scripts
		playerManager.ToggleScripts (true);

		isRespawning = false;
		playerDidDie = false;

		followPlayer.PlayerRespawned (player.transform.position);
	}
		
	public void RespawnEnemies(){
	}

	IEnumerator KillPlayer(){
		// Wait for death animation
		playerManager.ToggleScripts(false);
		// Death Sound
		deathSound.Play();
		yield return new WaitForSeconds (0.5f);
		// Respawn the player
		RespawnPlayer();
		// Respawn all the enemies beyond the current checkpoint
		// [TO-DO: IMPLEMENT CHECKPOINT ENEMY RESPAWN DATA SAVING AND CHECKING]
		RespawnEnemies ();
	}

	public void SaveInitialLevelConditions(){
		originalSpawnPoint = spawnPoint;
	}

	public void LoadInitialLevelConditions(){
		spawnPoint.position = originalSpawnPoint.position;
		player.transform.position = spawnPoint.position;
	}

	public void KillEnemy(GameObject enemy){
		Debug.Log ("Destroy enemy game object");
		Destroy(enemy);
	}
}
