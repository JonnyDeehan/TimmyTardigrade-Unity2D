using UnityEngine;
using System.Collections;

// NOT IN USE

public class CameraFollow : MonoBehaviour {

	public GameObject player;
	private Vector3 newPosition;
	private Camera gameCamera;
	private bool didEnterNextSection = false;
	private Vector3 currentCameraPosition;
	private float screenEdgeX;
	private float screenEdgeY;

	void Awake () {
		gameCamera = GetComponent<Camera> ();
		currentCameraPosition = transform.position;
		screenEdgeX = currentCameraPosition.x + Screen.width/2;
		screenEdgeY = currentCameraPosition.y + Screen.height/2;
	}
		
	void LateUpdate () {

		if (player != null) {
			if (player.transform.position.x > screenEdgeX) {
				currentCameraPosition = new Vector3 (currentCameraPosition.x + Screen.width, currentCameraPosition.y, currentCameraPosition.z);
			} 
			if (player.transform.position.y > screenEdgeY) {
				currentCameraPosition = new Vector3 (currentCameraPosition.x, currentCameraPosition.y + Screen.height, currentCameraPosition.z);
			}
		}
	}
}
