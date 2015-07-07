using UnityEngine;
using System.Collections;

public class DashBtn : MonoBehaviour {
	private GameObject playerObj;
	private PlayerControll playercontroll;
	
	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find("Boxs2nd2");
		playercontroll = playerObj.GetComponent<PlayerControll>();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void fDashStart(){
		playercontroll.setDashFlgON ();
	}
	public void fDashEnd(){
		playercontroll.setDashFlgOFF ();
	}
}
