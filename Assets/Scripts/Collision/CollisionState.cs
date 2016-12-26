using UnityEngine;
using System.Collections;

public class CollisionState : MonoBehaviour {

	public LayerMask collisionLayer;
	public bool standing;
	public bool onWall;
	public bool onMovingPlatform;
	public Vector2 bottomPosition = Vector2.zero;
	public Vector2 rightPosition = Vector2.zero;
	public Vector2 leftPosition = Vector2.zero;
	public float collisionRadius = 10f;
	public Color debugCollionColor = Color.red;

	private InputState inputState;

	void Awake(){
		inputState = GetComponent<InputState> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void FixedUpdate(){
		var pos = bottomPosition;
		// To take into account the current offset position of the transform relative to the bottom of the screen.
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		// Checks to see if the circle overlaps with the collision layer (returns true/false)
		standing = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);

		pos = inputState.direction == Directions.Right ? rightPosition : leftPosition;
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		onWall = Physics2D.OverlapCircle (pos, collisionRadius, collisionLayer);
	}

	void OnDrawGizmos(){
		// Draws the collision circle corresponding with the player for debugging purposes
		Gizmos.color = debugCollionColor;

		var positions = new Vector2[] { rightPosition, bottomPosition, leftPosition };

		foreach (var position in positions) {
			var pos = position;
			pos.x += transform.position.x;
			pos.y += transform.position.y;
			Gizmos.DrawWireSphere (pos, collisionRadius);
		}
	}
}
