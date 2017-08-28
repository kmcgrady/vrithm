using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ProblemMenuItem : MonoBehaviour {

	public int id;
	public String imageUrl;
	public String sceneUrl;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void LoadProblem() {
		Debug.Log ("Running");
		StartCoroutine(GetAssetBundle());
	}

	IEnumerator GetAssetBundle() {
		UnityWebRequest www = UnityWebRequest.GetAssetBundle("https://s3-us-west-2.amazonaws.com/vrithm-scenes/problem2");
		www.Send();
		Debug.Log("Done");
		while (!www.isDone)
		{
			Debug.Log("Inside waiting loop, updating progress");
			Debug.Log(www.downloadProgress);
			Debug.Log(www.isDone);
			yield return new WaitForEndOfFrame();
		}

		if(www.isNetworkError) {
			Debug.Log(www.error);
		}
		else {
			Debug.Log ("Success");
			AssetBundle bundle = ((DownloadHandlerAssetBundle)www.downloadHandler).assetBundle;
			string[] scenePath = bundle.GetAllScenePaths();
			Debug.Log(scenePath[0]); // -> "Assets/scene.unity"
			Application.LoadLevel(scenePath[0]);
		}
	}
}
