
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Mods : UdonSharpBehaviour
{
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public float beat;
    public float duration;
    public string function;
    public int modFunc;
    public float magnitude;
    public GameObject actor;

    public string smoothing;
    public string motion;
    public bool flipMotion;
    public float motionFreq;

    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public bool playing;
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public float waitTimer;
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public float progress = 1;
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public int repetitions; //how many times does it have to play
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public int iteration; //Which iteration are we in
    [VRC.Udon.Serialization.OdinSerializer.OdinSerialize]
    public float frequency = 0;
    public bool addValues;
    public float originalMag;

    public string param;

    public float originalVal;
    public float finalVal;

    public float originalFloat;

    public Vector3 currentPos;
    public Vector3 originalScl;
    public Vector3 finalScl;

    public Vector3 pointVector;
    public GameObject objectReference;

    public Quaternion originalRot;

    public Transform originalTrans;
    public Transform finalTrans;

    public Animator anim;
    public ParticleSystem particles;
    public Playfield playfield;
    public SongPlayer song;

    public ModChart mods;

    public void CheckUpdates()
    {
        if (playing)
        {          
            waitTimer += (Time.deltaTime * mods.song.bpm) / 60f;
            PlayMod();
        }
        else if(iteration < repetitions && !playing && progress == duration)
        {
            //Tick down the timer for the next repetition
            waitTimer += (Time.deltaTime * mods.song.bpm) / 60f;
            if(waitTimer >= frequency)

            {   ///For some reason, using waitTimer = 0 slowly desyncs repetition mods????? (Seen in Cyber's World) 
                ///But seems like this causes mods to not play at the same time when multiple mods are made on the same beat?? This logic makes no sense since the value shouldn't be adding up over time or getting done earlier??
                waitTimer -= frequency; 
                playing = true;
                progress = 0;

                //New iteration, check for addValues if we want to update the final value as the new original and the final value updates to a new value
                if (addValues)
                {
                    RecalculateValues();
                }

                PlayMod();
            }
        }
        else
        {
            //No more repetitions left
        }
    }

    public void PlayMod()
    {
        switch (modFunc)
        {
            case 0:
                mods.moveX(this, actor);
                break;
            case 1:
                mods.moveY(this, actor);
                break;
            case 2:
                mods.moveZ(this, actor);
                break;
            case 3:
                mods.rotateX(this, actor);
                break;
            case 4:
                mods.rotateY(this, actor);
                break;
            case 5:
                mods.rotateZ(this, actor);
                break;
            case 6:
                mods.scale(this, actor);
                break;
            case 7:
                mods.scaleX(this, actor);
                break;
            case 8:
                mods.scaleY(this, actor);
                break;
            case 9:
                mods.scaleZ(this, actor);
                break;
            case 10:
                mods.playAnimation(this, anim);
                break;
            case 11:
                mods.playParticles(this, particles);
                break;
            case 12:
                mods.resetPlayfield(this, playfield);
                break;
            case 13:
                mods.resetPosition(this, actor);
                break;
            case 14:
                mods.resetRotation(this, actor);
                break;
            case 15:
                mods.toggleObject(this, actor);
                break;
            case 16:
                mods.togglePlayfield(this, playfield);
                break;
            case 17:
                mods.rotateAround(this, actor);
                break;
            case 18:
                mods.rotateAround(this, actor);
                break;
            case 19:
                mods.rotateAround(this, actor);
                break;
            case 20:
                mods.pathType(this, playfield); //All PathTypeX,Y,Z
                break;
            case 23:
                mods.pathMagnitude(this, playfield); //All PathMagX,Y,Z
                break;
            case 26:
                mods.pathFrequency(this, playfield); //All PathFreqX,Y,Z
                break;
            case 29:
                mods.dark(this, playfield);
                break;
            case 30:
                mods.stealth(this, playfield);
                break;
            case 31:
                mods.explode(this, playfield);
                break;
            case 32:
                mods.whiteout(this, playfield);
                break;
            case 33:
                mods.arrowRotation(this, playfield); //All arrowRotationX,Y,Z
                break;
            case 36:
                mods.reverse(this, playfield);
                break;
            case 37:
                mods.flip(this, playfield);
                break;
            case 38:
                mods.invert(this, playfield);
                break;
            case 39:
                mods.arrowSize(this, playfield); //All arrowSizeX,Y,Z
                break;
            case 42:
                mods.setAnimFloat(this, anim);
                break;
            case 43:
                mods.bpm(this);
                break;
            case 44:
                mods.xmod(this);
                break;
            case 45:
                mods.fadeStart(this, playfield);
                break;
            case 48:
                mods.fadeEnd(this, playfield);
                break;
            case 49:
                mods.fadeEdge(this, playfield);
                break;
            default:
                //???
                break;
        }
    }

    public void RecalculateValues()
    {
        switch (function)
        {
            case "moveX":
                originalVal = actor.transform.localPosition.x;
                finalVal = actor.transform.localPosition.x + magnitude;
                break;
            case "moveY":
                originalVal = actor.transform.localPosition.y;
                finalVal = actor.transform.localPosition.y + magnitude;
                break;
            case "moveZ":
                originalVal = actor.transform.localPosition.z;
                finalVal = actor.transform.localPosition.z + magnitude;
                break;
            case "rotateX":
                originalVal = actor.transform.localRotation.eulerAngles.x;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.x;
                break;
            case "rotateY":
                originalVal = actor.transform.localRotation.eulerAngles.y;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.y;
                break;
            case "rotateZ":
                originalVal = actor.transform.localRotation.eulerAngles.z;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.z;
                break;
            case "scale":
                originalScl = actor.transform.localScale;
                finalScl = originalScl + (Vector3.one * magnitude);
                break;
            case "scaleX":
                originalScl = actor.transform.localScale;
                finalScl = originalScl + (Vector3.one * magnitude);
                break;
            case "scaleY":
                originalScl = actor.transform.localScale;
                finalScl = originalScl + (Vector3.one * magnitude);
                break;
            case "scaleZ":
                originalScl = actor.transform.localScale;
                finalScl = originalScl + (Vector3.one * magnitude);
                break;
            case "resetPosition":
                currentPos = actor.transform.localPosition;
                break;
            case "resetRotation":
                originalRot = actor.transform.localRotation;
                break;
            case "pathMagX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeX;
                magnitude = originalFloat + originalMag;
                break;
            case "pathMagY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeY;
                magnitude = originalFloat + originalMag;
                break;
            case "pathMagZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeZ;
                magnitude = originalFloat + originalMag;
                break;
            case "pathFreqX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyX;
                magnitude = originalFloat + originalMag;
                break;
            case "pathFreqY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyY;
                magnitude = originalFloat + originalMag;
                break;
            case "pathFreqZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyZ;
                magnitude = originalFloat + originalMag;
                break;
            case "dark":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.dark;
                magnitude = originalFloat + originalMag;
                break;
            case "stealth":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.stealth;
                magnitude = originalFloat + originalMag;
                break;
            case "explode":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.explode;
                magnitude = originalFloat + originalMag;
                break;
            case "whiteout":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.whiteOut;
                magnitude = originalFloat + originalMag;              
                break;
            case "arrowRotationX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationX;
                magnitude = originalFloat + originalMag;
                break;
            case "arrowRotationY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationY;
                magnitude = originalFloat + originalMag;
                break;
            case "arrowRotationZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationZ;
                magnitude = originalFloat + originalMag;
                break;
            case "reverse": //Unused
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.reverse;
                break;
            case "flip": //Unused
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.flip;
                break;
            case "invert": //Unused
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.invert;
                break;
            case "arrowSizeX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeX;
                magnitude = originalFloat + originalMag;
                break;
            case "arrowSizeY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeY;
                magnitude = originalFloat + originalMag;
                break;
            case "arrowSizeZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeZ;
                magnitude = originalFloat + originalMag;
                break;
            case "setAnimFloat":
                anim = actor.GetComponent<Animator>();
                originalFloat = anim.GetFloat(param);
                magnitude = originalFloat + originalMag;
                break;
            case "setmoveX":
                originalVal = actor.transform.localPosition.x;
                finalVal = magnitude;
                break;
            case "setmoveY":
                originalVal = actor.transform.localPosition.y;
                finalVal = magnitude;
                break;
            case "setmoveZ":
                originalVal = actor.transform.localPosition.z;
                finalVal = magnitude;
                break;
            case "setrotateX":
                originalVal = actor.transform.localRotation.eulerAngles.x;
                finalVal = magnitude;
                break;
            case "setrotateY":
                originalVal = actor.transform.localRotation.eulerAngles.y;
                finalVal = magnitude;
                break;
            case "setrotateZ":
                originalVal = actor.transform.localRotation.eulerAngles.z;
                finalVal = magnitude;
                break;
            case "fadeXStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeXStart;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeYStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeYStart;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeZStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeZStart;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeXEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeXEnd;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeYEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeYEnd;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeZEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeZEnd;
                magnitude = originalFloat + originalMag;
                break;
            case "fadeEdge":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeEdge;
                magnitude = originalFloat + originalMag;
                break;
            default:
                Debug.LogWarning("Mod doesn't support updated iterations: " + function);
                modFunc = -1;
                break;
        }
    }

    public void Restore()
    {
        duration = 0;
        progress = 0;
        playing = false;
        iteration = 0;
        repetitions = 0;
        frequency = 0;
        modFunc = -1;
    }

    public void StartMod()
    {
        //Find a better way to do this?

        waitTimer = 0f;
        progress = 0f;
        playing = true;
        iteration = 0;
        motion = "";
        flipMotion = false;
        motionFreq = 1;
        originalMag = magnitude;

        if(smoothing != "")
        {
            if (smoothing.StartsWith("Flip"))
            {
                flipMotion = true;
            }

            string mag = "";

            if (flipMotion)
            {

                if (smoothing.Contains(","))
                {
                    mag = smoothing.Substring(smoothing.IndexOf(',') + 1, smoothing.Length - smoothing.IndexOf(',') - 1);
                    motionFreq = float.Parse(mag, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                    motion = smoothing.Substring(smoothing.IndexOf(':') + 1, smoothing.IndexOf(',') - smoothing.IndexOf(':') - 1); //smoothing, flip and mag
                }
                else
                {
                    motion = smoothing.Substring(smoothing.IndexOf(':') + 1, smoothing.Length - smoothing.IndexOf(':') - 1); //smoothing, flip, no mag
                }
            }
            else
            {
                if (smoothing.Contains(","))
                {
                    mag = smoothing.Substring(smoothing.IndexOf(',') + 1, smoothing.Length - smoothing.IndexOf(',') - 1);
                    motionFreq = float.Parse(mag, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);

                    motion = smoothing.Substring(0, smoothing.IndexOf(',')); //smoothing and mag, no flip
                }
                else
                {
                    motion = smoothing; //You literally only have the smoothing
                }
            }
        }

        switch (function)
        {
            case "moveX":
                originalVal = actor.transform.localPosition.x;
                finalVal = actor.transform.localPosition.x + magnitude;
                modFunc = 0;
                break;
            case "moveY":
                originalVal = actor.transform.localPosition.y;
                finalVal = actor.transform.localPosition.y + magnitude;
                modFunc = 1;
                break;
            case "moveZ":
                originalVal = actor.transform.localPosition.z;
                finalVal = actor.transform.localPosition.z + magnitude;
                modFunc = 2;
                break;
            case "rotateX":
                originalVal = actor.transform.localRotation.eulerAngles.x;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.x;
                modFunc = 3;
                break;
            case "rotateY":
                originalVal = actor.transform.localRotation.eulerAngles.y;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.y;
                modFunc = 4;
                break;
            case "rotateZ":
                originalVal = actor.transform.localRotation.eulerAngles.z;
                finalVal = magnitude + actor.transform.localRotation.eulerAngles.z;
                modFunc = 5;
                break;
            case "scale":
                originalScl = actor.transform.localScale;
                finalScl = Vector3.one * magnitude;
                modFunc = 6;
                break;
            case "scaleX":
                originalScl = actor.transform.localScale;
                finalScl = Vector3.one * magnitude;
                modFunc = 7;
                break;
            case "scaleY":
                originalScl = actor.transform.localScale;
                finalScl = Vector3.one * magnitude;
                modFunc = 8;
                break;
            case "scaleZ":
                originalScl = actor.transform.localScale;
                finalScl = Vector3.one * magnitude;
                modFunc = 9;
                break;
            case "playAnimation":
                anim = actor.GetComponent<Animator>();
                modFunc = 10;
                break;
            case "playParticles":
                particles = actor.GetComponent<ParticleSystem>();
                modFunc = 11;
                break;
            case "resetPlayfield":
                currentPos = actor.transform.localPosition;
                playfield = actor.GetComponent<Playfield>();
                modFunc = 12;
                break;
            case "resetPosition":
                currentPos = actor.transform.localPosition;
                modFunc = 13;
                break;
            case "resetRotation":
                originalRot = actor.transform.localRotation;
                modFunc = 14;
                break;
            case "toggleObject":
                modFunc = 15;
                break;
            case "togglePlayfield":
                playfield = actor.GetComponent<Playfield>();
                modFunc = 16;
                break;
            case "rotateAroundX": ///Come back to these later
                currentPos = actor.transform.localPosition;

                finalTrans = gameObject.transform;
                finalTrans.SetPositionAndRotation(actor.transform.localPosition, actor.transform.localRotation);

                finalTrans.RotateAround(pointVector, Vector3.right, magnitude);
                modFunc = 17;
                break;
            case "rotateAroundY": ///Come back to these later
                currentPos = actor.transform.localPosition;

                finalTrans = gameObject.transform;
                finalTrans.SetPositionAndRotation(actor.transform.localPosition, actor.transform.localRotation);

                finalTrans.RotateAround(pointVector, Vector3.up, magnitude);
                modFunc = 18;
                break;
            case "rotateAroundZ": ///Come back to these later
                currentPos = actor.transform.localPosition;

                finalTrans = gameObject.transform;
                finalTrans.SetPositionAndRotation(actor.transform.localPosition, actor.transform.localRotation);

                finalTrans.RotateAround(pointVector, Vector3.forward, magnitude);
                modFunc = 19;
                break;
            case "pathTypeX":
                playfield = actor.GetComponent<Playfield>();
                modFunc = 20;
                break;
            case "pathTypeY":
                playfield = actor.GetComponent<Playfield>();
                modFunc = 20;
                break;
            case "pathTypeZ":
                playfield = actor.GetComponent<Playfield>();
                modFunc = 20;
                break;
            case "pathMagX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeX;
                modFunc = 23;
                break;
            case "pathMagY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeY;
                modFunc = 23;
                break;
            case "pathMagZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathMagnitudeZ;
                modFunc = 23;
                break;
            case "pathFreqX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyX;
                modFunc = 26;
                break;
            case "pathFreqY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyY;
                modFunc = 26;
                break;
            case "pathFreqZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.pathFrequencyZ;
                modFunc = 26;
                break;
            case "dark":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.dark;
                modFunc = 29;
                break;
            case "stealth":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.stealth;
                modFunc = 30;
                break;
            case "explode":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.explode;
                modFunc = 31;
                break;
            case "whiteout":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.whiteOut;
                modFunc = 32;
                break;
            case "arrowRotationX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationX;
                modFunc = 33;
                break;
            case "arrowRotationY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationY;
                modFunc = 33;
                break;
            case "arrowRotationZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowRotationZ;
                modFunc = 33;
                break;
            case "reverse":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.reverse;
                modFunc = 36;
                break;
            case "flip":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.flip;
                modFunc = 37;
                break;
            case "invert":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.invert;
                modFunc = 38;
                break;
            case "arrowSizeX":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeX;
                modFunc = 39;
                break;
            case "arrowSizeY":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeY;
                modFunc = 39;
                break;
            case "arrowSizeZ":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.arrowSizeZ;
                modFunc = 39;
                break;
            case "setAnimFloat":
                anim = actor.GetComponent<Animator>();
                originalFloat = anim.GetFloat(param);
                modFunc = 42;
                break;
            case "setmoveX":
                originalVal = actor.transform.localPosition.x;
                finalVal = magnitude;
                modFunc = 0;
                break;
            case "setmoveY":
                originalVal = actor.transform.localPosition.y;
                finalVal = magnitude;
                modFunc = 1;
                break;
            case "setmoveZ":
                originalVal = actor.transform.localPosition.z;
                finalVal = magnitude;
                modFunc = 2;
                break;
            case "setrotateX":
                originalVal = actor.transform.localRotation.eulerAngles.x;
                finalVal = magnitude;
                modFunc = 3;
                break;
            case "setrotateY":
                originalVal = actor.transform.localRotation.eulerAngles.y;
                finalVal = magnitude;
                modFunc = 4;
                break;
            case "setrotateZ":
                originalVal = actor.transform.localRotation.eulerAngles.z;
                finalVal = magnitude;
                modFunc = 5;
                break;
            case "bpm":
                song = actor.GetComponent<SongPlayer>();
                modFunc = 43;
                break;
            case "xmod":
                song = actor.GetComponent<SongPlayer>();
                originalFloat = song.xmod;
                modFunc = 44;
                break;
            case "fadeXStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeXStart;
                modFunc = 45;
                break;
            case "fadeYStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeYStart;
                modFunc = 45;
                break;
            case "fadeZStart":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeZStart;
                modFunc = 45;
                break;
            case "fadeXEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeXEnd;
                modFunc = 48;
                break;
            case "fadeYEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeYEnd;
                modFunc = 48;
                break;
            case "fadeZEnd":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeZEnd;
                modFunc = 48;
                break;
            case "fadeEdge":
                playfield = actor.GetComponent<Playfield>();
                originalFloat = playfield.fadeEdge;
                modFunc = 49;
                break;
            default:
                Debug.LogError($"MOD ERROR: Mod {function} not found at {beat}");
                modFunc = -1;
                break;
        }
    }
    
}
