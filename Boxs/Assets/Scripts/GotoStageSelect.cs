using UnityEngine;
using System.Collections;

public class GotoStageSelect : MonoBehaviour {
	public string changeSceneName;


	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void fGotoStageSelect()
	{
		Application.LoadLevel(changeSceneName);
	}

}
