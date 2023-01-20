
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CybersWorldCharts : SongChart
{

    //117bpm Cybers world
    //0.059f CYBERS WORLD

    // Your .sm/.ssc goes here. CTRL+A, CTRL+C, CTRL+V. (Select all, copy+paste.)
    #region SM FILE HERE

    private string CHART_FILE = @"#TITLE:A CYBERS WORLD;
#SUBTITLE:Deltarune Ch. 2 ;
#ARTIST:Toby Fox;
#TITLETRANSLIT:;
#SUBTITLETRANSLIT:;
#ARTISTTRANSLIT:;
#GENRE:;
#CREDIT:Kinix, HeySora, Xeirla;
#MUSIC:A_CYBERS_WORLD--cut.mp3;
#BANNER:bn.png;
#BACKGROUND:bg.png;
#CDTITLE:;
#SAMPLESTART:147.747;
#SAMPLELENGTH:20.513;
#SELECTABLE:YES;
#OFFSET:-0.070;
#BPMS:0.000=117.000;
#STOPS:;
#BGCHANGES:;
#FGCHANGES:;
//--------------- dance-single - Kinix ----------------
#NOTES:
     dance-single:
     Kinix:
     Easy:
     2:
     0,0,0,0,0:
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
1000
0001
0100
0000
,
0001
0100
0010
0000
,
1000
0010
0100
0000
,
0001
1000
0100
0000
,
1001
0001
1000
0010
,
0100
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0001
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0010
0010
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0100
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
,
1000
0001
1000
0001
,
0010
0100
0010
0100
0010
0000
0001
0000
,
1000
0010
0100
0001
,
0100
0010
1000
0001
,
1000
0010
1000
0001
,
0100
0010
0100
0010
0100
0000
0001
0000
,
0000
1000
0000
1000
0000
0010
0000
1000
,
0000
1000
0000
1000
0000
0100
0000
0010
,
0000
0010
0000
0010
0000
0100
0000
0010
,
0000
0000
0000
0000
0000
0000
0000
0010
,
0000
0010
0000
0010
0000
0100
0001
0010
,
0000
0010
0000
0010
0000
0100
1000
0010
,
0000
0010
0000
0010
0000
1000
0100
0010
,
0000
0001
0100
0010
1000
0001
0010
0001
,
0000
0100
0000
0100
0000
0010
1000
0100
,
0000
0100
0000
0100
0000
1000
0010
0100
,
0000
0100
0000
0100
0000
0001
0010
0100
,
0000
0010
0001
0010
0100
1000
0100
0010
,
0000
0010
0000
0010
0000
0001
1000
0100
,
0000
0100
0000
0100
0000
1000
0100
0010
,
0000
0010
0000
0010
0000
0100
1000
0001
,
0000
0001
0100
0001
1000
0100
0010
0000
,
0001
0001
0001
0001
,
0100
0100
0010
0010
,
1000
1000
0100
1000
,
0100
0000
0010
0000
0001
0100
0001
0000
,
1000
1000
1000
1000
,
0010
0010
0100
0100
,
0001
0010
0100
1000
,
0100
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0010
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0001
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
1000
1000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
0000
,
0001
0100
0001
0100
0001
0100
0000
0010
,
0000
1000
0100
1000
0100
1000
0000
0000
,
1000
0010
1000
0010
1000
0010
0000
0100
,
0000
0001
0100
0001
0100
0001
0010
0100
,
1000
0100
1000
0100
1000
0100
0000
0010
,
0000
0001
0100
0001
0100
0001
0000
0000
,
0001
0010
0001
0010
0001
0010
0000
0100
,
0000
1000
0010
1000
0100
0010
0001
1000
,
0001
0000
0001
0000
0001
0000
0001
1000
,
0000
0000
0001
0000
1000
0010
0100
0010
,
1000
0000
1000
0000
1000
0000
1000
0001
,
0000
0000
1000
0000
0001
1000
0001
0000
,
0000
0101
0000
1010
,
0000
1100
0000
0011
,
0000
0101
0000
1100
,
0000
1010
0000
0011
,
0001
0000
0000
0000
0000
0000
0000
0010
,
0000
0000
0100
0000
1000
0010
1000
0010
,
0000
0000
0000
0000
0000
0000
0000
0100
,
0000
0000
0001
0000
1000
0100
1000
0000
,
0100
0000
0000
0000
0000
0000
0000
0010
,
0000
0000
0100
0000
1000
0010
1000
0010
,
0000
0000
0000
0000
0000
0000
0000
1000
,
0000
0000
0001
0000
0010
0100
1000
0000
,
1001
0000
1000
0000
0001
1000
0010
0000
,
1000
0000
0001
0000
1000
0001
0010
0000
,
0001
0000
1000
0000
0001
0010
0100
0000
,
1000
0001
1000
0100
0010
0100
1000
0000
,
0100
0000
0010
0000
0001
1000
0001
0000
,
0100
0000
0010
0000
1000
0001
1000
0000
,
0100
0000
0010
0000
0001
0010
0001
0000
,
1000
0000
0000
0010
0000
0000
1000
0000
0000
0100
0000
0000
0010
0000
0001
0010
0000
0000
0100
0000
0000
1000
0000
0000
;
//--------------- dance-single - heysora normal ----------------
#NOTES:
     dance-single:
     heysora normal:
     Hard:
     5:
     0,0,0,0,0:
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
1000
0010
0100
0001
1000
1000
0000
0000
,
0001
0100
0010
1000
0001
0001
0000
0000
,
1000
0010
0100
0010
1000
1000
0000
0000
,
0001
0000
0000
0100
0000
0000
0001
0000
0000
0010
0000
0000
0100
0000
1000
0100
0000
0000
0010
0000
0000
0000
0000
0000
,
1001
0000
0001
0000
1000
0001
0010
0000
,
0001
0000
1000
0000
0001
1000
0010
0000
,
1000
0000
0001
0000
1000
0010
0100
0000
,
0001
1000
0001
0100
0010
0100
0001
0000
,
0100
0000
0010
0000
1000
0001
1000
0000
,
0100
0000
0010
0000
0001
1000
0001
0000
,
0100
0000
0010
0000
1000
0010
1000
0000
,
0001
0000
0000
0010
0000
0000
0001
0000
0000
0100
0000
0000
0010
0000
1000
0010
0000
0000
0100
0000
0000
0000
0000
0000
,
0000
0001
0000
0001
0000
0100
0010
1000
,
0000
1000
0000
1000
0000
0100
0010
0001
,
0000
0001
0000
0001
0000
1000
0100
0010
,
0000
0001
1000
0010
0100
0001
0100
0010
,
0000
0010
0000
0010
0000
0100
0001
0010
,
0000
0010
0000
0010
0000
0100
1000
0010
,
0000
0010
0000
0010
0000
1000
0100
0010
,
0000
0001
0100
0010
1000
0001
0010
0001
,
0000
0100
0000
0100
0000
0010
1000
0100
,
0000
0100
0000
0100
0000
1000
0010
0100
,
0000
0100
0000
0100
0000
0001
0010
0100
,
0000
0010
0001
0010
0100
1000
0100
0010
,
0000
0010
0000
0010
0000
0001
1000
0100
,
0000
0100
0000
0100
0000
1000
0100
0010
,
0000
0010
0000
0010
0000
0100
1000
0001
,
0000
0001
0100
0001
1000
0100
0010
0000
,
0001
0001
0001
0001
,
0100
0100
0010
0010
,
1000
1000
0100
1000
,
0100
0000
0010
0000
0001
0100
0001
0000
,
1000
1000
1000
1000
,
0010
0010
0100
0100
,
0001
0010
0100
1000
,
0100
0010
0001
1000
,
0001
0100
0001
0100
0001
0100
0000
0010
,
0000
1000
0100
1000
0100
1000
0000
0000
,
1000
0010
1000
0010
1000
0010
0000
0100
,
0000
0001
0100
0001
0100
0001
0010
0100
,
1000
0100
1000
0100
1000
0100
0000
0010
,
0000
0001
0100
0001
0100
0001
0000
0000
,
0001
0010
0001
0010
0001
0010
0000
0100
,
0000
1000
0010
1000
0100
0010
0001
1000
,
0001
0000
0001
0000
0001
0000
0001
1000
,
0000
0000
0001
0000
1000
0010
0100
0010
,
1000
0000
1000
0000
1000
0000
1000
0001
,
0000
1000
0001
0001
,
0000
0101
0000
1010
,
0000
1100
0000
0011
,
0000
0101
0000
1100
,
0000
1010
0000
0011
,
0001
0000
0000
0000
0000
0000
0000
0010
,
0000
0000
0100
0000
1000
0010
1000
0010
,
0000
0000
0000
0000
0000
0000
0000
0100
,
0000
0000
0001
0000
1000
0100
1000
0000
,
0100
0000
0000
0000
0000
0000
0000
0010
,
0000
0000
0100
0000
1000
0010
1000
0010
,
0000
0000
0000
0000
0000
0000
0000
1000
,
0000
0000
0001
0000
0010
0100
1000
0000
,
1001
0000
1000
0000
0001
1000
0010
0000
,
1000
0000
0001
0000
1000
0001
0010
0000
,
0001
0000
1000
0000
0001
0010
0100
0000
,
1000
0001
1000
0100
0010
0100
1000
0000
,
0100
0000
0010
0000
0001
1000
0001
0000
,
0100
0000
0010
0000
1000
0001
1000
0000
,
0100
0000
0010
0000
0001
0010
0001
0000
,
1000
0000
0000
0010
0000
0000
1000
0000
0000
0100
0000
0000
0010
0000
0001
0010
0000
0000
0100
0000
0000
1000
0000
0000
;
//--------------- dance-single - heysora slumpo ----------------
#NOTES:
     dance-single:
     heysora slumpo:
     Challenge:
     7:
     0,0,0,0,0:
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
0000
0000
0000
0000
,
1000
0000
0000
0100
0000
0000
0010
0000
0000
0001
0000
0000
1000
0000
0100
1000
0000
0000
0000
0000
0000
0000
0000
0000
,
0001
0000
0000
0010
0000
0000
0100
0000
0000
1000
0000
0000
0001
0000
0010
0001
0000
0000
0000
0000
0000
0000
0000
0000
,
1000
0000
0000
0010
0000
0000
0100
0000
0000
0010
0000
0000
1000
0000
0001
1000
0000
0000
0000
0000
0000
0000
0000
0000
,
0001
0000
0000
0010
0000
0000
0001
0000
0000
0100
0000
0000
0010
0000
1000
0010
0000
0000
0100
0000
0000
0001
0000
0000
,
1001
0000
0000
1000
0000
0000
0001
0000
0000
0001
0000
0000
1000
0000
0100
1000
0000
0000
0001
0000
1000
0100
0000
1000
,
0010
0000
0000
0010
0000
0000
0100
0000
0000
0100
0000
0000
0001
0000
0010
0001
0000
0000
1000
0000
0001
0010
0000
0001
,
0100
0000
0000
0100
0000
0000
0010
0000
0000
0010
0000
0000
1000
0000
0100
0010
0000
0000
0001
0000
0010
0100
0000
1000
,
0001
0000
0000
0100
0000
0000
0010
0000
0000
1000
0000
0000
0100
0000
0010
0100
0000
0000
1000
0000
0000
0001
0000
0000
,
0101
0000
0000
0001
0000
0000
0100
0000
0000
0100
0000
0000
0001
0000
1000
0100
0000
0000
0010
0000
0100
1000
0000
0100
,
0010
0000
0000
0010
0000
0000
0100
0000
0000
0100
0000
0000
1000
0000
0010
0100
0000
0000
0001
0000
0100
0010
0000
0100
,
0001
0000
0000
0001
0000
0000
0010
0000
0000
0010
0000
0000
0100
0000
1000
0010
0000
0000
0100
0000
0010
1000
0000
0001
,
1000
0000
0000
0100
0000
0000
0010
0000
0000
0001
0000
0000
0100
0000
0010
0100
0000
0000
0001
0000
0000
1000
0000
0000
,
1001
0001
0000
0001
0000
0010
0100
1000
,
0000
1000
0000
1000
0000
0010
0100
0001
,
0000
0001
0000
0001
0000
1000
0010
0100
,
0000
0001
1000
0100
0010
0001
0010
0100
,
0000
0100
0000
0100
0000
0010
0001
0100
,
0000
0100
0000
0100
0000
0010
0100
1000
,
0000
1000
0000
1000
0000
0100
0010
1000
,
0000
0001
0010
0100
1000
0001
0100
0001
,
0000
0010
0100
0010
0000
0100
1000
0100
,
0000
0010
0001
0010
0100
1000
0100
0010
,
0000
0001
0100
0001
0000
1000
0010
1000
,
0000
0100
0010
0001
0100
0010
1000
0100
,
0000
0010
0100
1000
0000
0001
0100
0010
,
0000
1000
0100
0010
0001
0010
0100
1000
,
0000
0001
0100
0010
0000
1000
0100
0010
,
0000
0001
0010
0001
1000
0010
0100
0000
,
0001
0001
0001
0001
,
0010
0010
0100
0100
,
1000
1000
0010
1000
,
0010
0000
0100
0000
0001
0010
0001
0000
,
1000
1000
1000
1000
,
0100
0100
0010
0010
,
0001
0100
0010
1000
,
0100
0010
0001
1000
,
0001
0100
0001
1000
0001
0010
0000
0100
,
0000
0100
1000
0100
0010
0001
0000
0000
,
1000
0100
1000
0001
1000
0010
0000
0001
,
0000
0010
0100
0001
1000
0100
0010
0001
,
1000
0010
1000
0001
1000
0100
0000
0010
,
0000
0010
0001
0100
0010
1000
0000
0000
,
0001
0010
0001
1000
0001
0100
0000
0010
,
0000
1000
0100
1000
0010
0100
0001
1000
,
0001
0000
0001
0000
0001
0000
0001
1000
,
0000
0000
0101
0000
1000
0100
0010
0100
,
1000
0000
1000
0000
1000
0000
1000
0001
,
0000
0000
1010
0000
0001
1000
0001
0000
,
1000
0011
0001
1010
,
1000
0101
0001
1100
,
0010
0101
0010
1100
,
0010
0101
0010
1100
,
0001
0000
0000
0000
0000
0000
0000
0100
,
0000
0000
0010
0000
1000
0001
0100
0010
,
0000
0000
0000
0000
0000
0000
0000
0100
,
0000
0000
0001
0000
1000
0010
1000
0000
,
0100
0000
0000
0000
0000
0000
0000
0010
,
0000
0000
0100
0000
1000
0001
0010
0100
,
0000
0000
0000
0000
0000
0000
0000
1000
,
0000
0000
0001
0000
0100
0010
1000
0000
,
1001
0000
0000
0001
0000
0000
1000
0000
0000
1000
0000
0000
0001
0000
0100
0001
0000
0000
1000
0000
0001
0100
0000
0001
,
0010
0000
0000
0010
0000
0000
0100
0000
0000
0100
0000
0000
1000
0000
0010
1000
0000
0000
0001
0000
1000
0010
0000
1000
,
0100
0000
0000
0100
0000
0000
0010
0000
0000
0010
0000
0000
0001
0000
0100
0010
0000
0000
1000
0000
0010
0100
0000
0001
,
1000
0000
0000
0100
0000
0000
0010
0000
0000
0001
0000
0000
0100
0000
0010
0100
0000
0000
0001
0000
0000
1000
0000
0000
,
1100
0000
0000
1000
0000
0000
0100
0000
0000
0100
0000
0000
1000
0000
0001
0100
0000
0000
0010
0000
0100
0001
0000
0100
,
0010
0000
0000
0010
0000
0000
0100
0000
0000
0100
0000
0000
0001
0000
0010
0100
0000
0000
1000
0000
0100
0010
0000
0100
,
1000
0000
0000
1000
0000
0000
0010
0000
0000
0010
0000
0000
0100
0000
0001
0010
0000
0000
0100
0000
0010
0001
0000
1000
,
0001
0000
0000
0100
0000
0000
0010
0000
0000
1000
0000
0000
0100
0000
0010
0100
0000
0000
1000
0000
0000
0001
0000
0000
;
  ";

    #endregion

    public override void InitCharts()
    {
        LoadChartFile();
        SetMetadataTags();
    }

    private void SetMetadataTags()
    {
        // Values being set by ChartLoader
        // songOffset = 0.000f;
        // bpm = 120.000f;
        //
        // Simple Difficulty
        // difficultyLevel[0] = 0;
        // Normal Difficulty
        // difficultyLevel[1] = 0;
        // Hard Difficulty
        // difficultyLevel[2] = 0;
        // Unsighted Difficulty
        // difficultyLevel[3] = 0;
        // songName = "Song Name";
        // songArtist = "Song Artist";
        // chartsBy = "Chart Author";

        // Values that have to be set in the inspector
        // songFile
        // icon

        // Edit these! Can be in the inspector or here
        //stageName = $@"My cool stage name";
        //description = $@"My cool stage description";
        //duration = "2:00"; // 2 minutes
        //modsBy = $@"Modder Name";
        //defaultDiff = 1; // Difficulty Index, 0 for simple, 1 for normal, etc

        description =
       ($@"This is the first modchart done for Sightstep. 

It features a variety of charts, where the enviroments are very motion friendly with a simple straight road.
The actual mod effects range from easy and simple to very over the top with lots of movement.
        ");
    }

    private void LoadChartFile()
    {
        // don't need to initialize it twice
        if (CHART_FILE == null)
            return;

        ChartLoader.Load(CHART_FILE, this);

        // done loading, can toss the .sm string away since we don't need to load it again
        CHART_FILE = null;
    }

    /* OLD WAY TO DO CHARTS. THIS IS DEPRECATED
     * 
    public void LoadSimpleChart()
    {
        simpleChart = new object[][]
               {
            new object[4]{ 0, 16.0f, false, 0.0f },
            new object[4]{ 1, 17.0f, false, 0.0f },
            new object[4]{ 2, 18.0f, false, 0.0f },
            new object[4]{ 3, 20.0f, false, 0.0f },
            new object[4]{ 2, 21.0f, false, 0.0f },
            new object[4]{ 1, 22.0f, false, 0.0f },
            new object[4]{ 0, 24.0f, false, 0.0f },
            new object[4]{ 2, 25.0f, false, 0.0f },
            new object[4]{ 1, 26.0f, false, 0.0f },
            new object[4]{ 3, 28.0f, false, 0.0f },
            new object[4]{ 1, 29.0f, false, 0.0f },
            new object[4]{ 2, 30.0f, false, 0.0f },
            new object[4]{ 0, 32.0f, false, 0.0f },
            new object[4]{ 3, 32.0f, false, 0.0f },
            new object[4]{ 0, 34.0f, false, 0.0f },
            new object[4]{ 1, 35.0f, false, 0.0f },
            new object[4]{ 2, 36.0f, false, 0.0f },
            new object[4]{ 3, 38.0f, false, 0.0f },
            new object[4]{ 1, 39.0f, false, 0.0f },
            new object[4]{ 2, 40.0f, false, 0.0f },
            new object[4]{ 0, 42.0f, false, 0.0f },
            new object[4]{ 2, 43.0f, false, 0.0f },
            new object[4]{ 1, 44.0f, false, 0.0f },
            new object[4]{ 3, 45.0f, false, 0.0f },
            new object[4]{ 2, 46.0f, false, 0.0f },
            new object[4]{ 1, 47.0f, false, 0.0f },
            new object[4]{ 0, 48.0f, false, 0.0f },
            new object[4]{ 3, 50.0f, false, 0.0f },
            new object[4]{ 2, 51.0f, false, 0.0f },
            new object[4]{ 1, 52.0f, false, 0.0f },
            new object[4]{ 0, 54.0f, false, 0.0f },
            new object[4]{ 2, 55.0f, false, 0.0f },
            new object[4]{ 1, 56.0f, false, 0.0f },
            new object[4]{ 3, 58.0f, false, 0.0f },
            new object[4]{ 1, 59.0f, false, 0.0f },
            new object[4]{ 2, 60.0f, false, 0.0f },
            new object[4]{ 0, 61.0f, false, 0.0f },
            new object[4]{ 1, 62.0f, false, 0.0f },
            new object[4]{ 2, 63.0f, false, 0.0f },
            new object[4]{ 0, 64.0f, false, 0.0f },
            new object[4]{ 3, 64.0f, false, 0.0f },
            new object[4]{ 0, 66.5f, false, 0.0f },
            new object[4]{ 0, 67.5f, false, 0.0f },
            new object[4]{ 3, 70.5f, false, 0.0f },
            new object[4]{ 3, 71.5f, false, 0.0f },
            new object[4]{ 1, 74.5f, false, 0.0f },
            new object[4]{ 1, 75.5f, false, 0.0f },
            new object[4]{ 2, 76.5f, false, 0.0f },
            new object[4]{ 2, 77.5f, false, 0.0f },
            new object[4]{ 1, 78.5f, false, 0.0f },
            new object[4]{ 1, 79.5f, false, 0.0f },
            new object[4]{ 3, 82.5f, false, 0.0f },
            new object[4]{ 3, 83.5f, false, 0.0f },
            new object[4]{ 0, 86.5f, false, 0.0f },
            new object[4]{ 0, 87.5f, false, 0.0f },
            new object[4]{ 1, 90.5f, false, 0.0f },
            new object[4]{ 1, 91.5f, false, 0.0f },
            new object[4]{ 2, 92.5f, false, 0.0f },
            new object[4]{ 2, 93.5f, false, 0.0f },
            new object[4]{ 3, 94.5f, false, 0.0f },
            new object[4]{ 0, 95.5f, false, 0.0f },
            new object[4]{ 1, 96.5f, false, 0.0f },
            new object[4]{ 1, 97.5f, false, 0.0f },
            new object[4]{ 2, 98.5f, false, 0.0f },
            new object[4]{ 2, 99.5f, false, 0.0f },
            new object[4]{ 1, 100.5f, false, 0.0f },
            new object[4]{ 1, 101.5f, false, 0.0f },
            new object[4]{ 0, 102.5f, false, 0.0f },
            new object[4]{ 0, 103.5f, false, 0.0f },
            new object[4]{ 2, 104.5f, false, 0.0f },
            new object[4]{ 2, 105.5f, false, 0.0f },
            new object[4]{ 1, 106.5f, false, 0.0f },
            new object[4]{ 1, 107.5f, false, 0.0f },
            new object[4]{ 3, 108.5f, false, 0.0f },
            new object[4]{ 3, 109.5f, false, 0.0f },
            new object[4]{ 0, 110.5f, false, 0.0f },
            new object[4]{ 0, 111.5f, false, 0.0f },
            new object[4]{ 1, 112.5f, false, 0.0f },
            new object[4]{ 1, 113.5f, false, 0.0f },
            new object[4]{ 2, 114.5f, false, 0.0f },
            new object[4]{ 2, 115.5f, false, 0.0f },
            new object[4]{ 3, 116.5f, false, 0.0f },
            new object[4]{ 3, 117.5f, false, 0.0f },
            new object[4]{ 1, 118.5f, false, 0.0f },
            new object[4]{ 1, 119.5f, false, 0.0f },
            new object[4]{ 3, 120.5f, false, 0.0f },
            new object[4]{ 3, 121.5f, false, 0.0f },
            new object[4]{ 2, 122.5f, false, 0.0f },
            new object[4]{ 2, 123.5f, false, 0.0f },
            new object[4]{ 1, 124.5f, false, 0.0f },
            new object[4]{ 1, 125.5f, false, 0.0f },
            new object[4]{ 0, 126.5f, false, 0.0f },
            new object[4]{ 0, 127.5f, false, 0.0f },
            new object[4]{ 3, 128.0f, false, 0.0f },
            new object[4]{ 3, 130.0f, false, 0.0f },
            new object[4]{ 3, 132.0f, false, 0.0f },
            new object[4]{ 3, 134.0f, false, 0.0f },
            new object[4]{ 1, 136.0f, false, 0.0f },
            new object[4]{ 1, 138.0f, false, 0.0f },
            new object[4]{ 2, 140.0f, false, 0.0f },
            new object[4]{ 2, 142.0f, false, 0.0f },
            new object[4]{ 0, 144.0f, false, 0.0f },
            new object[4]{ 0, 146.0f, false, 0.0f },
            new object[4]{ 0, 148.0f, false, 0.0f },
            new object[4]{ 0, 150.0f, false, 0.0f },
            new object[4]{ 1, 152.0f, false, 0.0f },
            new object[4]{ 1, 154.0f, false, 0.0f },
            new object[4]{ 2, 156.0f, false, 0.0f },
            new object[4]{ 2, 158.0f, false, 0.0f },
            new object[4]{ 3, 160.0f, false, 0.0f },
            new object[4]{ 2, 161.0f, false, 0.0f },
            new object[4]{ 1, 162.0f, false, 0.0f },
            new object[4]{ 0, 163.5f, false, 0.0f },
            new object[4]{ 1, 165.0f, false, 0.0f },
            new object[4]{ 2, 166.0f, false, 0.0f },
            new object[4]{ 0, 168.0f, false, 0.0f },
            new object[4]{ 1, 169.0f, false, 0.0f },
            new object[4]{ 2, 170.0f, false, 0.0f },
            new object[4]{ 3, 171.5f, false, 0.0f },
            new object[4]{ 2, 173.0f, false, 0.0f },
            new object[4]{ 1, 174.0f, false, 0.0f },
            new object[4]{ 3, 176.0f, false, 0.0f },
            new object[4]{ 1, 177.0f, false, 0.0f },
            new object[4]{ 2, 178.0f, false, 0.0f },
            new object[4]{ 0, 179.5f, false, 0.0f },
            new object[4]{ 2, 181.0f, false, 0.0f },
            new object[4]{ 1, 182.0f, false, 0.0f },
            new object[4]{ 0, 184.0f, false, 0.0f },
            new object[4]{ 2, 185.0f, false, 0.0f },
            new object[4]{ 1, 186.0f, false, 0.0f },
            new object[4]{ 3, 187.5f, false, 0.0f },
            new object[4]{ 1, 189.0f, false, 0.0f },
            new object[4]{ 2, 190.0f, false, 0.0f },
            new object[4]{ 0, 192.0f, false, 0.0f },
            new object[4]{ 3, 192.0f, false, 0.0f },
            new object[4]{ 0, 195.5f, false, 0.0f },
            new object[4]{ 3, 197.0f, false, 0.0f },
            new object[4]{ 1, 198.0f, false, 0.0f },
            new object[4]{ 2, 199.5f, false, 0.0f },
            new object[4]{ 3, 203.5f, false, 0.0f },
            new object[4]{ 0, 205.0f, false, 0.0f },
            new object[4]{ 2, 206.0f, false, 0.0f },
            new object[4]{ 0, 208.0f, false, 0.0f },
            new object[4]{ 3, 208.0f, false, 0.0f },
            new object[4]{ 0, 211.5f, false, 0.0f },
            new object[4]{ 1, 213.0f, false, 0.0f },
            new object[4]{ 2, 214.0f, false, 0.0f },
            new object[4]{ 3, 215.5f, false, 0.0f },
            new object[4]{ 3, 219.5f, false, 0.0f },
            new object[4]{ 2, 221.0f, false, 0.0f },
            new object[4]{ 1, 222.0f, false, 0.0f },
            new object[4]{ 0, 223.0f, false, 0.0f },
            new object[4]{ 3, 224.0f, false, 0.0f },
            new object[4]{ 2, 227.5f, false, 0.0f },
            new object[4]{ 1, 229.0f, false, 0.0f },
            new object[4]{ 0, 230.0f, false, 0.0f },
            new object[4]{ 0, 231.0f, false, 0.0f },
            new object[4]{ 1, 231.5f, false, 0.0f },
            new object[4]{ 0, 235.5f, false, 0.0f },
            new object[4]{ 1, 237.0f, false, 0.0f },
            new object[4]{ 2, 238.0f, false, 0.0f },
            new object[4]{ 2, 239.0f, false, 0.0f },
            new object[4]{ 3, 240.0f, false, 0.0f },
            new object[4]{ 1, 243.5f, false, 0.0f },
            new object[4]{ 2, 245.0f, false, 0.0f },
            new object[4]{ 0, 246.0f, false, 0.0f },
            new object[4]{ 0, 247.0f, false, 0.0f },
            new object[4]{ 2, 247.5f, false, 0.0f },
            new object[4]{ 0, 251.5f, false, 0.0f },
            new object[4]{ 1, 253.0f, false, 0.0f },
            new object[4]{ 2, 254.0f, false, 0.0f },
            new object[4]{ 3, 255.0f, false, 0.0f },
            new object[4]{ 0, 256.0f, false, 0.0f },
            new object[4]{ 1, 257.0f, false, 0.0f },
            new object[4]{ 2, 258.0f, false, 0.0f },
            new object[4]{ 3, 259.0f, false, 0.0f },
            new object[4]{ 0, 260.0f, false, 0.0f },
            new object[4]{ 1, 261.0f, false, 0.0f },
            new object[4]{ 2, 262.0f, false, 0.0f },
            new object[4]{ 3, 263.0f, false, 0.0f },
            new object[4]{ 0, 264.0f, false, 0.0f },
            new object[4]{ 1, 265.0f, false, 0.0f },
            new object[4]{ 0, 266.0f, false, 0.0f },
            new object[4]{ 1, 267.0f, false, 0.0f },
            new object[4]{ 2, 268.0f, false, 0.0f },
            new object[4]{ 3, 269.0f, false, 0.0f },
            new object[4]{ 2, 270.0f, false, 0.0f },
            new object[4]{ 3, 271.0f, false, 0.0f },
            new object[4]{ 0, 272.0f, false, 0.0f },
            new object[4]{ 2, 273.0f, false, 0.0f },
            new object[4]{ 1, 274.0f, false, 0.0f },
            new object[4]{ 3, 275.0f, false, 0.0f },
            new object[4]{ 0, 276.0f, false, 0.0f },
            new object[4]{ 2, 277.0f, false, 0.0f },
            new object[4]{ 1, 278.0f, false, 0.0f },
            new object[4]{ 3, 279.0f, false, 0.0f },
            new object[4]{ 0, 280.0f, false, 0.0f },
            new object[4]{ 2, 281.0f, false, 0.0f },
            new object[4]{ 0, 282.0f, false, 0.0f },
            new object[4]{ 2, 283.0f, false, 0.0f },
            new object[4]{ 1, 284.0f, false, 0.0f },
            new object[4]{ 3, 286.0f, false, 0.0f },
            new object[4]{ 0, 287.5f, false, 0.0f },
               };
    }
    public void LoadNormalChart()
    {

    }
    public void LoadHardChart()
    {
        hardChart = new object[][]
        {
            new object[4]{ 0, 16.0f, false, 0.0f },
            new object[4]{ 2, 16.5f, false, 0.0f },
            new object[4]{ 1, 17.0f, false, 0.0f },
            new object[4]{ 3, 17.5f, false, 0.0f },
            new object[4]{ 0, 18.0f, false, 0.0f },
            new object[4]{ 0, 18.5f, false, 0.0f },
            new object[4]{ 3, 20.0f, false, 0.0f },
            new object[4]{ 1, 20.5f, false, 0.0f },
            new object[4]{ 2, 21.0f, false, 0.0f },
            new object[4]{ 0, 21.5f, false, 0.0f },
            new object[4]{ 3, 22.0f, false, 0.0f },
            new object[4]{ 3, 22.5f, false, 0.0f },
            new object[4]{ 0, 24.0f, false, 0.0f },
            new object[4]{ 2, 24.5f, false, 0.0f },
            new object[4]{ 1, 25.0f, false, 0.0f },
            new object[4]{ 2, 25.5f, false, 0.0f },
            new object[4]{ 0, 26.0f, false, 0.0f },
            new object[4]{ 0, 26.5f, false, 0.0f },
            new object[4]{ 3, 28.0f, false, 0.0f },
            new object[4]{ 1, 28.5f, false, 0.0f },
            new object[4]{ 3, 29.0f, false, 0.0f },
            new object[4]{ 2, 29.5f, false, 0.0f },
            new object[4]{ 1, 30.0f, false, 0.0f },
            new object[4]{ 0, 30.333f, false, 0.0f },
            new object[4]{ 1, 30.5f, false, 0.0f },
            new object[4]{ 2, 31.0f, false, 0.0f },
            new object[4]{ 0, 32.0f, false, 0.0f },
            new object[4]{ 3, 32.0f, false, 0.0f },
            new object[4]{ 3, 33.0f, false, 0.0f },
            new object[4]{ 0, 34.0f, false, 0.0f },
            new object[4]{ 3, 34.5f, false, 0.0f },
            new object[4]{ 2, 35.0f, false, 0.0f },
            new object[4]{ 3, 36.0f, false, 0.0f },
            new object[4]{ 0, 37.0f, false, 0.0f },
            new object[4]{ 3, 38.0f, false, 0.0f },
            new object[4]{ 0, 38.5f, false, 0.0f },
            new object[4]{ 2, 39.0f, false, 0.0f },
            new object[4]{ 0, 40.0f, false, 0.0f },
            new object[4]{ 3, 41.0f, false, 0.0f },
            new object[4]{ 0, 42.0f, false, 0.0f },
            new object[4]{ 2, 42.5f, false, 0.0f },
            new object[4]{ 1, 43.0f, false, 0.0f },
            new object[4]{ 3, 44.0f, false, 0.0f },
            new object[4]{ 0, 44.5f, false, 0.0f },
            new object[4]{ 3, 45.0f, false, 0.0f },
            new object[4]{ 1, 45.5f, false, 0.0f },
            new object[4]{ 2, 46.0f, false, 0.0f },
            new object[4]{ 1, 46.5f, false, 0.0f },
            new object[4]{ 3, 47.0f, false, 0.0f },
            new object[4]{ 1, 48.0f, false, 0.0f },
            new object[4]{ 2, 49.0f, false, 0.0f },
            new object[4]{ 0, 50.0f, false, 0.0f },
            new object[4]{ 3, 50.5f, false, 0.0f },
            new object[4]{ 0, 51.0f, false, 0.0f },
            new object[4]{ 1, 52.0f, false, 0.0f },
            new object[4]{ 2, 53.0f, false, 0.0f },
            new object[4]{ 3, 54.0f, false, 0.0f },
            new object[4]{ 0, 54.5f, false, 0.0f },
            new object[4]{ 3, 55.0f, false, 0.0f },
            new object[4]{ 1, 56.0f, false, 0.0f },
            new object[4]{ 2, 57.0f, false, 0.0f },
            new object[4]{ 0, 58.0f, false, 0.0f },
            new object[4]{ 2, 58.5f, false, 0.0f },
            new object[4]{ 0, 59.0f, false, 0.0f },
            new object[4]{ 3, 60.0f, false, 0.0f },
            new object[4]{ 2, 60.5f, false, 0.0f },
            new object[4]{ 3, 61.0f, false, 0.0f },
            new object[4]{ 1, 61.5f, false, 0.0f },
            new object[4]{ 2, 62.0f, false, 0.0f },
            new object[4]{ 0, 62.333f, false, 0.0f },
            new object[4]{ 2, 62.5f, false, 0.0f },
            new object[4]{ 1, 63.0f, false, 0.0f },
            new object[4]{ 3, 64.5f, false, 0.0f },
            new object[4]{ 3, 65.5f, false, 0.0f },
            new object[4]{ 1, 66.5f, false, 0.0f },
            new object[4]{ 2, 67.0f, false, 0.0f },
            new object[4]{ 0, 67.5f, false, 0.0f },
            new object[4]{ 0, 68.5f, false, 0.0f },
            new object[4]{ 0, 69.5f, false, 0.0f },
            new object[4]{ 1, 70.5f, false, 0.0f },
            new object[4]{ 2, 71.0f, false, 0.0f },
            new object[4]{ 3, 71.5f, false, 0.0f },
            new object[4]{ 3, 72.5f, false, 0.0f },
            new object[4]{ 3, 73.5f, false, 0.0f },
            new object[4]{ 0, 74.5f, false, 0.0f },
            new object[4]{ 1, 75.0f, false, 0.0f },
            new object[4]{ 2, 75.5f, false, 0.0f },
            new object[4]{ 3, 76.5f, false, 0.0f },
            new object[4]{ 0, 77.0f, false, 0.0f },
            new object[4]{ 2, 77.5f, false, 0.0f },
            new object[4]{ 1, 78.0f, false, 0.0f },
            new object[4]{ 3, 78.5f, false, 0.0f },
            new object[4]{ 1, 79.0f, false, 0.0f },
            new object[4]{ 2, 79.5f, false, 0.0f },
            new object[4]{ 2, 80.5f, false, 0.0f },
            new object[4]{ 2, 81.5f, false, 0.0f },
            new object[4]{ 1, 82.5f, false, 0.0f },
            new object[4]{ 3, 83.0f, false, 0.0f },
            new object[4]{ 2, 83.5f, false, 0.0f },
            new object[4]{ 2, 84.5f, false, 0.0f },
            new object[4]{ 2, 85.5f, false, 0.0f },
            new object[4]{ 1, 86.5f, false, 0.0f },
            new object[4]{ 0, 87.0f, false, 0.0f },
            new object[4]{ 2, 87.5f, false, 0.0f },
            new object[4]{ 2, 88.5f, false, 0.0f },
            new object[4]{ 2, 89.5f, false, 0.0f },
            new object[4]{ 0, 90.5f, false, 0.0f },
            new object[4]{ 1, 91.0f, false, 0.0f },
            new object[4]{ 2, 91.5f, false, 0.0f },
            new object[4]{ 3, 92.5f, false, 0.0f },
            new object[4]{ 1, 93.0f, false, 0.0f },
            new object[4]{ 2, 93.5f, false, 0.0f },
            new object[4]{ 0, 94.0f, false, 0.0f },
            new object[4]{ 3, 94.5f, false, 0.0f },
            new object[4]{ 2, 95.0f, false, 0.0f },
            new object[4]{ 3, 95.5f, false, 0.0f },
            new object[4]{ 1, 96.5f, false, 0.0f },
            new object[4]{ 1, 97.5f, false, 0.0f },
            new object[4]{ 2, 98.5f, false, 0.0f },
            new object[4]{ 0, 99.0f, false, 0.0f },
            new object[4]{ 1, 99.5f, false, 0.0f },
            new object[4]{ 1, 100.5f, false, 0.0f },
            new object[4]{ 1, 101.5f, false, 0.0f },
            new object[4]{ 0, 102.5f, false, 0.0f },
            new object[4]{ 2, 103.0f, false, 0.0f },
            new object[4]{ 1, 103.5f, false, 0.0f },
            new object[4]{ 1, 104.5f, false, 0.0f },
            new object[4]{ 1, 105.5f, false, 0.0f },
            new object[4]{ 3, 106.5f, false, 0.0f },
            new object[4]{ 2, 107.0f, false, 0.0f },
            new object[4]{ 1, 107.5f, false, 0.0f },
            new object[4]{ 2, 108.5f, false, 0.0f },
            new object[4]{ 3, 109.0f, false, 0.0f },
            new object[4]{ 2, 109.5f, false, 0.0f },
            new object[4]{ 1, 110.0f, false, 0.0f },
            new object[4]{ 0, 110.5f, false, 0.0f },
            new object[4]{ 1, 111.0f, false, 0.0f },
            new object[4]{ 2, 111.5f, false, 0.0f },
            new object[4]{ 2, 112.5f, false, 0.0f },
            new object[4]{ 2, 113.5f, false, 0.0f },
            new object[4]{ 3, 114.5f, false, 0.0f },
            new object[4]{ 0, 115.0f, false, 0.0f },
            new object[4]{ 1, 115.5f, false, 0.0f },
            new object[4]{ 1, 116.5f, false, 0.0f },
            new object[4]{ 1, 117.5f, false, 0.0f },
            new object[4]{ 0, 118.5f, false, 0.0f },
            new object[4]{ 1, 119.0f, false, 0.0f },
            new object[4]{ 2, 119.5f, false, 0.0f },
            new object[4]{ 2, 120.5f, false, 0.0f },
            new object[4]{ 2, 121.5f, false, 0.0f },
            new object[4]{ 1, 122.5f, false, 0.0f },
            new object[4]{ 0, 123.0f, false, 0.0f },
            new object[4]{ 3, 123.5f, false, 0.0f },
            new object[4]{ 3, 124.5f, false, 0.0f },
            new object[4]{ 1, 125.0f, false, 0.0f },
            new object[4]{ 3, 125.5f, false, 0.0f },
            new object[4]{ 0, 126.0f, false, 0.0f },
            new object[4]{ 1, 126.5f, false, 0.0f },
            new object[4]{ 2, 127.0f, false, 0.0f },
            new object[4]{ 3, 128.0f, false, 0.0f },
            new object[4]{ 3, 129.0f, false, 0.0f },
            new object[4]{ 3, 130.0f, false, 0.0f },
            new object[4]{ 3, 131.0f, false, 0.0f },
            new object[4]{ 1, 132.0f, false, 0.0f },
            new object[4]{ 1, 133.0f, false, 0.0f },
            new object[4]{ 2, 134.0f, false, 0.0f },
            new object[4]{ 2, 135.0f, false, 0.0f },
            new object[4]{ 0, 136.0f, false, 0.0f },
            new object[4]{ 0, 137.0f, false, 0.0f },
            new object[4]{ 1, 138.0f, false, 0.0f },
            new object[4]{ 0, 139.0f, false, 0.0f },
            new object[4]{ 1, 140.0f, false, 0.0f },
            new object[4]{ 2, 141.0f, false, 0.0f },
            new object[4]{ 3, 142.0f, false, 0.0f },
            new object[4]{ 1, 142.5f, false, 0.0f },
            new object[4]{ 3, 143.0f, false, 0.0f },
            new object[4]{ 0, 144.0f, false, 0.0f },
            new object[4]{ 0, 145.0f, false, 0.0f },
            new object[4]{ 0, 146.0f, false, 0.0f },
            new object[4]{ 0, 147.0f, false, 0.0f },
            new object[4]{ 2, 148.0f, false, 0.0f },
            new object[4]{ 2, 149.0f, false, 0.0f },
            new object[4]{ 1, 150.0f, false, 0.0f },
            new object[4]{ 1, 151.0f, false, 0.0f },
            new object[4]{ 3, 152.0f, false, 0.0f },
            new object[4]{ 2, 153.0f, false, 0.0f },
            new object[4]{ 1, 154.0f, false, 0.0f },
            new object[4]{ 0, 155.0f, false, 0.0f },
            new object[4]{ 1, 156.0f, false, 0.0f },
            new object[4]{ 2, 157.0f, false, 0.0f },
            new object[4]{ 3, 158.0f, false, 0.0f },
            new object[4]{ 0, 159.0f, false, 0.0f },
            new object[4]{ 3, 160.0f, false, 0.0f },
            new object[4]{ 1, 160.5f, false, 0.0f },
            new object[4]{ 3, 161.0f, false, 0.0f },
            new object[4]{ 1, 161.5f, false, 0.0f },
            new object[4]{ 3, 162.0f, false, 0.0f },
            new object[4]{ 1, 162.5f, false, 0.0f },
            new object[4]{ 2, 163.5f, false, 0.0f },
            new object[4]{ 0, 164.5f, false, 0.0f },
            new object[4]{ 1, 165.0f, false, 0.0f },
            new object[4]{ 0, 165.5f, false, 0.0f },
            new object[4]{ 1, 166.0f, false, 0.0f },
            new object[4]{ 0, 166.5f, false, 0.0f },
            new object[4]{ 0, 168.0f, false, 0.0f },
            new object[4]{ 2, 168.5f, false, 0.0f },
            new object[4]{ 0, 169.0f, false, 0.0f },
            new object[4]{ 2, 169.5f, false, 0.0f },
            new object[4]{ 0, 170.0f, false, 0.0f },
            new object[4]{ 2, 170.5f, false, 0.0f },
            new object[4]{ 1, 171.5f, false, 0.0f },
            new object[4]{ 3, 172.5f, false, 0.0f },
            new object[4]{ 1, 173.0f, false, 0.0f },
            new object[4]{ 3, 173.5f, false, 0.0f },
            new object[4]{ 1, 174.0f, false, 0.0f },
            new object[4]{ 3, 174.5f, false, 0.0f },
            new object[4]{ 2, 175.0f, false, 0.0f },
            new object[4]{ 1, 175.5f, false, 0.0f },
            new object[4]{ 0, 176.0f, false, 0.0f },
            new object[4]{ 1, 176.5f, false, 0.0f },
            new object[4]{ 0, 177.0f, false, 0.0f },
            new object[4]{ 1, 177.5f, false, 0.0f },
            new object[4]{ 0, 178.0f, false, 0.0f },
            new object[4]{ 1, 178.5f, false, 0.0f },
            new object[4]{ 2, 179.5f, false, 0.0f },
            new object[4]{ 3, 180.5f, false, 0.0f },
            new object[4]{ 1, 181.0f, false, 0.0f },
            new object[4]{ 3, 181.5f, false, 0.0f },
            new object[4]{ 1, 182.0f, false, 0.0f },
            new object[4]{ 3, 182.5f, false, 0.0f },
            new object[4]{ 3, 184.0f, false, 0.0f },
            new object[4]{ 2, 184.5f, false, 0.0f },
            new object[4]{ 3, 185.0f, false, 0.0f },
            new object[4]{ 2, 185.5f, false, 0.0f },
            new object[4]{ 3, 186.0f, false, 0.0f },
            new object[4]{ 2, 186.5f, false, 0.0f },
            new object[4]{ 1, 187.5f, false, 0.0f },
            new object[4]{ 0, 188.5f, false, 0.0f },
            new object[4]{ 2, 189.0f, false, 0.0f },
            new object[4]{ 0, 189.5f, false, 0.0f },
            new object[4]{ 1, 190.0f, false, 0.0f },
            new object[4]{ 2, 190.5f, false, 0.0f },
            new object[4]{ 3, 191.0f, false, 0.0f },
            new object[4]{ 0, 191.5f, false, 0.0f },
            new object[4]{ 3, 192.0f, false, 0.0f },
            new object[4]{ 3, 193.0f, false, 0.0f },
            new object[4]{ 3, 194.0f, false, 0.0f },
            new object[4]{ 3, 195.0f, false, 0.0f },
            new object[4]{ 0, 195.5f, false, 0.0f },
            new object[4]{ 3, 197.0f, false, 0.0f },
            new object[4]{ 0, 198.0f, false, 0.0f },
            new object[4]{ 2, 198.5f, false, 0.0f },
            new object[4]{ 1, 199.0f, false, 0.0f },
            new object[4]{ 2, 199.5f, false, 0.0f },
            new object[4]{ 0, 200.0f, false, 0.0f },
            new object[4]{ 0, 201.0f, false, 0.0f },
            new object[4]{ 0, 202.0f, false, 0.0f },
            new object[4]{ 0, 203.0f, false, 0.0f },
            new object[4]{ 3, 203.5f, false, 0.0f },
            new object[4]{ 0, 205.0f, false, 0.0f },
            new object[4]{ 3, 206.0f, false, 0.0f },
            new object[4]{ 0, 206.5f, false, 0.0f },
            new object[4]{ 3, 207.0f, false, 0.0f },
            new object[4]{ 1, 209.0f, false, 0.0f },
            new object[4]{ 3, 209.0f, false, 0.0f },
            new object[4]{ 0, 211.0f, false, 0.0f },
            new object[4]{ 2, 211.0f, false, 0.0f },
            new object[4]{ 0, 213.0f, false, 0.0f },
            new object[4]{ 1, 213.0f, false, 0.0f },
            new object[4]{ 2, 215.0f, false, 0.0f },
            new object[4]{ 3, 215.0f, false, 0.0f },
            new object[4]{ 1, 217.0f, false, 0.0f },
            new object[4]{ 3, 217.0f, false, 0.0f },
            new object[4]{ 0, 219.0f, false, 0.0f },
            new object[4]{ 1, 219.0f, false, 0.0f },
            new object[4]{ 0, 221.0f, false, 0.0f },
            new object[4]{ 2, 221.0f, false, 0.0f },
            new object[4]{ 2, 223.0f, false, 0.0f },
            new object[4]{ 3, 223.0f, false, 0.0f },
            new object[4]{ 3, 224.0f, false, 0.0f },
            new object[4]{ 2, 227.5f, false, 0.0f },
            new object[4]{ 1, 229.0f, false, 0.0f },
            new object[4]{ 0, 230.0f, false, 0.0f },
            new object[4]{ 2, 230.5f, false, 0.0f },
            new object[4]{ 0, 231.0f, false, 0.0f },
            new object[4]{ 2, 231.5f, false, 0.0f },
            new object[4]{ 1, 235.5f, false, 0.0f },
            new object[4]{ 3, 237.0f, false, 0.0f },
            new object[4]{ 0, 238.0f, false, 0.0f },
            new object[4]{ 1, 238.5f, false, 0.0f },
            new object[4]{ 0, 239.0f, false, 0.0f },
            new object[4]{ 1, 240.0f, false, 0.0f },
            new object[4]{ 2, 243.5f, false, 0.0f },
            new object[4]{ 1, 245.0f, false, 0.0f },
            new object[4]{ 0, 246.0f, false, 0.0f },
            new object[4]{ 2, 246.5f, false, 0.0f },
            new object[4]{ 0, 247.0f, false, 0.0f },
            new object[4]{ 2, 247.5f, false, 0.0f },
            new object[4]{ 0, 251.5f, false, 0.0f },
            new object[4]{ 3, 253.0f, false, 0.0f },
            new object[4]{ 2, 254.0f, false, 0.0f },
            new object[4]{ 1, 254.5f, false, 0.0f },
            new object[4]{ 0, 255.0f, false, 0.0f },
            new object[4]{ 0, 256.0f, false, 0.0f },
            new object[4]{ 3, 256.0f, false, 0.0f },
            new object[4]{ 0, 257.0f, false, 0.0f },
            new object[4]{ 3, 258.0f, false, 0.0f },
            new object[4]{ 0, 258.5f, false, 0.0f },
            new object[4]{ 2, 259.0f, false, 0.0f },
            new object[4]{ 0, 260.0f, false, 0.0f },
            new object[4]{ 3, 261.0f, false, 0.0f },
            new object[4]{ 0, 262.0f, false, 0.0f },
            new object[4]{ 3, 262.5f, false, 0.0f },
            new object[4]{ 2, 263.0f, false, 0.0f },
            new object[4]{ 3, 264.0f, false, 0.0f },
            new object[4]{ 0, 265.0f, false, 0.0f },
            new object[4]{ 3, 266.0f, false, 0.0f },
            new object[4]{ 2, 266.5f, false, 0.0f },
            new object[4]{ 1, 267.0f, false, 0.0f },
            new object[4]{ 0, 268.0f, false, 0.0f },
            new object[4]{ 3, 268.5f, false, 0.0f },
            new object[4]{ 0, 269.0f, false, 0.0f },
            new object[4]{ 1, 269.5f, false, 0.0f },
            new object[4]{ 2, 270.0f, false, 0.0f },
            new object[4]{ 1, 270.5f, false, 0.0f },
            new object[4]{ 0, 271.0f, false, 0.0f },
            new object[4]{ 1, 272.0f, false, 0.0f },
            new object[4]{ 2, 273.0f, false, 0.0f },
            new object[4]{ 3, 274.0f, false, 0.0f },
            new object[4]{ 0, 274.5f, false, 0.0f },
            new object[4]{ 3, 275.0f, false, 0.0f },
            new object[4]{ 1, 276.0f, false, 0.0f },
            new object[4]{ 2, 277.0f, false, 0.0f },
            new object[4]{ 0, 278.0f, false, 0.0f },
            new object[4]{ 3, 278.5f, false, 0.0f },
            new object[4]{ 0, 279.0f, false, 0.0f },
            new object[4]{ 1, 280.0f, false, 0.0f },
            new object[4]{ 2, 281.0f, false, 0.0f },
            new object[4]{ 3, 282.0f, false, 0.0f },
            new object[4]{ 2, 282.5f, false, 0.0f },
            new object[4]{ 3, 283.0f, false, 0.0f },
            new object[4]{ 0, 284.0f, false, 0.0f },
            new object[4]{ 2, 284.5f, false, 0.0f },
            new object[4]{ 0, 285.0f, false, 0.0f },
            new object[4]{ 1, 285.5f, false, 0.0f },
            new object[4]{ 2, 286.0f, false, 0.0f },
            new object[4]{ 3, 286.333f, false, 0.0f },
            new object[4]{ 2, 286.5f, false, 0.0f },
            new object[4]{ 1, 287.0f, false, 0.0f },
            new object[4]{ 0, 287.5f, false, 0.0f }
        };
    }
    public void LoadUnsightedChart()
    {
        unsightedChart = new object[][]
        {
            new object[4]{ 0, 16.0f, false, 0.0f },
            new object[4]{ 1, 16.5f, false, 0.0f },
            new object[4]{ 2, 17.0f, false, 0.0f },
            new object[4]{ 3, 17.5f, false, 0.0f },
            new object[4]{ 0, 18.0f, false, 0.0f },
            new object[4]{ 1, 18.333f, false, 0.0f },
            new object[4]{ 0, 18.5f, false, 0.0f },
            new object[4]{ 3, 20.0f, false, 0.0f },
            new object[4]{ 2, 20.5f, false, 0.0f },
            new object[4]{ 1, 21.0f, false, 0.0f },
            new object[4]{ 0, 21.5f, false, 0.0f },
            new object[4]{ 3, 22.0f, false, 0.0f },
            new object[4]{ 2, 22.333f, false, 0.0f },
            new object[4]{ 3, 22.5f, false, 0.0f },
            new object[4]{ 0, 24.0f, false, 0.0f },
            new object[4]{ 2, 24.5f, false, 0.0f },
            new object[4]{ 1, 25.0f, false, 0.0f },
            new object[4]{ 2, 25.5f, false, 0.0f },
            new object[4]{ 0, 26.0f, false, 0.0f },
            new object[4]{ 3, 26.333f, false, 0.0f },
            new object[4]{ 0, 26.5f, false, 0.0f },
            new object[4]{ 3, 28.0f, false, 0.0f },
            new object[4]{ 2, 28.5f, false, 0.0f },
            new object[4]{ 3, 29.0f, false, 0.0f },
            new object[4]{ 1, 29.5f, false, 0.0f },
            new object[4]{ 2, 30.0f, false, 0.0f },
            new object[4]{ 0, 30.333f, false, 0.0f },
            new object[4]{ 2, 30.5f, false, 0.0f },
            new object[4]{ 1, 31.0f, false, 0.0f },
            new object[4]{ 3, 31.5f, false, 0.0f },
            new object[4]{ 0, 32.0f, false, 0.0f },
            new object[4]{ 3, 32.0f, false, 0.0f },
            new object[4]{ 0, 32.5f, false, 0.0f },
            new object[4]{ 3, 33.0f, false, 0.0f },
            new object[4]{ 3, 33.5f, false, 0.0f },
            new object[4]{ 0, 34.0f, false, 0.0f },
            new object[4]{ 1, 34.333f, false, 0.0f },
            new object[4]{ 0, 34.5f, false, 0.0f },
            new object[4]{ 3, 35.0f, false, 0.0f },
            new object[4]{ 0, 35.333f, false, 0.0f },
            new object[4]{ 1, 35.5f, false, 0.0f },
            new object[4]{ 0, 35.833f, false, 0.0f },
            new object[4]{ 2, 36.0f, false, 0.0f },
            new object[4]{ 2, 36.5f, false, 0.0f },
            new object[4]{ 1, 37.0f, false, 0.0f },
            new object[4]{ 1, 37.5f, false, 0.0f },
            new object[4]{ 3, 38.0f, false, 0.0f },
            new object[4]{ 2, 38.333f, false, 0.0f },
            new object[4]{ 3, 38.5f, false, 0.0f },
            new object[4]{ 0, 39.0f, false, 0.0f },
            new object[4]{ 3, 39.333f, false, 0.0f },
            new object[4]{ 2, 39.5f, false, 0.0f },
            new object[4]{ 3, 39.833f, false, 0.0f },
            new object[4]{ 1, 40.0f, false, 0.0f },
            new object[4]{ 1, 40.5f, false, 0.0f },
            new object[4]{ 2, 41.0f, false, 0.0f },
            new object[4]{ 2, 41.5f, false, 0.0f },
            new object[4]{ 0, 42.0f, false, 0.0f },
            new object[4]{ 1, 42.333f, false, 0.0f },
            new object[4]{ 2, 42.5f, false, 0.0f },
            new object[4]{ 3, 43.0f, false, 0.0f },
            new object[4]{ 2, 43.333f, false, 0.0f },
            new object[4]{ 1, 43.5f, false, 0.0f },
            new object[4]{ 0, 43.833f, false, 0.0f },
            new object[4]{ 3, 44.0f, false, 0.0f },
            new object[4]{ 1, 44.5f, false, 0.0f },
            new object[4]{ 2, 45.0f, false, 0.0f },
            new object[4]{ 0, 45.5f, false, 0.0f },
            new object[4]{ 1, 46.0f, false, 0.0f },
            new object[4]{ 2, 46.333f, false, 0.0f },
            new object[4]{ 1, 46.5f, false, 0.0f },
            new object[4]{ 0, 47.0f, false, 0.0f },
            new object[4]{ 3, 47.5f, false, 0.0f },
            new object[4]{ 1, 48.0f, false, 0.0f },
            new object[4]{ 3, 48.0f, false, 0.0f },
            new object[4]{ 3, 48.5f, false, 0.0f },
            new object[4]{ 1, 49.0f, false, 0.0f },
            new object[4]{ 1, 49.5f, false, 0.0f },
            new object[4]{ 3, 50.0f, false, 0.0f },
            new object[4]{ 0, 50.333f, false, 0.0f },
            new object[4]{ 1, 50.5f, false, 0.0f },
            new object[4]{ 2, 51.0f, false, 0.0f },
            new object[4]{ 1, 51.333f, false, 0.0f },
            new object[4]{ 0, 51.5f, false, 0.0f },
            new object[4]{ 1, 51.833f, false, 0.0f },
            new object[4]{ 2, 52.0f, false, 0.0f },
            new object[4]{ 2, 52.5f, false, 0.0f },
            new object[4]{ 1, 53.0f, false, 0.0f },
            new object[4]{ 1, 53.5f, false, 0.0f },
            new object[4]{ 0, 54.0f, false, 0.0f },
            new object[4]{ 2, 54.333f, false, 0.0f },
            new object[4]{ 1, 54.5f, false, 0.0f },
            new object[4]{ 3, 55.0f, false, 0.0f },
            new object[4]{ 1, 55.333f, false, 0.0f },
            new object[4]{ 2, 55.5f, false, 0.0f },
            new object[4]{ 1, 55.833f, false, 0.0f },
            new object[4]{ 3, 56.0f, false, 0.0f },
            new object[4]{ 3, 56.5f, false, 0.0f },
            new object[4]{ 2, 57.0f, false, 0.0f },
            new object[4]{ 2, 57.5f, false, 0.0f },
            new object[4]{ 1, 58.0f, false, 0.0f },
            new object[4]{ 0, 58.333f, false, 0.0f },
            new object[4]{ 2, 58.5f, false, 0.0f },
            new object[4]{ 1, 59.0f, false, 0.0f },
            new object[4]{ 2, 59.333f, false, 0.0f },
            new object[4]{ 0, 59.5f, false, 0.0f },
            new object[4]{ 3, 59.833f, false, 0.0f },
            new object[4]{ 0, 60.0f, false, 0.0f },
            new object[4]{ 1, 60.5f, false, 0.0f },
            new object[4]{ 2, 61.0f, false, 0.0f },
            new object[4]{ 3, 61.5f, false, 0.0f },
            new object[4]{ 1, 62.0f, false, 0.0f },
            new object[4]{ 2, 62.333f, false, 0.0f },
            new object[4]{ 1, 62.5f, false, 0.0f },
            new object[4]{ 3, 63.0f, false, 0.0f },
            new object[4]{ 0, 63.5f, false, 0.0f },
            new object[4]{ 0, 64.0f, false, 0.0f },
            new object[4]{ 3, 64.0f, false, 0.0f },
            new object[4]{ 3, 64.5f, false, 0.0f },
            new object[4]{ 3, 65.5f, false, 0.0f },
            new object[4]{ 2, 66.5f, false, 0.0f },
            new object[4]{ 1, 67.0f, false, 0.0f },
            new object[4]{ 0, 67.5f, false, 0.0f },
            new object[4]{ 0, 68.5f, false, 0.0f },
            new object[4]{ 0, 69.5f, false, 0.0f },
            new object[4]{ 2, 70.5f, false, 0.0f },
            new object[4]{ 1, 71.0f, false, 0.0f },
            new object[4]{ 3, 71.5f, false, 0.0f },
            new object[4]{ 3, 72.5f, false, 0.0f },
            new object[4]{ 3, 73.5f, false, 0.0f },
            new object[4]{ 0, 74.5f, false, 0.0f },
            new object[4]{ 2, 75.0f, false, 0.0f },
            new object[4]{ 1, 75.5f, false, 0.0f },
            new object[4]{ 3, 76.5f, false, 0.0f },
            new object[4]{ 0, 77.0f, false, 0.0f },
            new object[4]{ 1, 77.5f, false, 0.0f },
            new object[4]{ 2, 78.0f, false, 0.0f },
            new object[4]{ 3, 78.5f, false, 0.0f },
            new object[4]{ 2, 79.0f, false, 0.0f },
            new object[4]{ 1, 79.5f, false, 0.0f },
            new object[4]{ 1, 80.5f, false, 0.0f },
            new object[4]{ 1, 81.5f, false, 0.0f },
            new object[4]{ 2, 82.5f, false, 0.0f },
            new object[4]{ 3, 83.0f, false, 0.0f },
            new object[4]{ 1, 83.5f, false, 0.0f },
            new object[4]{ 1, 84.5f, false, 0.0f },
            new object[4]{ 1, 85.5f, false, 0.0f },
            new object[4]{ 2, 86.5f, false, 0.0f },
            new object[4]{ 1, 87.0f, false, 0.0f },
            new object[4]{ 0, 87.5f, false, 0.0f },
            new object[4]{ 0, 88.5f, false, 0.0f },
            new object[4]{ 0, 89.5f, false, 0.0f },
            new object[4]{ 1, 90.5f, false, 0.0f },
            new object[4]{ 2, 91.0f, false, 0.0f },
            new object[4]{ 0, 91.5f, false, 0.0f },
            new object[4]{ 3, 92.5f, false, 0.0f },
            new object[4]{ 2, 93.0f, false, 0.0f },
            new object[4]{ 1, 93.5f, false, 0.0f },
            new object[4]{ 0, 94.0f, false, 0.0f },
            new object[4]{ 3, 94.5f, false, 0.0f },
            new object[4]{ 1, 95.0f, false, 0.0f },
            new object[4]{ 3, 95.5f, false, 0.0f },
            new object[4]{ 2, 96.5f, false, 0.0f },
            new object[4]{ 1, 97.0f, false, 0.0f },
            new object[4]{ 2, 97.5f, false, 0.0f },
            new object[4]{ 1, 98.5f, false, 0.0f },
            new object[4]{ 0, 99.0f, false, 0.0f },
            new object[4]{ 1, 99.5f, false, 0.0f },
            new object[4]{ 2, 100.5f, false, 0.0f },
            new object[4]{ 3, 101.0f, false, 0.0f },
            new object[4]{ 2, 101.5f, false, 0.0f },
            new object[4]{ 1, 102.0f, false, 0.0f },
            new object[4]{ 0, 102.5f, false, 0.0f },
            new object[4]{ 1, 103.0f, false, 0.0f },
            new object[4]{ 2, 103.5f, false, 0.0f },
            new object[4]{ 3, 104.5f, false, 0.0f },
            new object[4]{ 1, 105.0f, false, 0.0f },
            new object[4]{ 3, 105.5f, false, 0.0f },
            new object[4]{ 0, 106.5f, false, 0.0f },
            new object[4]{ 2, 107.0f, false, 0.0f },
            new object[4]{ 0, 107.5f, false, 0.0f },
            new object[4]{ 1, 108.5f, false, 0.0f },
            new object[4]{ 2, 109.0f, false, 0.0f },
            new object[4]{ 3, 109.5f, false, 0.0f },
            new object[4]{ 1, 110.0f, false, 0.0f },
            new object[4]{ 2, 110.5f, false, 0.0f },
            new object[4]{ 0, 111.0f, false, 0.0f },
            new object[4]{ 1, 111.5f, false, 0.0f },
            new object[4]{ 2, 112.5f, false, 0.0f },
            new object[4]{ 1, 113.0f, false, 0.0f },
            new object[4]{ 0, 113.5f, false, 0.0f },
            new object[4]{ 3, 114.5f, false, 0.0f },
            new object[4]{ 1, 115.0f, false, 0.0f },
            new object[4]{ 2, 115.5f, false, 0.0f },
            new object[4]{ 0, 116.5f, false, 0.0f },
            new object[4]{ 1, 117.0f, false, 0.0f },
            new object[4]{ 2, 117.5f, false, 0.0f },
            new object[4]{ 3, 118.0f, false, 0.0f },
            new object[4]{ 2, 118.5f, false, 0.0f },
            new object[4]{ 1, 119.0f, false, 0.0f },
            new object[4]{ 0, 119.5f, false, 0.0f },
            new object[4]{ 3, 120.5f, false, 0.0f },
            new object[4]{ 1, 121.0f, false, 0.0f },
            new object[4]{ 2, 121.5f, false, 0.0f },
            new object[4]{ 0, 122.5f, false, 0.0f },
            new object[4]{ 1, 123.0f, false, 0.0f },
            new object[4]{ 2, 123.5f, false, 0.0f },
            new object[4]{ 3, 124.5f, false, 0.0f },
            new object[4]{ 2, 125.0f, false, 0.0f },
            new object[4]{ 3, 125.5f, false, 0.0f },
            new object[4]{ 0, 126.0f, false, 0.0f },
            new object[4]{ 2, 126.5f, false, 0.0f },
            new object[4]{ 1, 127.0f, false, 0.0f },
            new object[4]{ 3, 128.0f, false, 0.0f },
            new object[4]{ 3, 129.0f, false, 0.0f },
            new object[4]{ 3, 130.0f, false, 0.0f },
            new object[4]{ 3, 131.0f, false, 0.0f },
            new object[4]{ 2, 132.0f, false, 0.0f },
            new object[4]{ 2, 133.0f, false, 0.0f },
            new object[4]{ 1, 134.0f, false, 0.0f },
            new object[4]{ 1, 135.0f, false, 0.0f },
            new object[4]{ 0, 136.0f, false, 0.0f },
            new object[4]{ 0, 137.0f, false, 0.0f },
            new object[4]{ 2, 138.0f, false, 0.0f },
            new object[4]{ 0, 139.0f, false, 0.0f },
            new object[4]{ 2, 140.0f, false, 0.0f },
            new object[4]{ 1, 141.0f, false, 0.0f },
            new object[4]{ 3, 142.0f, false, 0.0f },
            new object[4]{ 2, 142.5f, false, 0.0f },
            new object[4]{ 3, 143.0f, false, 0.0f },
            new object[4]{ 0, 144.0f, false, 0.0f },
            new object[4]{ 0, 145.0f, false, 0.0f },
            new object[4]{ 0, 146.0f, false, 0.0f },
            new object[4]{ 0, 147.0f, false, 0.0f },
            new object[4]{ 1, 148.0f, false, 0.0f },
            new object[4]{ 1, 149.0f, false, 0.0f },
            new object[4]{ 2, 150.0f, false, 0.0f },
            new object[4]{ 2, 151.0f, false, 0.0f },
            new object[4]{ 3, 152.0f, false, 0.0f },
            new object[4]{ 1, 153.0f, false, 0.0f },
            new object[4]{ 2, 154.0f, false, 0.0f },
            new object[4]{ 0, 155.0f, false, 0.0f },
            new object[4]{ 1, 156.0f, false, 0.0f },
            new object[4]{ 2, 157.0f, false, 0.0f },
            new object[4]{ 3, 158.0f, false, 0.0f },
            new object[4]{ 0, 159.0f, false, 0.0f },
            new object[4]{ 3, 160.0f, false, 0.0f },
            new object[4]{ 1, 160.5f, false, 0.0f },
            new object[4]{ 3, 161.0f, false, 0.0f },
            new object[4]{ 0, 161.5f, false, 0.0f },
            new object[4]{ 3, 162.0f, false, 0.0f },
            new object[4]{ 2, 162.5f, false, 0.0f },
            new object[4]{ 1, 163.5f, false, 0.0f },
            new object[4]{ 1, 164.5f, false, 0.0f },
            new object[4]{ 0, 165.0f, false, 0.0f },
            new object[4]{ 1, 165.5f, false, 0.0f },
            new object[4]{ 2, 166.0f, false, 0.0f },
            new object[4]{ 3, 166.5f, false, 0.0f },
            new object[4]{ 0, 168.0f, false, 0.0f },
            new object[4]{ 1, 168.5f, false, 0.0f },
            new object[4]{ 0, 169.0f, false, 0.0f },
            new object[4]{ 3, 169.5f, false, 0.0f },
            new object[4]{ 0, 170.0f, false, 0.0f },
            new object[4]{ 2, 170.5f, false, 0.0f },
            new object[4]{ 3, 171.5f, false, 0.0f },
            new object[4]{ 2, 172.5f, false, 0.0f },
            new object[4]{ 1, 173.0f, false, 0.0f },
            new object[4]{ 3, 173.5f, false, 0.0f },
            new object[4]{ 0, 174.0f, false, 0.0f },
            new object[4]{ 1, 174.5f, false, 0.0f },
            new object[4]{ 2, 175.0f, false, 0.0f },
            new object[4]{ 3, 175.5f, false, 0.0f },
            new object[4]{ 0, 176.0f, false, 0.0f },
            new object[4]{ 2, 176.5f, false, 0.0f },
            new object[4]{ 0, 177.0f, false, 0.0f },
            new object[4]{ 3, 177.5f, false, 0.0f },
            new object[4]{ 0, 178.0f, false, 0.0f },
            new object[4]{ 1, 178.5f, false, 0.0f },
            new object[4]{ 2, 179.5f, false, 0.0f },
            new object[4]{ 2, 180.5f, false, 0.0f },
            new object[4]{ 3, 181.0f, false, 0.0f },
            new object[4]{ 1, 181.5f, false, 0.0f },
            new object[4]{ 2, 182.0f, false, 0.0f },
            new object[4]{ 0, 182.5f, false, 0.0f },
            new object[4]{ 3, 184.0f, false, 0.0f },
            new object[4]{ 2, 184.5f, false, 0.0f },
            new object[4]{ 3, 185.0f, false, 0.0f },
            new object[4]{ 0, 185.5f, false, 0.0f },
            new object[4]{ 3, 186.0f, false, 0.0f },
            new object[4]{ 1, 186.5f, false, 0.0f },
            new object[4]{ 2, 187.5f, false, 0.0f },
            new object[4]{ 0, 188.5f, false, 0.0f },
            new object[4]{ 1, 189.0f, false, 0.0f },
            new object[4]{ 0, 189.5f, false, 0.0f },
            new object[4]{ 2, 190.0f, false, 0.0f },
            new object[4]{ 1, 190.5f, false, 0.0f },
            new object[4]{ 3, 191.0f, false, 0.0f },
            new object[4]{ 0, 191.5f, false, 0.0f },
            new object[4]{ 3, 192.0f, false, 0.0f },
            new object[4]{ 3, 193.0f, false, 0.0f },
            new object[4]{ 3, 194.0f, false, 0.0f },
            new object[4]{ 3, 195.0f, false, 0.0f },
            new object[4]{ 0, 195.5f, false, 0.0f },
            new object[4]{ 1, 197.0f, false, 0.0f },
            new object[4]{ 3, 197.0f, false, 0.0f },
            new object[4]{ 0, 198.0f, false, 0.0f },
            new object[4]{ 1, 198.5f, false, 0.0f },
            new object[4]{ 2, 199.0f, false, 0.0f },
            new object[4]{ 1, 199.5f, false, 0.0f },
            new object[4]{ 0, 200.0f, false, 0.0f },
            new object[4]{ 0, 201.0f, false, 0.0f },
            new object[4]{ 0, 202.0f, false, 0.0f },
            new object[4]{ 0, 203.0f, false, 0.0f },
            new object[4]{ 3, 203.5f, false, 0.0f },
            new object[4]{ 0, 205.0f, false, 0.0f },
            new object[4]{ 2, 205.0f, false, 0.0f },
            new object[4]{ 3, 206.0f, false, 0.0f },
            new object[4]{ 0, 206.5f, false, 0.0f },
            new object[4]{ 3, 207.0f, false, 0.0f },
            new object[4]{ 0, 208.0f, false, 0.0f },
            new object[4]{ 2, 209.0f, false, 0.0f },
            new object[4]{ 3, 209.0f, false, 0.0f },
            new object[4]{ 3, 210.0f, false, 0.0f },
            new object[4]{ 0, 211.0f, false, 0.0f },
            new object[4]{ 2, 211.0f, false, 0.0f },
            new object[4]{ 0, 212.0f, false, 0.0f },
            new object[4]{ 1, 213.0f, false, 0.0f },
            new object[4]{ 3, 213.0f, false, 0.0f },
            new object[4]{ 3, 214.0f, false, 0.0f },
            new object[4]{ 0, 215.0f, false, 0.0f },
            new object[4]{ 1, 215.0f, false, 0.0f },
            new object[4]{ 2, 216.0f, false, 0.0f },
            new object[4]{ 1, 217.0f, false, 0.0f },
            new object[4]{ 3, 217.0f, false, 0.0f },
            new object[4]{ 2, 218.0f, false, 0.0f },
            new object[4]{ 0, 219.0f, false, 0.0f },
            new object[4]{ 1, 219.0f, false, 0.0f },
            new object[4]{ 2, 220.0f, false, 0.0f },
            new object[4]{ 1, 221.0f, false, 0.0f },
            new object[4]{ 3, 221.0f, false, 0.0f },
            new object[4]{ 2, 222.0f, false, 0.0f },
            new object[4]{ 0, 223.0f, false, 0.0f },
            new object[4]{ 1, 223.0f, false, 0.0f },
            new object[4]{ 3, 224.0f, false, 0.0f },
            new object[4]{ 1, 227.5f, false, 0.0f },
            new object[4]{ 2, 229.0f, false, 0.0f },
            new object[4]{ 0, 230.0f, false, 0.0f },
            new object[4]{ 3, 230.5f, false, 0.0f },
            new object[4]{ 1, 231.0f, false, 0.0f },
            new object[4]{ 2, 231.5f, false, 0.0f },
            new object[4]{ 1, 235.5f, false, 0.0f },
            new object[4]{ 3, 237.0f, false, 0.0f },
            new object[4]{ 0, 238.0f, false, 0.0f },
            new object[4]{ 2, 238.5f, false, 0.0f },
            new object[4]{ 0, 239.0f, false, 0.0f },
            new object[4]{ 1, 240.0f, false, 0.0f },
            new object[4]{ 2, 243.5f, false, 0.0f },
            new object[4]{ 1, 245.0f, false, 0.0f },
            new object[4]{ 0, 246.0f, false, 0.0f },
            new object[4]{ 3, 246.5f, false, 0.0f },
            new object[4]{ 2, 247.0f, false, 0.0f },
            new object[4]{ 1, 247.5f, false, 0.0f },
            new object[4]{ 0, 251.5f, false, 0.0f },
            new object[4]{ 3, 253.0f, false, 0.0f },
            new object[4]{ 1, 254.0f, false, 0.0f },
            new object[4]{ 2, 254.5f, false, 0.0f },
            new object[4]{ 0, 255.0f, false, 0.0f },
            new object[4]{ 0, 256.0f, false, 0.0f },
            new object[4]{ 3, 256.0f, false, 0.0f },
            new object[4]{ 3, 256.5f, false, 0.0f },
            new object[4]{ 0, 257.0f, false, 0.0f },
            new object[4]{ 0, 257.5f, false, 0.0f },
            new object[4]{ 3, 258.0f, false, 0.0f },
            new object[4]{ 1, 258.333f, false, 0.0f },
            new object[4]{ 3, 258.5f, false, 0.0f },
            new object[4]{ 0, 259.0f, false, 0.0f },
            new object[4]{ 3, 259.333f, false, 0.0f },
            new object[4]{ 1, 259.5f, false, 0.0f },
            new object[4]{ 3, 259.833f, false, 0.0f },
            new object[4]{ 2, 260.0f, false, 0.0f },
            new object[4]{ 2, 260.5f, false, 0.0f },
            new object[4]{ 1, 261.0f, false, 0.0f },
            new object[4]{ 1, 261.5f, false, 0.0f },
            new object[4]{ 0, 262.0f, false, 0.0f },
            new object[4]{ 2, 262.333f, false, 0.0f },
            new object[4]{ 0, 262.5f, false, 0.0f },
            new object[4]{ 3, 263.0f, false, 0.0f },
            new object[4]{ 0, 263.333f, false, 0.0f },
            new object[4]{ 2, 263.5f, false, 0.0f },
            new object[4]{ 0, 263.833f, false, 0.0f },
            new object[4]{ 1, 264.0f, false, 0.0f },
            new object[4]{ 1, 264.5f, false, 0.0f },
            new object[4]{ 2, 265.0f, false, 0.0f },
            new object[4]{ 2, 265.5f, false, 0.0f },
            new object[4]{ 3, 266.0f, false, 0.0f },
            new object[4]{ 1, 266.333f, false, 0.0f },
            new object[4]{ 2, 266.5f, false, 0.0f },
            new object[4]{ 0, 267.0f, false, 0.0f },
            new object[4]{ 2, 267.333f, false, 0.0f },
            new object[4]{ 1, 267.5f, false, 0.0f },
            new object[4]{ 3, 267.833f, false, 0.0f },
            new object[4]{ 0, 268.0f, false, 0.0f },
            new object[4]{ 1, 268.5f, false, 0.0f },
            new object[4]{ 2, 269.0f, false, 0.0f },
            new object[4]{ 3, 269.5f, false, 0.0f },
            new object[4]{ 1, 270.0f, false, 0.0f },
            new object[4]{ 2, 270.333f, false, 0.0f },
            new object[4]{ 1, 270.5f, false, 0.0f },
            new object[4]{ 3, 271.0f, false, 0.0f },
            new object[4]{ 0, 271.5f, false, 0.0f },
            new object[4]{ 0, 272.0f, false, 0.0f },
            new object[4]{ 1, 272.0f, false, 0.0f },
            new object[4]{ 0, 272.5f, false, 0.0f },
            new object[4]{ 1, 273.0f, false, 0.0f },
            new object[4]{ 1, 273.5f, false, 0.0f },
            new object[4]{ 0, 274.0f, false, 0.0f },
            new object[4]{ 3, 274.333f, false, 0.0f },
            new object[4]{ 1, 274.5f, false, 0.0f },
            new object[4]{ 2, 275.0f, false, 0.0f },
            new object[4]{ 1, 275.333f, false, 0.0f },
            new object[4]{ 3, 275.5f, false, 0.0f },
            new object[4]{ 1, 275.833f, false, 0.0f },
            new object[4]{ 2, 276.0f, false, 0.0f },
            new object[4]{ 2, 276.5f, false, 0.0f },
            new object[4]{ 1, 277.0f, false, 0.0f },
            new object[4]{ 1, 277.5f, false, 0.0f },
            new object[4]{ 3, 278.0f, false, 0.0f },
            new object[4]{ 2, 278.333f, false, 0.0f },
            new object[4]{ 1, 278.5f, false, 0.0f },
            new object[4]{ 0, 279.0f, false, 0.0f },
            new object[4]{ 1, 279.333f, false, 0.0f },
            new object[4]{ 2, 279.5f, false, 0.0f },
            new object[4]{ 1, 279.833f, false, 0.0f },
            new object[4]{ 0, 280.0f, false, 0.0f },
            new object[4]{ 0, 280.5f, false, 0.0f },
            new object[4]{ 2, 281.0f, false, 0.0f },
            new object[4]{ 2, 281.5f, false, 0.0f },
            new object[4]{ 1, 282.0f, false, 0.0f },
            new object[4]{ 3, 282.333f, false, 0.0f },
            new object[4]{ 2, 282.5f, false, 0.0f },
            new object[4]{ 1, 283.0f, false, 0.0f },
            new object[4]{ 2, 283.333f, false, 0.0f },
            new object[4]{ 3, 283.5f, false, 0.0f },
            new object[4]{ 0, 283.833f, false, 0.0f },
            new object[4]{ 3, 284.0f, false, 0.0f },
            new object[4]{ 1, 284.5f, false, 0.0f },
            new object[4]{ 2, 285.0f, false, 0.0f },
            new object[4]{ 0, 285.5f, false, 0.0f },
            new object[4]{ 1, 286.0f, false, 0.0f },
            new object[4]{ 2, 286.333f, false, 0.0f },
            new object[4]{ 1, 286.5f, false, 0.0f },
            new object[4]{ 0, 287.0f, false, 0.0f },
            new object[4]{ 3, 287.5f, false, 0.0f }
        };
    }

    */

}

