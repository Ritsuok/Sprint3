/*
 * 	0703 yamaguchi
 * 
 * 　testのため 
 * 　Application.LoadLevel ("GameOver");
 * 	を
 * 　Application.LoadLevel ("stage01");
 * 
 * 　へ変更
*/
/*aho
 * 0630 yamaguchi count down
 * 
 * 実装
 * 　スタート時のカウントダウン
 * 
 * 内容
 * 　右上のカウントダウンエリアのtextに player内のstartFlgの値分カウントダウンの表示がされる
 * 
 * 必要なオブジェクト
 * 　CountDownCanvas および その子オブジェクト全て
 * 
 * 必要なスクリプト
 *　PlayerControll.cs内
 *　0630 yamaguchi count downで囲まれている2箇所
 *
 *
 ****************************************************************************************************
 ****************************************************************************************************
 ****************************************************************************************************
 *
 * 0630 yamaguchi dash
 *
 *実装
 *　speed up button 押下時 速度up
 *
 *内容
 *　左上のSPEED UP button押下中のみplayerのspeedが3倍になる
 *
 *必要なオブジェクト
 *　SpeedUpCanvas および その子オブジェクト全て
 *
 *必要なスクリプト
 *　PlayerControll.cs内
 *　0630 yamaguchi dashで囲まれている4箇所
 *
*/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
	//-----------------------------------0706 igarashi add some field
	//isSelectHeadButt
	//isSelectDown
	//ヘッドバットと後ろに転ぶどうさが、コライダーの判定上、時間差でほぼ同時に起こることがある
	//どちらか片方が選択されてらどちらか片方は発生しないようにする変数
	//この変数は毎回fNextMoveが呼ばれるたびに初期化される
	bool isSelectButt = false;
	bool isSelectDown = false;
	//isMoveYaxis
	//Y軸移動が始まってるかどうかのflag
	//fclimbPlayerなどでY軸移動がはじまったらもう後ろに転ばさないようにして破壊のみにする
	//この変数は毎回fNextMoveが呼ばれるたびに初期化される
	bool isMoveYaxis = false;
	
	public GameObject HitEffect;
	//---------------------------------------------------------------------



	const int NONE =0;
	const int BLOCK =1;
	const int GOAL =2;
	
	//********************************************* 0620 yamaguchi start
	const int RIGHT = 3;
	const int LEFT = 4;
	
	const int LOWBLOCK = 5;
	//********************************************* 0620 yamaguchi finish
	
	const int SPLING = 6;
	
	public float speed;
	
	public int frontFlg = NONE;
	public int downFlg = BLOCK;
	public int upFlg = NONE;
	public int floorFlg = BLOCK;
	
	public int cnt;
	
	
	Animator anim;
	bool isGoAhead;
	
	string direction;
	
	//*************************************************** 0630 yamaguchi count down start
	private GameObject cdCanvas;
	private Text cdText;
	
	public int startFlg;
	
	public void fStartButton(){
		this.startFlg = 1;
	}
	//*************************************************** 0630 yamaguchi count down finish
	
	//*************************************************** 0630 yamaguchi dash start
	private int dashFlg = 0;
	private float speed0;
	
	private int testCnt=0;
	//*************************************************** 0630 yamaguchi dash finish
	//*************************************************** 0702 kawashima changeGreen start
	private GameObject startcube;
	private startCubeAnim startcubeanim;
	
	private GameObject startbtnObj;
	private StartBtn startbtn;
	
	//*************************************************** 0702 kawashima changeGreen finish
	//*************************************************** 0703 kawashima changeGreen start
	private GameObject circleObj;
	private RadialTimerScript radicaltimer;
	private GameObject subCamera;
	private PlayerCamera playerCamera;
	//*************************************************** 0703 kawashima changeGreen finish

	//*************************************************** 0703 yamaguchi DebugTest start
	public string methodName = "aho";

	void Awake()
	{
		downFlg = BLOCK;
		
		direction = "North";
		
		anim = GetComponent<Animator> ();
		anim.SetBool ("isMove", true);
		
		isGoAhead = true;
		
	}
	
	// Use this for initialization
	void Start () {
		
		//		fNextMove ();

		
		//*************************************************** 0630 yamaguchi dash start
		speed0 = speed;
		//*************************************************** 0630 yamaguchi dash finish
		//*************************************************** 0703 kawashima changeGreen start
		circleObj = GameObject.Find ("CircleGageDummy");
		radicaltimer = circleObj.GetComponent<RadialTimerScript> ();
		subCamera = GameObject.Find ("SubCamera");
		playerCamera = subCamera.GetComponent<PlayerCamera> ();
		//*************************************************** 0703 kawashima changeGreen finish
		StartCoroutine ("fStart");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
	
	//*************************************************** 0630 yamaguchi dash start
	public void setDashFlgON(){
		this.dashFlg = 1;
		//print ("set dashFlg ON");
	}
	public void setDashFlgOFF(){
		this.dashFlg = 0;
		//print ("set dashFlg OFF");
	}
	
	//*************************************************** 0630 yamaguchi dash finish
	
	IEnumerator fStart(){

		//********************************************************************* 0630 yamaguchi count down start
		cdCanvas = GameObject.Find ("CountDownText");
		cdText = cdCanvas.GetComponent<Text>();
		
		for (int i = 0; i < cnt; i++) {
			//print (i);
			cdText.text = (cnt - i - 1).ToString();
			//			Text target = cdCanvas.GetComponent<Text>();

			
			if(startFlg == 1){
				//break;
			}else{
				yield return new WaitForSeconds (1.0f);
			}
			//ここでカウントダウン用表示
		}

		fSendCountStart ();
		
		cdText.text = "GO";
	
		//********************************************************************* 0630 yamaguchi count down finish
		//*************************************************** 0702 kawashima changeGreen start
		startcube = GameObject.Find ("StartCube");
		startcubeanim = startcube.GetComponent<startCubeAnim> ();
		startcubeanim.fChangeGreen ();
		
		startbtnObj = GameObject.Find ("ButtonStart");
		if (startbtnObj != null) {
			startbtn = startbtnObj.GetComponent<StartBtn> ();
			startbtn.fStartBtnOff ();
		}

		//*************************************************** 0702 kawashima changeGreen finish
		fNextMove ();
	}
	
	//********************************************* 0620 yamaguchi start
	public void fNextMove()
	{
		//-------------------------------------------------igarashi add
		//初期化
		isSelectButt = false;
		isSelectDown = false;
		isMoveYaxis = false;
		//-------------------------------------------------igarashi add


		//*************************************************** 0630 yamaguchi dash start
		//print ("dashFlg check" + dashFlg);
		if (dashFlg == 1) {
			this.speed *= 3;
			dashFlg = 2;
		} else if (dashFlg == 0) {
			this.speed = speed0;
		}

		//*************************************************** 0703 yamaguchi DebugTest start

		StartCoroutine ("testFreeze");
		print (methodName);
		//*************************************************** 0703 yamaguchi DebugTest finish

		//*************************************************** 0630 yamaguchi dash finish
		
		//print ("fNextMove player.position = " + transform.position.x + " , " + transform.position.y + " , " + transform.position.z);
		//print ("floorFlg = " + floorFlg + " downFlg = " + downFlg);
		
		if (floorFlg == GOAL) {
			//print ("fGoalPlayer");
			StartCoroutine ("fGoalPlayer");
			//*************************************************** 0703 kawashima changeGreen start
			radicaltimer.fStopCount();
			playerCamera.fChangeToSubCamera(gameObject.transform);
			//*************************************************** 0703 kawashima changeGreen finish
		}
		
		//********************************************* 0620 yamaguchi start
		else if (floorFlg == RIGHT) {
			//print ("fTurnRightPlayer");
			StartCoroutine ("fTurnRightPlayer");
			
			//********************************************* 0620 yamaguchi start
		} else if (floorFlg == LEFT) {
			//print ("fTurnLeftPlayer");
			StartCoroutine ("fTurnLeftPlayer");
		}
		//********************************************* 0620 yamaguchi finish
		
		else if (frontFlg == BLOCK && upFlg == BLOCK) {
			//print ("fTrouble");
			//			fTroublePlayer ();
			StartCoroutine("fTroublePlayer");
		}
		//********************************************* 0620 yamaguchi finish
		
		else if (frontFlg == BLOCK) {
			//print ("fClimbPlayer");
			StartCoroutine ("fClimbPlayer");
		}
		/*		//********************************************* 0620 yamaguchi finish
		else if (floorFlg == NONE) {
			//print ("fFallPlayer");
			StartCoroutine("fFallPlayer");
		}
*/
		else if (downFlg == NONE) {
			//print ("fDownPlayer");
			StartCoroutine ("fDownPlayer");
		}
		else if (floorFlg == SPLING) {
			
			//print ("fSpling1Player");
			StartCoroutine ("fSpling1Player");
		}
		else if (frontFlg== NONE)
		{
			print ("fWalkPlayer" + transform.position);
			StartCoroutine ("fWalkPlayer");
		}
		
	}
	
	
	//********************************************* 0620 yamaguchi finish
	//Walk Player
	//*******************************************************************
	
	IEnumerator fWalkPlayer()
	{

		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fWalkPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		//GetComponent<Rigidbody> ().useGravity = true;
		//GetComponent<CapsuleCollider> ().enabled = true;
		//print ("fWalkPlayer" + transform.position.z);
		
		
		for (int i = 0; i < (int)(1.0f / speed); i++)
		{
			transform.Translate (0, 0, speed);
			yield return new WaitForSeconds (0.01F);
			
		}
		
		//残りの移動
		transform.Translate (0, 0, 1.0f - (int)(1.0f / speed) * speed);

		print ("test");
		print(1.0f - (int)(1.0f / speed) * speed);
		//GetComponent<Rigidbody> ().useGravity = false;
		//GetComponent<CapsuleCollider> ().enabled = false;
		
		//		//print ("fWalkPlayer");
		//print ("fWalkPlayer2" + transform.position.z);
		fNextMove ();
		
	}
	//********************************************* 0620 yamaguchi start
	//RightTurn Player
	//*******************************************************************
	
	IEnumerator fTurnRightPlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fTurnRightPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		transform.Rotate (0, 90, 0);
		
		floorFlg = BLOCK;
		//print ("fTurnRightPlayer , " + floorFlg);
		yield return new WaitForSeconds (0.1F);
		
		
		fNextMove ();
	}
	
	//********************************************* 0620 yamaguchi start
	//LeftTurn Player
	//*******************************************************************
	
	IEnumerator fTurnLeftPlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fTurnLeftPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		transform.Rotate (0, -90, 0);
		
		floorFlg= BLOCK;
		
		yield return new WaitForSeconds (0.1F);
		
		fNextMove ();
	}
	
	//********************************************* 0620 yamaguchi start
	//Down Player
	//*******************************************************************
	
	IEnumerator fDownPlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fDownPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		anim.SetTrigger ("isJumpTrigger");
		
		//		transform.Translate (0, -1, 1);
		//		//print ((int)((1 / speed) * (2.0f/3)));
		float dist = 1F;
		
		float speed3 = 0.04f;
		
		//print ("横ジャンプ" + speed3 + " , " + (int)(1.0f / speed3));
		
		float deltaDistZ = 0;
		float deltaDistY = 0;
		//for (int i = 0; i < (int)(dist / speed); i++)
		for (int i = 0; i < 20; i++)
		{
			
			transform.Translate (0, 0, speed3);
			deltaDistZ += speed3;
			yield return new WaitForSeconds (0.01F);
		}
		
		//print ("downFlg = " + downFlg);
		yield return new WaitForSeconds (0.03f);
		
		
		//		if (downFlg == NONE) {
		//			StartCoroutine("fFallPlayer");
		//			//print ("GAMEOVER");
		//			yield return new WaitForSeconds(10.0f);
		//		}
		//		transform.Translate (0, 0, 1.0f - 25.0f*(int)(dist / speed3));
		
		//yield return new WaitForSeconds (0.1F);
		
		
		//print ((int)(dist / speed));
		//print (1.0f - (int)(dist / speed)*speed);
		
		float xx = transform.position.x;
		float zz = transform.position.z;
		
		//		GetComponent<Rigidbody> ().useGravity = true;
		//		GetComponent<BoxCollider> ().enabled = true;
		
		float speed2 = 0.05F;
		
		
		
		//print ("落下" + speed2 + " , " + (int)(1.0f / speed2));
		for (int i = 0; i < (int)(1.0f / speed2); i++) {
			
			transform.Translate (0, - speed2, speed3/5.0f);
			yield return new WaitForSeconds (0.01F);
			
			deltaDistZ += speed3/5.0f;
			deltaDistY += speed2;
		}
		
		//		GetComponent<Rigidbody> ().useGravity = false;
		//		GetComponent<BoxCollider> ().enabled = false;
		
		transform.Translate (0, -(1.0f - deltaDistY), 1 - deltaDistZ);
		
		if (floorFlg != NONE) {
			yield return new WaitForSeconds (0.5f);
			//print ("deltaDistY = " + deltaDistY);
		}
		
		
		/*
		int fallCnt = 0;
		while (floorFlg == NONE) {
			StartCoroutine("fFallPlayer");
			yield return new WaitForSeconds(0.05f);
			fallCnt++;

			if(fallCnt >5){
				Application.LoadLevel("GameOver");
			}
		}
		if(fallCnt >0){
			yield return new WaitForSeconds(0.5f);
			Application.LoadLevel("GameOver");
		}
		*/
		StartCoroutine ("fFallGameOverChk");
		/*		
		//print ("横ジャンプ" + speed3 + " , " + (int)(1.0f / speed3));
		
		//for (int i = 0; i < (int)(dist / speed); i++)
		for (int i = 0; i < (int)(1.0f / speed3); i++)
		{
			
			transform.Translate (0, 0, speed3);
			yield return new WaitForSeconds (0.01F);
		}
		//		transform.Translate (0, 0, 1.0f - 25.0f*(int)(dist / speed3));
		
		//yield return new WaitForSeconds (5F);
		
		//print ((int)(dist / speed));
		//print (1.0f - (int)(dist / speed)*speed);
		
		
		float speed2 = 0.06F;
		
		
		//print ("落下" + speed2 + " , " + (int)(1.0f / speed2));
		for (int i = 0; i < (int)(1.0f / speed2); i++) {
			
			transform.Translate (0, - speed2, 0);
			yield return new WaitForSeconds (0.01F);
		}
		
*/		
		yield return new WaitForSeconds (0.5F);
		//print ("downFlg = " + downFlg);
		
		
		
		anim.SetTrigger ("isMoveTrigger");
		
		//print (transform.position.z);
		fNextMove ();
	}
	
	//********************************************* 0620 yamaguchi start
	//Climb Player
	//*******************************************************************
	
	IEnumerator fClimbPlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fClimbPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		//print (transform.position.x + " , " + transform.position.y + " , " + transform.position.z);
		
		
		
		anim.SetTrigger ("isMoveTrigger");
		
		//int count = 10;
		float dist = 0.2F;
		
		float sumDist = 0;
		//print ("speed = " + speed + " , dist = " + dist);
		for (int i = 0; i < (int)(dist / speed); i++)
		{
			
			transform.Translate (0, 0, speed);
			sumDist += speed;
			yield return new WaitForSeconds (0.01F);
		}
		transform.Translate (0, 0,  ( (dist / speed) - (int)( dist / speed)) * speed );
		//print ("speed = " + speed + " , dist = " + dist + " , sumDist = " + sumDist);
		sumDist += ((dist / speed) - (int)(dist / speed)) * speed;
		//print (((dist / speed) - (int)(dist / speed)) * speed);
		
		anim.SetTrigger ("isClimbTrigger");
		yield return new WaitForSeconds (0.5F);
		
		float speed2 = 0.1F;
		
		
		
		for (int i = 0; i < (int)1 / speed2; i++) {
			
			transform.Translate (0, speed2, 0);
			yield return new WaitForSeconds (0.01F);
		}
		
		anim.SetTrigger ("isMoveTrigger");
		
		
		//		//print ("count - dist " + (1 - (count * dist)));
		for (int i = 0; i < (int)((1- dist) / speed); i++) {
			
			
			transform.Translate (0, 0, speed);
			sumDist += speed;
			yield return new WaitForSeconds (0.01F);
			
		}
		
		
		transform.Translate (0, 0, ((1 - dist) - ((int)((1 - dist) / speed)) * speed));
		
		//print ("***speed = " + speed + " , dist = " + dist + " , sumDist = " + sumDist);
		sumDist += ((1 - dist) - ((int)((1 - dist) / speed)) * speed) * speed;
		//print (((1 - dist) - ((int)((1 - dist) / speed)) * speed) * speed);
		//print (((1 - dist) - ((int)((1 - dist) / speed)) * speed));
		//print ((1 - dist) + " , - " +  ((int)((1 - dist) / speed)) * speed);
		
		//		transform.Translate (0, 0, Mathf.RoundToInt (transform.position.z) - transform.position.z);
		//print(Mathf.RoundToInt(transform.position.z));
		
		fNextMove ();
	}
	
	//********************************************* 0620 yamaguchi start
	//Trouble Player
	//*******************************************************************
	
	IEnumerator fTroublePlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fTroublePlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		//		anim.SetBool ("isTrouble", true);
		
		anim.SetBool ("isTrouble", true);
		
		
		float timeUp = 0;
		
		while(frontFlg == BLOCK && upFlg == BLOCK)
		{
			yield return new WaitForSeconds (0.1F);
			timeUp += 0.1F;
			
			if (timeUp > 3)
			{
				fPeePlayer();
			}
		}
	}
	
	//********************************************* 0620 yamaguchi start
	//Peeing Player
	//*******************************************************************
	
	void fPeePlayer()
	{
		//print ("失禁");
		
		Application.LoadLevel ("GameOver");
	}
	
	//********************************************* 0620 yamaguchi start
	//Goal Player
	//*******************************************************************
	IEnumerator fGoalPlayer()
	{
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fGoalPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		//anim.SetBool ("isMove", false);
		anim.SetTrigger ("isWinTrigger");
		yield return new WaitForSeconds (0.01F);
		
	}
	
	//******************************************************************************
	//
	//******************************************************************************
	IEnumerator fSpling1Player(){
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fSpling1Player";
		//*************************************************** 0703 yamaguchi DebugTest finish

		yield return new WaitForSeconds(0.01f);
		
		anim.SetTrigger ("isJumpTrigger");
		//		anim.SetBool ("test",true);
		//print (testCnt);
		testCnt++;
		
		int count = (int)(1.0f / 0.02f)-2;
		for (int i = 0; i < count; i++) {
			yield return new WaitForSeconds(0.01f);
			transform.Translate(0,0,2.0f/count);
		}
		
		yield return new WaitForSeconds (0.01f);
		if (floorFlg == NONE) {
			StartCoroutine("fFallGameOverChk");
		}
		
		yield return new WaitForSeconds(0.5f);
		
		//		floorFlg = BLOCK;
		//		anim.SetTrigger ("isMoveTrigger");
		
		fNextMove ();
	}
	
	IEnumerator fFallPlayer(){
		//*************************************************** 0703 yamaguchi DebugTest start
		methodName = "fFallPlayer";
		//*************************************************** 0703 yamaguchi DebugTest finish

		float t = 0f;
		float dt = 0.15f;
		while (t<1.0f) {
			transform.Translate(0,-dt,0);
			t+=dt;
			yield return new WaitForSeconds (0.01f);
		}
		
		//		floorFlg = BLOCK;
		
		yield return new WaitForSeconds (1.5f);
		
		anim.SetTrigger ("isMoveTrigger");
		//fNextMove ();
	}
	
	IEnumerator fFallGameOverChk(){
		GetComponent<Rigidbody> ().useGravity = true;
		GetComponent<BoxCollider> ().enabled = true;
		
		int fallCnt = 0;
		while (floorFlg == NONE) {
			StartCoroutine("fFallPlayer");
			yield return new WaitForSeconds(0.05f);
			fallCnt++;
			
			if(fallCnt >5){
				Application.LoadLevel("GameOver");
			}
		}
		if(fallCnt >0){
			yield return new WaitForSeconds(0.5f);
			Application.LoadLevel("GameOver");
		}
		
		GetComponent<Rigidbody> ().useGravity = false;
		GetComponent<BoxCollider> ().enabled = false;
		
	}
	

	
	private void fSendCountStart(){
		
		radicaltimer.fStartCount ();
	}
	
	IEnumerator testFreeze(){
		print ("testFreeze" + transform.position);
		yield return new WaitForSeconds(1.0f);
	}


	//-------------------------------------------------0630 method add by igarashi
	//fAvoidBlock()
	
	public IEnumerator fAvoidBlock(GameObject objectC)
	{
		if (isSelectDown == true) yield break;
		isSelectButt = true;
		
		//print ("ヘッドバンド!");
		GetComponent<Animator>().SetTrigger("isHeadButtTrigger");
		Instantiate (HitEffect, new Vector3(transform.localPosition.x, transform.localPosition.y + 1.5F, transform.localPosition.z), transform.localRotation);
		
		Destroy (objectC);
		
		yield return new WaitForSeconds (0.02F);
		frontFlg = NONE;
		print ("ここで変えてる");
	}
	
	//-----------------------------------------------------0702 method add by igarashi
	//fWaitPlayer
	
	IEnumerator fWaitPlayer(GameObject objectC)
	{
		if (isSelectButt == true) yield break;
		
		print ("ここにきてないか");
		
		float calcY = transform.localPosition.y ;
		
		for (int count = 0; count < 10; count++)
		{
			if(calcY > 1) calcY -= 1;
			else break;
			print (calcY);
		}
		
		//Y軸移動がはじまってるいる場合
		if ( calcY < 0.5700F || calcY > 0.57001)
		{
			print ("ここか？" + calcY + "    " +  transform.localPosition.y);
			isMoveYaxis = true;
			yield break;
		}
		
		float playerPosX = transform.localPosition.x;
		float playerPosZ = transform.localPosition.z;
		float blockPosX = objectC.transform.position.x;
		float blockPosZ = objectC.transform.position.z;
		
		const int NORTH = 0;
		const int EAST = 1;
		const int SOUTH = 2;
		const int WEST = 3;
		
		int direction = 0;
		float distanceX = 0;
		float distanceZ = 0;
		
		bool backMove = false;
		
		print( "があｇ" + playerPosX + blockPosX );
		print( "があｇ" + Mathf.RoundToInt(playerPosX) + Mathf.RoundToInt(blockPosX) );
		
		//中心点からの移動距離を算出
		switch ((int)transform.localRotation.y)
		{
			
			
			
			//z軸に++で動いてる
		case 0:
			if ( Mathf.RoundToInt(playerPosX) == Mathf.RoundToInt(blockPosX) )
			{
				
				float dis = blockPosZ - playerPosZ;
				print("ここかddd" + dis);
				print("ここかhhh" + playerPosZ);
				if ( dis < 1 && isMoveYaxis == false)
				{
					print("おかしくね");
					GetComponent<Animator>().SetTrigger("isDownTrigger");
					fAllMoveCancel();
					distanceX = 0;
					distanceZ = - (1 - dis);
					backMove = true;
					
					isSelectDown = true;
				}
			}
			break;
			
			//x軸に++で動いてる
		case 90:
			if ( Mathf.RoundToInt(playerPosZ) == Mathf.RoundToInt(blockPosZ) )
			{
				float dis = blockPosX - playerPosX;
				
				if ( dis < 1 && isMoveYaxis == false)
				{
					fAllMoveCancel();
					distanceX = 1 - dis;
					distanceZ = 0;
					backMove = true;
					
					isSelectDown = true;
				}
			}
			break;
			
			//z軸に--で動いてる
		case 180:
			if ( Mathf.RoundToInt(playerPosX) == Mathf.RoundToInt(blockPosX) )
			{
				float dis = playerPosZ - blockPosZ;
				
				if ( dis < 1 && isMoveYaxis == false)
				{
					fAllMoveCancel();
					distanceX = 0;
					distanceZ = 1 - dis;;
					backMove = true;
					
					isSelectDown = true;
				}		
			}
			break;
			
			//x軸に--で動いてる
		case 270:
			if ( Mathf.RoundToInt(playerPosZ) == Mathf.RoundToInt(blockPosZ) )
			{
				float dis = playerPosX - blockPosX;
				
				if ( dis < 1 && isMoveYaxis == false)
				{
					fAllMoveCancel();
					distanceX = 1 - dis;
					distanceZ = 0;
					backMove = true;
					
					isSelectDown = true;
				}
			}
			break;
		}
		
		
		for ( int count = 0; count < 10 ; count ++)
		{
			//向き替えたから逆方向に進むよそりゃ
			transform.Translate(distanceX / 10, 0,  distanceZ / 10);
			yield return new WaitForSeconds (0.01F);
		}
		
		
		//起き上がりアニメ修了までしばし待つ
		yield return new WaitForSeconds (1.5F);
		//ブロックが落ち切るまでたいき、落ち切ったらfNextMoveを呼ぶ
		while (true)
		{
			if (objectC.GetComponent<RollArrow>().IsGround == true)
			{
				fNextMove();
				break;
			}
			yield return new WaitForSeconds (0.01F);
		}
		
	}
	
	
	//-----------------------------------------------------0702 method add by igarashi
	//fAllMoveCancel
	
	void fAllMoveCancel()
	{
		StopCoroutine ("fWalkPlayer");
		StopCoroutine ("fTurnRightPlayer");
		StopCoroutine ("fTurnLeftPlayer");
		StopCoroutine ("fTurnDownPlayer");
		StopCoroutine ("fClimbPlayer");
		StopCoroutine ("fDownPlayer");
	}
	//^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

}