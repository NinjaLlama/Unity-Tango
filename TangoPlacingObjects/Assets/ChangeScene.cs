using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour {

	public void LoadScene()  {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		Application.LoadLevel ("scene1");
	}
}
