using UnityEngine;
using System.Collections;

public class ColliderController : MonoBehaviour {

	public Vector2 _Size;

	public float _gridSize;
	public GameObject _colliderPrefab;

	private Vector2 _numBlock;
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


		_blockSize = _gridSize;
		_numBlock = _Size;
		_numMaxBlock = (int)(_numBlock.x * _numBlock.y);
		_colliderObjects = new GameObject[_numMaxBlock];
		
		_parentObject = GameObject.Find ("ColliderRoot");


		for(int i = 0; i < _numBlock.y; i++){
			for(int j = 0; j < _numBlock.x; j++){
				_colliderObjects[(i * (int)_numBlock.x) + j] = (GameObject)Instantiate(_colliderPrefab, transform.position, transform.rotation);
				_colliderObjects[(i * (int)_numBlock.x) + j].transform.localScale = new Vector3 (_blockSize, _blockSize, 0);

				_colliderObjects[(i * (int)_numBlock.x) + j].transform.position = new Vector3(_blockSize * -( _numBlock.x / 2 ) +  (_blockSize * j), _blockSize * ( _numBlock.y /2 ) - (_blockSize * i), 0);
				_colliderObjects[(i * (int)_numBlock.x) + j].transform.Translate(_blockSize / 2, -_blockSize / 2, 0);
				_colliderObjects[(i * (int)_numBlock.x) + j].transform.parent = _parentObject.transform;
			}
		}
	}
}
