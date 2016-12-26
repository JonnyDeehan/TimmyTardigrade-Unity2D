using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	private float health;
	public bool immunity = false;
	public float maxHealth = 100;
	private FlashSprite flashSprite;
	private Dash dash;

	void Awake(){
		health = maxHealth;
	}

	public float GetHealth(){
		return health;
	}

	public void SetHealth(float value){
		health = value;
	}

	public void TakeDamage(float damage){
		if (!immunity) {
			health -= damage;
			// Start Co-routine after taking damage to make invulnerable and change the colour of the player
			StartCoroutine(ImmuneForDuration());
		}
	}

	IEnumerator ImmuneForDuration(){
		immunity = true;
		yield return new WaitForSeconds (1.5f);
		immunity = false;
	}

	void Update(){
		if (gameObject.tag == "Player" && immunity ) {
			if (!gameObject.GetComponent<Dash> ().didDash) {
				flashSprite = gameObject.GetComponent<FlashSprite> ();
				SpriteRenderer[] sprites = new SpriteRenderer[1];
				SpriteRenderer currentSprite = gameObject.GetComponent<SpriteRenderer> ();
				sprites [0] = currentSprite;
				flashSprite.StartCoroutine (flashSprite.FlashSprites (sprites, 2, 0.1f, true));
			}
		}
	}

	public void RestoreHealth(float restoreAmount){
		if (health + restoreAmount > maxHealth)
			health = maxHealth;
		else
			health += restoreAmount;
	}
}
