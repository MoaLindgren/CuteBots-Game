using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour {

	public static EndingManager instance;
	void Awake(){

		if(instance == null){

			instance = this as EndingManager;

		}else{
			Destroy(gameObject);
		}
	}

	public GameObject[] endings;

	public void ActivateEnding(int _ending){

		endings [_ending-1].SetActive (true);

		Debug.Log ("Ending " + _ending + "Activated");
	}
}
