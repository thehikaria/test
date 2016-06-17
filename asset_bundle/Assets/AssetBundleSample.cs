using System;
using UnityEngine;
using System.Collections;


public class AssetBundleSample : MonoBehaviour {

	/// <summary>
	/// アセットバンドルのダウンロード リミット時間
	/// </summary>
	public readonly static float LIMIT_TIME = 5f;

	public GUIText guitext;

	// Use this for initialization
	void Start () {
		// Clear Cache
		Caching.CleanCache();

		//string loadUrl = "http://styleport.jp/webgl_build/Cube";
		string loadUrl = "https://s3-ap-northeast-1.amazonaws.com/styleport-staging/Cube";

		//#if   UNITY_ANDROID && !UNITY_EDITOR
		//loadUrl += ".android.unity3d";
		//#elif UNITY_IPHONE  && !UNITY_EDITOR
		//loadUrl += ".iphone.unity3d";
		//#else
		//loadUrl += ".unity3d";
		//#endif

		#if UNITY_WEBGL
		loadUrl += ".unity3d";
		#endif

		//StartCoroutine(load(loadUrl, 1));
		StartCoroutine(load(loadUrl, 1));
	}

	// Update is called once per frame
	void Update () {
		// progress
		//int percent = (int)(www.progress * 100);
		//guitext.text = percent.ToString() + "%";
	}

	private WWW www;

	private IEnumerator load(string url, int version) {
		// wait for the caching system to be ready
		while (!Caching.ready)
			yield return null;

		// load AssetBundle file from Cache if it exists with the same version or download and store it in the cache
		www = WWW.LoadFromCacheOrDownload(url, version);

		// ③ダウンロード完了を待つ
		var startTime = Time.realtimeSinceStartup;
		//AssetBundle assetBundle = null;
		while (www.isDone == false) {
			yield return 0;
			if (www.progress == 0f && Time.realtimeSinceStartup - startTime > LIMIT_TIME) {
				// timeout
				break;
			}
		}
		yield return www;

		Debug.Log("Loaded ");

		if (www.error != null)
			throw new Exception("WWW download had an error: " + www.error);

		AssetBundle assetBundle = www.assetBundle;
		Instantiate(assetBundle.mainAsset); // Instantiate(assetBundle.Load("AssetName"));

		// Unload the AssetBundles compressed contents to conserve memory
		assetBundle.Unload(false);
	}

}