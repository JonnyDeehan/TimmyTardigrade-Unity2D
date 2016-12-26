using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGoal : MonoBehaviour {

	public GameObject GameMasterObject;
	private GameMaster gameMaster;

	// Reset the level gameobjects to initial state -> refer to GameMaster script

	void Awake(){
		gameMaster = GameMasterObject.GetComponent<GameMaster> ();
	}

	void ResetLevelConditions(){

	}

	void OnTriggerEnter2D(Collider2D colliderTrigger){
		if (colliderTrigger.gameObject.tag == "Player") {
			SceneManager.LoadScene ("GameWindows");
		}
	}
}
