using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public GameObject player;
	public GameObject coinSourceObject;
	public GameObject powerUpSourceObject;
	public GameObject dustSourceObject;
	public GameObject bounceSourceObject;
	public GameObject backgroundMusicObject;
	private AudioSource coinSource;
	private AudioSource powerUpSource;
	private AudioSource dustSource;
	private AudioSource bounceSource;
	private AudioSource backgroundMusic;

	void Awake(){
		// Collectable Sounds
		coinSource = coinSourceObject.GetComponent<AudioSource> ();
		powerUpSource = powerUpSourceObject.GetComponent<AudioSource> ();
		dustSource = dustSourceObject.GetComponent<AudioSource> ();
		bounceSource = bounceSourceObject.GetComponent<AudioSource> ();
		// Background Music
		backgroundMusic = backgroundMusicObject.GetComponent<AudioSource>();
		backgroundMusic.Play ();
	}

	void Update(){
		transform.position = player.transform.position;
	}

	public void PlayAudio(GameObject collectable){
		if (collectable.gameObject.tag == "Coin") {
			// Play Sound
			coinSource.Play ();
		} else if (collectable.name == "PowerUpFlower") {
			powerUpSource.Play ();
		} else if (collectable.name == "DustSlide(Clone)") {
			dustSource.Play ();
		} else if (collectable.name == "BouncyPlatform") {
			bounceSource.Play ();
		}
	}
}
