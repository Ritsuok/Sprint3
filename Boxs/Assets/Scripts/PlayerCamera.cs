using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
	private Camera mainCamera;
	private Camera subCamera;
	private GameObject clearedObj;
	private ClearedUi clearedUi;

	//cameraが見つめるgameobjectを格納する変数
	private Transform lookTransform;
	//goalのTransform
	private Transform goalTransform;
	//カメラの移動すべき場所
	private Vector3 subCameraVect3;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find ("Camera").GetComponent<Camera>();
		mainCamera.enabled = true;
		subCamera = gameObject.GetComponent<Camera>();
		subCamera.enabled = false;
		lookTransform = GameObject.Find ("LookAtPlayer").transform;
		goalTransform = GameObject.FindGameObjectWithTag ("Goal").transform;
		//cameraの最初の座標を取得
		subCameraVect3 = transform.localPosition;

		transform.LookAt (lookTransform);

		clearedObj = GameObject.Find ("TextCleared");
		clearedUi = clearedObj.GetComponent<ClearedUi> ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void fChangeToSubCamera(Transform playerTrans){
		mainCamera.enabled = false;
		subCamera.enabled = true;
		subCameraVect3 = playerTrans.TransformPoint (Vector3.forward * 3);
		gameObject.transform.position = subCameraVect3;
		transform.LookAt (lookTransform);
		clearedUi.fClearedOn ();
	}
}