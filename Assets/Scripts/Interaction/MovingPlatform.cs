using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public Transform whatToAvoid;
	public Vector3 move;
	private Vector3 sightStart, sightEnd;
	private bool collision = false;
	private Color debugColor = Color.yellow;

	// Update is called once per frame
	void Update () {
		Vector3 velocity = move * Time.deltaTime;
		transform.Translate (velocity);
		sightStart = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		sightEnd = new Vector3 (whatToAvoid.position.x, whatToAvoid.position.y, whatToAvoid.position.z);
		collision = Physics2D.Linecast (sightStart, sightEnd, 1 << LayerMask.NameToLayer ("Solid"));
		if (collision) {
			transform.localScale = new Vector3 ((transform.localScale.y == 1) ? -1 : 1, 1, 1);
			move *= -1;
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = debugColor;
		Gizmos.DrawLine (sightStart, sightEnd);
	}
}
