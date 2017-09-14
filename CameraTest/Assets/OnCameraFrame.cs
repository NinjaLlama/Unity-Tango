using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tango;
using Vectrosity;
using UnityEngine.SceneManagement;

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
	//private TangoApplication m_tangoApplication;
	private TangoPointCloud m_pointcloud;
	VectorLine line;
	public List<Color> colors;

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
		//m_tangoApplication = FindObjectOfType<TangoApplication>();
		m_pointcloud = PointCloudGUI.FindObjectOfType<TangoPointCloud>();
		createBox = true;
		boxCount = 5;
//		float xleft = .55f;
//		float xright = .78f;
//		float ytop = .29f;
//		float ybottom = .82f;
//
//
//		//calucate 4 viewport corners
//		Vector2 viewportTopLeft = new Vector2(xleft, ytop);
//		Vector2 viewportTopRight = new Vector2(xright, ytop);
//		Vector2 viewportBottomLeft = new Vector2(xleft, ybottom);
//		Vector2 viewportBottomRight = new Vector2(xright, ybottom);
//
//		List<Vector2> linepoints = new List<Vector2> {
//			viewportTopLeft,
//			viewportTopRight,
//			viewportBottomRight,
//			viewportBottomLeft,
//			viewportTopLeft
//		};
//
//		line = new VectorLine ("viewport box", linepoints, 5.0f);
//		line.color = Color.red;
//		line.useViewportCoords = true;
//		line.Draw ();

		string rootPrefix = PlayerPrefs.GetString ("RootPrefix");
		string userId = PlayerPrefs.GetString ("UserID"); // "mobile-terminal0";
		string serviceType = PlayerPrefs.GetString ("ServiceType");
		string serviceInstance = PlayerPrefs.GetString ("ServiceInstance"); // "yolo";

		Debug.Log ("Player prefs test: " + rootPrefix);
		Debug.Log ("Player prefs test: " + userId);
		Debug.Log ("Player prefs test: " + serviceType);
		Debug.Log ("Player prefs test: " + serviceInstance);

		colors = new List<Color> {
			new Color (255f/255, 109f/255, 124f/255),
			new Color (119f/255, 231f/255, 255f/255),
			new Color (82f/255, 255f/255, 127f/255),
			new Color (252f/255, 187f/255, 255f/255),
			new Color (255f/255, 193f/255, 130f/255)
		};
	}

	public void OnDestroy()
	{
		VectorLine.Destroy (ref line);
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
//		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//		var stringChars = new char[Random.Range(1, 10)];
//		var random = new Random();
//
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		string finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, 0, finalString, Color.green);
//	
	}

	public void spawnBoxes()
	{
//		var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
//		var stringChars = new char[Random.Range(1, 10)];
//		var random = new Random();
//
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		string finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, 0, finalString, Color.green);
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
//		for (int i = 0; i < stringChars.Length; i++)
//		{
//			stringChars[i] = chars[Random.Range(0,chars.Length)];
//		}
//
//		finalString = new string(stringChars);
//		//boxMgr.CreateBoundingBoxObject(new Vector3 (0, 0, .5f), Random.value, Random.value, Random.value);
//		boxMgr.CreateBoundingBoxObject(new Vector3 (Random.Range(-2f, 2f), 0, Random.Range(0f, 2f)), Random.value, Random.value, Mathf.Abs(Random.value - .5f), finalString, Color.green);
//

//		label: bottle
//		xleft: 0.55
//		xright: 0.78
//		ytop: 0.29
//		ybottom: 0.82
		Debug.Log ("box: entered spawn boxes");
		List <string> items = new List<string> {
			"bottle", "chair", "car", "tv", "sofa", "table", "mug", "dog", "backpack"
		};
		string label = items[Random.Range(0, items.Count)];
		if (boxCount > 3)
			label = "chair";
		else
			label = "bottle";
		float xleft = .55f;
		float xright = .78f;
		float ytop = .82f;
		float ybottom = .29f;
		float averageZ = 0;
		int numWithinBox = 0;

		//calucate 4 viewport corners
		Vector2 viewportTopLeft = new Vector2(xleft, ytop);
		Vector2 viewportTopRight = new Vector2(xright, ytop);
		Vector2 viewportBottomLeft = new Vector2(xleft, ybottom);
		Vector2 viewportBottomRight = new Vector2(xright, ybottom);


		//calulate 4 world corners
//		Vector3 worldTopLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopLeft.x, viewportTopLeft.y, 1f));
//		Vector3 worldTopRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopRight.x, viewportTopRight.y, 1f));
//		Vector3 worldBottomLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomLeft.x, viewportBottomLeft.y, 1f));
//		Vector3 worldBottomRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomRight.x, viewportBottomRight.y, 1f));

		//calculate center of box in viewport coords
		Vector2 centerPosXY = new Vector2( xleft + Mathf.Abs(viewportTopLeft.x - viewportTopRight.x)/2, ybottom + Mathf.Abs(viewportTopLeft.y - viewportBottomLeft.y)/2);


		Debug.Log ("box: before points[]");
		Vector3 min = new Vector3(100, 100, 100);

		Vector3[] points = m_pointcloud.m_points;
		int count = m_pointcloud.m_pointsCount;

		Debug.Log ("box: after points[]");
		//search points[]
		//todo: search with 2d coords and take the average z
		for (int i = 0; i < count; i++)
		{
			//calculate center of box in world coords
			Vector2 worldCenter = Camera.main.ViewportToWorldPoint(new Vector2(centerPosXY.x, centerPosXY.y));
			//find point in points[] that most nearly matches center position
			if (Vector2.Distance(Camera.main.WorldToViewportPoint(new Vector2(points[i].x, points[i].y)), worldCenter) < Vector2.Distance(Camera.main.WorldToViewportPoint(new Vector2(min.x, min.y)), worldCenter)) {
				min = points [i];
			}
			//find if points[i] is outside of the bounding box
			Vector3 viewportPoint = Camera.main.WorldToViewportPoint(points[i]);
			if (viewportPoint.x < xleft || viewportPoint.x > xright || viewportPoint.y < ybottom || viewportPoint.y > ytop) {
				//points[i] is out of the limits of the bounding box
			} else {
				//points[i] is in the bounding box
				averageZ += points[i].z;
				numWithinBox++;
			}
		}
		averageZ /= numWithinBox;
			
		if (Mathf.Abs (averageZ) < 0.5f)
			averageZ = 0.5f;
		if (Mathf.Abs (min.z) < 0.5f)
			min.z = 0.5f;
		//calculate center of box in world coords w/ z = min.z
		Vector3 position = Camera.main.ViewportToWorldPoint( new Vector3(centerPosXY.x, centerPosXY.y, Mathf.Abs(averageZ)));
		//position.z = min.z;
		Debug.Log ("box position: " + position.ToString());
		textbox.text = "" + min.ToString();
		Debug.Log ("box position found min " + min.ToString());

//		GameObject ob = GameObject.Find ("Sphere");
//		ob.transform.position = position;

		//calculate Z value for world corners
		Vector3 worldTopLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopLeft.x, viewportTopLeft.y, Mathf.Abs(averageZ)));
		Vector3 worldTopRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopRight.x, viewportTopRight.y, Mathf.Abs(averageZ)));
		Vector3 worldBottomLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomLeft.x, viewportBottomLeft.y, Mathf.Abs(averageZ)));
		Vector3 worldBottomRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomRight.x, viewportBottomRight.y, Mathf.Abs(averageZ)));



//		Vector3 position = Camera.main.ViewportToWorldPoint( new Vector3(centerPosXY.x, centerPosXY.y, 1f));
//
//
//		Vector3 worldTopLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopLeft.x, viewportTopLeft.y, 1));
//		Vector3 worldTopRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportTopRight.x, viewportTopRight.y, 1));
//		Vector3 worldBottomLeft = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomLeft.x, viewportBottomLeft.y, 1));
//		Vector3 worldBottomRight = Camera.main.ViewportToWorldPoint( new Vector3(viewportBottomRight.x, viewportBottomRight.y, 1));
//


		//calculate x, y, z size values
		float x = Mathf.Abs(Vector3.Distance(worldTopLeft, worldTopRight));
		float y = Mathf.Abs(Vector3.Distance(worldTopLeft, worldBottomLeft));
		float z = 0;
		Debug.Log ("box: before create box");
		//create bounding box
		boxMgr.CreateBoundingBoxObject(position, x, y, z, label, colors[Random.Range(0, colors.Count)]);
		//boxMgr.CreateBoundingBoxObject(new Vector3(-1.8f, -0.9f, 1.0f), .5f, .5f, 0, label, Color.green);
		Debug.Log ("box: after create box");
		boxCount--;

	}

//	public Vector3 ViewportToWorldPoint(Vector3 v)
//	{
//		Matrix4x4 projectionMatrix = GL.GetGPUProjectionMatrix(Camera.main.projectionMatrix, false);
//		Matrix4x4 worldToView = projectionMatrix * Camera.main.worldToCameraMatrix;
//		Matrix4x4 viewToWorld = worldToView.inverse;
//
//		float viewport_z01 = v.z;
//		float zFull = viewport_z01 * Camera.main.farClipPlane;
//		float zNearToFar = Mathf.Lerp(Camera.main.nearClipPlane, Camera.main.farClipPlane, viewport_z01);
//		float alpha = zFull / zNearToFar;
//		Vector4 position = viewToWorld * new Vector4(v.x, v.y, 1 - alpha, 1f);
//		position.x /= position.w;
//		position.y /= position.w;
//		position.z /= position.w;
//		return new Vector3 (position.x, position.y, position.z);
//	}
		

	void Update()
	{
		if(Input.GetKeyDown ("space"))
		{
			Debug.Log ("box: pressed space");
			spawnBoxes();
		}
		if (Input.GetKey (KeyCode.Escape)) {
			SceneManager.LoadSceneAsync ("menu");
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
