using UnityEngine;
using System.Collections;

public enum Buttons{
	Right,
	Left,
	Up,
	Down,
	A,
	B,
	RT
}

public enum Condition {
	GreaterThan,
	LessThan
}

// The System.Serializable allows unity ide to present the properties of the class
// for a given game object using this class
[System.Serializable]
public class InputAxisState {
	public string axisName;
	public float offValue;
	public Buttons button;
	public Condition condition;

	public bool value {
		get {
			var val = Input.GetAxis (axisName);

			switch (condition) {
			case Condition.GreaterThan:
				return val > offValue;
			case Condition.LessThan:
				return val < offValue;
			}
			return false;
		}
	}
}

public class InputManager : MonoBehaviour {

	public InputAxisState[] inputs;
	public InputState inputState;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var input in inputs) {
			inputState.SetButtonValue (input.button, input.value);
		}
	}
}
