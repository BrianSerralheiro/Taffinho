using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

/// <summary>
/// Classe that contains music info
/// </summary>
/// <param name="filename">Name of the audio file</param>
/// <param name="songname">Name of the Song</param>
/// <param name="notes">Notes of the song</param>
/// <param name="preview">Short of the music</param>
/// <param name="stars">User score based on it's performance playing the song from 0 to 3</param>

public class MusicInfo {

	public string filename;
	public string songname;
	public string notes;
	public AudioClip preview;
	public int stars;

}
