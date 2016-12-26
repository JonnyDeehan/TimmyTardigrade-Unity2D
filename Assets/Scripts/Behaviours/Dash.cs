using UnityEngine;
using System.Collections;

public class Dash : AbstractBehaviour {
	// public for testing
	public Vector2 initialDashVelocity = new Vector2 (60, 0);
	public bool didDash = false; // Did the player dash
	public float dashDamage = 100f; // Dash damage
	private float centerOffsetY = 0f; 
	private float newOffset = 5f;
	private float dashDelay = 0.5f; // Dash delay
	private float lastDashTime = 0; // Last dash time
	private float dashDuration = 0.75f; // Duration of a dash
	private float speedAddition = 0f; // Speed multiplier
	private CircleCollider2D circleCollider;
	private Vector2 originalCenter;
	private Health playerHealth; // Reference to player health
	public AudioSource source; // Dash sound effect

	protected override void Awake(){
		base.Awake ();

		circleCollider = GetComponent<CircleCollider2D> ();
		originalCenter = circleCollider.offset;
		playerHealth = GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update () {
		// Receive dash input button
		var pressedDash = inputState.GetButtonValue (inputButtons [0]);
		// Dash button hold time
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		// Perform a Dash if player is standing and time since last dash is above dash delay
		if (pressedDash && collisionState.standing && holdTime < .1f && (Time.time - lastDashTime > dashDelay)) {
			didDash = true;
			OnDash ();
		}

		// Increase Speed
		if (didDash) {
			speedAddition += 0.1f; 
			var velX = (float)inputState.direction;
			body2d.velocity = new Vector2 (velX * (initialDashVelocity.x + speedAddition), initialDashVelocity.y);
		}  

		// Check for when you stop dashing or on the Wall
		if (Time.time - lastDashTime > dashDuration || collisionState.onWall) {
			OnDashEnd ();
		}
	}

	// Perform Dash action.
	void OnDash(){
		speedAddition = 0f;
		var velX = (float)inputState.direction;
		// Dash velocity
		body2d.velocity = new Vector2 (velX * initialDashVelocity.x, initialDashVelocity.y);
		// Dash sound effect
		source.Play ();
		// Set time of dash pressed
		lastDashTime = Time.time;
		// Player is immune to damage during a dash
		playerHealth.immunity = true;
		// Toggle scripts
		ToggleScripts (false);
	}

	// End the dash action once the dash duration has ended
	void OnDashEnd(){
		source.Stop ();
		didDash = false;
		speedAddition = 0f;
		playerHealth.immunity = false;
		ToggleScripts (true);
	}

	// On Collision with an enemy, apply damage. Otherwise, end the dash action.
	void OnCollisionEnter2D(Collision2D target){
		if (didDash) {
			if (target.gameObject.tag == "Hydra") {
				Health hydraHealth = target.gameObject.GetComponent<Health> ();
				hydraHealth.TakeDamage (dashDamage);
			} else {
				OnDashEnd ();
			}
		}
	}
		
}
