using UnityEngine;
using System.Collections;

public class DropCubBtnKawashima : MonoBehaviour {
	private GameObject main;
	private MainKawashima mainkawashima;
	public bool isSplingNoMore = false;
	public bool isStraightNoMore = false;
	public bool isLeftNoMore = false;
	public bool isRightNoMore = false;
	public bool isBombNoMore = false; // < 0629 igarashi add
	// Use this for initialization
	void Start () {
		main =  GameObject.Find ("Main");
		mainkawashima = main.GetComponent<MainKawashima> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}public void fSplingClicked(){
		print ("start fSplingClickedClicked");
		if(isSplingNoMore) {
			return;
		}
		main.SendMessage ("fSplingUsed");
		mainkawashima.fDrop (0);
	}
	public void fLeftClicked(){
		print ("start fLeftClickedClicked");
		if(isLeftNoMore) {
			return;
		}
		main.SendMessage ("fLeftUsed");
		mainkawashima.fDrop (1);
	}
	public void fStraightClicked(){
		print("isStraightNoMore =  + isStraightNoMore");
		if(isStraightNoMore) {
			return;
		}
		print ("start fStraightClicked");
		main.SendMessage ("fStraightUsed");
		mainkawashima.fDrop (2);
	}

	public void fRightClicked(){
		print ("start fRightClicked");
		if(isRightNoMore) {
			return;
		}
		main.SendMessage ("fRightUsed");
		mainkawashima.fDrop (3);
	}
//********************************************* 0629 igarashi start
	public void fBombClicked() {
		print ("start fBombClicked");
		if(isBombNoMore) {
			return;
		}
		main.SendMessage("fBombUsed");
		mainkawashima.fDrop(4);
	}
//********************************************* 0629 igarashi end
}