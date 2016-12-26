using UnityEngine;
using System.Collections;

public class DynamicClamping : MonoBehaviour {

	// Script dynamically checks the player position relative to the boundaries of the level,
	// and clamps the camera accordingly

	private Vector3 playerPosition;
	public Vector3 lookRight, lookLeft, lookUp, lookDown;
	private Vector3 rightOrigin, leftOrigin, upOrigin, downOrigin;
	public GameObject RightLimit;
	public GameObject LeftLimit;
	public GameObject UpLimit;
	public GameObject DownLimit; 
	private Color debugColor = Color.yellow;
	private FollowPlayer followPlayer;
	private GameObject mainCamera;
	private InputState inputState;
	public LayerMask whatToDetect;

	void Awake(){
		if (GameObject.FindGameObjectWithTag ("MainCamera") != null) {
			mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
			followPlayer = mainCamera.GetComponent<FollowPlayer> ();
			inputState = gameObject.GetComponent<InputState> ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// Origin point for projecting the linecast for each direction
		playerPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		// Where to look in each direction
		if (inputState.direction == Directions.Right) {
			lookRight = new Vector3 (RightLimit.transform.position.x, RightLimit.transform.position.y, RightLimit.transform.position.z);
			lookLeft = new Vector3 (LeftLimit.transform.position.x, LeftLimit.transform.position.y, LeftLimit.transform.position.z);

		} else {
			lookLeft = new Vector3 (RightLimit.transform.position.x, RightLimit.transform.position.y, RightLimit.transform.position.z);
			lookRight = new Vector3 (LeftLimit.transform.position.x, LeftLimit.transform.position.y, LeftLimit.transform.position.z);
		}
		lookUp = new Vector3 (UpLimit.transform.position.x, UpLimit.transform.position.y, UpLimit.transform.position.z);
		lookDown = new Vector3 (DownLimit.transform.position.x, DownLimit.transform.position.y, DownLimit.transform.position.z);

		// Linecast for each direction to determine distance between player and level boundaries
		var rightLine = Physics2D.Linecast (playerPosition, lookRight, whatToDetect);
		var leftLine = Physics2D.Linecast (playerPosition, lookLeft, whatToDetect);
		var upLine = Physics2D.Linecast (playerPosition, lookUp, whatToDetect);
		var downLine = Physics2D.Linecast (playerPosition, lookDown, whatToDetect);

		// Let the camera know the collision state for bounds
		followPlayer.DidReachBounds (rightLine, 1, rightLine.point);
		followPlayer.DidReachBounds (leftLine, 2, leftLine.point);
		followPlayer.DidReachBounds (upLine, 3, upLine.point);
		followPlayer.DidReachBounds (downLine, 4, downLine.point);

	}

	void OnDrawGizmos(){
		Gizmos.color = debugColor;
		// DRAW EACH OF THE LINES FOR EACH DIRECTION FOR DEBUGGING
		Gizmos.DrawLine(playerPosition, lookRight);
		Gizmos.DrawLine (playerPosition, lookLeft);
		Gizmos.DrawLine (playerPosition, lookUp);
		Gizmos.DrawLine (playerPosition, lookDown);
	}
}
