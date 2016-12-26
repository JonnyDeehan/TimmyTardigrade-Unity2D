using UnityEngine;
using System.Collections;

public class Jump : AbstractBehaviour {

	public float jumpSpeed = 200f;
	public float jumpDelay = .1f;
	// Handle camera shaking
	public float camShakeAmt = 0.05f;
	public float camShakeLength = 0.1f;
	public int jumpCount = 2;
	public GameObject dustEffectPrefab;
	private GameObject cameraObject;
	private Equip equip;
	private CameraShake cameraShake;
	protected float lastJumpTime = 0;
	protected int jumpsRemaining = 0;

	void Awake(){
		base.Awake ();
		cameraObject = GameObject.FindGameObjectWithTag ("MainCamera");
		equip = GetComponent<Equip> ();
		cameraShake = cameraObject.GetComponent<CameraShake> ();
	}
	
	// Update is called once per frame
	// So we can extend it in longJump
	protected virtual void Update () {

		var canJump = inputState.GetButtonValue (inputButtons [0]);
		// So that you can't just hold down the jump button and keep jumping
		var holdTime = inputState.GetButtonHoldTime (inputButtons [0]);

		if (collisionState.standing) {
			if (canJump && holdTime < .1f) {
				jumpsRemaining = jumpCount - 1;
				OnJump ();
			}
		} else {
			if (canJump && holdTime < .1f && Time.time - lastJumpTime > jumpDelay) {
				if (jumpsRemaining > 0) {
					OnJump ();
					jumpsRemaining--;
					var clone = Instantiate (dustEffectPrefab);
					clone.transform.position = transform.position;
				}
			}
		}
	
	}

	// We make a protected virtual method so that we can override this and change it for different functionalities
	protected virtual void OnJump(){
		// Shake camera on jump for fat tardigrade
//		if (equip.currentItem == 1) {
//			cameraShake.Shake(camShakeAmt, camShakeLength);
//		}
		var vel = body2d.velocity;
		lastJumpTime = Time.time;
		body2d.velocity = new Vector2 (vel.x, jumpSpeed);
	}
}
