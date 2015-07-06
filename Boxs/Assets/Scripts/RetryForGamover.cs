using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetryForGamover : MonoBehaviour {
	private GameObject memoryObj;
	private Memorize memorize;


	private string stagename;

	// Use this for initialization
	void Start () {
		
		memoryObj = GameObject.Find ("MemoryObject");
		memorize = memoryObj.GetComponent<Memorize> ();
		stagename = memorize.fGetStageName ();
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void fLetsRetry(){

		Application.LoadLevel (stagename);

	}
}