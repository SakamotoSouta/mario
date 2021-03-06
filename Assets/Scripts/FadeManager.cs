﻿using UnityEngine;
using System.Collections;

public class FadeManager : SingletonMonoBehaviour<FadeManager>
{
	// 暗転用黒テクスチャ
	private Texture2D Texture;
	// フェード中の透明度
	private float fadeAlpha = 0;
	// フェード中かどうか
	private bool isFading = false;
	
	public void Awake ()
	{
		if (this != Instance) {
			Destroy (this);
			return;
		}
		DontDestroyOnLoad (gameObject);
		// ここでテクスチャ作る
		this.Texture = new Texture2D (32, 32, TextureFormat.RGB24, false);
		this.Texture.ReadPixels (new Rect (0, 0, 32, 32), 0, 0, false);
		this.Texture.SetPixel (0, 0, Color.white);
		this.Texture.Apply ();
	}
	
	public void OnGUI ()
	{
		if (!this.isFading)
			return;
		
		// 透明度を更新してテクスチャを描画
		GUI.color = new Color (0, 0, 0, this.fadeAlpha);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), this.Texture);
	}
	

	public void LoadLevel(string scene, float interval)
	{
		StartCoroutine (TransScene (scene, interval));
	}
	public void LoadLevel(int scene, float interval)
	{
		StartCoroutine (TransScene (scene, interval));
	}

	private IEnumerator TransScene (string scene, float interval)
	{
		//だんだん暗く
		this.isFading = true;
		float time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (0f, 1f, time / interval);      
			time += Time.deltaTime;
			yield return 0;
		}
		
		//シーン切替
		Application.LoadLevel (scene);
		
		//だんだん明るく
		time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (1f, 0f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
		
		this.isFading = false;
	}
	private IEnumerator TransScene (int scene, float interval)
	{
		//だんだん暗く
		this.isFading = true;
		float time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (0f, 1f, time / interval);      
			time += Time.deltaTime;
			yield return 0;
		}
		//シーン切替
		Application.LoadLevel (scene);
		
		//だんだん明るく
		time = 0;
		while (time <= interval) {
			this.fadeAlpha = Mathf.Lerp (1f, 0f, time / interval);
			time += Time.deltaTime;
			yield return 0;
		}
		
		this.isFading = false;
	}
	
}