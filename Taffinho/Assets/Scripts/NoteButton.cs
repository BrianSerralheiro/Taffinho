using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButton : MonoBehaviour {
	public GameObject note;
    public ParticleSystem particle;
	public KeyCode key=KeyCode.Q;
	void Start () {
		
	}
	
	void Update () {
		if(Input.GetKeyDown(key))OnMouseDown();
		if(Input.GetKeyUp(key))OnMouseUp();

	}
    public void OnMouseUp()
	{
		if(note && note.GetComponent<TrailPart>())
		{
			note.GetComponent<TrailPart>().TurnOff();
		}
	}
    public void OnMouseDown()
	{
        if (note)
        {
			TrailPart trail= note.GetComponent<TrailPart>();
			if(trail)
			{
				if(!trail.isOff){
					particle.Emit(10);
					trail.lockon=transform;
					Destroy(note.GetComponent<MeshRenderer>());
				}
			}else
			{
				print("hit");
				particle.Emit(10);
				Destroy(note);
			}
        }
        else
        {
            print("miss");
            Handheld.Vibrate();
        }

    }
	void OnTriggerEnter(Collider col)
	{
		if(note)Destroy(note);
		note=col.gameObject;
	}
	void OnTriggerExit(Collider col)
	{
		TrailPart trail = note.GetComponent<TrailPart>();
		if(trail)
		{
			trail.lockon=transform;
			trail.TurnOff();
			Destroy(note.GetComponent<MeshRenderer>());
		}
		else
			Destroy(note);
		note=null;
		print("miss");
        Handheld.Vibrate();

    }
}
