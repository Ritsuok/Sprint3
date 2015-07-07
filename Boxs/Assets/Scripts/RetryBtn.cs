using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RetryBtn : MonoBehaviour {
	public GameObject startBtn;
	public GameObject confirmPanel;
	public GameObject retryConfirmBtn;
	public GameObject cancelBtn;
	private StartBtn startbtnScript;
	private string stagename;
	private Button button;
	private Button buttonconfirm;
	private Button buttonCancel;
	// Use this for initialization
	void Start () {

		
		//startBtn = GameObject.Find ("ButtonStart");
		print (startBtn);

		stagename = Application.loadedLevelName;
		startbtnScript = startBtn.GetComponent<StartBtn> ();
		button = gameObject.GetComponent<Button>();
		buttonconfirm = retryConfirmBtn.GetComponent<Button>();
		buttonCancel = cancelBtn.GetComponent<Button>();
		confirmPanel.SetActive (false);

		
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void fLetsRetry(){
		confirmPanel.SetActive (true);
		button.enabled = false;
		buttonconfirm.onClick.AddListener (() => {
			Debug.Log ("Clicked.");
			Application.LoadLevel (stagename);
			startbtnScript.fStartBtnOn ();
			confirmPanel.SetActive (false);
			button.enabled = true;
		});
			

		buttonCancel.onClick.AddListener (() => {
			confirmPanel.SetActive (false);
			button.enabled = true;
		});

		Application.LoadLevel ("stage01");

	}
}