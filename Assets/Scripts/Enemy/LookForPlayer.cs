using UnityEngine;
using System.Collections;

public class LookForPlayer : MonoBehaviour {

	public Transform lineOfSight;
	public float shootDelay = .5f;
	public GameObject projectilePrefab;
	public Vector2 firePosition = Vector2.zero;
	public float debugRadius = 3f;

	private float timeElapsed = 0f;
	private Vector3 sightStart, sightEnd;
	private bool collision = false;
	private Color debugColor = Color.green;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		sightStart = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		sightEnd = new Vector3 (lineOfSight.position.x, lineOfSight.position.y, lineOfSight.position.z);
		collision = Physics2D.Linecast (sightStart, sightEnd, 1 << LayerMask.NameToLayer ("Player"));
		if (collision) {
			if (projectilePrefab != null) {

				if (timeElapsed > shootDelay) {
					CreateProjectile (CalculateFirePosition());
					timeElapsed = 0;
				}
				timeElapsed += Time.deltaTime;
			}

		}
	}

	Vector2 CalculateFirePosition(){
		var pos = firePosition;
		// Multiply the x position of the vector by the direction the enemy is facing
		pos.x *= transform.localScale.x;
		pos.x += transform.position.x;
		pos.y += transform.position.y;

		return pos;
	}

	public void CreateProjectile(Vector2 pos){
		var clone = Instantiate (projectilePrefab, pos, Quaternion.identity) as GameObject;
		clone.transform.localScale = transform.localScale;
	}
		

	void OnDrawGizmos(){
		Gizmos.color = debugColor;
		Gizmos.DrawLine (sightStart, sightEnd);
	}
}
