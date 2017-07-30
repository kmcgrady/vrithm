using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {

	public AudioSource problem;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GotoProblem() {
		// Update flags first by copying from device and other inputs.
		if (UnityEngine.SceneManagement.SceneManager.GetActiveScene ().name == "MathRoom") {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("UnderwaterProblem");
		} else {
			UnityEngine.SceneManagement.SceneManager.LoadScene ("MathRoom");
		}
	}

	public void ReadProblem() {
		problem.Play();
	}
}
