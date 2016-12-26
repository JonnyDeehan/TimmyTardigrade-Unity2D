using UnityEngine;
using System.Collections;

public class FireBlasterProjectile : MonoBehaviour {

	public Vector2 initialVelocity = new Vector2 (0, 100);
	private Rigidbody2D body2d;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		var startVelX = initialVelocity.x * transform.localScale.x;
		body2d.velocity = new Vector2 (startVelX, initialVelocity.y);
		body2d.constraints = RigidbodyConstraints2D.FreezePositionX;
	}

	void OnCollisionEnter2D(Collision2D target){
		if(target.gameObject.tag != "FireBlaster")
			Destroy (gameObject);
	}

}
