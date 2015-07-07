using UnityEngine;
using System.Collections;

public class CoordinatesOfGoal : MonoBehaviour {
	const int NONE =0;
	const int BLOCK =1;
	const int GOAL =2;
	const int RIGHT = 3;
	const int LEFT = 4;
	const int SPLING = 6;
	
	
	public GameObject player;
	PlayerControll ctl;
	
	// Use this for initialization
	void Start () {
		ctl = player.GetComponent<PlayerControll> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider c)
	{
		if (player.GetComponent<PlayerControll> ().frontFlg != GOAL) 
		{
			if (c.gameObject.tag == "Goal") {
				ctl.floorFlg = GOAL;
			}
			
			//********************************************* 0620 yamaguchi start
			else if(c.gameObject.tag == "Block" || c.gameObject.tag == "Floor")
			{
				ctl.floorFlg = BLOCK;
			}

			else if(c.gameObject.tag == "TurnR")
			{
				ctl.floorFlg = RIGHT;
				c.gameObject.tag = "CheckOut";
				
				//				print ("HHH" + c.gameObject.tag);
			}
			else if(c.gameObject.tag == "TurnL")
			{
				ctl.floorFlg = LEFT;
				c.gameObject.tag = "CheckOut";
			}
			else if(c.gameObject.tag == "Spling"){
				ctl.floorFlg = SPLING;
				c.gameObject.tag = "CheckOut";
			}
			//********************************************* 0620 yamaguchi finish
			
		}
	}
	
	void OnTriggerExit(Collider c){

		print ("change floorFlag");

//		ctl.floorFlg = NONE;
//		StartCoroutine ("fChangeFlg");
	}

	IEnumerator fChangeFlg(){
		yield return new WaitForSeconds (3.5f);
		ctl.floorFlg = NONE;
	}
}
