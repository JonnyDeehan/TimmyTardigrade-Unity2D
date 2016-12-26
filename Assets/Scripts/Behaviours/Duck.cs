using UnityEngine;
using System.Collections;

public class Duck : AbstractBehaviour {

	public float scale = .5f; // We divide the size of the collider in half essentially while ducking
	public bool ducking; // Is the player ducking
	public float centerOffsetY = 0f;

	private CircleCollider2D circleCollider;
	private Vector2 originalCenter;

	protected override void Awake(){
		base.Awake ();

		circleCollider = GetComponent<CircleCollider2D> ();
		originalCenter = circleCollider.offset;
	}

	// Change the size of the circle collider on the Duck action.
	protected virtual void OnDuck(bool value){
		ducking = value;

		ToggleScripts (!ducking);

		var size = circleCollider.radius;

		float newOffsetY;
		float sizeReciprocal;

		if (ducking) {
			sizeReciprocal = scale;
			newOffsetY = circleCollider.offset.y - size / 2 + centerOffsetY;
		} else {
			sizeReciprocal = 1 / scale;
			newOffsetY = originalCenter.y;
		}

		size = size * sizeReciprocal;
		circleCollider.radius = size;
		circleCollider.offset = new Vector2 (circleCollider.offset.x, newOffsetY);

	}

	// Update is called once per frame
	void Update () {
		var canDuck = inputState.GetButtonValue (inputButtons [0]);
		if (canDuck && collisionState.standing && !ducking) {
			OnDuck (true);
		} else if (ducking && !canDuck) {
			OnDuck (false);
		}
	}
}
