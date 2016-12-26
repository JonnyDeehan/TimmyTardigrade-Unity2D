using UnityEngine;
using System.Collections;

public class FaceDirection : AbstractBehaviour {
	
	// Update is called once per frame
	void Update () {
		// Facing right
		var right = inputState.GetButtonValue (inputButtons [0]);
		// Facing left
		var left = inputState.GetButtonValue (inputButtons [1]);

		// Update InputState class with direction
		if (right) {
			inputState.direction = Directions.Right;
		} else if (left) {
			inputState.direction = Directions.Left;
		}

		// Good practice even in 2d to use vector 3 for the game object transform, so that you don't lose the z component option
		transform.localScale = new Vector3 ((float)inputState.direction, 1, 1);
	}
}
