using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tango;

public class OnCameraFrame : MonoBehaviour, ITangoVideoOverlay {

	TangoApplication tango;
	public UnityEngine.UI.Text textbox;
	public FrameObjectData prefab;
	public long frameNumber;
	public FrameObjectData spawn;
	public LastFrameData lastFrame;
	public Dictionary<long, FrameObjectData> frameObjects;
	public BoundingBoxPoolManager boxMgr;
	public bool createBox;
	public int boxCount;


	/*public BoundingBoxObjectData boxPrefab;
	public BoundingBoxObjectData boxSpawn;
	public Dictionary<long, BoundingBoxObjectData> boundingBoxObjects;*/

	void Awake () {
		QualitySettings.vSyncCount = 0;  // VSync must be disabled
		Application.targetFrameRate = 30;
	}

	void Start()
	{
		tango = FindObjectOfType <TangoApplication> ();
		tango.Register (this);
		frameNumber = 0;
		lastFrame = gameObject.GetComponent<LastFrameData>();
		frameObjects = new Dictionary<long, FrameObjectData> ();
		//boundingBoxObjects = new Dictionary<long, BoundingBoxObjectData> ();
		boxMgr = GameObject.FindObjectOfType<BoundingBoxPoolManager>();
		createBox = true;
		boxCount = 5;
	}

	public void OnDestroy()
	{
		tango.Unregister(this);
	}

	public void OnTangoImageAvailableEventHandler(Tango.TangoEnums.TangoCameraId cameraId, Tango.TangoUnityImageData imageBuffer)
	{

		//holds latest frame data to access outside of this script
		lastFrame.setLastFrameData(gameObject.GetComponent<TangoARScreen> ().m_screenUpdateTime,
			imageBuffer,
			gameObject.GetComponent<TangoPoseController> ().finalPosition,
			gameObject.GetComponent<TangoPoseController> ().finalRotation,
			gameObject.GetComponent<TangoARScreen> ().m_uOffset,
			gameObject.GetComponent<TangoARScreen> ().m_vOffset);
		
		//for testing
		//frameNumber++;

		//add FrameObjects to dictionary
		//frameObjects.Add (frameNumber, spawn);

		//access and remove FrameObject
		/*FrameObjectData value = null;

		if (frameObjects.TryGetValue(frameNumber-1, out value))
		{
			textbox.text = "" + frameObjects.Count;
			frameObjects.Remove (frameNumber - 1);
			value.Release();
		}*/

		//create a FrameObject and save associated data
		//saveData (imageBuffer, frameNumber);

		//for testing
		frameNumber++;

		//add FrameObjects to dictionary
		//boundingBoxObjects.Add (frameNumber, boxSpawn);

		//access and remove FrameObject
		/*FrameObjectData value = null;

		if (frameObjects.TryGetValue(frameNumber-1, out value))
		{
			textbox.text = "" + frameObjects.Count;
			frameObjects.Remove (frameNumber - 1);
			value.Release();
		}*/

		//create a FrameObject and save associated data
		//makeBox (frameNumber);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Random.value, "Hello World");
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var stringChars = new char[Random.Range(1, 10)];
		var random = new Random();

		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		string finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, 0, finalString, Color.green);
	
	}

	public void spawnBoxes()
	{
		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		var stringChars = new char[Random.Range(1, 10)];
		var random = new Random();

		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		string finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, 0, finalString, Color.green);
		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
		for (int i = 0; i < stringChars.Length; i++)
		{
			stringChars[i] = chars[Random.Range(0,chars.Length)];
		}

		finalString = new string(stringChars);
		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);

	}

	void Update()
	{
		if(Input.GetKeyDown ("space"))
		{
			spawnBoxes();
		}
//		if (Input.GetKeyDown ("space")) {
//			//if (createBox) {
//				boxCount--;
//				if (boxCount == 0)
//					createBox = false;
//				Debug.Log ("here");
//				var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//				var stringChars = new char[Random.Range (1, 10)];
//				var random = new Random ();
//
//				for (int i = 0; i < stringChars.Length; i++) {
//					stringChars [i] = chars [Random.Range (0, chars.Length)];
//				}
//
//				string finalString = new string (stringChars);
//				//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//				boxMgr.CreateBoundingBoxObject (new Vector3 (Random.Range (-2f, 2f), 0, Random.Range (0f, 2f)), Random.value, Random.value, Random.value, finalString, Color.green);
//		//	}
//		}
	}

	void saveData(Tango.TangoUnityImageData imageBuffer, long frameNumber)
	{
		spawn = prefab.GetPooledInstance<FrameObjectData>();
		//this doesn't work for some reason
		/*spawn.setFrameObjectData(gameObject.GetComponent<TangoARScreen> ().m_screenUpdateTime,
			frameNumber, imageBuffer, 
			gameObject.GetComponent<TangoPointCloud> ().m_points,
			gameObject.GetComponent<TangoPointCloud> ().m_pointsCount,
			gameObject.GetComponent<TangoPoseController> ().finalPosition,
			gameObject.GetComponent<TangoPoseController> ().finalRotation,
			gameObject.GetComponent<TangoARScreen> ().m_uOffset,
			gameObject.GetComponent<TangoARScreen> ().m_vOffset);*/
		spawn.timestamp = gameObject.GetComponent<TangoARScreen> ().m_screenUpdateTime;
		spawn.frameNumber = frameNumber;
		spawn.imageBuffer = imageBuffer;
		spawn.points = gameObject.GetComponent<TangoPointCloud> ().m_points;
		spawn.numPoints = gameObject.GetComponent<TangoPointCloud> ().m_pointsCount;
		spawn.camPos = gameObject.GetComponent<TangoPoseController> ().finalPosition;
		spawn.camRot = gameObject.GetComponent<TangoPoseController> ().finalRotation;
		spawn.uOffset = gameObject.GetComponent<TangoARScreen> ().m_uOffset;
		spawn.vOffset = gameObject.GetComponent<TangoARScreen> ().m_vOffset;

	}

	/*void makeBox(long frameNumber)
	{
		boxSpawn = boxPrefab.GetPooledInstance<BoundingBoxObjectData>();
		boxSpawn.position = new Vector3 (Random.value, Random.value, Random.value);
		boxSpawn.x = Random.value;
		boxSpawn.y = Random.value;
		boxSpawn.z = Random.value;
		boxSpawn.box.transform.position = new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f));
		boxSpawn.line.MakeCube (boxSpawn.box.transform.position, Random.value, Random.value, Random.value);
		boxSpawn.line.active = true;
		boxSpawn.line.Draw3DAuto ();


	}*/
		
}
