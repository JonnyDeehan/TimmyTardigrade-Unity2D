using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

	public Transform otherPortal;
	public GameObject other;

	public bool canTeleport;
	public GameObject CameraObject;
	private FollowPlayer followPlayer;

	void Awake(){
		followPlayer = CameraObject.GetComponent<FollowPlayer> ();
	}
		
	void OnTriggerEnter2D(Collider2D target){
		if (target.gameObject.tag == "Player" && !canTeleport) {
			Teleport teleport = otherPortal.gameObject.GetComponent<Teleport>();
			teleport.canTeleport = true;
			// Move the player position to the other portal
			Vector3 newPosition = new Vector3 (otherPortal.position.x, otherPortal.position.y, 0);
			target.gameObject.transform.position = newPosition;
			followPlayer.PlayerRespawned (target.gameObject.transform.position);
			// Set canTeleport to false
		}
	}

	void OnTriggerExit2D(Collider2D target){
		if (target.gameObject.tag == "Player") {
			canTeleport = false;
		}
	}


}
