using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour 
{
	[SerializeField]
	public MusicInfo[] song;
    private string[] names;
	void Start () 
    {
		gameObject.SetActive(false);
	}
	public void Select(int i)
    {
		NotePosition.music = song[i];
		NotePosition.world = name;
		SceneManager.LoadScene(1);            
    }

    // Update is called once per frame
    public string[] Names()
    {
        //if(names==null || names.Length == 0)
        {
            names = new string[song.Length];
            for(int i = 0; i < song.Length; i++)
            {
                if (song[i].filename != "" && song[i].filename != null) names[i] = song[i].filename;
                else names[i] = "Vazio"+(i+1);
            }
        }
        return names;
    }
    void Update () 
    {
		
	}
}
