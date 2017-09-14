using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLayout : MonoBehaviour {

	InputField prefixInput;
	InputField userIDInput;
	InputField serviceTypeInput;
	InputField serviceInstanceInput;
	float offset = 30f;
	float height = 150f;
	float top = Screen.height - Screen.height / 3;

	// Use this for initialization
	void Start () {
		prefixInput = GameObject.Find ("InputFieldRootPrefix").GetComponent<InputField>();
		userIDInput = GameObject.Find ("InputFieldUserID").GetComponent<InputField>();
		serviceTypeInput = GameObject.Find ("InputFieldServiceType").GetComponent<InputField>();
		serviceInstanceInput = GameObject.Find ("InputFieldServiceInstance").GetComponent<InputField>();

		prefixInput.transform.position = new Vector3(prefixInput.transform.position.x, top, prefixInput.transform.position.z);
		userIDInput.transform.position = new Vector3(userIDInput.transform.position.x, top - (offset + height), userIDInput.transform.position.z);
		serviceTypeInput.transform.position = new Vector3(serviceTypeInput.transform.position.x, top - ((offset * 2) + (height * 2)), serviceTypeInput.transform.position.z);
		serviceInstanceInput.transform.position = new Vector3(serviceInstanceInput.transform.position.x, top - ((offset * 3) + (height * 3)), serviceInstanceInput.transform.position.z);

	}

	public void SaveRootPrefixString()
	{
		PlayerPrefs.SetString ("RootPrefix", prefixInput.text);
	}

	public void SaveUserIDString()
	{
		PlayerPrefs.SetString ("UserID", userIDInput.text);
	}

	public void SaveServiceTypeString()
	{
		PlayerPrefs.SetString ("ServiceType", serviceTypeInput.text);
	}

	public void SaveSerivceInstanceString()
	{
		PlayerPrefs.SetString ("ServiceInstance", serviceInstanceInput.text);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadSceneAsync ("menu");
		}
	}
}
