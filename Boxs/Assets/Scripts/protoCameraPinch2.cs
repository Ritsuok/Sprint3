using UnityEngine;
using System.Collections;

public class protoCameraPinch2 : MonoBehaviour {

	//このスクリプトはカメラのPinchOut,PinchZoom実装のためのものです

	//Zoomスピード調整用変数
	public float perspectiveZoomSpeed = 0.1f; 

	//FOV（拡大・縮小値）の最小値と最大値を格納する変数
	public float fovMin = 30f;
	public float fovMax = 120f;
	//cameraが見つめるgameobjectを格納する変数
	private Transform LOOK;

	void Start(){
		LOOK = GameObject.Find("LookAtPoint").transform;
	}

	void Update()
	{
		if (Input.touchCount == 2)
		{
			//2点の座標を格納
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);
			
			//元の座標を取得
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
			
			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;
			
			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			
			if(GetComponent<Camera>().fieldOfView >=fovMin && GetComponent<Camera>().fieldOfView <= fovMax){
				
				//　change the field of view based on the change in distance between the touches.
				GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
				
				// Clamp the field of view to make sure it's between 0 and 180.
				GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);

				//FOVの最大値と最小値を設定
				if(GetComponent<Camera>().fieldOfView <fovMin){
					GetComponent<Camera>().fieldOfView = fovMin;
				}
				if(GetComponent<Camera>().fieldOfView > fovMax){
					GetComponent<Camera>().fieldOfView = fovMax;
				}
			}
		}
		//設定されたgameobjectを見つめる
		transform.LookAt(LOOK);	
	}
}