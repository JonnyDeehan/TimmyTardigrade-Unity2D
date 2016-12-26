using UnityEngine;
using System.Collections;

public class PowerUp : Collectable {

	public int itemId = 1;
	public GameObject projectilePrefab;
	public GameObject audioManagerObject;
	private AudioManager audioManager;

	void Awake(){
		audioManager = audioManagerObject.GetComponent<AudioManager> ();
	}


	override protected void OnCollect(GameObject target){
		var equipBehaviour = target.GetComponent<Equip> ();
		if (equipBehaviour != null) {
			equipBehaviour.currentItem = itemId;
		}
		// Do something with the coin for the player
		audioManager.PlayAudio(gameObject);

		var shootBehaviour = target.GetComponent<FireProjectile> ();
		if (shootBehaviour != null) {
			shootBehaviour.projectilePrefab = projectilePrefab;
		}
	}
		
}
