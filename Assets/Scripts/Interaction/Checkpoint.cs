using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

	public GameObject GameMasterObject;
	private GameMaster gameMaster;

	void Awake(){
		gameMaster = GameMasterObject.GetComponent<GameMaster> ();
	}

	void OnTriggerEnter2D(Collider2D colliderTrigger){
		if (colliderTrigger.gameObject.tag == "Player") {
			gameMaster.spawnPoint.position = transform.position;
		}
	}
}
