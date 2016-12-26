using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	private GameObject player;
	private Vector2 velocity;
	private bool rightBounds = false;
	private bool leftBounds = false;
	private bool upBounds = false;
	private bool downBounds = false;
	private Vector2 rightClamp, leftClamp, upClamp, downClamp;
	public float smoothTimeY;
	public float smoothTimeX;
	private DynamicClamping dynamicClamping;

	void Start(){
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			player = GameObject.FindGameObjectWithTag ("Player");
			dynamicClamping = player.GetComponent<DynamicClamping> ();
		}
	}

	// Set the current boundary conditions and clamping position
	public void DidReachBounds(bool isBounds, int direction, Vector2 clampPosition){
		switch (direction) {
		case 1: 
			this.rightBounds = isBounds;
			this.rightClamp = clampPosition;
			break;
		case 2:
			this.leftBounds = isBounds;
			this.leftClamp = clampPosition;
			break;
		case 3:
			this.upBounds = isBounds;
			this.upClamp = clampPosition;
			break;
		case 4:
			this.downBounds = isBounds;
			this.downClamp = clampPosition;
			break;
		default:
			break;
		}
	}

	public void PlayerRespawned(Vector3 newPosition){
		// Retrieve the new position of the player after respawning
		transform.position = new Vector3 (newPosition.x, newPosition.y, transform.position.z);
	}

	void FixedUpdate(){
		if (player != null) {
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

			// Conditions for line cast reaching level boundaries: clamp the camera accordingly

			// TODO FIX CAMERA STUCK BUG --> Check for a didRespawn flag and then call a method to reset camera position

			if (rightBounds) {
				posX = Mathf.Clamp(transform.position.x, transform.position.x, rightClamp.x - 145);
			}

			if (leftBounds) {
				posX = Mathf.Clamp(transform.position.x, leftClamp.x + 145, transform.position.x);
			}
				
			if (upBounds) {
				posY = Mathf.Clamp (transform.position.y, transform.position.y, upClamp.y - 70);
			}


			if (downBounds) {
				posY = Mathf.Clamp (transform.position.y, downClamp.y + 70, transform.position.y);
			}

			transform.position = new Vector3 (posX, posY, transform.position.z);

				
		} else {
//			if (GameObject.FindGameObjectWithTag ("Player") != null) {
//				this.player = GameObject.FindGameObjectWithTag ("Player");
			Debug.Log("Player is null");
			transform.position = player.transform.position;

//			}
		}

	}
}