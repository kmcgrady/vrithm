using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class LoadActivities : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var encoding = new System.Text.UTF8Encoding();
		var postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");
		var jsonString = "{ \"query\": \"{\\n  me(id: 2) {\\n    firstName,\\n    activities {\\n      id,\\n      name,\\n      dueDate,\\n      isPublished\\n    }\\n  }\\n}\" }";
		string url = "http://localhost:8000/api";
		WWW www = new WWW(url, encoding.GetBytes(jsonString), postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;

		// check for errors
		if (www.error != null)
		{
			var output = JSON.Parse (www.text);
			var activities = output ["data"] ["me"] ["activities"];
			for (int i = 1; i <= 5; i++)
			{
				if (i <= activities.Count) {
					var activity = activities [i - 1];
					Text activityViewText = GameObject.Find ("Activity (" + (i) + ")/TextContainer/Topline/Text").GetComponent<Text> ();
					activityViewText.text = activity ["name"];
				} else {
					GameObject.Find ("Activity (" + i + ")").SetActive (false);
				}
			}

		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
