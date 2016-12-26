using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	public Vector2 initialVelocity = new Vector2 (100, 0);
	public GameObject player;
	private Rigidbody2D body2d;
	private CollisionState collisionState;
	private GameObject gameMasterObject;
	private GameMaster gameMaster;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		gameMasterObject = GameObject.FindGameObjectWithTag ("GameController");
		gameMaster = gameMasterObject.GetComponent<GameMaster> ();
	}

	// Use this for initialization
	void Start () {
		var startVelX = initialVelocity.x * transform.localScale.x;
		CollisionState collisionState = player.gameObject.GetComponent<CollisionState>();
		if (collisionState.onWall)
			startVelX *= -1;
			
		body2d.velocity = new Vector2 (startVelX, initialVelocity.y);
		body2d.constraints = RigidbodyConstraints2D.FreezePositionY;
	}

	void OnCollisionEnter2D(Collision2D target){
		if (target.gameObject.tag == "Hydra") {
			gameMaster.KillEnemy (target.gameObject);
		}
		Destroy (gameObject);
	}
}
