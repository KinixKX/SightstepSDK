using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class SongPlayer : UdonSharpBehaviour
{

    public NetworkPlayerData localPlayerData; //Null at start, players must be assigned in order to play on their local end, otherwise an auto-play mode should start
    public AudioSource _song;
    public StageSelector stageSelector;

    public Playfield playfield1; //This is the main Playfield, all the logic will be calculated here
    public Playfield playfield2; //Sub-Playfield will not run logic, but still show arrows, change the colors, receptor flashes and if the arrows disappear or not 

    public bool songPlaying; //Is the song playing? A lot of the update checks a
    public int comboCounter; 
    public int accScore;
    public float accuracy;

    public float xmod;

    public int loadedLevel = -1;

    public bool pressedL;
    public bool pressedD;
    public bool pressedU;
    public bool pressedR;

    public float songOffset; //This is the song offset set by the charter
    public float globalOffset; //This is the player changed offset
    public bool wirelessPreset; //Sora Magic

    public Slider offsetAssign;
    public Text displayStateOffset; //Change this to another script or not?
    public Text buttonWired;
    public Text buttonWireless;
    public Text offsetDisplay;

    public int lastArrowHit;
    public int currentArrow;
    [HideInInspector]
    public float lastSongBeat;
    public float currentSongBeat;
    public float bpm;
    public bool songFullBeat;
    public float songTime;

    Color fantasticColor = new Color(0.7971698f, 1, 0.9747423f);
    Color excellentColor = new Color(1, 0.955617f, 0.6839622f);
    Color greatColor = new Color(0.4417514f, 0.9245283f, 0.2834639f);
    Color decentColor = new Color(0.5865811f, 0.4575472f, 1);

    void Start()
    {
        Networking.LocalPlayer.SetRunSpeed(4);
        Networking.LocalPlayer.SetWalkSpeed(2);
        Networking.LocalPlayer.SetStrafeSpeed(2);
        Networking.LocalPlayer.SetJumpImpulse(2);

        songPlaying = false;
        currentArrow = 0;
        lastArrowHit = 0;
    }

    private float oldSongTime;
    private float lmao2;
    private bool resetTimer = true;
    private bool noMoreArrows = false;

    //Sora Magic
    private void SongTimerRefresh()
    {

        if (!songPlaying) return;

        songTime = _song.time;

        if (songTime > oldSongTime) // Unity refreshes it, DO NOT DO ANYTHING
        {
            oldSongTime = songTime;
            resetTimer = true;
        }
        else if (songTime == oldSongTime) // Unity did not refresh the song time, let's make an approximation
        {
            songTime = (resetTimer ? songTime : lmao2) + Time.deltaTime * 0.95f;
            resetTimer = false;
            lmao2 = songTime;
        }
        else
        {
            Debug.LogError("SONG PLAYER: Something went wrong with the timing aproximation");
            resetTimer = true;
        }

        // 0.110f => Unity latency
        // globalOffset => Fine-tune offset, set in the UI
        // Wireless mode => adds 120ms of audio latency
        songTime -= 0.110f + globalOffset + (wirelessPreset ? 0.12f : 0f);
        currentSongBeat = songTime * bpm / 60;

        stageSelector.defaultMods.CheckUpdate();

        ///BUG: Arrows will not load properly if placed in beats earlier than the set xmod value (i.e. arrows set on beat 1.5 when xmod is 2 will load them incorrectly)

        if(!noMoreArrows && currentSongBeat + xmod >= (float)stageSelector.defaultCharts.notes[currentArrow][1]) //If there are arrows to load and the current beat of the song is greater than the next arrow's timing
        {
            for (int column = 0; column < 3; ++column) //We are going to check 3 array entries further to check for double, triple or quad arrow spawns
            {
                if (currentArrow + column > stageSelector.defaultCharts.notes.Length - 1) //In case we loaded the last arrow, we exit here before causing an exception
                {
                    noMoreArrows = true;
                    Debug.Log("SONG PLAYER: No more arrows to load");
                    break;
                }

                if ((float)stageSelector.defaultCharts.notes[currentArrow][1] == (float)stageSelector.defaultCharts.notes[currentArrow + column][1]) //If the arrow to load has the same timing as the next one, then load it too
                {
                    //TODO: Make both playfields load an arrow instead of just this
                    Arrow a1 = playfield1.arrowObjects.GetArrow();
                    Arrow a2 = playfield2.arrowObjects.GetArrow();
                    LoadArrow(a1, column + currentArrow);
                    SpawnArrow(a1);
                    LoadArrow(a2, column + currentArrow);
                    SpawnArrow(a2);
                }
                else //When it only loads one arrow, column is already 1, so it will get added to it. When it loads 2, 3 or 4 that value gets added. 4 arrows hasn't been tested!
                {
                    currentArrow += column; 
                    break;
                }
            }
        }
      
        if((int)currentSongBeat > (int)lastSongBeat) //New beat hit! Play any effect that must play every beat!
        {

            playfield1.receptorAnimator.Play("ReceptorLights", 1);
            playfield2.receptorAnimator.Play("ReceptorLights", 1);

            songFullBeat = true;
        }
        else
        {
            ///Not sure the usage of this variable
            songFullBeat = false;
        }

        lastSongBeat = currentSongBeat;

        if (!_song.isPlaying) //This is controlled by the audio source playing. Once it stops we stop the rest of the logic with our boolean. 
        {
            SongFinished();
        }
    }

    public void SongFinished()
    {
        songPlaying = false;

        Networking.LocalPlayer.SetRunSpeed(4);
        Networking.LocalPlayer.SetWalkSpeed(2);
        Networking.LocalPlayer.SetStrafeSpeed(2);
        Networking.LocalPlayer.SetJumpImpulse(2);

        playfield1.receptors.SetActive(false);
        playfield2.receptors.SetActive(false);
        
        if (localPlayerData != null)
        {
            if (Networking.LocalPlayer != Networking.GetOwner(localPlayerData.gameObject)) { return; } //SPECTATOR: This wouldn't break things if it's not checked but just to be sure

            GetRank();
        }
    }

    public void GetRank()
    {
        if (localPlayerData == null) return;

        if (accuracy >= 90) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankS");
        if (accuracy < 90 && accuracy >= 80) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankA");
        if (accuracy < 80 && accuracy >= 70) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankB");
        if (accuracy < 70 && accuracy >= 60) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankC");
        if (accuracy < 60 && accuracy >= 50) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankD");
        if (accuracy < 50) localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "RankF");


    }

    private void Update()
    {
        SongTimerRefresh();
        HandleInputs();
    }

    public void HandleInputs()
    {
        if (localPlayerData == null) return;

        ///SST NOTE: Here we handle the start, reset and load of song difficulties
        if (!songPlaying)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetGame();
                Debug.Log("Game Reset, Press P to play");
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (loadedLevel != -1)
                {
                    StartSong();
                    Debug.Log("Now Playing");

                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha1)) { UseSimpleChart(); Debug.Log("Diff easy Loaded"); loadedLevel = 0; }
            if (Input.GetKeyDown(KeyCode.Alpha2)) { UseNormalChart(); Debug.Log("Diff normal Loaded"); loadedLevel = 1; }
            if (Input.GetKeyDown(KeyCode.Alpha3)) { UseHardChart(); Debug.Log("Diff hard Loaded"); loadedLevel = 2; }
            if (Input.GetKeyDown(KeyCode.Alpha4)) { UseUnsightedChart(); Debug.Log("Diff unsighted Loaded"); loadedLevel = 3; }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TriggerPlayerInput(0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TriggerPlayerInput(1);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TriggerPlayerInput(2);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TriggerPlayerInput(3);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            pressedL = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            pressedD = false;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            pressedU = false;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            pressedR = false;
        }
    }

    public void LoadArrow(Arrow a, int index)
    {
        a.direction = (int)stageSelector.defaultCharts.notes[index][0];
        a.setTiming = (float)stageSelector.defaultCharts.notes[index][1];

        a.InitArrow(-songOffset + (wirelessPreset ? 0.04f : 0.005f)); //Sora magic part 2

        /*
           //If holds were to be implemented, you can put them here

           a.isHold = (bool)stageSelector.defaultCharts.notes[index][2];
           if (a.isHold)
           {
               a.holdDuration = (float)stageSelector.defaultCharts.notes[index][3];
           }

        */

    }

    public void StartSong() //This is called by the game manager
    {
        _song.Play();
        songPlaying = true;

        ///Use a new system for enabling / disabling objects.... cause oh fuck this is bad

        playfield1.ToggleElements(true);
        playfield2.ToggleElements(false); 

        playfield1.receptors.SetActive(true);
        playfield2.receptors.SetActive(true);

        if (localPlayerData == null) return;

        if (Networking.LocalPlayer != Networking.GetOwner(localPlayerData.gameObject)) { return; } //SPECTATOR: They can still move

        Networking.LocalPlayer.SetRunSpeed(0.01f);
        Networking.LocalPlayer.SetWalkSpeed(0.01f);
        Networking.LocalPlayer.SetStrafeSpeed(0.01f);
        Networking.LocalPlayer.SetJumpImpulse(0);
    }

    public void TriggerPlayerInput(int direction)
    {
        switch (direction)
        {
            case 0:
                pressedL = true;
                playfield1.receptorAnimator.Play("ReceptorLPress", 2);
                playfield2.receptorAnimator.Play("ReceptorLPress", 2);        
                CheckArrowPress(0);
                break;
            case 1:
                pressedD = true;
                playfield1.receptorAnimator.Play("ReceptorDPress", 3);
                playfield2.receptorAnimator.Play("ReceptorDPress", 3);
                CheckArrowPress(1);
                break;
            case 2:
                pressedU = true;
                playfield1.receptorAnimator.Play("ReceptorUPress", 4);
                playfield2.receptorAnimator.Play("ReceptorUPress", 4);
                CheckArrowPress(2);
                break;
            case 3:
                pressedR = true;
                playfield1.receptorAnimator.Play("ReceptorRPress", 5);
                playfield2.receptorAnimator.Play("ReceptorRPress", 5);
                CheckArrowPress(3);
                break;
        }
    }

    public void FreePlayerInput(int direction)
    {
        switch (direction)
        {
            case 0:
                pressedL = false;
                break;
            case 1:
                pressedD = false;
                break;
            case 2:
                pressedU = false;
                break;
            case 3:
                pressedR = false;
                break;
        }
    }

    //Unused, but could be used for other scripts and mod stuff!
    public bool CheckPlayerInput(int direction)
    {
        switch (direction)
        {
            case 0:
                if (pressedL) return true;
                else return false;
            case 1:
                if (pressedD) return true;
                else return false;
            case 2:
                if (pressedU) return true;
                else return false;
            case 3:
                if (pressedR) return true;
                else return false;
            default:
                return false;
        }
    }

    public void CheckArrowPress(int direction)
    {
        if (!songPlaying) { return; }

        if (playfield1.arrowObjects.nextArrow == stageSelector.defaultCharts.notes.Length)
        {
            playfield1.arrowObjects.nextArrow--;
        }

        lastArrowHit = playfield1.arrowObjects.nextArrow;

        for (int i = Mathf.Clamp(lastArrowHit - 4, 0, stageSelector.defaultCharts.notes.Length - 1); i <= Mathf.Clamp(lastArrowHit + 4, 0, stageSelector.defaultCharts.notes.Length - 1); i++)
        {
            int index = i % playfield1.arrowObjects.pooledObjects.Length;

            if (playfield1.arrowObjects.pooledObjects[index].canBeHit)
            {
                if (((playfield1.arrowObjects.pooledObjects[index].setTiming - currentSongBeat) / bpm * 60f) + songOffset < 0.15f && ((playfield1.arrowObjects.pooledObjects[index].setTiming - currentSongBeat) / bpm * 60f) + songOffset > -0.15f)
                {
                    if (playfield1.arrowObjects.pooledObjects[index].direction == direction)
                    {
                        playfield1.arrowObjects.pooledObjects[index].DisableArrow();
                        playfield2.arrowObjects.pooledObjects[index].DisableArrow();
                        UpdateScore(((playfield1.arrowObjects.pooledObjects[index].setTiming - currentSongBeat) / bpm * 60) + songOffset, playfield1.arrowObjects.pooledObjects[index].direction);

                        playfield1.arrowObjects.nextArrow++;
                        playfield2.arrowObjects.nextArrow++;

                        return;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if (playfield1.arrowObjects.nextArrow == stageSelector.defaultCharts.notes.Length)
        {
            Debug.Log("Last note was pressed");
            //This never executes? - Kinix 
            //Because of the return; up there dumbass LOL - Kinix
        }

    }


    public void DisplayMiss()
    {
        if(localPlayerData == null) return;

        if (Networking.LocalPlayer != Networking.GetOwner(localPlayerData.gameObject)) { return; }

        accScore -= 12;
        CalculateAccuracy();

        localPlayerData.s_missCount++;
        localPlayerData.s_Combo = 0;
        localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowMiss");
        localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "HideComboCounter");

        comboCounter = 0;
        playfield1.arrowObjects.nextArrow++;
        playfield2.arrowObjects.nextArrow++;

    }

    public void UpdateScore(float timing, int direction)
    {
        if (localPlayerData == null) return;

        if(Networking.LocalPlayer != Networking.GetOwner(localPlayerData.gameObject)) { return; } //SPECTATOR: shouldn't be able to play or attempt to show scores I guess

        if (timing > .11f || timing < -.11f)
        {
            localPlayerData.s_almostCount++;
            localPlayerData.s_Combo = 0;

            if (-timing > 0) { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowAlmostLate");
                localPlayerData.s_lateCount++; }
            else { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowAlmostEarly");
                localPlayerData.s_earlyCount++; }
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "HideComboCounter");

            comboCounter = 0;
            playfield1.PlayLightOnReceptor(direction, decentColor);
            playfield2.PlayLightOnReceptor(direction, decentColor);
            //Almost
        }
        else if (timing > .065f || timing < -.065f)
        {
            accScore += 2;

            localPlayerData.s_greatCount++;
            localPlayerData.s_Combo++;

            if (-timing > 0) { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowGreatLate");
                localPlayerData.s_lateCount++; }
            else { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowGreatEarly");
                localPlayerData.s_earlyCount++; }

            comboCounter++;
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ComboBounce");
            playfield1.PlayLightOnReceptor(direction, greatColor);
            playfield2.PlayLightOnReceptor(direction, greatColor);
            //Great
        }
        else if (timing > .032f || timing < -.032f)
        {
            accScore += 4;
            localPlayerData.s_excellentCount++;
            localPlayerData.s_Combo++;

            if (-timing > 0) { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowExcellentLate");
                localPlayerData.s_lateCount++; }
            else { localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowExcellentEarly");
                localPlayerData.s_earlyCount++; }

            comboCounter++;
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ComboBounce");
            playfield1.PlayLightOnReceptor(direction, excellentColor);
            playfield2.PlayLightOnReceptor(direction, excellentColor);
            //Excellent
        }
        else
        {
            accScore += 5;

            localPlayerData.s_fantasticCount++;
            localPlayerData.s_Combo++;

            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowFantastic");

            comboCounter++;
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ComboBounce");
            playfield1.PlayLightOnReceptor(direction, fantasticColor);
            playfield2.PlayLightOnReceptor(direction, fantasticColor);
            //Fantastic
        }


        CalculateAccuracy();
        localPlayerData.comboDisplay.text = comboCounter.ToString();

        if (comboCounter == 4)
        {
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "ShowComboCounter");
        }

        if(comboCounter > localPlayerData.s_maxCombo)
        {
            localPlayerData.s_maxCombo = comboCounter;
        }

        if(comboCounter == stageSelector.defaultCharts.notes.Length)
        {
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "PlayFCBurst");
        }

        if(comboCounter % 100 == 0 && comboCounter != 0)
        {
            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "PlayComboBurst");
        }
    }

    public void CalculateAccuracy()
    {
        accuracy = accScore / (5f * stageSelector.defaultCharts.notes.Length);
        accuracy = Mathf.Round(accuracy*10000f) / 100f;
        localPlayerData.accuracyDisplay.text = accuracy.ToString();
        localPlayerData.s_Accuracy = accuracy;
    }
    
    public void SpawnArrow(Arrow a)
    {
        if(a == null) { return; }
        a.gameObject.SetActive(true);
    }

    public void LinkPlayer(NetworkPlayerData player)
    {
        localPlayerData = player;
    }
    public void UnlinkPlayer()
    {
        localPlayerData = null;
    }

    //We dont have a way to set a parameter, so each button for each setting
    public void UseWired()
    {
        if (wirelessPreset)
        {
            wirelessPreset = false;
            displayStateOffset.text = Networking.LocalPlayer.IsUserInVR() ? "Wired VR" : "Desktop";
        }
    }
    public void UseWireless()
    {
        if (!wirelessPreset)
        {
            wirelessPreset = true;
            displayStateOffset.text = Networking.LocalPlayer.IsUserInVR() ? "Wireless VR" : "Cloud PC";

        }
    }
    public void UpdateOffset()
    {
        float newoffset = offsetAssign.value;
        newoffset = Mathf.Round(newoffset * 1000f) / 1000f;
        globalOffset = newoffset;
        offsetDisplay.text = globalOffset.ToString();
    }

    public void UseSimpleChart()
    {
        xmod = stageSelector.defaultMods.xmodS;
        stageSelector.defaultCharts.notes = stageSelector.defaultCharts.simpleChart;

            stageSelector.defaultMods.modsChart = stageSelector.defaultMods.simpleModChart;
        
    }
    public void UseNormalChart()
    {
        xmod = stageSelector.defaultMods.xmodN;
        stageSelector.defaultCharts.notes = stageSelector.defaultCharts.normalChart;

            stageSelector.defaultMods.modsChart = stageSelector.defaultMods.normalModChart;
        
    }
    public void UseHardChart()
    {
        xmod = stageSelector.defaultMods.xmodH;
        stageSelector.defaultCharts.notes = stageSelector.defaultCharts.hardChart;

            stageSelector.defaultMods.modsChart = stageSelector.defaultMods.hardModChart;
        
    }
    public void UseUnsightedChart()
    {
        xmod = stageSelector.defaultMods.xmodU;
        stageSelector.defaultCharts.notes = stageSelector.defaultCharts.unsightedChart;

            stageSelector.defaultMods.modsChart = stageSelector.defaultMods.unsightedModChart;
        
    }

    public void SetSongProperties()
    {
        bpm = stageSelector.stageBPM;
        songOffset = stageSelector.stageOffset;
        _song.clip = stageSelector.stageAudio;
    }

    public void StopSong()
    {
        _song.Stop();
        songPlaying = false;
        playfield1.arrowObjects.DeactiveAll();
        playfield2.arrowObjects.DeactiveAll();

        if (localPlayerData != null)
        {

            localPlayerData.SendCustomNetworkEvent(NetworkEventTarget.All, "HideComboCounter");

        }

        Networking.LocalPlayer.SetRunSpeed(4);
        Networking.LocalPlayer.SetWalkSpeed(2);
        Networking.LocalPlayer.SetStrafeSpeed(2);
        Networking.LocalPlayer.SetJumpImpulse(2);
    }

    public void ResetGame()
    {

        playfield1.receptors.SetActive(false);
        playfield2.receptors.SetActive(false);

        resetTimer = true;
        noMoreArrows = false;

        lastSongBeat = 0;
        currentSongBeat = 0;

        oldSongTime = 0;
        lmao2 = 0;

        comboCounter = 0;
        accScore = 0;
        accuracy = 0;

        currentArrow = 0;
        lastArrowHit = 0;

        playfield1.arrowObjects.nextArrow = 0;
        playfield1.arrowObjects.indexLocation = 0;
        playfield2.arrowObjects.indexLocation = 0;
        playfield2.arrowObjects.nextArrow = 0;

        stageSelector.ResetMods();
    }

}
