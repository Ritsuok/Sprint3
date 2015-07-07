using UnityEngine;
using System.Collections;

public class RollArrow : MonoBehaviour {
	float rollSpeed = 5;
	public GameObject turnCanvas;
	GameObject clone;

	//-------------------------------------------------igarashi add
	bool isGround = false;
	public bool IsGround { get { return isGround; } }
	//---------------------------------------------------------------

	// Use this for initialization
	void Start ()
	{
		StartCoroutine ("fBlockIsOnGroundFlag");
		
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
	
	//-------------------------------------------------------------0702 method change by igarashi
	//fBombObject
	
	IEnumerator fBombObject()
	{
		print ("来てる");
		
		Collider collider;
		
		float prePos;
		float nowPos;
		
		while (true)
		{
			prePos = transform.localPosition.y;
			yield return new WaitForSeconds (0.03F);
			nowPos = transform.localPosition.y;
			
			if ((prePos -nowPos) < 0.01F) {
				Collider[] c = Physics.OverlapSphere (new Vector3 (transform.localPosition.x,
				                                                   transform.localPosition.y - 1,
				                                                   transform.localPosition.z), 0.1F);
				collider = c[0];
				break;
			}
		}
		
		//sound
		Sounds.SEbomb ();
		
		if (collider.gameObject.tag == "Block" ||
		    collider.gameObject.tag == "Bomb" ||
		    collider.gameObject.tag == "TurnR" ||
		    collider.gameObject.tag == "TurnL")
		{
			Destroy (collider.gameObject);
			Destroy (gameObject);
		}
	}
	
	//--------------------------------------------------------------------0702 method change by igarashi
	//fFlagAvoid()
	//ブロックがおりきったときの判定
	IEnumerator fBlockIsOnGroundFlag ()
	{
		float prePos;
		float nowPos;
		
		while (true)
		{
			//if (searchForAvoid == true) break;
			
			prePos = transform.localPosition.y;
			yield return new WaitForSeconds (0.01F);
			nowPos = transform.localPosition.y;
			
			if ((prePos -nowPos) < 0.01F)
			{
				bool onGround = Physics.CheckSphere (new Vector3 (transform.localPosition.x,
				                                                  transform.localPosition.y - 1,
				                                                  transform.localPosition.z), 0.1F);
				if (onGround == true)
				{
					print ("設置完了");
					isGround = true;
					break;
				}
			}
		}
		
	} // end fFlagAvoid()
	
	void OnTriggerEnter(Collider c)
	{
		print ("ここにはきてるか");
		if (c.tag == "SensorForAvoidBlock")	
		{
			print ("こらあstep3");
			
			//PlayerControllスクリプトのfAvoid
			c.gameObject.GetComponentInParent<PlayerControll> ().StartCoroutine("fAvoidBlock", gameObject);
			//searchForAvoid = true;
		}
		
		
		if (c.tag == "SensorFrontUP")
		{
			print ("こここここらあstep3");
			
			c.gameObject.GetComponentInParent<PlayerControll> ().StartCoroutine("fWaitPlayer", gameObject);
		}
		
	}
}
