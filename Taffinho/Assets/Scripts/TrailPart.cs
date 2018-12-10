using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPart : MonoBehaviour {
	public LineRenderer line;
	public Vector3 end;
	public Transform lockon;
	public Material mat;
	public bool isOff;
	void Start () {
		
		line=gameObject.AddComponent<LineRenderer>();
		line.positionCount=2;
		line.material=mat;
		line.startWidth=0.3f;
	}
	public void TurnOff()
	{
		isOff=true;
		line.material.color=Color.black;
	}
	void Update () {
		Debug.Log(line.endColor);
		if(lockon)transform.position=lockon.position;
		line.SetPosition(0,transform.position);
		line.SetPosition(1,transform.parent.position+end);
		if((transform.parent.position+end).z<transform.position.z)Destroy(gameObject);
	}
	}
