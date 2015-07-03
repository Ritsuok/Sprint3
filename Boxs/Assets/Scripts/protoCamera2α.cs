using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class protoCamera2α : MonoBehaviour {
	public float RotateSpeed = 0.3f;

	// Update is called once per frame
	void Update () {
		int touchCount = Input.touches.Count 
			(t => t.phase != TouchPhase.Ended && t.phase != TouchPhase.Canceled);
		if (touchCount == 1) {
			Touch t = Input.touches.First ();
			switch(t.phase)
			{
			case TouchPhase.Moved:
				//移動量
				float xDelta = t.deltaPosition.x * RotateSpeed;
				//左右回転
				this.transform.Rotate (0, -xDelta, 0, Space.World);
				break;
			}
		}
	}
}
