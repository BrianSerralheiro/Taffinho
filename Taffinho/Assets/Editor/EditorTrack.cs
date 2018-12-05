using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorTrack : EditorWindow
{
	public float slider;
	public AudioClip song;
	public AudioClip songpreview;
	public static NotePosition guitar;
	public SelectMenu menu;
	public int id;
	public GameObject go;
	public GameObject Guitargo;
	public AudioSource audio;
	private bool[] notes;
    private SelectMenu[] worlds;
    private string[] names;
    private string songname;
	private int index;

    private AutoGenerator generator;
    private bool prevQ;
	private bool prevW;
	private bool prevE;
	private bool prevR;
	Texture tex;
	[MenuItem("Window/Track Generator")]
	static void Init()
	{
		EditorTrack window = (EditorTrack)EditorWindow.GetWindow(typeof(EditorTrack));
		window.Show();
	}
	void OnEnable(){
        worlds=(SelectMenu[])Object.FindObjectsOfType(typeof(SelectMenu));
        names = new string[worlds.Length];
        for(int i = 0; i < worlds.Length; i++)
        {
            names[i] = worlds[i].name;
        }
		if(go)return;
		if(song)notes=new bool[Mathf.FloorToInt(song.length)*4*4];
		go=new GameObject("audiopreview",typeof(AudioSource));
		audio=go.GetComponent<AudioSource>();
		audio.playOnAwake=false;
		go.hideFlags = HideFlags.HideAndDontSave;
		tex  = (Texture)EditorGUIUtility.Load("guitar bg.png");
        generator = new AutoGenerator();
		//Guitargo=GameObject.Find("Guitar");
		//guitar=Guitargo.GetComponent<NotePosition>();
		//guitar.pos=notes;
	}
	void OnInspectorUpdate()
	{
		Repaint();
        if (generator.Generating())
        {
            generator.Update();
            notes = generator.notes;
        }

    }

	void OnGUI()
	{
        if (generator.Generating())
        {
            GUILayout.HorizontalSlider(generator.Progress(), 0, 1);
            GUILayout.Label(generator.Progress() * 100 + "%");
            if(GUILayout.Button("Cancel")) generator.Cancel();
            return;
        }
		//if(!guitar)guitar=Guitargo.GetComponent<NotePosition>();
		//Guitargo= (GameObject)EditorGUILayout.ObjectField("go",Guitargo, typeof(GameObject),true);
		EditorGUILayout.BeginHorizontal();
        //menu= (SelectMenu)EditorGUILayout.ObjectField("Menu",menu, typeof(SelectMenu),true);
        index = EditorGUILayout.Popup(index, names);
        menu = worlds[index];
        if (menu) id = EditorGUILayout.Popup(id, menu.Names());
		if(song!=null){
			songname=EditorGUILayout.TextField("Name:",songname);
		}else
		{
			if(GUILayout.Button("Load"))
			{
				song=Resources.Load<AudioClip>("Songs/"+menu.name+"/"+menu.song[id].filename);
				songname=menu.song[id].songname;
				//Debug.Log(menu.name);
				songpreview=menu.song[id].preview;
				audio.clip=song;
				notes=generator.Load(menu.song[id].notes);
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		song = (AudioClip)EditorGUILayout.ObjectField("Song", song, typeof(AudioClip), false);
		songpreview = (AudioClip)EditorGUILayout.ObjectField("Preview", songpreview, typeof(AudioClip), false);

		EditorGUILayout.EndHorizontal();
		/*GameObject g=null;
		if(guitar)g=guitar.gameObject;
		g=(GameObject)EditorGUILayout.ObjectField("Guitar",g, typeof(GameObject),true);
		if(!guitar && g)guitar=g.GetComponent<NotePosition>();*/
		if (audio.clip!=song && song){
			notes=new bool[Mathf.FloorToInt(song.length)*4*4];
			//Debug.Log("creating "+notes);
			//guitar.pos=notes;
		}
		//EditorGUI.DrawPreviewTexture(new Rect(0,20,300,200),AssetPreview.GetAssetPreview(song));
		audio.clip=song;
		if(song){
			/*EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(audio.clip.name);
			EditorGUILayout.LabelField(audio.clip.length+" seconds");

			EditorGUILayout.EndHorizontal();*/
		
			
			//GUILayout.BeginArea(new Rect(5, 40, (int)(position.width*0.8)-5, position.height-40));


			if(notes!=null){
				//GUILayout.Box(tex);
				GUI.DrawTextureWithTexCoords(new Rect(5,40,(int)(position.width*0.8),400),tex,new Rect(0,slider/2,1,1));
				GUILayout.Label((notes.Length/4).ToString());
				for(int i=Mathf.Max(0,Mathf.CeilToInt(slider*4));i<Mathf.Min(notes.Length/4,Mathf.CeilToInt(slider*4)+8);i++){
					for(int j=0;j<4;j++){
						notes[i*4+j]=GUI.Toggle(new Rect((int)(position.width*0.8)/8+(int)(position.width*0.8)/4*j,430+(slider*4*50)-50*i,60,25),notes[i*4+j],i+"-"+j);
					}
				}
				//GUI.BeginScrollView(new Rect(0, notes.Length/6*400, (int)(position.width*0.8), 100),new Vector2(0,0),new Rect(0,0,(int)(position.width*0.8), position.height-40),false,true);
				//GUI.BeginScrollView(new Rect(0, 40, (int)(position.width*0.8), position.height-40),new Vector2(0,(notes.Length/8*200)-(slider*4*100)),new Rect(0,0,(int)(position.width*0.8)-40, notes.Length/8*200));

					//GUILayout.BeginArea(new Rect(0, notes.Length/6*400, (int)(position.width*0.8), 100));
					//GUILayout.VerticalSlider(slider,song.length, 0.0F);
						//GUILayout.EndArea();

			
				//GUI.EndScrollView();
				/*----------------------------------------------------------BUTTONS AREA----------------------------------------------------*/
				GUILayout.BeginArea(new Rect((int)(position.width*0.8)+10, 100, (int)(position.width/10), 100));
				if(GUILayout.Button("Copy")) EditorGUIUtility.systemCopyBuffer=Generate();
                if (GUILayout.Button("Save")){
					menu.song[id].notes= Generate();
                    menu.song[id].filename=song.name;
                    menu.song[id].songname=songname;
					menu.song[id].preview=songpreview;
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
				if(GUILayout.Button("Load")){
					song=Resources.Load<AudioClip>("Songs/"+menu.name+"/"+menu.song[id].filename);
					songname=menu.song[id].songname;
					//Debug.Log(menu.name);
					songpreview=menu.song[id].preview;
					audio.clip=song;
					notes=generator.Load(menu.song[id].notes);
				}
                if (GUILayout.Button("Auto"))
                {
                    generator.Start(audio, 8, notes);
                    return;
                }
				if(GUILayout.Button("Test"))
                    {
                        /*MusicInfo info = new MusicInfo();
                        info.notes = Generate();
                        info.song = song;
                        EditorApplication.isPlaying = true;
                        EditorApplication.LoadLevelInPlayMode("scenes/cen");*/
                        Debug.Log("Button not working now");
                }
				GUILayout.EndArea();
			}
			//GUILayout.EndArea();
			/*----------------------------------------------------------------- OPTIONS AREA------------------------------------------------------*/
			GUILayout.BeginArea(new Rect((int)(position.width*0.8), 40, (int)(position.width/5), position.height-40));
			if(GUILayout.Button(audio.isPlaying?"Pause":"Play")){
				if(audio.isPlaying)audio.Pause();
				else audio.Play();
			}
			EditorGUILayout.BeginHorizontal();

				GUILayout.Label("Volume");
				audio.volume = GUILayout.HorizontalSlider(audio.volume,0, 1);
			EditorGUILayout.EndHorizontal();

			EditorGUILayout.BeginHorizontal();
				GUILayout.Label("Speed");
				audio.pitch = GUILayout.HorizontalSlider(audio.pitch,0, 1);
			EditorGUILayout.EndHorizontal();
			
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();

				slider=audio.time;
				slider = GUILayout.VerticalSlider(slider,song.length-1, 0.0F);
				if(slider>=song.length-1){
					//slider=0;
					audio.Pause();
				}
				audio.time=slider;
			EditorGUILayout.EndHorizontal();
			/*---------------------------------------------------------------SKIP AREA-----------------------------------------------------------*/
			GUILayout.BeginArea(new Rect(10, 200, (int)(position.width/20), position.height/2));
			if(GUILayout.Button("TOP"))slider=song.length-2;
			if(GUILayout.Button("/\\\n/\\"))slider+=20f;
			if(GUILayout.RepeatButton("/\\"))slider+=0.1f;
			if(GUILayout.RepeatButton("\\/"))slider-=0.2f;
			if(GUILayout.Button("\\/\n\\/"))slider-=20f;
			if(GUILayout.Button("BOT"))slider=0f;
			GUILayout.EndArea();

			GUILayout.EndArea();

			if(Event.current.type == EventType.ScrollWheel){
				slider+=Event.current.delta.y/10;
			}
			if(slider<0)slider=0;
            if (slider > song.length-1) slider=song.length-1;
			audio.time=slider;
			/*                                         END OPTIONS                                                     */
			if(Event.current.type == EventType.keyDown){
				int i=Mathf.Max(0,Mathf.CeilToInt(slider*4));
				if(Event.current.keyCode == KeyCode.Q && !prevQ){
					notes[i*4]=true;
					prevQ=true;
				}
				if(Event.current.keyCode == KeyCode.W && !prevW){
					notes[i*4+1]=true;
					prevW=true;
				}
				if(Event.current.keyCode == KeyCode.E && !prevE){
					notes[i*4+2]=true;
					prevE=true;
				}
				if(Event.current.keyCode == KeyCode.R && !prevR){
					notes[i*4+3]=true;
					prevR=true;
				}

			}
			if(Event.current.type == EventType.keyUp){
				if(Event.current.keyCode == KeyCode.Q && prevQ)
					prevQ=false;
				if(Event.current.keyCode == KeyCode.W && prevW)
					prevW=false;
				if(Event.current.keyCode == KeyCode.E && prevE)
					prevE=false;
				if(Event.current.keyCode == KeyCode.R && prevR)
					prevR=false;
				
			}
		}
		//Editor.DrawPreview(new Rect(0,0,100,100));
	}
	string Generate(){
		string s="";
		int spaces=0;
		for(int i=0;i<notes.Length/4;i++){
			if(notes[i*4] && !notes[i*4+1] && !notes[i*4+2] && !notes[i*4+3]){	s+="A";
				continue;}
			if(!notes[i*4] && notes[i*4+1] && !notes[i*4+2] && !notes[i*4+3]){	s+="B";
				continue;}
			if(!notes[i*4] && !notes[i*4+1] && notes[i*4+2] && !notes[i*4+3]){	s+="C";
			continue;}
			if(!notes[i*4] && !notes[i*4+1] && !notes[i*4+2] && notes[i*4+3]){	s+="D";
				continue;}
			if(notes[i*4] && notes[i*4+1] && !notes[i*4+2] && !notes[i*4+3]){	s+="E";
				continue;}
			if(notes[i*4] && !notes[i*4+1] && notes[i*4+2] && !notes[i*4+3]){	s+="F";
				continue;}
			if(notes[i*4] && !notes[i*4+1] && !notes[i*4+2] && notes[i*4+3]){	s+="G";
				continue;}
			if(!notes[i*4] && notes[i*4+1] && notes[i*4+2] && !notes[i*4+3]){	s+="H";
				continue;}
			if(!notes[i*4] && notes[i*4+1] && !notes[i*4+2] && notes[i*4+3]){	s+="I";
				continue;}
			if(!notes[i*4] && !notes[i*4+1] && notes[i*4+2] && notes[i*4+3]){	s+="J";
				continue;}
			if(notes[i*4] && notes[i*4+1] && notes[i*4+2] && !notes[i*4+3]){	s+="K";
				continue;}
			if(notes[i*4] && notes[i*4+1] && !notes[i*4+2] && notes[i*4+3]){	s+="L";
				continue;}
			if(notes[i*4] && !notes[i*4+1] && notes[i*4+2] && notes[i*4+3]){	s+="M";
				continue;}
			if(!notes[i*4] && notes[i*4+1] && notes[i*4+2] && notes[i*4+3]){	s+="N";
				continue;}
			if(notes[i*4] && notes[i*4+1] && notes[i*4+2] && notes[i*4+3]){		s+="O";
				continue;}


			if(!notes[i*4] && !notes[i*4+1] && !notes[i*4+2] && !notes[i*4+3])	spaces++;
			if(spaces==9 || i<notes.Length/4-2 && (notes[(i+1)*4] || notes[(i+1)*4+1] || notes[(i+1)*4+2] || notes[(i+1)*4+3])){
				s+=spaces;
				spaces=0;
			}

				
		}
		for(int i=s.Length-1;i>0;i--){
			if(char.IsLetter(s[i]))break;
			s=s.Substring(0,i);
		}

		//Debug.Log("gerado"+s);

		return s;
	}
}