using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;

public class Activity : MonoBehaviour {

	public int id;
	public GameObject panel;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void HandleMenuClick() {
		var encoding = new System.Text.UTF8Encoding();
		var postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");
		var jsonString = "{ \"query\": \"{\\n  activity(id: " + this.id + ") {\\n    name,\\n    problems {\\n      id,\\n      name,\\n      sceneUrl,\\n      imageUrl\\n    }\\n  }\\n}\\n\"}";

		string url = "http://www.vrithm.com/api";
		WWW www = new WWW(url, encoding.GetBytes(jsonString), postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;

		// check for errors
		if (www.error != null) {
			Debug.Log ("Text: " + www.text);
			var output = JSON.Parse (www.text);
			GameObject.Find ("ActivitiesPanel").SetActive (false);
			panel.SetActive (true);
			var problems = output ["data"] ["activity"] ["problems"];

			for (int i = 1; i <= 5; i++) {
				if (i <= problems.Count) {
					var problem = problems [i - 1];
					ProblemMenuItem p = GameObject.Find ("Problem (" + (i) + ")").GetComponent<ProblemMenuItem> ();
					p.id = problem ["id"];
					p.imageUrl = problem ["imageUrl"];
					p.sceneUrl = problem ["sceneUrl"];
					Text pNameText = GameObject.Find ("Problem (" + (i) + ")/TextContainer/TopLine/Text").GetComponent<Text> ();
					pNameText.text = problem ["name"];
					Text pStatusText = GameObject.Find ("Problem (" + (i) + ")/TextContainer/BotLine/Status").GetComponent<Text> ();
					pStatusText.text = i == 1 ? "Completed" : "In Progress";

				} else {
					GameObject.Find ("Problem (" + i + ")").SetActive (false);
				}
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
}
