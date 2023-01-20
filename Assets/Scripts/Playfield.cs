
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

    public int[] arrowPath = new int[3];
    public Vector3 pathMagnitude = Vector3.zero;
    public Vector3 pathFrequency = Vector3.zero;

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

    public void UpdateArrows()
    {
        //for (int i = 0; i < arrowObjects.pooledObjects.Length; i++)
        //{
        //    arrowObjects.pooledObjects[i].LogicUpdate();
        //}
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
        float doubleInvert = invert * 2;
        Vector3 invertUp = _defaultReceptorU * doubleInvert;
        Vector3 invertDown = _defaultReceptorD * doubleInvert;

        ReceptorL.transform.localPosition += invertUp;
        ReceptorD.transform.localPosition += invertDown;
        ReceptorU.transform.localPosition += invertUp;
        ReceptorR.transform.localPosition += invertDown;
    }

    public void SetFlip()
    {
        float doubleFlip = flip * 2;
        ReceptorL.transform.localPosition += (_defaultReceptorR * doubleFlip);
        ReceptorD.transform.localPosition += (_defaultReceptorU * doubleFlip);
        ReceptorU.transform.localPosition += (_defaultReceptorD * doubleFlip);
        ReceptorR.transform.localPosition += (_defaultReceptorL * doubleFlip);
    }

    public void SetReverse()
    {
        Vector3 reverseReceptorR = _defaultReceptorR * reverse;
        Vector3 reverseReceptorU = _defaultReceptorU * reverse;
        Vector3 reverseReceptorD = _defaultReceptorD * reverse;
        Vector3 reverseReceptorL = _defaultReceptorL * reverse;

        ReceptorL.transform.localPosition -= reverseReceptorR;
        ReceptorD.transform.localPosition -= reverseReceptorU;
        ReceptorU.transform.localPosition -= reverseReceptorD;
        ReceptorR.transform.localPosition -= reverseReceptorL;

        spawnD.transform.localPosition += reverseReceptorU;
        spawnL.transform.localPosition += reverseReceptorR;
        spawnU.transform.localPosition += reverseReceptorD;
        spawnR.transform.localPosition += reverseReceptorL;
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

        Vector3 graphicScale = new Vector3(0.0676f, 0.0617f, 0.0617f);
        recGraphicL.transform.localScale = graphicScale;
        recGraphicD.transform.localScale = graphicScale;
        recGraphicU.transform.localScale = graphicScale;
        recGraphicR.transform.localScale = graphicScale;

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

        for (int i = 0; i < arrowPath.Length; i++)
        {
            arrowPath[i] = -1;
        }

        pathMagnitude = Vector3.zero;

        pathFrequency = Vector3.zero;

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

        fadeEdge = 0.15f;

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
