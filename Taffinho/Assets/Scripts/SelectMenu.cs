using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectMenu : MonoBehaviour {
	[SerializeField]
	public MusicInfo[] song;
    private string[] names;
    // Use this for initialization
    public GUIStyle style;
	void Start () {
        gameObject.SetActive(false);
	}
    private void OnGU()
    {
        int x = Screen.width / 10;
        int y = Screen.height / 10;
        for (int i = 0; i < song.Length; i++)
        {
            if (GUI.Button(new Rect(x * 3, y + i * y * 2, x * 4, y * 2), song[i].name, style))
            {
                NotePosition.music = song[i];
                SceneManager.LoadScene(1);
            }
        }

    }
    // Update is called once per frame
    public string[] Names()
    {
        //if(names==null || names.Length == 0)
        {
            names = new string[song.Length];
            for(int i = 0; i < song.Length; i++)
            {
                if (song[i].name != "" && song[i].name != null) names[i] = song[i].name;
                else names[i] = "Vazio"+(i+1);
            }
        }
        return names;
    }
    void Update () {
		
	}
}
