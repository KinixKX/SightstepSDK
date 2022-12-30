
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class StageSelector : UdonSharpBehaviour
{
    //References to ALL modcharts and charts. Something like "ModChart1, SongChart1, ModChart2, SongChart2, etc etc" SORRY SORA THIS IS THE ONLY WAY I CAN THINK OF IT.
    //You have all the rights to send me into elden ring oblivion later on - Kinix

    public SongPlayer songPlayer;
    public ModChart defaultMods;
    public SongChart defaultCharts;

    public SongChart[] songCharts; //Slots for all custom charts
    public ModChart[] modCharts; //Slots for all custom mods

    [UdonSynced(UdonSyncMode.None)]
    public int s_stageIndex;

    public AudioClip stageAudio;
    public float stageOffset;
    public float stageBPM;
    public string stageDuration;

    public string stageSong;
    public string stageArtist;

    public string stageDescription;

    public string stageChartsBy;
    public string stageModsBy;


    public void Start()
    {
        PreloadStage(s_stageIndex);
    }

    public void PreloadStage(int stage)
    {

        SetDefaultActors(modCharts[stage]); //Set the actors
        SetStageData(stage);

        modCharts[stage].InitMods();
        songCharts[stage].InitCharts();
 
        songPlayer.SetSongProperties();

        if (songCharts[stage].simpleChart != null)
        {
            defaultCharts.simpleChart = songCharts[stage].simpleChart;
            defaultMods.xmodS = modCharts[stage].xmodS;

            if (modCharts[stage].simpleModChart != null)
            {
                defaultMods.simpleModChart = modCharts[stage].simpleModChart;
            }
            else
            {
                defaultMods.simpleModChart = new object[][] { };
            }
        }
        else
        {
            defaultCharts.simpleChart = new object[][] { };
        }

        if (songCharts[stage].normalChart != null)
        {
            defaultCharts.normalChart = songCharts[stage].normalChart;
            defaultMods.xmodN = modCharts[stage].xmodN;

            if (modCharts[stage].normalModChart != null)
            {
                defaultMods.normalModChart = modCharts[stage].normalModChart;
            }
            else
            {
                defaultMods.normalModChart = new object[][] { };
            }
        }
        else
        {
            defaultCharts.normalChart = new object[][] { };
        }

        if (songCharts[stage].hardChart != null)
        {
            defaultCharts.hardChart = songCharts[stage].hardChart;
            defaultMods.xmodH = modCharts[stage].xmodH;

            if (modCharts[stage].hardModChart != null)
            {
                defaultMods.hardModChart = modCharts[stage].hardModChart;
            }
            else
            {
                defaultMods.hardModChart = new object[][] { };
            }
        }
        else
        {
            defaultCharts.hardChart = new object[][] { };
        }

        if (songCharts[stage].unsightedChart != null)
        {
            defaultCharts.unsightedChart = songCharts[stage].unsightedChart;
            defaultMods.xmodU = modCharts[stage].xmodU;

            if (modCharts[stage].unsightedModChart != null)
            {
                defaultMods.unsightedModChart = modCharts[stage].unsightedModChart;
            }
            else
            {
                defaultMods.unsightedModChart = new object[][] { };
            }
        }
        else
        {
            defaultCharts.unsightedChart = new object[][] { };
        }

        Debug.Log("Preloaded stage " + stage);

    }

    public void SetStageData(int stage)
    {
        stageAudio = songCharts[stage].songFile;
        stageOffset = songCharts[stage].songOffset;
        stageBPM = songCharts[stage].bpm;
        stageDuration = songCharts[stage].duration;
        stageSong = songCharts[stage].songName;
        stageArtist = songCharts[stage].songArtist;
        stageDescription = songCharts[stage].description;
        stageChartsBy = songCharts[stage].chartsBy;
        stageModsBy = songCharts[stage].modsBy;

    }

    public void SetDefaultActors(ModChart currentModChart)
    {
        currentModChart.P1 = defaultMods.P1;
        currentModChart.P2 = defaultMods.P2;

        currentModChart.P1Combo = defaultMods.P1Combo;
        currentModChart.P2Combo = defaultMods.P2Combo;
        currentModChart.P3Combo = defaultMods.P3Combo;
        currentModChart.P4Combo = defaultMods.P4Combo;

        currentModChart.P1Judgement = defaultMods.P1Judgement;
        currentModChart.P2Judgement = defaultMods.P2Judgement;
        currentModChart.P3Judgement = defaultMods.P3Judgement;
        currentModChart.P4Judgement = defaultMods.P4Judgement;

        currentModChart.Lights = defaultMods.Lights;
        currentModChart.BG = defaultMods.BG;

        currentModChart._defaultPlayfieldPosition = defaultMods._defaultPlayfieldPosition;

    }

    public void ResetMods()
    {
        defaultMods.ResetMods();
        modCharts[s_stageIndex].ResetMods();
    }

}
