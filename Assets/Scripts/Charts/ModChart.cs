
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

    float lastModBeat;

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
    public void resetPosition(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localPosition = Vector3.LerpUnclamped(modifier.currentPos, Vector3.zero, perc);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void resetRotation(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localRotation = Quaternion.SlerpUnclamped(modifier.originalRot, Quaternion.identity, perc);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    //This is exclusively for the playfield. I need to find a way to be able to have a global resetPosition function, and searches for the respective default position of the gameobject's name
    //Using a constant with a name, that way you don't have to be estimating where the gameobjects are and give them the needed value to "reset" them to their regular positions
    public void resetPlayfield(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.transform.localPosition = Vector3.LerpUnclamped(modifier.currentPos, _defaultPlayfieldPosition, perc);

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

    public void moveX(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f ;
        if(modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float x = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(x, actor.transform.localPosition.y, actor.transform.localPosition.z);
        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void moveY(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;

        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float y = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(actor.transform.localPosition.x, y, actor.transform.localPosition.z);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void moveZ(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;

        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;

        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }
        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float z = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);
        actor.transform.localPosition = new Vector3(actor.transform.localPosition.x, actor.transform.localPosition.y, z);


        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }   

    public void rotateX(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(val, actor.transform.localRotation.eulerAngles.y, actor.transform.localRotation.eulerAngles.z);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void rotateY(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(actor.transform.localRotation.eulerAngles.x, val, actor.transform.localRotation.eulerAngles.z);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void rotateZ(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;

        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        float val = Mathf.LerpUnclamped(modifier.originalVal, modifier.finalVal, perc);

        actor.transform.localRotation = Quaternion.Euler(actor.transform.localRotation.eulerAngles.x, actor.transform.localRotation.eulerAngles.y, val);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void scale(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;

        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);
        actor.transform.localScale = Vector3.LerpUnclamped(modifier.originalScl, modifier.finalScl, perc);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void scaleX(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = modifier.progress / modifier.duration;
        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(Mathf.LerpUnclamped(modifier.originalScl.x, modifier.finalScl.x, perc), actor.transform.localScale.y, actor.transform.localScale.z);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void scaleY(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;

        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(actor.transform.localScale.x, Mathf.LerpUnclamped(modifier.originalScl.y, modifier.finalScl.y, perc), actor.transform.localScale.z);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void scaleZ(Mods modifier, GameObject actor)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localScale = new Vector3(actor.transform.localScale.x, actor.transform.localScale.y, Mathf.LerpUnclamped(modifier.originalScl.z, modifier.finalScl.z, perc));

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
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
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;

        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        anim.SetFloat(modifier.param, Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc));

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void playParticles(Mods modifier, ParticleSystem ps)
    {
        ps.Play();

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

    public void toggleObject(Mods modifier, GameObject actor)
    {
        actor.SetActive(!actor.activeSelf);

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
    public void togglePlayfield(Mods modifier, Playfield pf)
    {
        pf.ToggleElements();

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

    //TODO: Find a way to make this mod work
    public void rotateAround(Mods modifier, GameObject actor) 
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.progress += (Time.deltaTime * song.bpm) / 60f;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        actor.transform.localPosition = Vector3.SlerpUnclamped(modifier.currentPos, modifier.finalTrans.localPosition, perc);
        actor.transform.localRotation = Quaternion.SlerpUnclamped(modifier.originalRot, modifier.finalTrans.localRotation, perc);
    }

    public void pathType(Mods modifier, Playfield pf)
    {
        switch(modifier.function)
        {
            case "pathTypeX":
                pf.arrowPathX = modifier.param;
                break;
            case "pathTypeY":
                pf.arrowPathY = modifier.param;
                break;
            case "pathTypeZ":
                pf.arrowPathZ = modifier.param;
                break;
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
    public void pathMagnitude(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "pathMagX":
                pf.pathMagnitudeX = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathMagY":
                pf.pathMagnitudeY = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathMagZ":
                pf.pathMagnitudeZ = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void pathFrequency(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        switch (modifier.function)
        {
            case "pathFreqX":
                pf.pathFrequencyX = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathFreqY":
                pf.pathFrequencyY = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
            case "pathFreqZ":
                pf.pathFrequencyZ = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
                break;
        }

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void dark(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.dark = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetDark();

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void stealth(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.stealth = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetStealth();

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void explode(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.explode = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetExplode();

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void whiteout(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.whiteOut = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetWhiteOut();

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void arrowRotation(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

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

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void arrowSize(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

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

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    //TODO: Find a way to make these mods work
    public void invert(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += (Time.deltaTime * song.bpm) / 60f;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.invert = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetInvert();
    }
    public void flip(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += (Time.deltaTime * song.bpm) / 60f;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.flip = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetFlip();
    }
    public void reverse(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency && modifier.progress == modifier.duration)
            {
                modifier.progress = 0;
                modifier.progress += (Time.deltaTime * song.bpm) / 60f;
                modifier.playing = true;
            }
            else
            {
                modifier.playing = false;
            }
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.reverse = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetReverse();
    }

    public void fadeStart(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

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

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }
    public void fadeEnd(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

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

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void fadeEdge(Mods modifier, Playfield pf)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;
        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);

        pf.fadeEdge = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);
        pf.SetFadeWidth();

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    /// TEST OTHER SONGS CUZ OH GOD THIS IS SCARY TO MESS WITH. No really I am afraid this could EASILY break some stuff
    public void bpm(Mods modifier)
    {
        song.bpm = modifier.magnitude;

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
    
    /// THIS HAS YET TO BE TESTED OH CRAP
    public void xmod(Mods modifier)
    {
        modifier.progress += (Time.deltaTime * song.bpm) / 60f;
        if (modifier.progress > modifier.duration)
        {
            modifier.iteration++;
            modifier.progress = modifier.duration;

        }

        float perc = 0;
        if (modifier.duration != 0)
        {
            perc = modifier.progress / modifier.duration;
        }
        else
        {
            perc = 1;
        }

        perc = ApplySmoothing(perc, modifier.motion, modifier.flipMotion, modifier.motionFreq);
        song.xmod = Mathf.LerpUnclamped(modifier.originalFloat, modifier.magnitude, perc);

        if (modifier.progress == modifier.duration)
        {
            if (modifier.iteration < modifier.repetitions && modifier.duration == modifier.frequency)
            {
                modifier.progress = 0;
                modifier.playing = true;

                if (modifier.addValues)
                    modifier.RecalculateValues();
            }
            else
            {
                modifier.playing = false;
            }

        }
    }

    public void CheckUpdate()
    {
        if(song == null) { return; }
        if (!song.songPlaying) { return; }

        for(int i = 0; i < mods.Length; i++)
        {
            mods[i].CheckUpdates();
        }

        if (lastModPlayed <= modsChart.Length - 1 )
        {

            if (lastModPlayed > modsChart.Length)
            {
                Debug.Log("Last mod loaded 2"); //Don't know why I have this
                lastModPlayed++;
            }

            if (lastSongBeat > (float)modsChart[lastModPlayed][0])
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
        if(modsChart[index][0].GetType() !=  typeof(float)) { Debug.LogError($"MOD ERROR: 'beat' at {modsChart[index][0]} on mod {index} is not a float!"); }
        mod.beat = (float)modsChart[index][0];
        if (modsChart[index][1].GetType() != typeof(float)) { Debug.LogError($"MOD ERROR: 'duration' at {modsChart[index][0]} on mod {index} is not a float!"); }
        mod.duration = (float)modsChart[index][1];
        mod.function = (string)modsChart[index][2];
        mod.magnitude = 0f;
        
        if(mod.beat < lastModBeat) { Debug.LogError($"MOD ERROR: Last mod beat was {lastModBeat} and next mod {mod.function} beat is {mod.beat} !"); }
        else { lastModBeat = mod.beat; }
 

        if (modsChart[index][3].GetType() == typeof(float) || modsChart[index][3].GetType() == typeof(int))
        {
            if(modsChart[index][3].GetType() != typeof(float)) { Debug.LogWarning($"MOD WARNING: 'magnitude' at {modsChart[index][0]} on mod {index} is not a float! This can cause bugs in your modchart."); }
            mod.magnitude = (float)modsChart[index][3];
            if (mod.function != "setBPM") //Special edge case for set bpm... because ofc
            {          
                mod.smoothing = (string)modsChart[index][4];
                mod.actor = (GameObject)modsChart[index][5];

                if (modsChart[index].Length >= 7)
                {
                    if (modsChart[index][6].GetType() == typeof(GameObject))
                    {
                        mod.objectReference = (GameObject)modsChart[index][6];
                        mod.pointVector = mod.objectReference.transform.position;
                    }
                    else if (modsChart[index][6].GetType() == typeof(Vector3))
                    {
                        mod.pointVector = (Vector3)modsChart[index][6];
                    }
                    else if (modsChart[index][6].GetType() == typeof(string)) //SetFloat cuz its a string
                    {
                        mod.param = (string)modsChart[index][6];
                    }
                }
            }
        }
        else if (modsChart[index][3].GetType() == typeof(GameObject))
        {
            //Actor OR ParticleSystem
            mod.actor = (GameObject)modsChart[index][3];
        }
        else //String
        {
            //PathType OR AnimationState
            mod.param = (string)modsChart[index][3];

            if (modsChart[index][4].GetType() == typeof(GameObject))
            {
                mod.actor = (GameObject)modsChart[index][4];

                if (modsChart[index].Length >= 6)
                {
                    if (modsChart[index][5].GetType() == typeof(string)) //If they don't include speed parameter  
                    {
                        mod.smoothing = (string)modsChart[index][5];
                    }
                    else
                    {
                        mod.smoothing = "";
                    }
                }
                else
                {
                    {
                        mod.smoothing = "";
                    }
                }
            }
        }

        //Check for repetitions!
        if (modsChart[index][modsChart[index].Length - 1].GetType() == typeof(float) && modsChart[index][modsChart[index].Length - 2].GetType() == typeof(int))
        {
            mod.repetitions = (int)modsChart[index][modsChart[index].Length - 2];
            mod.frequency = (float)modsChart[index][modsChart[index].Length - 1];
            mod.addValues = false;
        }
        else if (modsChart[index][modsChart[index].Length - 1].GetType() == typeof(bool))
        {
            mod.repetitions = (int)modsChart[index][modsChart[index].Length - 3];
            mod.frequency = (float)modsChart[index][modsChart[index].Length - 2];
            mod.addValues = (bool)modsChart[index][modsChart[index].Length - 1];
        }
        else
        {
            mod.repetitions = 1;
            mod.frequency = 0;
            mod.addValues = false;
        }
    }

    public float ApplySmoothing(float t, string type, bool flip, float mag)
    {

        switch (type)
        {
            case "Linear":
                break;
            case "Float":
                t = Mathf.Sin(Mathf.PI * t * 2 * mag);
                break;          
            case "Bounce": //Sine-like bounce, smoother
                t = Mathf.Sin(Mathf.PI * t);
                break; 
            case "BounceV2": //Sharper bounce
                t = (-t * Mathf.Exp(1) * 1.457f * (t - 1));
                break;
            case "Pull": //Sustained bounce? Similar to Bell from Mirin's eases
                t = t < 0.5
                ? (Mathf.Pow(2*t-1,3) + 1)
                : (-Mathf.Pow(2 * t - 1, 3) + 1);
                break;

            case "WhipOut": //Progressively whips the object, starts at 0 and ends at 1
                t = t * Mathf.Cos(Mathf.PI * (t - 1) * mag);
                break;
            case "WhipIn": //Use .5 values on mag to start at 0 with strong motions and end at 0 with weaker motions. Use whole values too start at -1 or 1 and end at 0
                t = (1 - t) * Mathf.Cos(Mathf.PI * (t - 1) * mag);
                break;
            case "WhipInOut": //Any values work on mag work, it progressive shakes more and then slows down, starts at 0 ends at 0
                t = (1 - t) * Mathf.Cos(Mathf.PI * t * mag) * t * 4;
                break;

            case "EaseOvershoot": //Always use even and half numbers to ensure proper behaviour (2.5, 4.5, 6,5...etc) on mag. The starting value is (should) be 0 and the end value is 1.
                t = (1 - t) * Mathf.Sin(Mathf.PI * (t - 1) * mag) + 1;
                break;

            case "InstantIn": //The starting value is 1 and the end is 0. Deprecated, we can use a flipped EaseOut...
                t = Mathf.Cos((Mathf.PI * t) / 2); 
                break;
            case "EaseOut":
                t = 1 - Mathf.Cos((t * Mathf.PI) / 2);
                break;
            case "EaseIn":
                t = Mathf.Sin(t * Mathf.PI * 0.5f);
                break;
            case "EaseInOut":
                t = -(Mathf.Cos(Mathf.PI * t) - 1) / 2;
                break;

            case "EaseInCirc":
                t = 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
                break;
            case "EaseOutCirc":
                t = Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
                break;
            case "EaseInOutCirc":
                t = t < 0.5
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
                break;

            case "EaseInQuad":
                t = t * t;
                break;
            case "EaseOutQuad":
                t = 1 - (1 - t) * (1 - t);
                break;
            case "EaseInOutQuad":
                t = t < 0.5 ? 2 * t * t : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
                break;

            case "EaseInExpo":
                t = t == 0 ? 0 : Mathf.Pow(2, 10 * t - 10);
                break;
            case "EaseOutExpo":
                t = t == 1 ? 1 : 1 - Mathf.Pow(2, -10 * t);
                break;
            case "EaseInOutExpo":
                t = t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : t < 0.5 ? Mathf.Pow(2, 20 * t - 10) / 2
                  : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
                break;

            case "EaseInBack":
                t = 2.70158f * t * t * t - 1.70158f * t * t;
                break;
            case "EaseOutBack":
                t = 1 + 2.70158f * Mathf.Pow(t - 1, 3) + 1.70158f * Mathf.Pow(t - 1, 2);
                break;
            case "EaseInOutBack":
                float c1 = 1.70158f;
                float c2 = c1 * 1.525f;

                t = t < 0.5
                  ? (Mathf.Pow(2 * t, 2) * ((c2 + 1) * 2 * t - c2)) / 2
                  : (Mathf.Pow(2 * t - 2, 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
                break;

                //TODO: Implement magnitude that can affect the amount of bounce for these functions
            case "EaseInElastic":
                float c5 = (2 * Mathf.PI) / 3;

                t = t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * c5);
                break;
            case "EaseOutElastic":
                float c6 = (2 * Mathf.PI) / 3;
                t = t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * c6) + 1;
                break;
            case "EaseInOutElastic":
                float c7 = (2f * Mathf.PI) / 4.5f;
                t = t == 0
                  ? 0
                  : t == 1
                  ? 1
                  : t < 0.5
                  ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * c7)) / 2
                  : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * c7)) / 2 + 1;
                break;

            case "Inverse":
                t = Mathf.Tan(t * Mathf.PI) / 10f;
                break;
            default:
                Debug.Log("Motion function wasn't found " + type);
                break;
        }

        return flip ? (1 - t) : t;

    }

    public Mods GetMod()
    {
        if (indexLocation >= mods.Length - 1)
        {
            indexLocation = 1;
            if(mods[indexLocation - 1].repetitions == mods[indexLocation - 1].iteration && !mods[indexLocation -1].playing) //If the mod is on the same iteration and it is done playing, return that
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
            if(mods[indexLocation].repetitions == mods[indexLocation].iteration && !mods[indexLocation].playing) //If the mod is done playing, send over that mod
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
        lastModBeat = 0;

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

        foreach(Mods m in mods) //Stop any mods that are playing!   
        {
            m.Restore();
        }

    }

    public virtual void InitMods() { } //Used for override to load modcharts

}
