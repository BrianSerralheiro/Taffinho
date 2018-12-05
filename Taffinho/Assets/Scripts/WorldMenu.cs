using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMenu : MonoBehaviour 
{
    public SelectMenu[] Worlds;
    public bool selecting;
    public GUIStyle style;

    [SerializeField]
    GameObject WorldSelection;

	// Use this for initialization
	void Start () 
    {
		
	}

    void OnEnable()
    {
        WorldSelection.SetActive(true);
    }

    void OnDisable()
    {
        WorldSelection.SetActive(false);
    }

    public void WorldSelect(int id)
    {
        Worlds[id].gameObject.SetActive(true);
        //World is active, but object is disable.
        enabled = false;
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
    void Update () 
    {
		
	}
}
