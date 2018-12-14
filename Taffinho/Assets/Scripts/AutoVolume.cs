using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof( AudioSource))]
public class AutoVolume : MonoBehaviour {
	AudioSource source;
	// Use this for initialization
	void Start () {
		source=GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		float volume=1;
		if(source.time<1)volume=source.time;
		else if(source.time>source.clip.length-1)volume=source.clip.length-source.time;
		source.volume=volume;
	}
}
