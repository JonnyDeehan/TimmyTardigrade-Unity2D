using UnityEngine;
using System.Collections;

// All other behaviour driven scripts have access to all the properties defined.
public abstract class AbstractBehaviour : MonoBehaviour {

	public Buttons[] inputButtons;
	// We want to pass references of other scripts to disable based on certain conditions
	// we don't want behaviours to know anything else about other behaviours, but we want
	// them to be able to disable other scripts based on their own state.
	public MonoBehaviour[] disableScripts;

	protected InputState inputState;
	protected Rigidbody2D body2d;
	protected CollisionState collisionState;

	// Having this protected virtual version of Awake allows us to override any extended version of this class's Awake method
	protected virtual void Awake(){
		inputState = GetComponent<InputState>();
		body2d = GetComponent<Rigidbody2D> ();
		collisionState = GetComponent<CollisionState> ();
	}

	// Turn off/on scripts in the disable scripts array during a given behaviour action.
	protected virtual void ToggleScripts(bool value){
		foreach (var script in disableScripts) {
			script.enabled = value;
		}
	}
}
