using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartWindow : GenericWindow {

	public override void Open(){

			
		base.Open ();
	}

	public void NewGame(){
		Debug.Log ("New game pressed");
		SceneManager.LoadScene ("World1");
	}

	// TODO IMPLEMENT FURTHER MENU BUTTONS

	public void Continue(){
		Debug.Log ("Continue pressed");
	}

	public void Options(){
		Debug.Log ("Options pressed");
	}
}
