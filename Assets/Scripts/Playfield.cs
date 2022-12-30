
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class Playfield : UdonSharpBehaviour
{
    public SongPlayer songPlayer;
    public ObjectPooler arrowObjects; //Each playfield has their own set of arrows to manage their spawns and etc
    public Animator receptorAnimator;

    public Renderer[] receptorsTextures;

    public bool mainPlayfield;

    #region Main Actors

    public GameObject spawners;
    public GameObject receptors;

    public GameObject spawnL;
    public GameObject spawnD;
    public GameObject spawnU;
    public GameObject spawnR;

    public GameObject ReceptorL;
    public GameObject ReceptorD;
    public GameObject ReceptorU;
    public GameObject ReceptorR;

    public GameObject recGraphicL;
    public GameObject recGraphicD;
    public GameObject recGraphicU;
    public GameObject recGraphicR;

    #endregion

    #region Reserved Positions

    public Vector3 _defaultReceptorL;
    public Vector3 _defaultReceptorD;
    public Vector3 _defaultReceptorU;
    public Vector3 _defaultReceptorR;

    public Vector3 _defaultSpawnL;
    public Vector3 _defaultSpawnD;
    public Vector3 _defaultSpawnU;
    public Vector3 _defaultSpawnR;

    public Vector3 _defaultDistanceRtoS;
    
    #endregion

    #region Judgement Graphics

    public ParticleSystem timingDisplayL;
    public ParticleSystem timingDisplayD;
    public ParticleSystem timingDisplayU;
    public ParticleSystem timingDisplayR;

    #endregion

    #region Arrow Paths

    public string arrowPathX;
    public string arrowPathY;
    public string arrowPathZ;

    public float pathMagnitudeX;
    public float pathMagnitudeY;
    public float pathMagnitudeZ;

    public float pathFrequencyX;
    public float pathFrequencyY;
    public float pathFrequencyZ;

    #endregion

    public float dizzy = 0; //TODO: Implement a way for dizzy to return the arrows to normal when eased from any value to ... 0

    public float dark = 0; //Hides receptors
    public float stealth = 0; //Hides arrows
    public float explode = 0; //Explodes the vertex of the arrows
    public float whiteOut = 0; //turns arrows completely white

    public float arrowRotationX = 0; //Spin arrows
    public float arrowRotationY = 0; //Spin arrows
    public float arrowRotationZ = 0; //Spin arrows

    public float reverse = 0; //move receptors and spawns
    public float flip = 0; // move receptors and spawns
    public float invert = 0; // move receptors move receptors and spawns

    public float arrowSizeX = 1;
    public float arrowSizeY = 1;
    public float arrowSizeZ = 1;

    public float fadeXStart = 0;
    public float fadeXEnd = 0;
    public float fadeYStart = 0;
    public float fadeYEnd = 0;
    public float fadeZStart = 0;
    public float fadeZEnd = 0;

    public float fadeEdge = 0f;

    //TODO: when playfields are enabled, the arrows seem to instantly spawn based on what arrows are meant to be active, but do not update their position. Should we find a way to disable playfields?
    //Temp Fix: Enable the playfield a few beats prior with stealth at 1, so glitched arrows wont be visible

    public void Start()
    {
        dark = 0;
        stealth = 0;
        explode = 0;
        whiteOut = 0;

        arrowRotationX = 0;
        arrowRotationY = 0;
        arrowRotationZ = 0;

        arrowSizeX = 1;
        arrowSizeY = 1;
        arrowSizeZ = 1;
    }

    public void SetDark()
    {
        Color c = receptorsTextures[0].material.GetColor("_Color");
        c.a = 1 - dark;

        foreach (Renderer r in receptorsTextures)
        {
            r.material.SetColor("_Color", c);
        }
    }
    public void SetStealth()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color c = a.rend.material.GetColor("_Color");
            c.a = 1 - stealth;
            a.rend.material.SetFloat("_EmissionStrength", 7f * (1 - stealth));
            a.rend.material.SetFloat("_EmissionStrength1", 20f * (1 - stealth));
            a.rend.material.SetColor("_Color", c);
        }
    }
    public void SetExplode()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.rend.material.SetFloat("_VertexManipulationHeight", explode);
        }
    }

    public void SetWhiteOut()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color c = a.rend.material.GetColor("_DecalColor");
            c.a = whiteOut;
            a.rend.material.SetColor("_DecalColor", c);
        }      
    }

    public void SetFadeWidth()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.rend.material.SetFloat("_DissolveP2PEdgeLength", fadeEdge);
        }
    }

    public void SetXFadeStart()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveStartPoint");
            v.r = fadeXStart;
            a.rend.material.SetColor("_DissolveStartPoint", v);
        }
    }
    public void SetXFadeEnd()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveEndPoint");
            v.r = fadeXEnd;
            a.rend.material.SetColor("_DissolveEndPoint", v);
        }
    }

    public void SetYFadeStart()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveStartPoint");
            v.g = fadeYStart;
            a.rend.material.SetColor("_DissolveStartPoint", v);
        }
    }
    public void SetYFadeEnd()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveEndPoint");
            v.g = fadeYEnd;
            a.rend.material.SetColor("_DissolveEndPoint", v);
        }
    }

    public void SetZFadeStart()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveStartPoint");
            v.b = fadeZStart;
            a.rend.material.SetColor("_DissolveStartPoint", v);
        }
    }
    public void SetZFadeEnd()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            Color v = a.rend.material.GetColor("_DissolveEndPoint");
            v.b = fadeZEnd;
            a.rend.material.SetColor("_DissolveEndPoint", v);
        }
    }

    public void SetArrowSize()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.ScaleApply();
        }
    }
    public void SetArrowRotations()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.OffsetApply();
        }
    }

    //TODO: broken cuz... I dont know how to do this 
    public void SetInvert()
    {
        ReceptorL.transform.localPosition = ReceptorL.transform.localPosition + (_defaultReceptorU * invert * 2);
        ReceptorD.transform.localPosition = ReceptorD.transform.localPosition + (_defaultReceptorD * invert * 2);
        ReceptorU.transform.localPosition = ReceptorU.transform.localPosition + (_defaultReceptorU * invert * 2);
        ReceptorR.transform.localPosition = ReceptorR.transform.localPosition + (_defaultReceptorD * invert * 2);
    }
    public void SetFlip()
    {
        ReceptorL.transform.localPosition = ReceptorL.transform.localPosition + (_defaultReceptorR * flip * 2);
        ReceptorD.transform.localPosition = ReceptorD.transform.localPosition + (_defaultReceptorU * flip * 2);
        ReceptorU.transform.localPosition = ReceptorU.transform.localPosition + (_defaultReceptorD * flip * 2);
        ReceptorR.transform.localPosition = ReceptorR.transform.localPosition + (_defaultReceptorL * flip * 2);
    }
    public void SetReverse()
    {
        ReceptorL.transform.localPosition = ReceptorL.transform.localPosition - (_defaultReceptorR * reverse);
        ReceptorD.transform.localPosition = ReceptorD.transform.localPosition - (_defaultReceptorU * reverse);
        ReceptorU.transform.localPosition = ReceptorU.transform.localPosition - (_defaultReceptorD * reverse);
        ReceptorR.transform.localPosition = ReceptorR.transform.localPosition - (_defaultReceptorL * reverse);

        spawnD.transform.localPosition = spawnD.transform.localPosition + (_defaultReceptorU * reverse);
        spawnL.transform.localPosition = spawnL.transform.localPosition + (_defaultReceptorR * reverse);
        spawnU.transform.localPosition = spawnU.transform.localPosition + (_defaultReceptorD * reverse);
        spawnR.transform.localPosition = spawnR.transform.localPosition + (_defaultReceptorL * reverse);
    }

    public void SendMiss()
    {
        if (mainPlayfield) songPlayer.DisplayMiss();    
    }

    public void ToggleElements()
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.mesh.SetActive(!a.mesh.activeSelf);
        }

        ReceptorL.SetActive(!ReceptorL.activeSelf);
        ReceptorD.SetActive(!ReceptorD.activeSelf);
        ReceptorU.SetActive(!ReceptorU.activeSelf);
        ReceptorR.SetActive(!ReceptorR.activeSelf);
    }

    public void ToggleElements(bool state)
    {
        foreach (Arrow a in arrowObjects.pooledObjects)
        {
            a.mesh.SetActive(state);
        }

        ReceptorL.SetActive(state);
        ReceptorD.SetActive(state);
        ReceptorU.SetActive(state);
        ReceptorR.SetActive(state);
    }                      

    public void ResetPlayfield()
    {
        receptors.transform.rotation = Quaternion.identity;
        spawners.transform.rotation = Quaternion.identity;

        receptors.transform.localPosition = Vector3.zero;
        spawners.transform.localPosition = Vector3.zero;

        receptors.transform.localScale = Vector3.one;
        spawners.transform.localScale = Vector3.one;

        ReceptorL.transform.localScale = Vector3.one;
        ReceptorD.transform.localScale = Vector3.one;
        ReceptorU.transform.localScale = Vector3.one;
        ReceptorR.transform.localScale = Vector3.one;

        ReceptorL.transform.localPosition = _defaultReceptorL;
        ReceptorD.transform.localPosition = _defaultReceptorD;
        ReceptorU.transform.localPosition = _defaultReceptorU;
        ReceptorR.transform.localPosition = _defaultReceptorR;

        ReceptorL.transform.rotation = Quaternion.identity;
        ReceptorD.transform.rotation = Quaternion.identity;
        ReceptorU.transform.rotation = Quaternion.identity;
        ReceptorR.transform.rotation = Quaternion.identity;

        recGraphicL.transform.localPosition = Vector3.zero;
        recGraphicD.transform.localPosition = Vector3.zero;
        recGraphicU.transform.localPosition = Vector3.zero;
        recGraphicR.transform.localPosition = Vector3.zero;

        recGraphicL.transform.localScale = new Vector3(0.0676f, 0.0617f, 0.0617f);
        recGraphicD.transform.localScale = new Vector3(0.0676f, 0.0617f, 0.0617f);
        recGraphicU.transform.localScale = new Vector3(0.0676f, 0.0617f, 0.0617f);
        recGraphicR.transform.localScale = new Vector3(0.0676f, 0.0617f, 0.0617f);

        recGraphicL.transform.localRotation = Quaternion.Euler(new Vector3(0,-90,90));
        recGraphicD.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
        recGraphicU.transform.localRotation = Quaternion.Euler(new Vector3(-90,180,0));
        recGraphicR.transform.localRotation = Quaternion.Euler(new Vector3(0,90,-90));

        spawnL.transform.localPosition = _defaultSpawnL;
        spawnD.transform.localPosition = _defaultSpawnD;
        spawnU.transform.localPosition = _defaultSpawnU;
        spawnR.transform.localPosition = _defaultSpawnR;

        dark = 0;
        stealth = 0;
        explode = 0;
        whiteOut = 0;

        arrowPathX = "Linear";
        arrowPathY = "Linear";
        arrowPathZ = "Linear";

        pathMagnitudeX = 0;
        pathMagnitudeY = 0;
        pathMagnitudeZ = 0;

        pathFrequencyX = 0;
        pathFrequencyY = 0;
        pathFrequencyZ = 0;

        arrowRotationX = 0;
        arrowRotationY = 0;
        arrowRotationZ = 0;

        arrowSizeX = 1;
        arrowSizeY = 1;
        arrowSizeZ = 1;

        fadeXStart = 0;
        fadeXEnd = 0;
        fadeYStart = 0;
        fadeYEnd = 0;
        fadeZStart = 0;
        fadeZEnd = 0;

        fadeEdge = 0;

        SetDark();
        SetStealth();
        SetExplode();
        SetWhiteOut();
        SetArrowRotations();
        SetArrowSize();

        SetFadeWidth();
        SetXFadeEnd();
        SetYFadeEnd();
        SetZFadeEnd();
        SetXFadeStart();
        SetYFadeStart();
        SetZFadeStart();

    }

    public void PlayLightOnReceptor(int direction, Color color)
    {
        switch (direction)
        {
            case 0:
                var mL = timingDisplayL.main;
                mL.startColor = color;
                timingDisplayL.Play();
                break;
            case 1:
                var mD = timingDisplayD.main;
                mD.startColor = color;
                timingDisplayD.Play();
                break;
            case 2:
                var mU = timingDisplayU.main;
                mU.startColor = color;
                timingDisplayU.Play();
                break;
            case 3:
                var mR = timingDisplayR.main;
                mR.startColor = color;
                timingDisplayR.Play();
                break;
        }
    }
}
