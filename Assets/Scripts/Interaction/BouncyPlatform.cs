using UnityEngine;
using System.Collections;

public class BouncyPlatform : MonoBehaviour {

	public float bouncyMultiplier = 60f;
	private string playerTag = "Player";
	private GameObject audioManagerObject;
	private AudioManager audioManager;

	void Awake(){
		audioManagerObject = GameObject.FindGameObjectWithTag ("AudioManager");
		audioManager = audioManagerObject.GetComponent<AudioManager> ();
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == playerTag) {
			// Bounce Sound
			audioManager.PlayAudio(gameObject);
			target.rigidbody.AddForce (new Vector2 (target.relativeVelocity.x, target.relativeVelocity.y * bouncyMultiplier));
		}
	}
}
