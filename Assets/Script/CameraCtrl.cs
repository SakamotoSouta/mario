using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
	public GameObject Target;
	public Vector3 CameraOffet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Target.transform.position.x + CameraOffet.x,
		                                 Target.transform.position.y + CameraOffet.y,
		                                 Target.transform.position.z + CameraOffet.z);
		transform.LookAt(Target.transform);
	}
}
