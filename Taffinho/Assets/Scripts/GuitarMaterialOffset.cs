using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarMaterialOffset : MonoBehaviour {

	/// <summary>
	/// Class with Guitar Material Offset
	/// It creates the effect of career grid sliding
	/// </summary>
	/// <param name="rendy">Renderer object</param>
	/// <param name="speed">Public fixed speed adjusted at inspector</param>
	/// <param name="offset">Times that renderer will update per second multiplied by speed</param>

	private Renderer rendy;
	public float speed;
	private float offset;


	/// <summary>
	/// Inicialization of Rendy as renderer
	/// </summary>
	void Start () {
		
		rendy = GetComponent<Renderer>();

	}
	
	/// <summary>
	/// Update methd
	/// Definition of offset * speed
	/// update rendy's material
	/// </summary>
	void Update () {
		
		offset = 0.01f * Time.time * speed;
		rendy.material.SetTextureOffset ("_MainTex", new Vector2 (0, -offset));
	}	
}
