using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPart : MonoBehaviour {
	
	/// <summary>
	/// Class that creates a Line-note
	/// </summary>
	/// <param id"LineRenderer">Object that creates a Line</param>
	/// <param id"end">Vector3 object with the end of the linenote</param>
	/// <param id"mat">Object that contains the material of the line renderer</param>
	/// <param id"isOff">Boolena that will check if the trail is off or not</param>
	public LineRenderer line;
	public Vector3 end;
	public Transform lockon;
	public Material mat;
	public bool isOff;

	/// <summary>
	/// Initialization
	/// </summary>
	void Start () {
		line=gameObject.AddComponent<LineRenderer>();
		line.positionCount=2;
		line.material=mat;
		line.startWidth=0.3f;
	}

	/// <summary>
	/// Method that will turn off the line-note
	/// Turns off line-note by changing it's collor to black
	/// </summary>
	public void TurnOff() {
		isOff=true;
		line.material.color=Color.black;
	}

	/// <summary>
	/// Update
	/// </summary>
	void Update () {
		Debug.Log(line.endColor);
		if(lockon)transform.position=lockon.position;
		line.SetPosition(0,transform.position);
		line.SetPosition(1,transform.parent.position+end);
		if((transform.parent.position+end).z<transform.position.z)Destroy(gameObject);
	}
	
}