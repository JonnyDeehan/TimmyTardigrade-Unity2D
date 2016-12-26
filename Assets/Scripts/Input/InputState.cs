using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState {
	public bool value;
	public float holdTime = 0;
}

public enum Directions{
	Right = 1,
	Left = -1
}

public class InputState : MonoBehaviour {

	// Here the assumption is that every object will be facing right upon being loaded up initially
	// Therefore all sprites that will move must also follow this
	public Directions direction = Directions.Right;
	public float absVelX = 0f;
	public float absVelY = 0f;

	private Rigidbody2D body2d;
	private Dictionary<Buttons,ButtonState> buttonStates = new Dictionary<Buttons,ButtonState>();

	void Awake(){
		body2d = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate(){
		// Here we are getting the absolute value of the velocity of the 2d body
		absVelX = Mathf.Abs (body2d.velocity.x);
		absVelY = Mathf.Abs (body2d.velocity.y);
	}

	public void SetButtonValue(Buttons key, bool value){
		if(!buttonStates.ContainsKey(key))
			buttonStates.Add(key, new ButtonState());

		// Set the button state value associated with the given button state key
		var state = buttonStates [key];


		if (state.value && !value) {
			// if the button state is true but the button value coming in is false, the button must be released
			state.holdTime = 0;
		} else if (state.value && value) {
			// if the button state is true and the button value coming in is also true, the button is still pressed
			state.holdTime += Time.deltaTime; 
		}

		state.value = value;
	}

	public bool GetButtonValue(Buttons key){
		if (buttonStates.ContainsKey (key))
			return buttonStates [key].value;
		else
			return false;
	}

	public float GetButtonHoldTime(Buttons key){
		if (buttonStates.ContainsKey (key))
			return buttonStates [key].holdTime;
		else
			return 0;
	}
}
