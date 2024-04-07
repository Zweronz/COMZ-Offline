using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockUnlock : MonoBehaviour {
	void Start(){
		Application.targetFrameRate = 240;
		DontDestroyOnLoad(base.gameObject);
	}
	void Update () {
					if (Input.GetKeyDown(KeyCode.F1))
			{
				Cursor.visible = !Cursor.visible;
				Screen.lockCursor = !Screen.lockCursor;
			}
	}
}
