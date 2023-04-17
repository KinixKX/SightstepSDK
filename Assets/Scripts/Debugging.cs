
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class Debugging : UdonSharpBehaviour
{
    public GameObject panel;
    public SongPlayer song;
    public Text songDebugText;
    public Text scoreDebugText;

    public bool debuggerDisplay = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            debuggerDisplay = !debuggerDisplay;
            panel.SetActive(debuggerDisplay);
            
        }

        if (song.songPlaying)
        {
            scoreDebugText.text = song.accuracy.ToString("F2");
            songDebugText.text = ($"Song Beat {song.currentSongBeat}");
        }
    }

}
