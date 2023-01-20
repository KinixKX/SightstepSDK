
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class ModChart : UdonSharpBehaviour
{

    #region Core Elements

    public Mods[] mods;

    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */
    public object[][] modsChart; //This remains empty to be asigned by one of the charts below 
    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */
    public object[][] simpleModChart;
    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */
    public object[][] normalModChart;
    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */
    public object[][] hardModChart;
    [HideInInspector]
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize] /* UdonSharp auto-upgrade: serialization */
    public object[][] unsightedModChart;

    public int indexLocation = 0;
    public int nextMod;
    public int lastModPlayed = 0;
    public float lastSongBeat;

    float lastModBeat = -9999f;

    public SongPlayer song;
    public float xmodS;
    public float xmodN;
    public float xmodH;
    public float xmodU;

    #endregion

    #region Reserved Actors

    public Vector3 _defaultPlayfieldPosition;

    public Playfield P1;
    public Playfield P2;

    public GameObject P1Judgement;
    public GameObject P2Judgement;
    public GameObject P3Judgement;
    public GameObject P4Judgement;
    public GameObject P1Combo;
    public GameObject P2Combo;
    public GameObject P3Combo;
    public GameObject P4Combo;

    public GameObject BG;
    public GameObject Lights;

    #endregion

    #region Reserved Parameters

    public const float receptor_distance = .640f;

    #endregion

    /* BUGS:
     * 
     * The rotation of the arrows is determined by the rotation of the receptor in that frame. This causes issues on funny mods that rotate the receptors very rapidly
     * Some mods that start as one ends and are affecting the same property of an actor (Say, moveX) will end up causing some drift to the property

        new object[]{295f, 1f, "moveX", 1f, "EaseOut", actor, this,1},
        new object[]{296f, 1f, "moveX", -1f, "EaseOut", actor, this,1},

    This can cause an issue where the position of the object might be something like 0.042069 instead of being a flat 0, since the mod at 296 plays "almost" exactly as when 295 is finishing
    But this is not super precise and causes driting...

     * Also the current way mods are done is basically like a mix of an "ease" and "add" where you are adding to the property of the actor, but you are not able to stack mods that do the same
    So if you do two moveY, the first mod will get overwritten by the second mod if they overlap in duration

     * It's not possible to do an instant mod, or anything with duration 0 since the mods apply the function based on mod.progress / mod.duration, either resulting in a 0/0 or 1/0

    */

    //TODO: Find a way to do good rotations around something ;_;

    //This resets a given gameobject to 0,0,0! But not all gameobjects should be at 0,0,0 by default. Read the next function on what I would love my approach to be.

    public void IncrementProgress(Mods modifier)
    {
        //modifier.progress += Time.deltaTime * modifier.bpmMult;

        modifier.progress = (song.currentSongBeat) - modifier.beatPivot; //This wont work for iterative mods yet

        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }
    }

    public void CheckDuration(Mods modifier, bool recalculate = true)
    {
        if (modifier.progress != modifier.duration)
            return;

        if (modifier.iteration >= modifier.repetitions || modifier.duration != modifier.frequency)
        {
            modifier.playing = false;
            return;
        }

        modifier.progress = 0;
        modifier.playing = true;
        modifier.beatPivot = modifier.beat + modifier.frequency * modifier.iteration;

        if (recalculate && modifier.addValues)
            modifier.RecalculateValues();
    }

    public void resetPosition(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localPosition = Vector3.LerpUnclamped(modifier.currentPos, Vector3.zero, perc);

        CheckDuration(modifier);
    }

    public void resetRotation(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localRotation = Quaternion.SlerpUnclamped(modifier.originalRot, Quaternion.identity, perc);

        CheckDuration(modifier);
    }

    //This is exclusively for the playfield. I need to find a way to be able to have a global resetPosition function, and searches for the respective default position of the gameobject's name
    //Using a constant with a name, that way you don't have to be estimating where the gameobjects are and give them the needed value to "reset" them to their regular positions
    public void resetPlayfield(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.transform.localPosition = Vector3.LerpUnclamped(modifier.currentPos, _defaultPlayfieldPosition, perc);

        CheckDuration(modifier);
    }

    public void moveX(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float x = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(x, actor.transform.localPosition.y, actor.transform.localPosition.z);

        CheckDuration(modifier);
    }
    public void moveY(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float y = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(actor.transform.localPosition.x, y, actor.transform.localPosition.z);

        CheckDuration(modifier);
    }
    public void moveZ(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;
        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float z = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(actor.transform.localPosition.x, actor.transform.localPosition.y, z);

        CheckDuration(modifier);
    }

    public void rotateX(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(val, actor.transform.localRotation.eulerAngles.y, actor.transform.localRotation.eulerAngles.z);

        CheckDuration(modifier);
    }
    public void rotateY(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(actor.transform.localRotation.eulerAngles.x, val, actor.transform.localRotation.eulerAngles.z);

        CheckDuration(modifier);
    }
    public void rotateZ(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(actor.transform.localRotation.eulerAngles.x, actor.transform.localRotation.eulerAngles.y, val);

        CheckDuration(modifier);
    }

    public void scale(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);
        actor.transform.localScale = Vector3.LerpUnclamped(modifier.originalScl, modifier.finalScl, perc);

        CheckDuration(modifier);
    }
    public void scaleX(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.progress / modifier.duration;
        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(Mathf.LerpUnclamped(modifier.originalScl.x, modifier.finalScl.x, perc), actor.transform.localScale.y, actor.transform.localScale.z);

        CheckDuration(modifier);
    }
    public void scaleY(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(actor.transform.localScale.x, Mathf.LerpUnclamped(modifier.originalScl.y, modifier.finalScl.y, perc), actor.transform.localScale.z);

        CheckDuration(modifier);
    }
    public void scaleZ(Mods modifier, GameObject actor)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(actor.transform.localScale.x, actor.transform.localScale.y, Mathf.LerpUnclamped(modifier.originalScl.z, modifier.finalScl.z, perc));

        CheckDuration(modifier);
    }

    public void playAnimation(Mods modifier, Animator anim)
    {
        anim.Play(modifier.param);
        if (modifier.smoothing != "")
        {
            anim.SetFloat(modifier.smoothing, ((song.bpm / 60f) / modifier.duration));
        }

        modifier.progress = modifier.duration;
        modifier.iteration++;

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void setAnimFloat(Mods modifier, Animator anim)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        anim.SetFloat(modifier.param, Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc));

        CheckDuration(modifier);
    }

    public void playParticles(Mods modifier, ParticleSystem ps)
    {
        ps.Play();

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }

    public void stopParticles(Mods modifier, ParticleSystem ps)
    {
        ps.Stop();

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }

    public void toggleObject(Mods modifier, GameObject actor)
    {
        actor.SetActive(!actor.activeSelf);

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }
    public void togglePlayfield(Mods modifier, Playfield pf)
    {
        pf.ToggleElements();

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }

    //TODO: Find a way to make this mod work
    public void rotateAround(Mods modifier, GameObject actor)
    {
        modifier.progress += Time.deltaTime * modifier.bpmMult;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.progress += Time.deltaTime * modifier.bpmMult;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localPosition = Vector3.SlerpUnclamped(modifier.currentPos, modifier.finalTrans.localPosition, perc);
        actor.transform.localRotation = Quaternion.SlerpUnclamped(modifier.originalRot, modifier.finalTrans.localRotation, perc);
    }

    public void pathType(Mods modifier, Playfield pf)
    {
        switch (modifier.function)
        {
            case "pathTypeX":
                pf.arrowPath[0] = modifier.ease;
                break;
            case "pathTypeY":
                pf.arrowPath[1] = modifier.ease;
                break;
            case "pathTypeZ":
                pf.arrowPath[2] = modifier.ease;
                break;
        }

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }
    public void pathMagnitude(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "pathMagX":
                pf.pathMagnitude.x = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathMagY":
                pf.pathMagnitude.y = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathMagZ":
                pf.pathMagnitude.z = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        CheckDuration(modifier);
    }
    public void pathFrequency(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "pathFreqX":
                pf.pathFrequency.x = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathFreqY":
                pf.pathFrequency.y = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathFreqZ":
                pf.pathFrequency.z = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        CheckDuration(modifier);
    }

    public void dark(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.dark = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetDark();

        CheckDuration(modifier);
    }
    public void stealth(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.stealth = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetStealth();

        CheckDuration(modifier);
    }
    public void explode(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.explode = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetExplode();

        CheckDuration(modifier);
    }
    public void whiteout(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.whiteOut = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetWhiteOut();

        CheckDuration(modifier);
    }

    public void arrowRotation(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "arrowRotationX":
                pf.arrowRotationX = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "arrowRotationY":
                pf.arrowRotationY = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "arrowRotationZ":
                pf.arrowRotationZ = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        pf.SetArrowRotations();

        CheckDuration(modifier);
    }
    public void arrowSize(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "arrowSizeX":
                pf.arrowSizeX = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "arrowSizeY":
                pf.arrowSizeY = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "arrowSizeZ":
                pf.arrowSizeZ = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        pf.SetArrowSize();

        CheckDuration(modifier);
    }

    //TODO: Find a way to make these mods work
    public void invert(Mods modifier, Playfield pf)
    {
        modifier.progress += Time.deltaTime * modifier.bpmMult;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += Time.deltaTime * modifier.bpmMult;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.invert = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetInvert();
    }
    public void flip(Mods modifier, Playfield pf)
    {
        modifier.progress += Time.deltaTime * modifier.bpmMult;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += Time.deltaTime * modifier.bpmMult;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.flip = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetFlip();
    }
    public void reverse(Mods modifier, Playfield pf)
    {
        modifier.progress += Time.deltaTime * modifier.bpmMult;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += Time.deltaTime * modifier.bpmMult;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.reverse = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetReverse();
    }

    public void fadeStart(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "fadeXStart":
                pf.fadeXStart = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetXFadeStart();
                break;
            case "fadeYStart":
                pf.fadeYStart = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetYFadeStart();
                break;
            case "fadeZStart":
                pf.fadeZStart = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetZFadeStart();
                break;
        }

        CheckDuration(modifier);
    }
    public void fadeEnd(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "fadeXEnd":
                pf.fadeXEnd = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetXFadeEnd();
                break;
            case "fadeYEnd":
                pf.fadeYEnd = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetYFadeEnd();
                break;
            case "fadeZEnd":
                pf.fadeZEnd = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                pf.SetZFadeEnd();
                break;
        }

        CheckDuration(modifier);
    }

    public void fadeEdge(Mods modifier, Playfield pf)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);

        pf.fadeEdge = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetFadeWidth();

        CheckDuration(modifier);
    }

    /// TEST OTHER SONGS CUZ OH GOD THIS IS SCARY TO MESS WITH. No really I am afraid this could EASILY break some stuff
    public void bpm(Mods modifier)
    {
        song.bpm = modifier.magnitude;

        modifier.progress = modifier.duration;
        modifier.iteration++;

        CheckDuration(modifier, false);
    }


    public void xmod(Mods modifier)
    {
        IncrementProgress(modifier);

        float perc = modifier.duration != 0 ? modifier.progress / modifier.duration : 1;

        perc = ApplySmoothing(perc, modifier.ease, modifier.flipMotion, modifier.motionFreq);
        song.xmod = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);

        CheckDuration(modifier);
    }

    public void InitializeBPMMult(float bpmMult)
    {
        for (int i = 0; i < mods.Length; i++)
        {
            mods[i].InitializeBPMMult(bpmMult);
        }
    }

    public void Update()
    {
        if (song == null) { return; }
        if (!song.songPlaying) { return; }

        /*for (int i = 0; i < mods.Length; i++) // This alone takes 5ms to run
        {
            mods[i].CheckUpdates();
        }*/

        if (lastModPlayed < modsChart.Length) // we still have mods to load
        {
            //Delay from running on another script
            if (lastSongBeat + (song.beatsPerSecond * 0.01f) > (float)modsChart[lastModPlayed][0])
            {
                for (int i = 0; i <= modsChart.Length; i++)
                {
                    if (lastModPlayed + i > modsChart.Length - 1)
                    {
                        Debug.Log("Last mod loaded 1");
                        lastModPlayed += i;
                        break;
                    }

                    if ((float)modsChart[lastModPlayed][0] == (float)modsChart[i + lastModPlayed][0])
                    {
                        Mods m = GetMod();
                        LoadMod(m, i + lastModPlayed);
                        m.StartMod();

                    }
                    else
                    {
                        lastModPlayed += i;
                        break;
                    }
                }
            }
        }

        lastSongBeat = song.currentSongBeat;
    }

    //How mods can be done:

    // 6 params                          new object[]{ 16f, .2f, "moveX", .5f, "Bounce", P1.receptorsRoot(, 4, 2f, false)},                                                       -- BASIC MODS
    // 6 params                          new object[]{ 30f, 2f, "stealth", 50, "Linear", P1.gameObject (, 2, .5f, true)}                                                                    -- SPECIAL MODS, DIRECTLY AFFECT A PLAYFIELD.CS
    // 6 params                          new object[]{ 30f, 2f, "pathFrequencyX", 50, "Linear", P1.gameObject (, 2, .5f)}     
    // 5 params, magnitude = smoothing2  new object[]{ 30f, 2f, "pathTypeX", "EaseIn", P1.gameObject (, 2, .5f)}     
    // 6 params + 1 vector               new object[]{ 30f, 2f, "rotateAround", 35, "Linear", P1.receptorsRoot, new Vector3(2,3,4) (, 2, .5f)}                             -- MODS THAT USE 2 ACTORS
    // 6 params + 1 gameobject           new object[]{ 30f, 2f, "rotateAround", 35, "Linear", P1.receptorsRoot, P2.receptorsRoot (, 2, .5f)}                               -- MODS THAT USE VECTOR 3
    // 6 params, smoothing = animation   new object[]{ 30f, 2f, "playAnimation", (magnitude removed), "FadeToWhite", backgroundAnimations, "speedParameter"(, 2, .5f)}     -- ANIMATIONS
    // 7 params                          new object[]{ 30f, 2f, "setAnimFloat", 1f, "Float", backgroundAnimations, "speedParameter"(, 2, .5f)}                             -- ANIMATIONS
    // 4 params                          new object[]{ 28f, 0.5f, "playParticles", (magnitude AND smoothing removed), postArrowR(, 4, 1f)},                                -- PARTICLES
    // 4 params                          new object[]{ 30f, 2f, "toggleObject", (magnitude AND smoothing removed), P1.receptorsRoot(, 2, .5f)}                             -- TOGGLE OBJECTS
    // 4?                                new object[]{ 4f, 0f, "setBPM", 45.5f}
    // 4?                                new object[]{ 4f, 0f, "xmod", 45.5f, "Linear"}

    public void LoadMod(Mods mod, int index)
    {
        mod.mods = this;

        object[] modData = modsChart[index];

        if (modData[0].GetType() != typeof(float)) { Debug.LogError($"MOD ERROR: 'beat' at {modData[0]} on mod {index} is not a float!"); }
        mod.beat = (float)modData[0];
        if (modData[1].GetType() != typeof(float)) { Debug.LogError($"MOD ERROR: 'duration' at {modData[0]} on mod {index} is not a float!"); }
        mod.duration = (float)modData[1];
        mod.function = (string)modData[2];
        mod.magnitude = 0f;

        if (mod.beat < lastModBeat) { Debug.LogError($"MOD ERROR: Last mod beat was {lastModBeat} and next mod {mod.function} beat is {mod.beat} !"); }
        else { lastModBeat = mod.beat; }


        if (modData[3].GetType() == typeof(float) || modData[3].GetType() == typeof(int))
        {
            if (modData[3].GetType() != typeof(float)) { Debug.LogWarning($"MOD WARNING: 'magnitude' at {modData[0]} on mod {index} is not a float! This can cause bugs in your modchart."); }
            mod.magnitude = (float)modData[3];

            mod.smoothing = (string)modData[4];
            if (mod.function != "xmod")
            {
                mod.actor = (GameObject)modData[5];

                if (modData.Length >= 7)
                {
                    if (modData[6].GetType() == typeof(GameObject))
                    {
                        mod.objectReference = (GameObject)modData[6];
                        mod.pointVector = mod.objectReference.transform.position;
                    }
                    else if (modData[6].GetType() == typeof(Vector3))
                    {
                        mod.pointVector = (Vector3)modData[6];
                    }
                    else if (modData[6].GetType() == typeof(string)) //SetFloat cuz its a string
                    {
                        mod.param = (string)modData[6];
                    }
                }

            }
        }
        else if (modData[3].GetType() == typeof(GameObject))
        {
            //Actor OR ParticleSystem
            mod.actor = (GameObject)modData[3];
        }
        else //String
        {
            //PathType OR AnimationState
            mod.param = (string)modData[3];

            if (modData[4].GetType() == typeof(GameObject))
            {
                mod.actor = (GameObject)modData[4];

                if (modData.Length >= 6)
                {
                    if (modData[5].GetType() == typeof(string)) //If they don't include speed parameter  
                    {
                        mod.smoothing = (string)modData[5];
                    }
                    else
                    {
                        mod.smoothing = string.Empty;
                    }
                }
                else
                {
                    mod.smoothing = string.Empty;
                }
            }
        }

        //Check for repetitions!
        if (modData[modData.Length - 1].GetType() == typeof(float) && modData[modData.Length - 2].GetType() == typeof(int))
        {
            mod.repetitions = (int)modData[modData.Length - 2];
            mod.frequency = (float)modData[modData.Length - 1];
            mod.addValues = false;
        }
        else if (modData[modData.Length - 1].GetType() == typeof(bool))
        {
            mod.repetitions = (int)modData[modData.Length - 3];
            mod.frequency = (float)modData[modData.Length - 2];
            mod.addValues = (bool)modData[modData.Length - 1];
        }
        else
        {
            mod.repetitions = 1;
            mod.frequency = 0;
            mod.addValues = false;
        }
    }

    public float ApplySmoothing(float t, int type, bool flip, float mag)
    {
        float ease = EasingFunctions.Ease(type, t, mag);
        return flip ? (1 - ease) : ease;
    }

    public Mods GetMod()
    {
        if (indexLocation >= mods.Length - 1)
        {
            indexLocation = 1;
            if (mods[indexLocation - 1].repetitions == mods[indexLocation - 1].iteration && !mods[indexLocation - 1].playing) //If the mod is on the same iteration and it is done playing, return that
            {
                //Mod 0 is ready to use, dont do anything
            }
            else //Mod 0 is being used, so go around to the next one
            {
                for (int i = indexLocation; i < mods.Length - 1; i++)
                {
                    if (!mods[i].playing && mods[i].repetitions == mods[i].iteration)
                    {
                        indexLocation = i;
                        return mods[i];
                    }
                }
                Debug.LogError("MOD ERROR: No more mod scripts available!");
            }
        }
        else
        {
            if (mods[indexLocation].repetitions == mods[indexLocation].iteration && !mods[indexLocation].playing) //If the mod is done playing, send over that mod
            {
                indexLocation++;
            }
            else //Mod not done playing, get the next one
            {
                // Debug.Log("Last mod was busy. Looking for mods IL : " + indexLocation);

                for (int i = indexLocation + 1; i < mods.Length; i++)
                {
                    if (mods[i].repetitions == mods[i].iteration && !mods[i].playing)
                    {
                        indexLocation = i;
                        return mods[indexLocation];
                    }
                }

                Debug.LogError("MOD ERROR: No more mod scripts available!"); //This happens when the last mods are 2 or more at the same time!
            }
        }
        return mods[indexLocation - 1];
    }

    public virtual void ResetMods()
    {
        lastModPlayed = 0;
        lastSongBeat = 0;
        indexLocation = 0;
        lastModBeat = -9999;

        P1.ResetPlayfield();
        P2.ResetPlayfield();

        P1Judgement.transform.localPosition = new Vector3(0, 5.25f, 10f);
        P2Judgement.transform.localPosition = new Vector3(0, 5.25f, 10f);
        P3Judgement.transform.localPosition = new Vector3(0, 5.25f, 10f);
        P4Judgement.transform.localPosition = new Vector3(0, 5.25f, 10f);
        P1Combo.transform.localPosition = new Vector3(0, 4.7f, 10f);
        P2Combo.transform.localPosition = new Vector3(0, 4.7f, 10f);
        P3Combo.transform.localPosition = new Vector3(0, 4.7f, 10f);
        P4Combo.transform.localPosition = new Vector3(0, 4.7f, 10f);

        P1Judgement.transform.localRotation = Quaternion.identity;
        P2Judgement.transform.localRotation = Quaternion.identity;
        P3Judgement.transform.localRotation = Quaternion.identity;
        P4Judgement.transform.localRotation = Quaternion.identity;
        P1Combo.transform.localRotation = Quaternion.identity;
        P2Combo.transform.localRotation = Quaternion.identity;
        P3Combo.transform.localRotation = Quaternion.identity;
        P4Combo.transform.localRotation = Quaternion.identity;

        P1Judgement.transform.localScale = Vector3.one;
        P2Judgement.transform.localScale = Vector3.one;
        P3Judgement.transform.localScale = Vector3.one;
        P4Judgement.transform.localScale = Vector3.one;
        P1Combo.transform.localScale = Vector3.one;
        P2Combo.transform.localScale = Vector3.one;
        P3Combo.transform.localScale = Vector3.one;
        P4Combo.transform.localScale = Vector3.one;

        P1.transform.position = _defaultPlayfieldPosition;
        P1.transform.rotation = Quaternion.identity;
        P1.transform.localScale = Vector3.one;

        P2.transform.position = _defaultPlayfieldPosition;
        P2.transform.rotation = Quaternion.identity;
        P2.transform.localScale = Vector3.one;

        BG.SetActive(true);
        BG.transform.localPosition = new Vector3(0, 2.5f, 0);
        BG.transform.rotation = Quaternion.Euler(90, 0, 0);
        BG.transform.localScale = new Vector3(15, 15, 15);

        Lights.SetActive(true);

        foreach (Mods m in mods) //Stop any mods that are playing!   
        {
            m.Restore();
        }

    }

    public virtual void InitMods() { } //Used for override to load modcharts

}
