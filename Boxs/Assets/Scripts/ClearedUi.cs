using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClearedUi : MonoBehaviour {
	private Text text;

	// Use this for initialization
	void Start () {
		text = gameObject.GetComponent<Text>();
		text.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void fClearedOn(){
		text.enabled = true;

		Debug.Log ("Cleared Text setActive(true)");
	}
}
