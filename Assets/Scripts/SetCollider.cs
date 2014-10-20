using UnityEngine;
using System.Collections;

public class SetCollider : MonoBehaviour {
	public GameObject colliderObjectPrefab;
	private GameObject[] colliderObject = new GameObject[2];

	// Use this for initialization
	void Start () {
		colliderObject[0] =  (GameObject)Instantiate(colliderObjectPrefab, transform.position, transform.rotation);
		colliderObject[1] =  (GameObject)Instantiate(colliderObjectPrefab, transform.position, transform.rotation);

	}
	
	// Update is called once per frame
	void Update () {
		colliderObject [0].transform.position = new Vector3 (transform.position.x, transform.position.y, 0.8f);
		colliderObject [1].transform.position = new Vector3 (transform.position.x, transform.position.y, -0.8f);
	}
}
