using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class protoCamera3 : MonoBehaviour {

	//cameraのy軸上の移動スピード調整用変数
	private float UpDownSpeed = 0.01f;
	//cameraが見つめるgameobjectを格納する変数
	private Transform LOOK;
	//cameraの座標
	private Vector3 y_pos;
	//cameraのy座標の許容範囲.
	public float yMin = 0.0f;
	public float yMax = 10.0f;

	// Use this for initialization
	void Start () {
		//cameraが見つめるgameobjectを取得
		LOOK = GameObject.Find("LookAtPoint").transform;
		//cameraの最初の座標を取得
		y_pos = transform.localPosition;
		transform.LookAt(LOOK);
	}

	//フリックで行き過ぎたy座標を適正値に戻す際に使用する値
	private float yMinMinor;
	private float yMaxMinor;

	float minFov = 15f;
	float maxFov  = 90f;
	float  sensitivity = 10f;

	// Update is called once per frame
	void Update () {

		//cameraの現在の座標を取得
		y_pos = transform.localPosition;

		//ここからフリックによる操作スクリプト
		int touchCount = Input.touches.Count 
			(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
		if (touchCount == 1) {
			Touch t = Input.touches.First ();
			switch(t.phase)
			{
			case TouchPhase.Moved:
				//移動量
				float yDelta = t.deltaPosition.y * UpDownSpeed;
				//上下移動（設定された最小値と最大値を超えないようにカメラが移動）
				if(yMin <= y_pos.y && y_pos.y <= yMax){
					Debug.Log ("上下移動なう");
					if(t.deltaPosition.y > 0) {
						this.transform.position += new Vector3(0, yDelta, 0);
					}
					if(t.deltaPosition.y < 0 ) {
						this.transform.position -= new Vector3(0, -yDelta,0);
					}	 
				}

				//カメラの現在地がフリック操作によって最小値・最大値を超えている場合は修正
				if(y_pos.y < yMin) {
					yMinMinor = y_pos.y;
					this.transform.position -= new Vector3(0, yMinMinor, 0);
				}
				if(y_pos.y > yMax) {
					yMaxMinor = y_pos.y - yMax;
					this.transform.position -= new Vector3(0, yMaxMinor,0);
				}
				//設定されたgameobjectを見つめる
				transform.LookAt(LOOK);				
				break;
			}
		}
	}
}
