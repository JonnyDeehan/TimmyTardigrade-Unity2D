using UnityEngine;
using System.Collections;

public class DustEffect : MonoBehaviour {

	private GameObject audioManagerObject;
	private AudioManager audioManager;

	void Awake(){
		audioManagerObject = GameObject.FindGameObjectWithTag ("AudioManager");
		audioManager = audioManagerObject.GetComponent<AudioManager> ();
	}

	void Start(){
		audioManager.PlayAudio (gameObject);
	}

	void OnDestroy(){
		Destroy (gameObject);
	}
}
