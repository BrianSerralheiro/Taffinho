using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

/// <summary>
/// Class that contains music info
/// </summary>
/// <param name="filename">Name of the audio file</param>
/// <param name="songname">Name of the Song</param>
/// <param name="notes">Notes of the song</param>
/// <param name="preview">Short of the music</param>
/// <param name="stars">Integer that define the difficult of the music</param>
public class MusicInfo {

	public string filename;
	public string songname;
	public string notes;
	public AudioClip preview;
	public int stars;

}
