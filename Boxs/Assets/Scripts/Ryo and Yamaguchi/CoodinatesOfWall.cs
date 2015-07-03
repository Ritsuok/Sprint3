using UnityEngine;
using System.Collections;

public class CoodinatesOfWall : MonoBehaviour {
	const int NONE =0;
	const int BLOCK =1;
	const int GOAL =2;
	const int LOWBLOCK = 5;
	const int WALL = 6;

	const int RIGHT = 3;
	const int LEFT = 4;
	
	public GameObject player;
	PlayerControll ctl;
	
	// Use this for initialization
	void Start () {
		ctl = player.GetComponent<PlayerControll> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay(Collider c)
	{
		if (ctl.frontFlg != RIGHT || ctl.frontFlg != LEFT) 
		{
			ctl.upFlg = BLOCK;
		}
		/*
		if (player.GetComponent<PlayerControll> ().flag == BLOCK) 
		{
			ctl.flag = WALL;
		}
		*/
	}
	
	
	void OnTriggerExit(Collider c)
	{
		if (ctl.frontFlg != RIGHT || ctl.frontFlg != LEFT) 
		{
			ctl.upFlg = NONE;
		}
		/*
		if (player.GetComponent<PlayerControll> ().flag != GOAL) 
		{
			ctl.flag = NONE;
		}
		*/
	}
	
}