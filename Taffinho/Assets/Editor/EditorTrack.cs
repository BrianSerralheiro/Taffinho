using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class EditorTrack : EditorWindow
{
	#region variables
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
	private Vector2 anchor;
	private int anchorstate;

    private AutoGenerator generator;
    private bool prevQ;
	private bool prevW;
	private bool prevE;
	private bool prevR;
	Texture tex;
	#endregion
	#region Initialization
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
	}
	#endregion

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
		#region Song info
		EditorGUILayout.BeginHorizontal();
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
				songpreview=menu.song[id].preview;
				audio.clip=song;
				notes=generator.Load(menu.song[id].notes,song.length);
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.BeginHorizontal();
		song = (AudioClip)EditorGUILayout.ObjectField("Song", song, typeof(AudioClip), false);
		songpreview = (AudioClip)EditorGUILayout.ObjectField("Preview", songpreview, typeof(AudioClip), false);

		EditorGUILayout.EndHorizontal();
		#endregion
		if(audio.clip!=song && song){
			notes=new bool[Mathf.FloorToInt(song.length)*4*4];
		}
		audio.clip=song;
		if(song){
			if(notes!=null){
				GUI.DrawTextureWithTexCoords(new Rect(5,40,(int)(position.width*0.8),400),tex,new Rect(0,slider/2,1,1));
				GUILayout.Label((notes.Length/4).ToString());
				for(int i=Mathf.Max(0,Mathf.CeilToInt(slider*4));i<Mathf.Min(notes.Length/4,Mathf.CeilToInt(slider*4)+8);i++){
					for(int j=0;j<4;j++){
						notes[i*4+j]=GUI.Toggle(new Rect((int)(position.width*0.8)/8+(int)(position.width*0.8)/4*j,430+(slider*4*50)-50*i,60,25),notes[i*4+j],i+"-"+j);
					}
				}
				#region Buttons
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
					songpreview=menu.song[id].preview;
					audio.clip=song;
					notes=generator.Load(menu.song[id].notes,song.length);
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
			#endregion
			#region Adjustments
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
			#endregion
			EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();

				slider=audio.time;
				slider = GUILayout.VerticalSlider(slider,song.length-1, 0.0F);
				if(slider>=song.length-1){
					audio.Pause();
				}
				audio.time=slider;
			EditorGUILayout.EndHorizontal();
			#region Slider Comands
			/*---------------------------------------------------------------SKIP AREA-----------------------------------------------------------*/
			GUILayout.BeginArea(new Rect(10, 200, (int)(position.width/20), position.height/2));
			if(GUILayout.Button("TOP"))slider=song.length-2;
			if(GUILayout.Button("/\\\n/\\"))slider+=20f;
			if(GUILayout.RepeatButton("/\\"))slider+=0.1f;
			if(GUILayout.RepeatButton("\\/"))slider-=0.2f;
			if(GUILayout.Button("\\/\n\\/"))slider-=20f;
			if(GUILayout.Button("BOT"))slider=0f;
			if(GUILayout.Button((anchorstate==0?"[":anchorstate==1?"]":"X"))){
				if(anchorstate==0){
					anchor.x=slider;
					anchorstate=1;
				}
					else if(anchorstate==1){
						anchor.y=slider;
						anchorstate=2;
					}
						else anchorstate=0;
			}
			GUILayout.EndArea();

			GUILayout.EndArea();
			#endregion
			if(Event.current.type == EventType.ScrollWheel){
				slider+=Event.current.delta.y/10;
			}
			if(anchorstate==2)
			{
				if(anchor.x>anchor.y)
				{
					float a=anchor.x;
					anchor.x=anchor.y;
					anchor.y=a;
				}
				if(slider<anchor.x)slider=anchor.x;
				if(slider>anchor.y)slider=anchor.x;
			}
			if(slider<0)slider=0;
            if (slider > song.length-1) slider=song.length-1;
			audio.time=slider;
			#region KeyBoard Controller
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
			#endregion
		}
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

		return s;
	}
}