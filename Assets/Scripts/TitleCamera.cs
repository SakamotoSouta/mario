using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {
	public GameObject Target;
	public Vector3 CameraOffet;
	private float CameraCount;
	// Use this for initialization
	void Start () {
		if (!Target) {
			Target = gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () {
		CameraCount += 0.01f;
		if (Target) {
			transform.Translate(Mathf.Cos(CameraCount % (Mathf.PI * 2)), 0, Mathf.Sin(CameraCount % (Mathf.PI * 2)));
			transform.LookAt (Target.transform);
		}

	}
}
