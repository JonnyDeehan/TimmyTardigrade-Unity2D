using UnityEngine;
using System.Collections;

public class BreakObject : MonoBehaviour {

	private string playerTag = "Player"; // Player Tag
	private string projectileTag = "Projectile"; // Projectile Tag
	private Animator animator; // Animator reference

	// Use this for initialization
	void Awake () {
		animator = GetComponent<Animator> ();
		// Idle block animation
		animator.SetInteger("AnimState", 0);
	}
		
	void OnCollisionEnter2D(Collision2D target){

		if (target.gameObject.tag == playerTag) {
			Dash dash = target.gameObject.GetComponent<Dash> ();
			// Obtain reference to the Dash object associated with the Player targer
			if(dash.didDash){
				// Play break block animation
				animator.SetInteger("AnimState", 1);
			}
		}
		if (target.gameObject.tag == projectileTag) {
			animator.SetInteger ("AnimState", 1);
		}
	}

	// Called on the last frame of the destroy block animation
	public void DestroyObject(){
		Destroy (gameObject);
	}

}
