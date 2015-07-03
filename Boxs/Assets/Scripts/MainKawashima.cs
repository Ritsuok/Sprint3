using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class MainKawashima : MonoBehaviour {
	//public int xPos;
	//public int zPos;
	
	public List<GameObject> listSelectableCubes;
	//public GameObject floor;
	//public GameObject goal;
	
	private float dt, t;
	
	private int rndX, rndZ;
	
	Vector3 goalPos;

	//added by Kawashima
	public List<Material> listMaterials; 
	private GameObject parentObject;
	private List<GameObject> listFloors;
	private int fallHeight;
	private int moveWeight = 1;
	private List<GameObject> listCubes;
	//private List<GameObject> listNotRayedFloors;
	private Ray ray;
	public float distance = 100; // Rayの届く距離
	public Camera camera;
	private GameObject touchedBeforeObject;
	private Material touchedBeforeMaterial;
	public bool isSelected = false;
	long timer1 = 0;
	long timer2 = 0;
	long timer3 = 0;
	//private GameObject uiPanel;
	//private float uiPannelHeight;
	public GameObject effectBox;
	public bool isUi;

	// Use this for initialization
	void Start () {
		t = Time.deltaTime;
		dt = Time.deltaTime;
		effectBox.SetActive (false);
		parentObject = GameObject.Find ("parentObject");
		fallHeight = 6;
		listFloors = new List<GameObject>();
		listCubes = new List<GameObject>();
		//uiPanel = GameObject.Find ("UIPanel");
		//uiPannelHeight = uiPanel.GetComponent<RectTransform> ().sizeDelta.y;



		//listSelectableCubes = new List<GameObject> ();
		//listNotRayedFloors = new List<GameObject>();
		ray = new Ray();

		//fCreateField ();


		
		//		var cube1 = Instantiate (cube, new Vector3(0,6,0), transform.rotation);
		//t = dt , this moment
		//if time.deltaTime = 3, t = dt =3
		
	}
/*	
	void fCreateField()
	{
		for( int xCount = 0 ; xCount < xPos ; xCount++)
		{
			for (int zCount = 0 ; zCount < zPos ; zCount++)
			{
				GameObject floorLoop = Instantiate(floor, new Vector3(xCount, 0, zCount), floor.transform.rotation) as GameObject;
				print("floorLoop.name = " + floorLoop.name);

				listFloors.Add(floorLoop);
				//listNotRayedFloors.Add(floorLoop);
				floorLoop.transform.SetParent(parentObject.transform);
			}
		}
		Debug.Log("listFloors.Count = " + listFloors.Count);
		rndX = Random.Range(0,xPos);
		rndZ = Random.Range(0,zPos);
		goalPos = new Vector3 (rndX, 0, rndZ);
		Instantiate (goal, goalPos, goal.transform.rotation);
	}
*/	
	// Update is called once per frame
	void Update () {
		fTouchChangeMaterial ();
//		Debug.Log("isUi = " + isUi);
		//fCreateCube ();

	}
/*
	void fCreateCube(){
		if (t - dt > 1) {
			
			rndX = Random.Range(0,xPos);
			rndZ = Random.Range(0,zPos);
			
			
			if(rndX == goalPos.x && rndZ == goalPos.z)
			{
			}
			else
			{
				
				GameObject cubeInst = Instantiate (listSelectableCubes[0], new Vector3 (rndX, fallHeight, rndZ), transform.rotation) as GameObject;
				listCubes.Add(cubeInst);

				//Raycast from the cubeInst to the floor
				Ray ray = new Ray(cubeInst.transform.position,Vector3.down);
				RaycastHit hitInfo;
				GameObject hitGameObject;
				MeshRenderer hitMeshRenderer;

				//Find the hit floor and change Material
				if(Physics.Raycast(ray, out hitInfo, (float)fallHeight)){
					print ("hitInfo = " + hitInfo.transform.position);
					hitGameObject = hitInfo.transform.gameObject;
					print("hitGameObject = " + hitGameObject.name);
					hitMeshRenderer = hitGameObject.GetComponent<MeshRenderer>();
					hitMeshRenderer.material = listMaterials[1];
					print("hitMeshRenderer.material.name = " + hitMeshRenderer.material.name);
				}else{
					Debug.Log("else");
				}

				//			dt = Time.deltaTime;
				//t = 2, dt = 3
				t -= 1;
			}
			//fRayFloor();
			//fMoveFloor();
		}
		// t = 5
		t += dt;
		//Debug.Log (t + " , " + dt);
	}
*/
	public void fRayFloor(){
		for (int i = 0; i < listFloors.Count; i++) {
			MeshRenderer notHitMeshRenderer;
			notHitMeshRenderer = listFloors[i].GetComponent<MeshRenderer>();
			//Debug.Log("Before Change NotRayedFloor.material.name = " + notHitMeshRenderer.material.name);
			if (notHitMeshRenderer.material == listMaterials[0]) {
				return;
			}
			notHitMeshRenderer.material = listMaterials[0];
			
			//Debug.Log("NotRayedFloor.material.name = " + notHitMeshRenderer.material.name);
		}

		//listNotRayedFloors = listFloors;
		for (int i = 0; i < listCubes.Count; i++) {
			//Ray ray = new Ray(listCubes[i].transform.position,Vector3.down);

			ray = new Ray(listCubes[i].transform.position,Vector3.down);

			RaycastHit hitInfo;
			GameObject hitGameObject;
			MeshRenderer hitMeshRenderer;
			if(Physics.Raycast(ray, out hitInfo, (float)fallHeight)){
				//print ("hitInfo = " + hitInfo.transform.position);
				hitGameObject = hitInfo.transform.gameObject;
				//print("hitGameObject = " + hitGameObject.name);
				hitMeshRenderer = hitGameObject.GetComponent<MeshRenderer>();
				if (hitMeshRenderer.material == listMaterials[1]) {
					return;
				}
				hitMeshRenderer.material = listMaterials[1];
				//print("hitMeshRenderer.material.name = " + hitMeshRenderer.material.name);
				//listNotRayedFloors.Remove(hitGameObject);
				//print("listFloors.Count = " + listFloors.Count);
				//print("listNotRayedFloors.Count = " + listNotRayedFloors.Count);
			}

		}

		//print ("finish Ray forloop");


	}
	void fMoveFloor(){
		int floorX = (int)parentObject.transform.position.x;
		int floorY = (int)parentObject.transform.position.y;
		int floorZ = (int)parentObject.transform.position.z;
		floorX++;
		parentObject.transform.position = new Vector3 (floorX, floorY, floorZ);
	}

	//Vector3 secondOrigin;
	void fTouchChangeMaterial(){

		if (Input.GetMouseButtonDown(0)) {
			//timer1 = Time.deltaTime;
			timer1 = System.DateTime.Now.Ticks;
			Debug.Log ("GetMouseButtonDown(0)" + timer1);
			Debug.Log("Input.mousePosition.y = " + Input.mousePosition.y);
			//Debug.Log("UIPanel transform.position = " + uiPanel.transform.position);
			//Debug.Log("UIPanel localScaleY = " + uiPanel.transform.localScale.y);
			//Debug.Log("UIPanel sizeDelta.y = " + uiPannelHeight);
		}
		if (Input.GetMouseButtonUp(0)) {
			//timer2 = Time.deltaTime;
			timer2 = System.DateTime.Now.Ticks;
			Debug.Log ("GetMouseButtonUp(0)" + timer2);
			timer3 = timer2 - timer1;
			Debug.Log("timer1 = " + timer1);
			Debug.Log("(timer2-timer1) = " + timer3);
/*
			Vector2 worldPoint2d = camera.ScreenToWorldPoint(Input.mousePosition);
			Collider2D collider2D = Physics2D.OverlapPoint(worldPoint2d);
			if(collider2D) {
				Debug.Log("Hit!");
				Debug.Log(collider2D);
			}


*/



//			if( timer3 < 3500000 && Input.mousePosition.y >= uiPannelHeight){
			if( timer3 < 3500000 && !isUi){
				print ("RaycastStarted");
				// メインカメラからクリックしたポジションに向かってRayを撃つ。
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				//Vector3 firstOrigin = ray.origin;
				//Debug.Log("start");
				//(メソッド,秒数)を指定
				//Invoke("hogehoge",0.01f);
				//StartCoroutine("hogehoge");

				// Bit shift the index of the layer (6) to get a bit mask
				int layerMask = 1 << 7;
				
				// This would cast rays only against colliders in layer 6.
				// But instead we want to collide against everything except layer 6. The ~ operator does this, it inverts a bitmask.

				layerMask = ~layerMask;

				
				RaycastHit hit = new RaycastHit();
				MeshRenderer hitMeshRenderer;

				if (Physics.Raycast(ray, out hit, distance, layerMask)) {

					//sound
					Sounds.SEcursor(); // < 0629 igarashi add

					if(touchedBeforeObject != null){
						hitMeshRenderer = touchedBeforeObject.GetComponent<MeshRenderer>();
						hitMeshRenderer.material = touchedBeforeMaterial;

					}
					GameObject selectedGameObject = hit.collider.gameObject;
					hitMeshRenderer = selectedGameObject.GetComponent<MeshRenderer>();
					if (hitMeshRenderer.material == listMaterials[1]) {
						return;
					}
					touchedBeforeMaterial = hitMeshRenderer.material;
					hitMeshRenderer.material = listMaterials[1];
					touchedBeforeObject = selectedGameObject;
					Vector3 effectBoxPosition = selectedGameObject.transform.position;
					effectBoxPosition.y = effectBoxPosition.y + 1;
					effectBox.transform.position = effectBoxPosition;
					effectBox.SetActive(true);

					isSelected = true;
				}
				
				
				
				
			}
		}

	}
	public void fDrop(int index){
		print ("start fDrop index = " + index);
		print ("listSelectableCubes[index] = " + listSelectableCubes[index].name);
		if (isSelected == false) {
			return;
		}

		//sound
		Sounds.SEdecision (); // < 0629 igarashi add

		float dropPositionX = touchedBeforeObject.transform.position.x;
		float dropPositionZ = touchedBeforeObject.transform.position.z;
		GameObject cubeInst = Instantiate (listSelectableCubes[index], new Vector3 (dropPositionX, fallHeight, dropPositionZ), transform.rotation) as GameObject;
		//cubeInst.transform.localrotation = 
		switch (index) {
		case 0:
			cubeInst.transform.localRotation = Quaternion.Euler (0, 270, 90);
			break;
		case 4:
			cubeInst.transform.localRotation = Quaternion.Euler (270, 0, 0);
			break;
			
		default:
			break;
		}
		//cubeInst.transform.localRotation = Quaternion.Euler (270, 0, 0);
		listCubes.Add(cubeInst);

	}


/*
	private float timer;
	private float waitingTime = 0.5f;
	private bool wait = false;
	
	private bool waitSec(){
		timer += Time.deltaTime;
		Debug.Log ("timer = " + timer);
		if (timer > waitingTime) {
			timer = 0;
			return true;
		} else {
			return false;
		}
		
	}
*/	
/*IEnumerator hogehoge(){
		
		Debug.Log("start");
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		yield return new WaitForSeconds(0.5);
		Ray rayDelay = camera.ScreenPointToRay(Input.mousePosition);
		Debug.Log("0.5秒経ちました");
		
	}
*/
/*
	void hogehoge(){
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		secondOrigin = ray.origin;
		Debug.Log("0.01秒後");

		
	}
*/	

}
