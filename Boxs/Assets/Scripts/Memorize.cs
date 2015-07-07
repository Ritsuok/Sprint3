using UnityEngine;
using System.Collections;

public class Memorize : MonoBehaviour {
	private static bool created = false;
	private string stagename;
	
	void Awake(){
		if(!created){
			// シーンを切り替えても指定のオブジェクトを破棄せずに残す
			DontDestroyOnLoad(this.gameObject);
			// 生成した
			created = true;
		} else {
			Destroy(this.gameObject);
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void fMemoryStageName(){
		stagename = Application.loadedLevelName;
		Debug.Log ("the stage name memorized is " + stagename);
	}
	public string fGetStageName(){
		return stagename;
	}
}