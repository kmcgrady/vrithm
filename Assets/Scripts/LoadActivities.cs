using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class LoadActivities : MonoBehaviour {

	bool hasRun = false;
	// Use this for initialization
	void Start () {
		if (hasRun) {
			return;
		}

		hasRun = true;
		var encoding = new System.Text.UTF8Encoding();
		var postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");
		var jsonString = "{ \"query\": \"{\\n  me(id: 2) {\\n    firstName,\\n    activities {\\n      id,\\n      name,\\n      dueDate,\\n      isPublished\\n    }\\n  }\\n}\" }";
		string url = "http://localhost:8000/api";
		WWW www = new WWW(url, encoding.GetBytes(jsonString), postHeader);
		StartCoroutine(WaitForRequest(www));
	}

	IEnumerator WaitForRequest(WWW www) {
		yield return www;

		// check for errors
		if (www.error != null) {
			Debug.Log ("Text: " + www.text);
			var output = JSON.Parse (www.text);
			var activities = output ["data"] ["me"] ["activities"];
			DateTime localDate = DateTime.Now;
			for (int i = 1; i <= 5; i++) {
				if (i <= activities.Count) {
					var activity = activities [i - 1];
					Activity a = GameObject.Find ("Activity (" + i + ")").GetComponent<Activity> ();
					a.id = activity ["id"];
					Text activityViewText = GameObject.Find ("Activity (" + (i) + ")/TextContainer/TopLine/Text").GetComponent<Text> ();
					activityViewText.text = activity ["name"];
					Text statusViewText = GameObject.Find ("Activity (" + (i) + ")/TextContainer/BotLine/Status").GetComponent<Text> ();
					statusViewText.text = i == 1 ? "In Progress" : "Completed";
					Text dueDateViewText = GameObject.Find ("Activity (" + (i) + ")/TextContainer/BotLine/Due Date").GetComponent<Text> ();
					if (activity ["dueDate"] != null) {
						DateTime dueDate = DateTime.Parse (activity ["dueDate"]);
						if (dueDate > localDate) {
							dueDateViewText.text = "Due in " + GetTimeDifference (localDate, dueDate);
						} else if (statusViewText.text == "In Progress") {
							dueDateViewText.text = "Past Due";
							dueDateViewText.color = new Color (1, 0, 0);
						}
					} else {
						dueDateViewText.text = "No Due Date";
					}
				} else {
					GameObject.Find ("Activity (" + i + ")").SetActive (false);
				}
			}
		} else {
			Debug.Log("WWW Error: "+ www.error);
		}    
	}

	String GetTimeDifference(DateTime date, DateTime laterDate) {
		if (laterDate <= date) {
			return "In the Past";
		}
		
		TimeSpan ts = laterDate - date;
		if (ts.Days > 0 && ts.TotalHours > 30) {
			return String.Format ("about {0} days", ts.Days);
		} else if (ts.TotalHours > 0) {
			return String.Format ("about {0} hours", ts.TotalHours);
		} else if (ts.TotalMinutes > 0) {
			return String.Format ("about {0} minutes", ts.TotalMinutes);
		} else {
			return "soon";
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
