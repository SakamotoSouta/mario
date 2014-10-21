using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour {

	private Grid _gridObject;
	public GameObject _colliderPrefab;

	private int _numBlock;
	private int _numMaxBlock;
	private float _blockSize;

	private GameObject[] _colliderObjects; 
	private GameObject _parentObject;


	// Use this for initialization
	void Start () {
		InitColloiderGrid ();
	}
	
	// Update is called once per frame
	void Update () {


	}

	private void InitColloiderGrid(){
		_gridObject = GetComponent<Grid> (); 
		_blockSize = _gridObject.gridSize;
		_numBlock = _gridObject.size;
		_numMaxBlock = (int)Mathf.Pow(_gridObject.size, 2);
		_colliderObjects = new GameObject[_numMaxBlock];
		
		_parentObject = GameObject.Find ("ColliderRoot");


		for(int i = 0; i < _numMaxBlock; i++){
			_colliderObjects[i] = (GameObject)Instantiate(_colliderPrefab, transform.position, transform.rotation);
			_colliderObjects[i].transform.localScale = new Vector3 (_blockSize, _blockSize, 0);

			_colliderObjects[i].transform.position = new Vector3(_blockSize * -( _numBlock / 2 ) +  (_blockSize * (i % _numBlock)), _blockSize * ( _numBlock /2 ) - (_blockSize * (i / _numBlock)), 0);
			_colliderObjects[i].transform.Translate(_blockSize / 2, -_blockSize / 2, 0);
			_colliderObjects[i].transform.parent = _parentObject.transform;
		}
	}
}
