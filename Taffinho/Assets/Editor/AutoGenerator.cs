using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AutoGenerator {
    public AudioSource source;
    public float[] samples = new float[512];
    public float[] band = new float[9];
    public bool[] bools;
    public bool[] notes;
    private bool generating;
    public int id;

	/*[Range(7, 10)]
    public int bands;*/
	// Use this for initialization

	public bool[] Load(string s)
	{
		bool[] notes=new bool[s.Length*4];
		for(int i=0;i<s.Length;i++){
			int n=s[i];
			if(n<=58) i+=n-49;
			else
			{
				int j=i*4;
				if(n==79 || n==65 || n==69 || n==70 || n==71 || n==75 || n==76 || n==77) notes[j]=true;
				if(n==79 || n==66 || n==69 || n==72 || n==73 || n==75 || n==76 || n==78) notes[j+1]=true;
				if(n==79 || n==67 || n==70 || n==72 || n==74 || n==75 || n==77 || n==78) notes[j+2]=true;
				if(n==79 || n==68 || n==71 || n==73 || n==74 || n==76 || n==77 || n==78) notes[j+3]=true;
			}
		}
		return notes;
	}
	public void Start (AudioSource audio, int bands,bool[] note) {
        source = audio;
        band = new float[bands];
        bools = new bool[bands];
        notes = note;
        samples = new float[(int)Mathf.Pow(2, bands)];
        audio.time = 0;
        audio.Play();
        generating = true;
        id = 0;
	}
	
	// Update is called once per frame
	public void Update () {

        source.time = id / 4f * 0.25f;
        source.GetSpectrumData(samples, 0, FFTWindow.Blackman);
        
        int samplecount=0;
        for(int i = 0; i < band.Length; i++)
        {
            float bandavarage=0;
            int sampleband=(int)Mathf.Pow(2,i);
            if (i == band.Length - 1) sampleband++;
            for(int j = 0; j < sampleband; j++)
            {
                bandavarage += samples[samplecount] * ++samplecount;
            }
            bandavarage /= samplecount;
            band[i] = bandavarage;
        }
       
        for (int i = 0; i < band.Length - 1;i++)
        {
            bools[i] = (int)(band[i] * 100)>5;
        }
        notes[id] = bools[0] || bools[1]  && bools[2];
        notes[id+1] = !bools[1] && (bools[2] || bools[3]);
        notes[id+2] = !bools[2] && (bools[4] || bools[5]);
        notes[id+3] = bools[5] && bools[6] || bools[7];
        id += 4;
        generating = Progress() < 1 && id<notes.Length;
    }
    public void Cancel()
    {
        generating = false;
        source.Stop();
    }
    public bool Generating()
    {
        return generating;
    }
    public float Progress()
    {
        return (id / 4f * 0.25f)/ source.clip.length;
    }
}
