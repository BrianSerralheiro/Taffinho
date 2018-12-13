using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Guide
/// </summary>
/// <param id"A 65">0 - - -</param>
/// <param id"B 66">- 0 - -</param>
/// <param id"C 67">- - 0 -</param>
/// <param id"D 68">- - - 0</param>
/// <param id"E 69">0 0 - -</param>
/// <param id"F 70">0 - 0 -</param>
/// <param id"G 71">0 - - 0</param>
/// <param id"H 72">- 0 0 -</param>
/// <param id"I 73">- 0 - 0</param>
/// <param id"J 74">- - 0 0</param>
/// <param id"K 75">0 0 0 -</param>
/// <param id"L 76">0 0 - 0</param>
/// <param id"M 77">0 - 0 0</param>
/// <param id"N 78">- 0 0 0</param>
/// <param id"O 79">0 0 0 0</param>
 
public class NotePosition : MonoBehaviour {
	/// <summary>
	/// Note Position Class
	/// Creates and determine the notes position of the song
	/// </summary>
	/// <param id"notes">String that will cointains the notes, according to the Guide</param>
	/// <param id"song">Auduiclip that contains the song</param>
	/// <param id"source">AudioSource object that will play the song</param>
	/// <param id"notas"></param>
	/// <param id"prevGo"></param>
	/// <param id"timer"></param>
	/// <param id"delay"></param>
	/// <param id"house"></param>
	/// <param id"zPos"></param>
	/// <param id"prevNote"></param>
	/// <param id"mat"></param>
	/// <param id"music"></param>
	/// <param id"world"></param>
	public string notes;
	public AudioClip song;
	public AudioSource source;
	public GameObject[] notas;
	public GameObject[] prevGO;
	public float timer;
	public float delay;
	public int house;
	public float zPos;
	public int prevNote;
	public Material mat;
    public static MusicInfo music;
    public static string world;

	void Start () {
		song =Resources.Load<AudioClip>("Songs/"+world+"/"+music.filename);
		notes = music.notes;
		source.clip=song;
		prevGO=new GameObject[4];
	}

	void Make(int i,int n)
	{
		if(prevNote==n)
		{
			if(prevGO[i]){
				TrailPart trail= prevGO[i].GetComponent<TrailPart>();
				if(trail)trail.end=new Vector3(-3+i*2,0,house+zPos);
				else{
					trail=prevGO[i].AddComponent<TrailPart>();
					trail.end=new Vector3(-3+i*2,0,house+zPos);
					trail.mat=mat;
				}
			}
		}
		else{
			GameObject go=Instantiate(notas[i])as GameObject;
			go.transform.parent=transform;
			go.transform.localPosition=new Vector3(-3+i*2,0,house+zPos);
			prevGO[i]=go;
		}

	}

	void Chose(int n){
		if(n<=58)zPos+=n-49;
		else {
			if(n==79 || n==65 || n==69 || n==70 || n==71 || n==75 || n==76 || n==77)Make(0,n);
			if(n==79 || n==66 || n==69 || n==72 || n==73 || n==75 || n==76 || n==78)Make(1,n);
			if(n==79 || n==67 || n==70 || n==72 || n==74 || n==75 || n==77 || n==78)Make(2,n);
			if(n==79 || n==68 || n==71 || n==73 || n==74 || n==76 || n==77 || n==78)Make(3,n);
			prevNote=n;
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
	}

    public void OnGUI(){
		//only fortesting, remove later
        if (GUI.Button(new Rect(0, 0, 100, 50), "back")) {
			// Application.LoadLevel(0);
			CanvasController.defaultMenu=1;
			SceneManager.LoadScene(0);
		}
    }

    void Update () {
		transform.Translate(-Vector3.forward*4*Time.deltaTime);
		timer+=Time.deltaTime;
        if (timer - delay > source.clip.length) Debug.Log("ENDOFSONG");
		if (!source.isPlaying && timer>delay && timer<source.clip.length)source.Play();
		if (notes.Length-1<house)return;
		if (house<timer*4)Chose(notes[house++]);
	}
}
