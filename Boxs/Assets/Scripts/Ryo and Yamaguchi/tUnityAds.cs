using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;

public class tUnityAds : MonoBehaviour {
	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize ("51199");

			print ("ads initialized");
		} else {
			Debug.Log("Platform not supported");
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(Advertisement.isReady()){ Advertisement.Show(); }
	}

/*
	void OnGUI() {
		if(GUI.Button(new Rect(10, 10, 150, 50), Advertisement.isReady() ? "Show Ad" : "Waiting...")) {
			// Show with default zone, pause engine and print result to debug log
			Advertisement.Show(null, new ShowOptions {
				pause = true,
				resultCallback = result => {
					Debug.Log(result.ToString());
				}
			});

		}
	}
*/
	public void adsTest(){
		print ("adsTest");

		Advertisement.Show (null, new ShowOptions {
			pause = true,
			resultCallback = result => {
				Debug.Log(result.ToString());
			}
		}
		);

	}
}

//Advertisement.Initialize (51199);