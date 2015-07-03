using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stage01ControlKawashima : MonoBehaviour {

	public int cubeSpling;
	public int cubeLeft;
	public int cubeStraight;
	public int cubeRight;
	public int cubeBomb; // < 0629 igarashi add

	private GameObject main;
	private MainKawashima mainkawashima;

	private Text splingTxt;
	private Text straightTxt;
	private Text leftTxt;
	private Text rightTxt;
	private Text bombTxt;  // < 0629 igarashi add

	private GameObject splingObj;
	private GameObject straightObj;
	private GameObject leftObj;
	private GameObject rightObj;
	private GameObject bombObj;  // < 0629 igarashi add

	private GameObject splingBtn;
	private GameObject straightBtn;
	private GameObject leftBtn;
	private GameObject rightBtn;
	private GameObject bombBtn;  // < 0629 igarashi add

	private DropCubBtnKawashima dropcubeJ;
	private DropCubBtnKawashima dropcubeS;
	private DropCubBtnKawashima dropcubeL;
	private DropCubBtnKawashima dropcubeR;
	private DropCubBtnKawashima dropcubeB;  // < 0629 igarashi add
	// Use this for initialization
	void Start () {
		main =  GameObject.Find ("Main");
		mainkawashima = main.GetComponent<MainKawashima> ();

		splingObj = GameObject.Find ("NumberSplingText");
		splingTxt = splingObj.GetComponent<Text>();
		splingTxt.text = cubeSpling.ToString();
		splingBtn = GameObject.Find ("ButtonDropSpling");
		dropcubeJ = splingBtn.GetComponent<DropCubBtnKawashima> ();

		straightObj = GameObject.Find ("NumberStraightText");
		straightTxt = straightObj.GetComponent<Text>();
		straightTxt.text = cubeStraight.ToString();
		straightBtn = GameObject.Find ("ButtonDropStraight");
		dropcubeS = straightBtn.GetComponent<DropCubBtnKawashima> ();

		leftObj = GameObject.Find ("NumberLeftText");
		leftTxt = leftObj.GetComponent<Text>();
		leftTxt.text = cubeLeft.ToString();
		leftBtn = GameObject.Find ("ButtonDropLeft");
		dropcubeL = leftBtn.GetComponent<DropCubBtnKawashima> ();


		rightObj = GameObject.Find ("NumberRightText");
		rightTxt = rightObj.GetComponent<Text>();
		rightTxt.text = cubeRight.ToString();
		rightBtn = GameObject.Find ("ButtonDropRight");
		dropcubeR = rightBtn.GetComponent<DropCubBtnKawashima> ();


//********************************************* 0629 igarashi start
		bombObj = GameObject.Find ("NumberBombText");
		bombTxt = bombObj.GetComponent<Text>();
		bombTxt.text = cubeBomb.ToString();
		bombBtn = GameObject.Find ("ButtonDropBomb");
		dropcubeB= bombBtn.GetComponent<DropCubBtnKawashima> ();
//********************************************* 0629 igarashi end
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void fSplingUsed(){
		if (mainkawashima.isSelected == false){
			return;
		}
		
		cubeSpling --;
		splingTxt.text = cubeSpling.ToString ();
		
		if (cubeSpling <= 0) {
			dropcubeJ.isSplingNoMore = true;
			return;
		}
	}
	public void fStraightUsed(){
		if (mainkawashima.isSelected == false){
			return;
		}

		cubeStraight --;
		straightTxt.text = cubeStraight.ToString ();

		if (cubeStraight <= 0) {
			dropcubeS.isStraightNoMore = true;
			return;
		}
	}
	public void fLeftUsed(){
		if (mainkawashima.isSelected == false){
			return;
		}

		cubeLeft --;
		leftTxt.text = cubeLeft.ToString ();

		if (cubeLeft <= 0) {
			dropcubeL.isLeftNoMore = true;
			return;
		}
	}
	public void fRightUsed(){
		if (mainkawashima.isSelected == false){
			return;
		}

		cubeRight --;
		rightTxt.text = cubeRight.ToString ();

		if (cubeRight <= 0) {
			dropcubeR.isRightNoMore = true;
			return;
		}
	}


//********************************************* 0629 igarashi start
	public void fBombUsed() {
		if (mainkawashima.isSelected == false){
			return;
		}

		cubeBomb --;
		bombTxt.text = cubeBomb.ToString ();
		
		if (cubeBomb <= 0) {
			dropcubeR.isRightNoMore = true;
			return;
		}
	}
//********************************************* 0629 igarashi end


}
