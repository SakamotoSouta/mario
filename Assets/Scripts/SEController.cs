using UnityEngine;
using System.Collections;

public class SEController : MonoBehaviour {

	// SE管理用ラベル
	public enum SE_LABEL{
		SE_TREAD = 0,
		SE_BREAK,
		SE_NOT_BREAK,
		SE_FIREBALL,
		SE_PAWERUPITEM_GENERATE,
		SE_COIN,
		SE_MAX
	}
	// オーディオソース
	private AudioSource Audio;

	// SEオーディオクリップ
	public AudioClip Tread;
	public AudioClip Break;
	public AudioClip NotBreak;
	public AudioClip FireBall;
	public AudioClip PawerUpItemGenerate;
	public AudioClip Coin;

	// Use this for initialization
	void Start () {
		Audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// SEの再生
	public void SEPlay(SE_LABEL Label){
		// ラベルによってSEを再生
		switch(Label){
		case SE_LABEL.SE_TREAD:
			Audio.PlayOneShot(Tread);
			break;
		case SE_LABEL.SE_BREAK:
			Audio.PlayOneShot(Break);
			break;
		case SE_LABEL.SE_NOT_BREAK:
			Audio.PlayOneShot(NotBreak);
			break;
		case SE_LABEL.SE_FIREBALL:
			Audio.PlayOneShot(FireBall);
			break;
		case SE_LABEL.SE_PAWERUPITEM_GENERATE:
			Audio.PlayOneShot(PawerUpItemGenerate);
			break;
		case SE_LABEL.SE_COIN:
			Audio.PlayOneShot(Coin);
			break;
		default:
			break;
		}
	}
}
