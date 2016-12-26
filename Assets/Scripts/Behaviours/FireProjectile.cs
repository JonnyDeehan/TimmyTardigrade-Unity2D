using UnityEngine;
using System.Collections;

public class FireProjectile : AbstractBehaviour {

	public float shootDelay = 0f; // Shooting delay
	public GameObject projectilePrefab; // Reference to projectile prefab
	public Vector2 firePosition = Vector2.zero; // Point of firing projectile
	public Color debugColor = Color.yellow;
	public float debugRadius = 3f; 
	public AudioSource source; // Fire projectile sound effect

	private float timeElapsed = 0f; // Elapsed time since shot

	// Update is called once per frame
	void Update () {
		if (projectilePrefab != null) {
			var canFire = inputState.GetButtonValue (inputButtons [0]);

			if (canFire && timeElapsed > shootDelay) {
				CreateProjectile (CalculateFirePosition());
				source.Play ();
				timeElapsed = 0;
			}

			timeElapsed += Time.deltaTime;
		}
	}

	Vector2 CalculateFirePosition(){
		var pos = firePosition;
		// Position fire point with respect to facing direction
		pos.x *= (float)inputState.direction;

		// If the player is on a wall, flip the firing position/direction
		if (collisionState.onWall)
			pos.x *= -1;

		// Set the firing position relative to the player transform position
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		return pos;
	}

	// Instantiate the projectile prefab at the calculated fire position
	public void CreateProjectile(Vector2 pos){
		var clone = Instantiate (projectilePrefab, pos, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
	}

	// Gizmos for visual debugging 
	void OnDrawGizmos(){

		Gizmos.color = debugColor;

		var pos = firePosition;

		if (inputState != null) {
			pos.x *= (float)inputState.direction;
		}
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		Gizmos.DrawWireSphere (pos, debugRadius);
	}
}
