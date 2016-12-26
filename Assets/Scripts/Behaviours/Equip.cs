using UnityEngine;
using System.Collections;

public class Equip : AbstractBehaviour {

	private int _currentItem = 0; // Local Current item id
	private Animator animator;

	// Public current item id
	public int currentItem{
		get {
			return _currentItem;
		}
		set{
			_currentItem = value;
			// Set the animator variable "EquippedItem" to the current item id
			animator.SetInteger ("EquippedItem", _currentItem);
			StartCoroutine (ItemDuration());
		}
	}

	override protected void Awake(){
		base.Awake ();
		animator = GetComponent<Animator> ();
	}

	IEnumerator ItemDuration(){
		yield return new WaitForSeconds (6f);
		if (_currentItem > 0)
			currentItem = 0;
	}
		
}
