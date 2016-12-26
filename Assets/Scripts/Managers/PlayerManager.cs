using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public MonoBehaviour[] disableScripts;
	public GameObject AudioManagerObject;

	private CameraShake camShake;
	private InputState inputState;
	private Walk walkBehaviour;
	private Animator animator;
	private CollisionState collisionState;
	private Duck duckBehaviour;
	private Dash dashBehaviour;
	private Health playerDead;
	private AudioManager audioManager;

	void Awake(){
		inputState = GetComponent<InputState> ();
		walkBehaviour = GetComponent<Walk> ();
		animator = GetComponent<Animator> ();
		collisionState = GetComponent<CollisionState> ();
		duckBehaviour = GetComponent<Duck> ();
		dashBehaviour = GetComponent<Dash> ();
		playerDead = GetComponent<Health> ();
		audioManager = AudioManagerObject.GetComponent<AudioManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (collisionState.standing)
			ChangeAnimationState (0);

		if (inputState.absVelX > 0)
			ChangeAnimationState (1);

		// Since we are detecting the jump animation AFTER the walking and standing, it will override those animations if we are jumping.
		if (inputState.absVelY > 0) {
			ChangeAnimationState (2);
		}

		if (duckBehaviour.ducking) {
			ChangeAnimationState (3);
		}

		if (!collisionState.standing && collisionState.onWall) {
			// TO-DO [THINK OF WAY TO IMPLEMENT DUST SLIDE SOUND]
			ChangeAnimationState (4);
		}

		if (dashBehaviour.didDash && !collisionState.onWall ) {
			ChangeAnimationState (5);
		}

		if (playerDead.GetHealth () <= 0) {
			ChangeAnimationState (6);
		}
	}



	void ChangeAnimationState(int value){
		animator.SetInteger("AnimState", value);

	}

	public void ToggleScripts(bool value){
		foreach (var script in disableScripts) {
			script.enabled = value;
		}
	}
}
