using UnityEngine;
using System.Collections;

public class Coordinates : MonoBehaviour {
	const int NONE =0;
	const int BLOCK =1;
	const int GOAL =2;

	const int WALL = 6;

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
		if (player.GetComponent<PlayerControll> ().frontFlg != GOAL ||
		    player.GetComponent<PlayerControll> ().frontFlg != WALL) 
		{
			if (c.gameObject.tag == "Block" || c.gameObject.tag == "Goal" ||
			    c.gameObject.tag == "TurnL" || c.gameObject.tag == "TurnR") {

				ctl.frontFlg = BLOCK;
			}
		}
	}


	void OnTriggerExit(Collider c)
	{
		if (player.GetComponent<PlayerControll> ().frontFlg != GOAL) 
		{
			ctl.frontFlg = NONE;
		}
	}

}
