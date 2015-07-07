using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class TimeScripts : MonoBehaviour {
	private float time;

	private GameObject circleObj;
	private RadialTimerScript radicaltimer;

	public bool isStart = false;

	//public GameObject UIPanelButton;
	//public GameObject gameOverText;

	//public GameObject gameOverobj;

	
	void Start () {	
		//gameOverobj.SetActive (false);
		//初期値60を表示
		circleObj = GameObject.Find ("CircleGageDummy");
		radicaltimer = circleObj.GetComponent<RadialTimerScript> ();
		time = radicaltimer.stageTimeLimit;
		//float型からint型へCastし、String型に変換して表示
		//GetComponent<Text>().text = ((int)time).ToString();
		GetComponent<Text> ().text = 
			((int)time / 60).ToString ("00") + ":" + 
				((int)time % 60).ToString ("00") + ":" + 
				(Mathf.Floor (time % 1 * 100)).ToString ("00") + " SEC";
		//time = RadialTimerScript.stageTimeLimit;

	}
	

	
	void Update (){
		if (isStart == true) {
			//1秒に1ずつ減らしていく
			time -= Time.deltaTime;
			
			/*		if (time < 0) {
			StartCoroutine ("GameOver");
		}
*/
			//マイナスは表示しない	
			if (time < 0) {
				time = 0;
			}
			
			GetComponent<Text> ().text = 
				((int)time / 60).ToString ("00") + ":" + 
					((int)time % 60).ToString ("00") + ":" + 
					(Mathf.Floor (time % 1 * 100)).ToString ("00") + " SEC";
		}

	}
	public void fStart(){
		isStart = true;
	}
	public void fStop(){
		isStart = false;
	}


/*	IEnumerator GameOver() {
		gameOverobj.SetActive (true);
		UIPanelButton.GetComponent<Button> ().interactable = false;
		//     .    =fales; //ここでGamaOver中のゲームの進行を停止させる
		//http://hiyotama.hatenablog.com/entry/2015/05/24/090000
		yield return new WaitForSeconds(2.0f);
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel("testAkamatsu1");
		}
	}
*/
}

