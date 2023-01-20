using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class SongChart : UdonSharpBehaviour
{
    [HideInInspector]
[VRC.Udon.Serialization.OdinSerializer.OdinSerialize] 
    public object[][] notes;
    [HideInInspector]
[VRC.Udon.Serialization.OdinSerializer.OdinSerialize] 
    public object[][] simpleChart;
    [HideInInspector]
[VRC.Udon.Serialization.OdinSerializer.OdinSerialize] 
    public object[][] normalChart;
    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] 
    public object[][] hardChart;
    [HideInInspector]
[VRC.Udon.Serialization.OdinSerializer.OdinSerialize] 
    public object[][] unsightedChart;

    public float songOffset;
    public float bpm;
    public AudioClip songFile;
    public int[] chartLevel = new int[4];
    public int[] modsLevel = new int[4];
    public int[] finalLevel = new int[4];
    public int defaultDiff = -1;

    public string stageName;

    public string description; //Describe your song any anything else around it, such as CHART HARD, but MODS EASY or EASY CHART, HARD MODS you name it

    public string songName;
    public string songArtist;
    public string duration;
    public string modsBy;
    public string chartsBy;

    public Sprite icon;

    public virtual void InitCharts() { }


}

