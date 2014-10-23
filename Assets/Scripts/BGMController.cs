using UnityEngine;
using System.Collections;

public class BGMController : MonoBehaviour {

	public AudioClip DefeultBGM;
	public AudioClip InvinsibleBGM;

	private AudioSource Audio;

	// Use this for initialization
	void Start () {
		// オーディオソースの取得と栗ポップの設定
		Audio = GetComponent<AudioSource> ();
		Audio.clip = DefeultBGM;

		Audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void StartInbisible(){
		Audio.clip = InvinsibleBGM;
		Audio.Play ();
	}

	public void EndInbinsible(){
		Audio.clip = DefeultBGM;
		Audio.Play();
	}
}
