using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vectrosity;
using TMPro;

public class LabelData : PooledObject {

	public GameObject text;
	public GameObject box;
	public BoxCollider col;
	public GameObject plane;
	public LabelBackground background;
	//public TextMesh mesh;
	public MeshRenderer render;
	public VectorLine textBox;
	public TextMeshPro mesh;
	//public TextContainer render;
	public RectTransform rect;
	public int frameCount;
	public Color color;
	public string labelText;
	public bool resize;

	// Use this for initialization
	void Start () {
//		//VectorLine.SetEndCap ("RoundedEnd", EndCap.Mirror);
//		points = new List<Vector3> ();
//		points.Add (col.center + new Vector3 (col.size.x/2, -col.size.y/2, -col.size.z/2) * 0.5f);
//		points.Add (col.center + new Vector3 (-col.size.x/2, -col.size.y/2, -col.size.z/2) * 0.5f);
//		textBack = new Vectrosity.VectorLine ("text holder", points, mesh.characterSize + 20f);
//		VectorLine.SetLine3D(Color.white, points[0], points[1]);
//		//textBack.endCap = "RoundedEnd";
//		textBack.drawTransform = text.transform;
//		textBack.Draw3DAuto ();
		resize = true;
		//label
		//mesh = gameObject.GetComponent<TextMeshPro>();
		mesh.color = Color.black;
		//mesh.fontSizeMin = 12f;
		//mesh.SetText (labelText);

		//rect = text.GetComponent<RectTransform> ();

//		rect.sizeDelta = new Vector2 (mesh.preferredWidth, mesh.preferredHeight);
//		rect.transform.position = rect.transform.position + new Vector3 (0, (mesh.preferredHeight / 2), 0);

		//render = gameObject.GetComponent<TextContainer> ();
		render = gameObject.GetComponent<MeshRenderer> ();
		//render.rectTransform.sizeDelta = new Vector2(mesh.preferredWidth, mesh.preferredHeight);
		//mesh.enableAutoSizing = true;

		//plane background
//		plane.transform.position = plane.transform.position + new Vector3(0, 0, 0.01f);
//		plane.transform.localScale = new Vector3(rect.sizeDelta.x/10, rect.sizeDelta.y/10, rect.sizeDelta.y/10);

		background = gameObject.GetComponentInChildren<LabelBackground>();
		//background.render.transform.localScale.Set (rect.sizeDelta.x/10, rect.sizeDelta.y/10, 0);
		background.render.material.color = color;

		//rotate to camera
		//text.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);

		frameCount = 30;
//		var thisMatrix = box.transform.localToWorldMatrix;
//		var storedRotation = box.transform.rotation;
//		box.transform.rotation = Quaternion.identity;
//		var vertices = new Vector3[4];
//
//
//		vertices[0] = col.center + new Vector3 (col.size.x, col.size.y, 0) * 0.5f;
//		vertices[1] = col.center + new Vector3 (-col.size.x, col.size.y, 0) * 0.5f;
//		vertices[2] = col.center + new Vector3 (col.size.x, -col.size.y, 0) * 0.5f;
//		vertices[3] = col.center + new Vector3 (-col.size.x, -col.size.y, 0) * 0.5f;
//
//		box.transform.rotation = storedRotation;
//
//		var boxPoints = new List<Vector3>{
//			vertices[0],
//			vertices[2],
//			vertices[1],
//			vertices[3],
//			vertices[2], 
//			vertices[0], 
//			vertices[0], 
//			vertices[1],
//			vertices[3], 
//			vertices[1], 
//			vertices[3], 
//			vertices[2]};
//
//
//		textBox = new VectorLine ("LabelBoxLines", boxPoints, 5.0f);
//		textBox.color = color;
//		//line.joins = Joins.Weld;
//		textBox.drawTransform = mesh.transform;
//		textBox.Draw3DAuto ();
		//VectorLine.canvas.sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {
		text.transform.LookAt (text.transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
		if (resize) {
			//SizeCollider ();
			resize = false;
		}
		frameCount--;
		if (frameCount == 0) {
			frameCount = 30;
			Release ();
		}
//		//rotate to camera
		//text.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);
//
//		//resize based on distance from camera
//		if (Mathf.Abs (Camera.main.transform.position.z - text.transform.position.z) > Mathf.Abs (Camera.main.transform.position.x - text.transform.position.x))
//		{
//			mesh.characterSize = Mathf.Abs (Camera.main.transform.position.z - text.transform.position.z) / 28;
//			//textBack.SetWidth (Mathf.Abs (Camera.main.transform.position.z - text.transform.position.z) + 20);
//		}
//		else {
//			mesh.characterSize = Mathf.Abs (Camera.main.transform.position.x - text.transform.position.x) / 28;
//			//textBack.SetWidth (Mathf.Abs (Camera.main.transform.position.x - text.transform.position.x) + 20);
//		}

	}

	public void SizeCollider()
	{   
//		col.center = new Vector3 (render.bounds.extents.x - render.bounds.size.x / 2, 0, 0);
//		col.size = new Vector3 (render.bounds.size.x + (mesh.fontSize/2) * .1f, render.bounds.extents.y + (mesh.fontSize/2) * .1f, 0);
//		var vertices = new Vector3[4];
//
//
//		vertices[0] = col.center + new Vector3 (col.size.x, col.size.y, 0) * 0.5f;
//		vertices[1] = col.center + new Vector3 (-col.size.x, col.size.y, 0) * 0.5f;
//		vertices[2] = col.center + new Vector3 (col.size.x, -col.size.y, 0) * 0.5f;
//		vertices[3] = col.center + new Vector3 (-col.size.x, -col.size.y, 0) * 0.5f;

		col.center = new Vector3 (0, 0, 0);
		col.size = new Vector3 (rect.sizeDelta.x, rect.sizeDelta.y, 0);
		var vertices = new Vector3[4];


		vertices[0] = col.center + new Vector3 (col.size.x, col.size.y, 0) * 0.5f;
		vertices[1] = col.center + new Vector3 (-col.size.x, col.size.y, 0) * 0.5f;
		vertices[2] = col.center + new Vector3 (col.size.x, -col.size.y, 0) * 0.5f;
		vertices[3] = col.center + new Vector3 (-col.size.x, -col.size.y, 0) * 0.5f;

//		col.center = rect.transform.position;
//		col.size = new Vector3 (rect.sizeDelta.x, rect.sizeDelta.y, 0);
//		var vertices = new Vector3[4];
//
//
//		vertices[0] = col.center + new Vector3 (col.size.x, col.size.y, 0) * 0.5f;
//		vertices[1] = col.center + new Vector3 (-col.size.x, col.size.y, 0) * 0.5f;
//		vertices[2] = col.center + new Vector3 (col.size.x, -col.size.y, 0) * 0.5f;
//		vertices[3] = col.center + new Vector3 (-col.size.x, -col.size.y, 0) * 0.5f;

		//Vector3[] vertices = new Vector3[4];
		//rect.GetWorldCorners (vertices);
		//var vertices = render.corners;
		textBox.points3 = new List<Vector3>{
			vertices[0],
			vertices[2],
			vertices[1],
			vertices[3],
			vertices[2], 
			vertices[0], 
			vertices[0], 
			vertices[1],
			vertices[3], 
			vertices[1], 
			vertices[3], 
			vertices[2]};
		
		//rect.transform.position = rect.transform.position + new Vector3 (0, (mesh.preferredHeight / 2), 0);
		//plane.transform.localScale = new Vector3((render.bounds.size.x + (mesh.fontSize/2) * .1f)/10, rect.sizeDelta.y/10, (render.bounds.extents.y + (mesh.fontSize/2) * .1f)/10);

	}
		

	public void Release()
	{
		textBox.active = false;
		//Destroy (mesh);
		ReturnToPool();
		//VectorLine.Destroy (ref textBox);
	}
}
