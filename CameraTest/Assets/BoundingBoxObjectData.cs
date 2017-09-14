using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class BoundingBoxObjectData : PooledObject {

//	public GameObject box;
//	public VectorLine line;
//	public float x;
//	public float y;
//	public float z;
//	public int frameCount;
//
//	void Awake()
//	{
//		line = new VectorLine ("BoundingBoxLines", new List<Vector3> (24), 5.0f);
//		line.color = Color.green;
//		line.joins = Joins.Weld;
//		line.drawTransform = box.transform;
//		line.active = false;
//	}
//
//	// Use this for initialization
//	void Start () {
//		//position = new Vector3(0, 0, 0);
//		//x = 0.0f;
//		//y = 0.0f;
//		//z = 0.0f;
////		line = new VectorLine ("BoundingBoxLines", new List<Vector3> (24), 5.0f);
////		line.color = Color.green;
////		line.joins = Joins.Weld;
////		line.drawTransform = box.transform;
////		line.active = false;
//		frameCount = 30;
//		//VectorManager.ObjectSetup (box, line, Visibility.Dynamic, Brightness.None);
//
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		box.transform.RotateAround (box.transform.position, box.transform.TransformDirection(Vector3.up), 5f);
//		frameCount--;
//		if (frameCount == 0) {
//			frameCount = 30;
//			Release();
//		}
//	}
//
//	void LastUpdate()
//	{
//		//line.Draw();
//	}
//
//	public void Release()
//	{
//		line.active = false;
//		ReturnToPool ();
//	}
//
//	void OnDestroy()
//	{
//		VectorLine.Destroy (ref line);
//	}

	public GameObject box;
	public BoxCollider col;
	public VectorLine line;
	public int frameCount;
	public Color color;


	// Use this for initialization
	void Start () {
		frameCount = 30;

		//rotate to camera
		//box.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);

		var vertices = new Vector3[8];
		var thisMatrix = box.transform.localToWorldMatrix;
		var storedRotation = box.transform.rotation;
		box.transform.rotation = Quaternion.identity;


		vertices[0] = col.center + new Vector3 (col.size.x, col.size.y, col.size.z) * 0.5f;
		vertices[1] = col.center + new Vector3 (-col.size.x, col.size.y, col.size.z) * 0.5f;
		vertices[2] = col.center + new Vector3 (col.size.x, col.size.y, -col.size.z) * 0.5f;
		vertices[3] = col.center + new Vector3 (-col.size.x, col.size.y, -col.size.z) * 0.5f;
		vertices[4] = col.center + new Vector3 (col.size.x, -col.size.y, col.size.z) * 0.5f;
		vertices[5] = col.center + new Vector3 (-col.size.x, -col.size.y, col.size.z) * 0.5f;
		vertices[6] = col.center + new Vector3 (col.size.x, -col.size.y, -col.size.z) * 0.5f;
		vertices[7] = col.center + new Vector3 (-col.size.x, -col.size.y, -col.size.z) * 0.5f;


		box.transform.rotation = storedRotation;

		var boxPoints = new List<Vector3>{
			vertices[5],
			vertices[4],
			vertices[1],
			vertices[5],
			vertices[4], 
			vertices[0], 
			vertices[0], 
			vertices[1],
			vertices[3], 
			vertices[1], 
			vertices[0], 
			vertices[2], 
			vertices[2], 
			vertices[3], 
			vertices[7],
			vertices[3], 
			vertices[2], 
			vertices[6], 
			vertices[6], 
			vertices[7], 
			vertices[5], 
			vertices[7], 
			vertices[6], 
			vertices[4]};


		line = new VectorLine ("BoundingBoxLines", boxPoints, 5.0f);
		line.color = color;
		//line.joins = Joins.Weld;
		line.drawTransform = box.transform;
		line.Draw3DAuto ();
		//line.active = false;
		//VectorManager.ObjectSetup (box, line, Visibility.Dynamic, Brightness.None);
		//VectorLine.canvas.sortingOrder = -1;

	}


	// Update is called once per frame
	void Update () {
		//box.transform.RotateAround (box.transform.position, box.transform.TransformDirection(Vector3.up), 5f);
		//box.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);
		box.transform.LookAt (box.transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
			frameCount--;
			if (frameCount == 0) {
				frameCount = 30;
				Release();
			}
	}
		

	public void Release()
	{
		line.active = false;
		ReturnToPool ();
	}

	void OnDestroy()
	{
		VectorLine.Destroy (ref line);
	}
		
}
