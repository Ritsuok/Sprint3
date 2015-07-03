using UnityEngine;
using System.Collections;

public class GotoTutorial : MonoBehaviour {
	public string changeSceneName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void fGotoTutorial()
	{
		Application.LoadLevel(changeSceneName);
	}
}
