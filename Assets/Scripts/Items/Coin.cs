using UnityEngine;
using System.Collections;

public class Coin : Collectable {

	float originalY;
	private float destroyDelay = .3f;
	public float floatStrength = 4f; 
	public GameObject audioManagerObject;
	private AudioManager audioManager;

	void Awake(){
		audioManager = audioManagerObject.GetComponent<AudioManager> ();
	}

	void Start(){
		this.originalY = this.transform.position.y;
	}

	override protected void OnCollect(GameObject target){
		// Do something with the coin for the player
		audioManager.PlayAudio(gameObject);
		// Destroy the coin
		Destroy(gameObject);
	}

	void Update(){
		// Floating the coin up and down
		transform.position = new Vector3(transform.position.x,
			originalY + ((float)Mathf.Sin(Time.time) * floatStrength),
			transform.position.z);

	}


}
