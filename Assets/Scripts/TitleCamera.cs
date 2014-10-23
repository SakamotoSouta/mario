using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {
	private float CameraCount = 0;
	public Vector3 CameraDefaultPosition;
	private int TitleCounter;
	private bool Demo = false;
	private Vector3 oldMousePosition;

	// Use this for initialization
	void Start () {

		transform.position = CameraDefaultPosition;
	}
	
	// Update is called once per frame
	void Update () {
		TitleCounter++;

		if(TitleCounter > 300){
			Demo = true;
		}

		if (Input.anyKey || Input.mousePosition != oldMousePosition) {
			TitleCounter = 0;
			CameraCount = 0;
			Demo = false;
			transform.position = CameraDefaultPosition;
		}

		if(Demo){
			CameraCount += 0.001f;
			if(CameraCount > Mathf.PI){
				Demo = false;
				CameraCount = 0;
				TitleCounter = 0;
				transform.position = CameraDefaultPosition;
			}
			transform.position = new Vector3(CameraDefaultPosition.x + Mathf.Sin(CameraCount) * 100, transform.position.y, transform.position.z);

		}
		oldMousePosition = Input.mousePosition;
	}
}
