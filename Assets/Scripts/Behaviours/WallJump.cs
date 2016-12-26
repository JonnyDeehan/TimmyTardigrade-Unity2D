using UnityEngine;
using System.Collections;

public class WallJump : AbstractBehaviour {

	public Vector2 jumpVelocity = new Vector2(50, 200);
	public bool jumpingOffWall;
	public float resetDelay = .2f;
	public AudioSource source;

	private float timeElapsed = 0;
	
	// Update is called once per frame
	void Update () {
		// So that you can't just hold down the jump button on the wall and keep jumping
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (collisionState.onWall && !collisionState.standing) {
			// Detect if jump button has been pressed
			var canJump = inputState.GetButtonValue (inputButtons [0]);

			if (canJump && !jumpingOffWall && holdTime < .1f) {
				inputState.direction = inputState.direction == Directions.Right ? Directions.Left : Directions.Right;
				body2d.velocity = new Vector2 (jumpVelocity.x * (float)inputState.direction, jumpVelocity.y);
				source.Play ();
				ToggleScripts (false);
				jumpingOffWall = true;
			}
		}

		if (jumpingOffWall) {
			timeElapsed += Time.deltaTime;

			if (timeElapsed > resetDelay) {
				ToggleScripts (true);
				jumpingOffWall = false;

				timeElapsed = 0;
			}
		}
	
	}
}
