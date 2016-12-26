using UnityEngine;
using System.Collections;

public class PortalController : MonoBehaviour {

	private bool canTeleport = true;
	private float timeElapsed = 0;
	public float teleportDelay = 4f;
	
	// Update is called once per frame
	void Update () {
		if (!canTeleport) {
			timeElapsed += Time.deltaTime;

			if (timeElapsed > teleportDelay) {
				this.canTeleport = true;
			}
		}
	}

	public bool CanTeleport(){
		return this.canTeleport;
	}

	public void DidTeleport(bool value){
		this.canTeleport = value;
	}
}
