using UnityEngine;
using System.Collections;

public class LookForward : MonoBehaviour {

	public Transform whatToAvoid;

	private Vector3 sightStart, sightEnd;
	private bool collision = false;
	private Color debugColor = Color.yellow;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		sightStart = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		sightEnd = new Vector3 (whatToAvoid.position.x, whatToAvoid.position.y, whatToAvoid.position.z);
		var line = Physics2D.Linecast (sightStart, sightEnd, 1 << LayerMask.NameToLayer ("Solid"));

		if (line) {
			transform.localScale = new Vector3 ((transform.localScale.x == 1) ? -1 : 1, 1, 1);
		}
	}

	void OnDrawGizmos(){
		Gizmos.color = debugColor;
		Gizmos.DrawLine (sightStart, sightEnd);
	}
}
