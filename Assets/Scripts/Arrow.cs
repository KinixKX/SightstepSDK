using System;
using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Arrow : UdonSharpBehaviour
{
    public float setTiming;
    public int direction;
    public bool canBeHit;
    public float visualOffset;
    public float spawnTime;

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

    private Color red = new Color(0.1686275f, 0.003081087f, 0);
    private Color blue = new Color(0, 0.01568628f, 0.1705882f);
    private Color purple = new Color(0.03310555f, 0.0004894992f, 0.1037736f);
    private Color green = new Color(0.01301234f, 0.09433961f, 0);
    private Color orange = new Color(0.2941177f, 0.04434219f, 0);
    private Color cyan = new Color(0, 0.05490196f, 0.04299222f);

    private Color red2 = new Color(1f, 0.05869148f, 0);
    private Color blue2 = new Color(0, 0.08039216f, 0.4901961f);
    private Color purple2 = new Color(0.07647059f, 0.001960784f, 0.1078431f);
    private Color green2 = new Color(0.01862745f, 0.1019608f, 0);
    private Color orange2 = new Color(0.1470588f, 0.07625756f, 0);
    private Color cyan2 = new Color(0, 0.1098039f, 0.09360918f);

    /// <summary>
    /// Set the arrow color based on their set timing, and rotation based on the direction. They also become an object that can be hit and they get their assigned time to travel between spawn and destination
    /// </summary>
    public void InitArrow(float visualOffset)
    {
        this.visualOffset = visualOffset;
        canBeHit = true;

        float t = setTiming % 1f;
        int type2 = (int)Mathf.Round(t * 1000f);

        switch (type2)
        {
            case 0: // 4ths
                rend.material.SetColor("_EmissionColor", red);
                rend.material.SetColor("_EmissionColor1", red2);
                break;
            case 500: // 8ths
                rend.material.SetColor("_EmissionColor", blue);
                rend.material.SetColor("_EmissionColor1", blue2);
                break;
            case 166: // 12ths & 24ths
            case 167:
            case 333:
            case 334:
            case 666:
            case 667:
            case 833:
            case 834:
                rend.material.SetColor("_EmissionColor", purple);
                rend.material.SetColor("_EmissionColor1", purple2);
                break;
            case 250: // 16ths
            case 750:
                rend.material.SetColor("_EmissionColor", green);
                rend.material.SetColor("_EmissionColor1", green2);
                break;
            case 125: // 32nds
            case 375:
            case 625:
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
                //mesh.transform.Rotate(playfield.transform.rotation.eulerAngles,Space.World);
                spawnPoint = playfield.spawnL.transform;
                targetPoint = playfield.ReceptorL.transform;
                break;
            case 1:
                mesh.transform.rotation = Quaternion.identity;
                defaultRot = 0;
                //mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnD.transform;
                targetPoint = playfield.ReceptorD.transform;
                break;
            case 2:
                mesh.transform.rotation = Quaternion.Euler(0, 0, 180);
                defaultRot = 180;
                //mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnU.transform;
                targetPoint = playfield.ReceptorU.transform;
                break;
            case 3:
                mesh.transform.rotation = Quaternion.Euler(0, 0, 90);
                defaultRot = 90;
                //mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
                spawnPoint = playfield.spawnR.transform;
                targetPoint = playfield.ReceptorR.transform;
                break;
        }
        mesh.transform.Rotate(playfield.transform.rotation.eulerAngles, Space.World);
        spawnTime = songPlayer.songTime;

        LogicUpdate();

        OffsetApply();
    }

    public void Update()
    {
        LogicUpdate();
    }

    public void LogicUpdate()
    {
        if (!canBeHit)
            return;

        float bps = songPlayer.beatsPerSecond;

        float songTime = songPlayer.songTime;
        float noteTime = setTiming * bps;

        if (songTime > noteTime + 0.150f)
        {
            playfield.SendMiss();
            DisableArrow();
        }

        float progress = (songTime - spawnTime) / (noteTime - spawnTime);

        Vector3 position = Vector3.LerpUnclamped(spawnPoint.position, targetPoint.position, progress);
        Vector3 smoothing = ApplySmoothing(progress);

        Vector3 finalPosition = position + smoothing;

        gameObject.transform.position = finalPosition;

        OffsetApply();
        ScaleApply();
    }

    public Vector3 ApplySmoothing(float perc)
    {
        if (playfield.pathMagnitude.sqrMagnitude == 0)
            return Vector3.zero;

        Vector3 smooth = ApplySmoothing(perc, playfield.pathFrequency, playfield.arrowPath);
        return smooth;
    }

    public Vector3 ApplySmoothing(float t, Vector3 freq, int[] type)
    {
        Vector3 smooth = Vector3.zero;
        for (int i = 0; i < 3; i++)
        {
            if (playfield.arrowPath[i] == -1)
                continue;
            if (playfield.pathMagnitude[i] == 0)
                continue;
            smooth[i] = ApplySmoothing(t, freq[i], type[i]) * playfield.pathMagnitude[i];
        }
        return smooth;
    }

    public float ApplySmoothing(float t, float freq, int type)
    {
        return EasingFunctions.Ease(type, t, freq);
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
