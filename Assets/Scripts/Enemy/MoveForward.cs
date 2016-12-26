using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

	public float speed = 5f;

	private Rigidbody2D body2d;

	void Awake() {
		body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		body2d.velocity = new Vector2 (transform.localScale.x * speed, body2d.velocity.y);
	}
}
