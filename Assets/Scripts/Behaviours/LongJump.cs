using UnityEngine;
using System.Collections;

public class LongJump : Jump {

	public float longJumpDelay = .15f;
	public float longJumpMultiplier = 1.5f;
	public bool canLongJump;
	public bool isLongJumping;
	public AudioSource source;

	protected override void Update(){
		// Logic before calling the base update method
		var canJump = inputState.GetButtonValue (inputButtons [0]);
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (!canJump) 
			canLongJump = false;

		if (collisionState.standing && isLongJumping)
			isLongJumping = false;

		base.Update ();

		// Logic after calling the base update method
		if (canLongJump && !collisionState.standing && holdTime > longJumpDelay) {
			var vel = body2d.velocity;
			body2d.velocity = new Vector2 (vel.x, jumpSpeed * longJumpMultiplier);
			canLongJump = false;
			isLongJumping = true;
		}
	}

	protected override void OnJump ()
	{
		base.OnJump ();
		source.Play ();
		canLongJump = true;
	}
}
