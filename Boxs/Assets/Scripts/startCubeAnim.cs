using UnityEngine;
using System.Collections;

public class startCubeAnim : MonoBehaviour {
	Animator animator;
	bool isGreen = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fChangeGreen(){
		isGreen = true;
		animator.SetBool ("isGreen", true);
		//animator.SetTrigger ("changeGreen");
		Debug.Log ("isGreen = " + isGreen);
	}
}
