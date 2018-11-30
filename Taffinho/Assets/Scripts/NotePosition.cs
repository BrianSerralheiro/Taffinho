using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*GUIDE
 * 0... A 65
 * .0.. B 66
 * ..0. C 67
 * ...0 D 68
 * 00.. E 69
 * 0.0. F 70
 * 0..0 G 71
 * .00. H 72
 * .0.0 I 73
 * ..00 J 74
 * 000. K 75
 * 00.0 L 76
 * 0.00 M 77
 * .000 N 78
 * 0000 O 79
 */
public class NotePosition : MonoBehaviour {
	public string notes;
	public AudioClip song;
	public AudioSource source;
	public GameObject[] notas;
	public float timer;
	public float delay;
	public int house;
	public Text text;
	public float Zpos;
	[SerializeField]
	public bool[] pos;
    public static MusicInfo music;
	// Use this for initialization
	void Start () {
        /*for(int i=0;i<notes.Length;i++){
			int n=notes[i];
			if(n<=58)print("Time out"+(n-48));
			else {
				if(n==79 || n==65 || n==69 || n==70 || n==71 || n==75 || n==76 || n==77)Make(0);
				if(n==79 || n==66 || n==69 || n==72 || n==73 || n==75 || n==76 || n==78)Make(1);
				if(n==79 || n==67 || n==70 || n==72 || n==74 || n==75 || n==77 || n==78)Make(2);
				if(n==79 || n==68 || n==71 || n==73 || n==74 || n==76 || n==77 || n==78)Make(3);
			}
		}*/
        song =Resources.Load<AudioClip>("/Audioclips/"+music.filename);

		notes = music.notes;

		source.clip=song;
	}
	void Make(int i){
		GameObject go=Instantiate(notas[i])as GameObject;
		go.transform.parent=transform;
		go.transform.localPosition=new Vector3(-3+i*2,0,house+Zpos);
	}
	void Chose(int n){
		if(n<=58)Zpos+=n-49;
		else {
			if(n==79 || n==65 || n==69 || n==70 || n==71 || n==75 || n==76 || n==77)Make(0);
			if(n==79 || n==66 || n==69 || n==72 || n==73 || n==75 || n==76 || n==78)Make(1);
			if(n==79 || n==67 || n==70 || n==72 || n==74 || n==75 || n==77 || n==78)Make(2);
			if(n==79 || n==68 || n==71 || n==73 || n==74 || n==76 || n==77 || n==78)Make(3);
		}
	}
	public void Restart(){
		enabled=true;
		transform.position=Vector3.zero;
		for(int i=0;i<transform.childCount;i++){
			Destroy(transform.GetChild(i).gameObject);
		}
		timer=0;
		house=0;
		notes=text.text;
	}
    public void OnGUI()
    {
		//only fortesting, remove later
        if (GUI.Button(new Rect(0, 0, 100, 50), "back")) Application.LoadLevel(0);
    }
    void Update () {
		transform.Translate(-Vector3.forward*4*Time.deltaTime);
		timer+=Time.deltaTime;
        if (timer - delay > source.clip.length) Debug.Log("ENDOFSONG");
		if(!source.isPlaying && timer>delay && timer<source.clip.length)source.Play();
		if(notes.Length-1<house)return;
		if(house<timer*4)Chose(notes[house++]);
	}
}
