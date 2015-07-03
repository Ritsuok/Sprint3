using UnityEngine;
using System.Collections;

public class RollArrow : MonoBehaviour {
	float rollSpeed = 5;
	public GameObject turnCanvas;
	GameObject clone;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("fFlagAvoid");
		
		if (gameObject.tag == "TurnL")
		{
			clone = Instantiate (turnCanvas, transform.position, Quaternion.Euler (90, 0, 0)) as GameObject;
			
			clone.transform.SetParent(transform);
			
			StartCoroutine ("fRollLObject");
		}
		
		if (gameObject.tag == "TurnR")
		{
			
			clone = Instantiate (turnCanvas, transform.position, Quaternion.Euler (90, 0, 0)) as GameObject;
			clone.transform.localScale = new Vector3(-1 * clone.transform.localScale.x,
			                                         clone.transform.localScale.y,
			                                         clone.transform.localScale.z);
			
			clone.transform.SetParent(transform);
			
			StartCoroutine ("fRollRObject");
		}
		
		//********************************************* 0629 igarashi start
		if (gameObject.tag == "Bomb") 
		{
			StartCoroutine ("fBombObject");
		}
		//********************************************* 0629 igarashi end
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	IEnumerator fRollLObject()
	{
		while (true)
		{
			clone.transform.Rotate (0, 0, rollSpeed);
			yield return new WaitForSeconds (0.01F);
		}
	}
	
	IEnumerator fRollRObject()
	{
		while (true)
		{
			//			//print ("aaaa");
			//			//print (rollSpeed);
			clone.transform.Rotate (0, 0, -rollSpeed);
			yield return new WaitForSeconds (0.01F);
		}
	}
	
	//********************************************* 0629 igarashi start
	IEnumerator fBombObject()
	{
		Collider collider;
		
		while (true)
		{
			
			if ((transform.localPosition.y % 1) >= 0.999F)
			{
				
				Collider[] c = Physics.OverlapSphere (new Vector3 (transform.localPosition.x,
				                                                   transform.localPosition.y - 1,
				                                                   transform.localPosition.z), 0.1F);
				if (c [0] != null)
				{
					collider = c [0];
					break;
				}
			}
			yield return new WaitForSeconds (0.01F);
		}
		
		//sound
		Sounds.SEbomb ();
		
		Destroy (collider.gameObject);
		Destroy (gameObject);
	}
	//********************************************* 0629 igarashi end
	
	//--------------------------------------------------------------------0630 method add by igarashi
	//fFlagAvoid()
	//ブロックが上から降ってきた時のフラグ判定、とりあえず、その場で立ち止まるか
	//もしくは通常のmoveを実行し続けるだけの判定
	//ダッシュやスライディング、ブロック破壊は後回し予定
	IEnumerator fFlagAvoid()
	{
		
		//print ("はははははは");
		
		bool searchFine = false;
		
		while (true)
		{
			
			//落下ブロックの座標を中心に半径１の球体センサーを作る
			Collider[] c = Physics.OverlapSphere (new Vector3 (transform.localPosition.x,
			                                                   transform.localPosition.y,
			                                                   transform.localPosition.z), 1F);
			foreach (var item in c)
			{
				//print ("くそおおお" + item.tag);
				if (item.tag == "SensorForAvoidBlock")
				{	
					//print ("どうどう");
					
					//PlayerControllスクリプトのfAvoid
					item.gameObject.GetComponentInParent<PlayerControll> ().StartCoroutine("fAvoidBlock", gameObject);
					searchFine = true;
					break;
				}
			}
			
			
			
			if (searchFine == true) break;
			
			if ((transform.localPosition.y % 1) >= 0.999F)
			{
				//落下ブロック座標よりｙ軸に-1に極小のセンサーを作る
				//ちな、この c2 は範囲と発生場所的に上の c より先に他コライダーと接触することはない（はず）
				bool endFlag = Physics.CheckSphere (new Vector3 (transform.localPosition.x,
				                                                 transform.localPosition.y - 1,
				                                                 transform.localPosition.z), 0.1F);
				//上記コライダーが何かブロック接触していない場合、このwhile文を抜け出し
				//fFlagAvoid()を終了する
				if (endFlag == true)
				{
					//print ("終わりだぜ");
					break;
				}
			}
			
			yield return new WaitForSeconds (0.01F);
			
		}
		
	} // end fFlagAvoid()
}
