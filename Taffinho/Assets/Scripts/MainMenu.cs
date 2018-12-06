using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour 
{

	[SerializeField]
	GameObject buttonGroup;
	
	// Use this for initialization
	void Start () 
	{

	}
	
	void OnEnabled()
	{
		buttonGroup.SetActive(true);
	}

	void OnDisable()
	{
		buttonGroup.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{    

	}
}
