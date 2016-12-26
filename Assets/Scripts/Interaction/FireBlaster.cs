using UnityEngine;
using System.Collections;

public class FireBlaster : MonoBehaviour {

	private Vector3 projectilePosition;
	private bool didFire = false;

	public GameObject fireBlasterProjectilePrefab; // Reference to projectile prefab
	public float fireOffset;

	// Instantiate the projectile prefab at the calculated fire position
	IEnumerator CreateProjectile(){
		didFire = true;
		yield return new WaitForSeconds(fireOffset);
		projectilePosition = new Vector3 (transform.position.x, transform.position.y + 10, transform.position.z);
		var clone = Instantiate (fireBlasterProjectilePrefab, projectilePosition, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
		yield return new WaitForSeconds (1.5f - fireOffset);
		didFire = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Create a projectile every second
		if (!didFire) {
			StartCoroutine(CreateProjectile ());
		}
	}
}
