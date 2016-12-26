using UnityEngine;
using System.Collections;

public class OnMovingPlatform : AbstractBehaviour {

	public bool onPlatformDetected;
	private GameObject movingPlatform;

	// Update is called once per frame
	void Update () {
		if (collisionState.onMovingPlatform) {
			if (!onPlatformDetected) {
				onPlatformDetected = true;
			}
		} else {
			if (onPlatformDetected) {
				onPlatformDetected = false;
				transform.parent = transform;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Moving Platform") {
			movingPlatform = target.gameObject;
			collisionState.onMovingPlatform = true;
		} else {
			collisionState.onMovingPlatform = false;
		}
	}
}
