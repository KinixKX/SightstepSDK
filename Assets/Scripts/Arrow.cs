
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Arrow : UdonSharpBehaviour
{
    public float setTiming;
    public int direction;
    public bool canBeHit; 
    public float travelTimer;

    public float defaultRot;

    //This currently goes unused as we don't support held notes due to limits of how to generate a mesh for the arrow holds
    //public bool isHold;
    //public float holdDuration;
    //public bool held;

    public Vector3 travelDirection;

    public SongPlayer songPlayer;
    public Playfield playfield;

    public Renderer rend;
    public GameObject mesh; //Possible support to change the meshes AKA arrow skins?

    public Transform spawnPoint;
    public Transform targetPoint;

    Color red = new Color(0.3137255f, 0.003921569f, 0);
    Color blue = new Color(0, 0.01568628f, 0.2941177f);
    Color purple = new Color(0.0627451f, 0, 0.2705882f);
    Color green = new Color(0.007843138f, 0.05882353f, 0);
    Color orange = new Color(0, 0.01568628f, 0.2941177f);
    Color cyan = new Color(0, 0.094f, 0.08972728f);

    Color red2 = new Color(0.6980392f, 0.06666667f, 0);
    Color blue2 = new Color(0, 0.1568628f, 0.6980392f);
    Color purple2 = new Color(0.1607843f, 0.01176471f, 0.5019608f);
    Color green2 = new Color(0.07450981f, 0.2392157f, 0);
    Color orange2 = new Color(0, 0.01568628f, 0.2941177f);
    Color cyan2 = new Color(0, 0.4235294f, 0.3789981f);

    /// <summary>
    /// Set the arrow color based on their set timing, and rotation based on the direction. They also become an object that can be hit and they get their assigned time to travel between spawn and destination
    /// </summary>
    public void InitArrow(float travelOffset)
    {
        travelTimer = travelOffset;
        canBeHit = true;

        float t = setTiming % 1f;
        int type2 = (int)Mathf.Round(t * 1000f);

        switch (type2)
        {
            case 0:
                rend.material.SetColor("_EmissionColor", red);
                rend.material.SetColor("_EmissionColor1", red2);
                break;
            case 125:
                rend.material.SetColor("_EmissionColor", orange);
                rend.material.SetColor("_EmissionColor1", orange2);
                break;
            case 167:
                rend.material.SetColor("_EmissionColor", purple);
                rend.material.SetColor("_EmissionColor1", purple2);
                break;
            case 250:
                rend.material.SetColor("_EmissionColor", green);
                rend.material.SetColor("_EmissionColor1", green2);
                break;
            case 333:
                rend.material.SetColor("_EmissionColor", purple);
                rend.material.SetColor("_EmissionColor1", purple2);
                break;
            case 375:
                rend.material.SetColor("_EmissionColor", orange);
                rend.material.SetColor("_EmissionColor1", orange2);
                break;
            case 500:
                rend.material.SetColor("_EmissionColor", blue);
                rend.material.SetColor("_EmissionColor1", blue2);
                break;
            case 625:
                rend.material.SetColor("_EmissionColor", orange);
                rend.material.SetColor("_EmissionColor1", orange2);
                break;
            case 666:
                rend.material.SetColor("_EmissionColor", purple);
                rend.material.SetColor("_EmissionColor1", purple2);
                break;
            case 750:
                rend.material.SetColor("_EmissionColor", green);
                rend.material.SetColor("_EmissionColor1", green2);
                break;
            case 833:
                rend.material.SetColor("_EmissionColor", purple);
                rend.material.SetColor("_EmissionColor1", purple2);
                break;
            case 850:
                rend.material.SetColor("_EmissionColor", orange);
                rend.material.SetColor("_EmissionColor1", orange2);
                break;
            default:
                rend.material.SetColor("_EmissionColor", cyan);
                rend.material.SetColor("_EmissionColor1", cyan2);
                break;
        }

        switch (direction)
        {
            case 0:
                mesh.transform.rotation = Quaternion.Euler(0, 0, -90);
                defaultRot = -90;
                mesh.transform.Rotate(playfield.transform.rotation.eulerAngles,Space.World);
                spawnPoint = playfield.spawnL.transform;
                targetPoint = playfield.ReceptorL.transform;
                break;
            case 1:
                mesh.transform.rotation = Quaternion.identity;
                defaultRot = 0;
                mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnD.transform;
                targetPoint = playfield.ReceptorD.transform;
                break;
            case 2:
                mesh.transform.rotation = Quaternion.Euler(0, 0, 180);
                defaultRot = 180;
                mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnU.transform;
                targetPoint = playfield.ReceptorU.transform;
                break;
            case 3:
                mesh.transform.rotation = Quaternion.Euler(0, 0, 90);
                defaultRot = 90;
                mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnR.transform;
                targetPoint = playfield.ReceptorR.transform;
                break;
        }

        gameObject.transform.position = spawnPoint.position;

        if (playfield.arrowPathX != "" && playfield.pathMagnitudeX != 0) gameObject.transform.position += new Vector3(ApplySmoothing(0, playfield.pathFrequencyX, playfield.arrowPathX) * playfield.pathMagnitudeX, 0, 0);
        if (playfield.arrowPathY != "" && playfield.pathMagnitudeY != 0) gameObject.transform.position += new Vector3(0, ApplySmoothing(0, playfield.pathFrequencyY, playfield.arrowPathY) * playfield.pathMagnitudeY, 0);
        if (playfield.arrowPathZ != "" && playfield.pathMagnitudeZ != 0) gameObject.transform.position += new Vector3(0, 0, ApplySmoothing(0, playfield.pathFrequencyZ, playfield.arrowPathZ) * playfield.pathMagnitudeZ);

        OffsetApply();

    }
    
    public void Update()
    {
        if (((setTiming - songPlayer.currentSongBeat + songPlayer.songOffset + 0.100f) / songPlayer.bpm * 60f) < -0.15f)
        {
            playfield.SendMiss();
            DisableArrow();
        }

        //Here's the movement logic for the arrows...
        //TODO: something can be done here... maybe referencing the song time directly instead of the xmod... I dunno, but this seems VERY off...

        travelTimer += (Time.deltaTime * songPlayer.bpm) / 60;
        float perc = travelTimer / songPlayer.xmod; 

        gameObject.transform.position = Vector3.LerpUnclamped(spawnPoint.position, targetPoint.position, perc);

        ///Oh GOD we need to optimize this... three switch cases PER frame like this is AWFUL HOW THE FUCK IS SMOOTHING DONE WITHOUT TANKING PERFORMANCE???
        ///For now, if players do not have an arrowpath and the magnitude is not 0, then it will do this. So you must have a set path and active magnitude to hit this performance
        if (playfield.arrowPathX != "" && playfield.pathMagnitudeX != 0) gameObject.transform.position += new Vector3(ApplySmoothing(perc, playfield.pathFrequencyX, playfield.arrowPathX) * playfield.pathMagnitudeX, 0, 0);
        if (playfield.arrowPathY != "" && playfield.pathMagnitudeY != 0) gameObject.transform.position += new Vector3(0, ApplySmoothing(perc, playfield.pathFrequencyY, playfield.arrowPathY) * playfield.pathMagnitudeY, 0);
        if (playfield.arrowPathZ != "" && playfield.pathMagnitudeZ != 0) gameObject.transform.position += new Vector3(0, 0, ApplySmoothing(perc, playfield.pathFrequencyZ, playfield.arrowPathZ) * playfield.pathMagnitudeZ);

        //gameObject.transform.localRotation = Quaternion.Euler( 0, 0, defaultRot + playfield.dizzy);

    }

    public float ApplySmoothing(float t, float freq, string type)
    {
        switch (type)
        {
            case "Linear":
                break;
            case "Float":
                t = 1 - Mathf.Sin(Mathf.PI * t * 2 * freq);
                break;
            case "Bounce": //Sine like bounce, smoother
                t = Mathf.Sin(Mathf.PI * t);
                break;
            case "BounceV2": //Sharper bounce
                t = (-t * Mathf.Exp(1) * 1.457f * (t - 1));
                break;

            case "WhipOut": //Progressively whips the object, starts at 0 and ends at 1
                t = t * Mathf.Cos(Mathf.PI * (t - 1) * freq);
                break;
            case "WhipIn": //Use .5 values to start at 0 with strong motions and end at 0 with strong motions. Use whole values too start at -1 or 1 and end at 0
                t = (1 - t) * Mathf.Cos(Mathf.PI * (t - 1) * freq);
                break;
            case "WhipInOut": //Any values work, it progressive shakes more and then slows down, starts at 0 ends at 0
                t = (1 - t) * Mathf.Cos(Mathf.PI * t * freq) * t * 4;
                break;

            case "EaseOvershoot": //Always use even and half numbers to ensure proper behaviour (2.5, 4.5, 6,5...etc) The starting value is (should) be 0 and the end value is 1.
                t = (1 - t) * Mathf.Sin(Mathf.PI * (t - 1) * freq) + 1;
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
                break;
        }

        return 1 - t;
    }

    public void OffsetApply()
    {
        mesh.transform.localRotation = Quaternion.Euler(playfield.arrowRotationX, playfield.arrowRotationY, defaultRot + playfield.arrowRotationZ);
    }

    public void ScaleApply()
    {
        gameObject.transform.localScale = new Vector3(playfield.arrowSizeX, playfield.arrowSizeY, playfield.arrowSizeZ);
    }

    /// <summary>
    /// It was hit by the player, make it disappear and can't be hit anymore
    /// </summary>
    public void DisableArrow()
    {
        canBeHit = false;
        gameObject.SetActive(false);
    }

}
