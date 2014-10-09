using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour {
	public GameObject Target;
	public Vector3 CameraOffet;
	public bool FillowTarget = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (FillowTarget) {
				transform.position = new Vector3 (Target.transform.position.x + CameraOffet.x,
                                 Target.transform.position.y + CameraOffet.y,
                                 Target.transform.position.z + CameraOffet.z);
				transform.LookAt (Target.transform);
		}

		if(transform.position.y < 0){
			FillowTarget = false;
		}
	}
}
