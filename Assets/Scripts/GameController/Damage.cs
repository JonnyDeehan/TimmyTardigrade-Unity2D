using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

	public float damageMultiplier = 1f;
	public string targetTag;
	private GameObject target;
	private float damage = 50f;
	private Health health;

	void Awake () {
		target = GameObject.FindGameObjectWithTag (targetTag);
		if (GameObject.FindGameObjectWithTag (targetTag)) {
			health = target.GetComponent<Health> ();
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.tag == targetTag) {
			health.TakeDamage (damage*damageMultiplier);
		}
	}
}
