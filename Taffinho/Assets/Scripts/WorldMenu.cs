using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMenu : MonoBehaviour {
    public SelectMenu[] Worlds;
    public bool selecting;
    public GUIStyle style;
	// Use this for initialization
	void Start () {
		
	}
    private void OnGUI()
    {
        int x = Screen.width / 10;
        int y = Screen.height / 10;
        if (selecting)
        {
            if (GUI.Button(new Rect(x, y, x, y), "Back"))
            {
                selecting = false;
                for (int i = 0; i < Worlds.Length; i++)
                {
                    Worlds[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            GUI.Box(new Rect(x*3, y, x*4, y*2), "Chose World");
            for (int i = 0; i < Worlds.Length;i++)
            {
                if (GUI.Button(new Rect(x*3, y*3 + i * y*2, x*4, y*2), Worlds[i].name, style))
                {
                    selecting = true;
                    Worlds[i].gameObject.SetActive(true);
                }
            }
        }
    }
    public void Select(int i)
    {
        for(int j = 0; j < Worlds.Length; j++)
        {
            if (Worlds[j].gameObject.activeSelf)
            {
                NotePosition.music = Worlds[j].song[i];
                SceneManager.LoadScene(1);
                return;
            }
        }   
    }
    // Update is called once per frame
    void Update () {
		
	}
}
