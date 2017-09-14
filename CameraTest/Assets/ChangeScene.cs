using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void LoadApp()  {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		SceneManager.LoadSceneAsync ("scene1");
	}

	public void LoadMenu()  {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		SceneManager.LoadSceneAsync ("menu");
	}

	public void LoadOptions()  {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		SceneManager.LoadSceneAsync ("options");
	}

	void Update()
	{

	}
}
