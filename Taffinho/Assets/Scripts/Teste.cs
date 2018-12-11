using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour {

	float tx;
	float ty;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		tx = Screen.width;
		ty = Screen.height;
		
	}

	void OnGUI() {

		GUI.Box (new Rect(tx/2, ty/20, tx/10, ty/10), StringSource.scoreText);

	}
}
