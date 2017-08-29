using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;

public class CubeBounds : MonoBehaviour {

	public GameObject cube;
	public BoxCollider col;
	public VectorLine line;

	// Use this for initialization
	void Start () {
		var vertices = new Vector3[8];
		var thisMatrix = cube.transform.localToWorldMatrix;
		var storedRotation = cube.transform.rotation;
		cube.transform.rotation = Quaternion.identity;


		vertices [0] = col.center + new Vector3 (col.size.x, col.size.y, col.size.z) * 0.5f;
		vertices [1] = col.center + new Vector3 (-col.size.x, col.size.y, col.size.z) * 0.5f;
		vertices [2] = col.center + new Vector3 (col.size.x, col.size.y, -col.size.z) * 0.5f;
		vertices[3] = col.center + new Vector3 (-col.size.x, col.size.y, -col.size.z) * 0.5f;
		vertices[4] = col.center + new Vector3 (col.size.x, -col.size.y, col.size.z) * 0.5f;
		vertices[5] = col.center + new Vector3 (-col.size.x, -col.size.y, col.size.z) * 0.5f;
		vertices[6] = col.center + new Vector3 (col.size.x, -col.size.y, -col.size.z) * 0.5f;
		vertices[7] = col.center + new Vector3 (-col.size.x, -col.size.y, -col.size.z) * 0.5f;

		/*var extents = col.bounds.extents;
		vertices[0] = thisMatrix.MultiplyPoint3x4(extents);
		vertices[1] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, extents.z));
		vertices[2] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, extents.y, -extents.z));
		vertices[3] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, extents.y, -extents.z));
		vertices[4] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, extents.z));
		vertices[5] = thisMatrix.MultiplyPoint3x4(new Vector3(-extents.x, -extents.y, extents.z));
		vertices[6] = thisMatrix.MultiplyPoint3x4(new Vector3(extents.x, -extents.y, -extents.z));
		vertices[7] = thisMatrix.MultiplyPoint3x4(-extents);*/

		cube.transform.rotation = storedRotation;

		var cubePoints = new List<Vector3>{
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


		line = new VectorLine ("BoundingBoxLines", cubePoints, 5.0f);
		line.color = Color.green;
		line.joins = Joins.Weld;
		line.drawTransform = cube.transform;
		line.Draw3DAuto ();
		//line.active = false;
		//VectorManager.ObjectSetup (cube, line, Visibility.Dynamic, Brightness.None);

	}

	
	// Update is called once per frame
	void Update () {
		cube.transform.RotateAround (cube.transform.position, cube.transform.TransformDirection(Vector3.up), 5f);
	}
}
