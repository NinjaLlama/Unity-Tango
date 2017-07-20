using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceVideo : MonoBehaviour {

	public GameObject m_plane;
	private TangoPointCloud m_pointCloud;
	private Stack<GameObject> objects = new Stack<GameObject> ();
	private bool buttonPress;

	// Use this for initialization
	void Start () {
		m_pointCloud = FindObjectOfType<TangoPointCloud>();
		buttonPress = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (buttonPress == false) { //can place object if not pressing the button
			if (Input.touchCount == 1) {
				// Trigger place plane function when single touch ended.
				Touch t = Input.GetTouch (0);
				if (t.phase == TouchPhase.Ended) {
					PlacePlane (t.position);
				}
			}
		} else //the button was already pressed (true), can place an object now so set to false
			buttonPress = false;
	}

	void PlacePlane(Vector2 touchPosition)
	{
		// Find the plane.
		Camera cam = Camera.main;
		Vector3 planeCenter;
		Plane plane;
		if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane))
		{
			Debug.Log("cannot find plane.");
			return;
		}

		// Place plane on the surface, and make it always face the camera.
		if (Vector3.Angle(plane.normal, Vector3.up) < 30.0f)
		{
			Vector3 up = plane.normal;
			Vector3 right = Vector3.Cross(plane.normal, cam.transform.forward).normalized;
			Vector3 forward = Vector3.Cross(right, plane.normal).normalized;
			objects.Push(Instantiate(m_plane, planeCenter, Quaternion.LookRotation(forward, up) * Quaternion.Euler(90, 0, 180)));

		}
		else
		{
			Debug.Log("surface is too steep for kitten to stand on.");
		}
			
	}

	public void RemoveObject()
	{
		buttonPress = true;
		Destroy(objects.Pop());
	}
}