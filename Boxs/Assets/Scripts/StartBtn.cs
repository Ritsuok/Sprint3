using UnityEngine;
using System.Collections;

public class StartBtn : MonoBehaviour {
	private GameObject playerObj;
	private PlayerControll playercontroll;

	private GameObject startCube;
	private startCubeAnim startcubeanim;

	public GameObject retryBtn;
	
	// Use this for initialization
	void Start () {
		gameObject.SetActive (true);
		playerObj = GameObject.Find("Boxs2nd1");
		playercontroll = playerObj.GetComponent<PlayerControll>();

		//retryBtn = GameObject.Find ("ButtonRetry");
		retryBtn.SetActive (false);

		startCube = GameObject.Find ("StartCube");
		startcubeanim = startCube.GetComponent<startCubeAnim> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void fLetsStart(){

		retryBtn.SetActive (true);
		gameObject.SetActive (false);
		playercontroll.fStartButton ();
		playercontroll.fNextMove();

		startcubeanim.fChangeGreen ();

	}
	public void fStartBtnOn(){
		gameObject.SetActive (true);
		retryBtn.SetActive (false);
	}
	public void fStartBtnOff(){
		gameObject.SetActive (false);
		retryBtn.SetActive (true);
	}
}