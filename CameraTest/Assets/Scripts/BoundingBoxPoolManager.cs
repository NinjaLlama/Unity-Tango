using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoundingBoxPoolManager : MonoBehaviour {

	public BoundingBoxObjectData prefab;
	public BoundingBoxObjectData spawn;
	public LabelData prefabText;
	public LabelData spawnText;
	public Dictionary<string, List<BoundingBoxObjectData>> boundingBoxObjects;


	public void CreateBoundingBoxObject(Vector3 position, float x, float y, float z, string label, Color color)
	{
//		spawn = prefab.GetPooledInstance<BoundingBoxObjectData>();
//		spawn.x = x;
//		spawn.y = y;
//		spawn.z = z;
//		spawn.box.transform.position = position;
//		//spawn.line.drawTransform = spawn.box.transform;
//		spawn.line.MakeCube (spawn.box.transform.position, x, y, z);
//		//spawn.line.MakeCube (position, x, y, z);
//		spawn.line.active = true;
//		spawn.line.Draw3DAuto ();

		//bounding box
		spawn = prefab.GetPooledInstance<BoundingBoxObjectData>();
		spawn.box.transform.position = position;
		spawn.box.transform.rotation = Quaternion.identity;
		//spawn.box.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);
		spawn.box.transform.LookAt (spawn.box.transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
		//spawn.box.transform.RotateAround(spawn.box.transform.position, spawn.box.transform.TransformDirection(Vector3.up), Vector3.Angle(Vector3.Normalize(Camera.main.transform.forward), Vector3.Normalize(spawn.box.transform.forward)));
		spawn.box.transform.localScale = new Vector3 (x, y, z);
		spawn.color = color;
		spawn.line.active = true;

		//label
		//spawnText = spawn.GetComponentInChildren<LabelData> ();
		spawnText = prefabText.GetPooledInstance<LabelData> ();
		//spawnText.text.transform.rotation = spawn.box.transform.rotation;
		//spawnText.text.transform.SetParent(spawn.box.transform);
		//spawnText.transform.parent = null;
		spawnText.text.transform.position = spawn.box.transform.position + new Vector3(0, y/2, -z/2);
		//spawnText.rect.transform.position = spawnText.rect.transform.position + new Vector3 (0, (spawnText.mesh.preferredHeight / 2), 0);
		//spawnText.mesh.color = color;

		//set label text
		spawnText.mesh.SetText(label);

		//set rect transform
		spawnText.rect.sizeDelta = new Vector2 (spawnText.mesh.preferredWidth, spawnText.mesh.preferredHeight);
		spawnText.rect.transform.position = spawnText.rect.transform.position + new Vector3 (0, (spawnText.mesh.preferredHeight / 2), 0);
		//spawnText.text.transform.position = spawnText.text.transform.position + new Vector3(spawnText.rect.sizeDelta.x/2, 0, 0);
		spawnText.plane.transform.position = spawnText.rect.transform.position;
		spawnText.plane.transform.localPosition = new Vector3 (0, 0, 0.05f);
		//spawnText.SizeCollider ();

		spawnText.rect.transform.LookAt (spawnText.rect.transform.position + Camera.main.transform.rotation * Vector3.forward,
			Camera.main.transform.rotation * Vector3.up);
//		spawnText.plane.transform.LookAt (spawnText.plane.transform.position + Camera.main.transform.rotation * Vector3.forward,
//			Camera.main.transform.rotation * Vector3.up);
//		spawnText.plane.transform.rotation = spawnText.plane.transform.rotation * Quaternion.Euler (90f, 0, 0);
		//spawnText.text.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);
		//spawnText.plane.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (0, 0, 0);

		//match plane to rect transform
//		if(spawnText.text.transform.rotation.y >= -.7f && spawnText.text.transform.rotation.y <= .7f)
//			spawnText.plane.transform.position = spawnText.rect.transform.position + new Vector3(0, 0, 0.05f);
// 		else
//			spawnText.plane.transform.position = spawnText.rect.transform.position + new Vector3(0, 0, -0.05f);
		spawnText.plane.transform.localScale = new Vector3(spawnText.rect.sizeDelta.x/9.5f, 0, spawnText.rect.sizeDelta.y/10);
		//spawnText.rect.transform.localRotation = Quaternion.Euler(0,gameObject.GetComponent<TangoPoseController> ().finalRotation.eulerAngles.y,0);
		spawnText.color = color;
		spawnText.textBox.active = true;

//		spawnText = prefabText.GetPooledInstance<LabelData> ();
//		//spawnText.text.transform.rotation = spawn.box.transform.rotation;
//		//spawnText.text.transform.SetParent(spawn.box.transform);
//		spawnText.text.transform.position = spawn.box.transform.position + new Vector3(0, y/2, -z/2);
//		spawnText.mesh.color = color;
//		spawnText.mesh.text = label;
//		spawnText.SizeCollider ();
//		spawnText.color = Color.red;
//		spawnText.textBox.active = true;

	}

	public void RemoveBoundingBoxObject(BoundingBoxObjectData box)
	{
		box.Release ();
	}

	// Use this for initialization
	void Start () {
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
		boundingBoxObjects = new Dictionary<string, List<BoundingBoxObjectData>> ();
	}
	
	// Update is called once per frame
	void Update () {
		//spawn.box.transform.rotation = Quaternion.LookRotation (Camera.main.transform.up, -Camera.main.transform.forward) * Quaternion.Euler (90f, 0, 0);
	}
}
