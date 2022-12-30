
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
            songDebugText.text = ($"Song Beat {song.currentSongBeat}");
        }
    }

}
