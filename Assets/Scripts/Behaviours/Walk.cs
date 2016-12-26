using UnityEngine;
using System.Collections;

public class Walk : AbstractBehaviour {

	public float speed = 75f;
	
	// Update is called once per frame
	void Update () {

		var right = inputState.GetButtonValue (inputButtons [0]);
		var left = inputState.GetButtonValue (inputButtons [1]);

		if (right || left) {
			var tmpSpeed = speed;

			var velX = tmpSpeed * (float)inputState.direction;

			body2d.velocity = new Vector2 (velX, body2d.velocity.y);
		}
	}
}
