using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teste : MonoBehaviour {

	/// <summary>
	/// Main Test Class
	/// Everything here will NOT be at the final game
	/// </summary>
	/// <param name="tx">float that contain Screen.width</param>
	/// <param name="ty">float that contain Screen.height</param> 
	float tx;
	float ty;

	void Start () {
		
	}
	
	void Update () {
		tx = Screen.width;
		ty = Screen.height;
		
	}

	void OnGUI() {

		GUI.Box (new Rect(tx/2, ty/20, tx/10, ty/10), StringSource.scoreText);

	}
}
