using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteButton : MonoBehaviour {
	public GameObject note;
    public ParticleSystem particle;
	public KeyCode key=KeyCode.Q;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(key)){
            if (note)
            {
                print("hit");
                particle.Emit(10);
                Destroy(note);
            }
            else
            {
                print("miss");
                Handheld.Vibrate();
            }
        }
    }
    public void OnMouseDown()
    {
        if (note)
        {
            print("hit");
            particle.Emit(10);
            Destroy(note);
        }
        else
        {
            print("miss");
            Handheld.Vibrate();
        }

    }
	void OnTriggerEnter(Collider col)
	{
		note=col.gameObject;
	}
	void OnTriggerExit(Collider col)
	{
		Destroy(note);
		note=null;
		print("miss");
        Handheld.Vibrate();

    }
}
