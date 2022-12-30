
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using VRC.Udon.Common.Interfaces;


[UdonBehaviourSyncMode(BehaviourSyncMode.Continuous)]
public class NetworkPlayerData : UdonSharpBehaviour
{

    #region Shared properties


    [UdonSynced(UdonSyncMode.None)]
    public float s_Accuracy;
    [UdonSynced(UdonSyncMode.None)]
    public int s_Combo;
    [UdonSynced(UdonSyncMode.None)]
    public int s_missCount;
    [UdonSynced(UdonSyncMode.None)]
    public int s_almostCount;
    [UdonSynced(UdonSyncMode.None)]
    public int s_greatCount;
    [UdonSynced(UdonSyncMode.None)]
    public int s_excellentCount;
    [UdonSynced(UdonSyncMode.None)]
    public int s_fantasticCount;
    [UdonSynced(UdonSyncMode.None)]
    public int s_maxCombo;

    [UdonSynced]
    public int s_lateCount;
    [UdonSynced]
    public int s_earlyCount;
    [UdonSynced]
    public int s_judgementFont;
    public int local_judgementFont;

    #endregion

        public GameObject gameUI;
        public GameObject scoreUI;
        public ParticleSystem gamePosition;

    //Sora be like
    #region Public shit you gotta set through the inspector / Score display


        public Animator scoreDisplay;
        public Text scoreSummary;
        public Text comboDisplay;
        public Text accuracyDisplay;
        public Text vrTagDisplay;
        public Text lateDisplay;
        public Text earlyDisplay;
        public Text maxComboText;
        
        public ParticleSystem comboBurst;
        public ParticleSystem fcBurst;

        private int judgementRot;                               
        public ParticleSystem judgement;
        public Sprite missGraphic;
        public Sprite fantasticGraphic;
        public Sprite almostGraphic;
        public Sprite greatGraphic;
        public Sprite excellentGraphic;
        public Sprite almostGraphicL;
        public Sprite greatGraphicL;
        public Sprite excellentGraphicL;

    public Sprite[] judgementsSST;
    public Sprite[] judgementsITG2;
    public Sprite[] judgements0b5vr;

    public SpriteRenderer rankDisplay;
        public Sprite rankSGraphic;
        public Sprite rankAGraphic;
        public Sprite rankBGraphic;
        public Sprite rankCGraphic;
        public Sprite rankDGraphic;
        public Sprite rankFGraphic;

    #endregion

    /// <summary>
    /// Gets called on each property change.
    /// Only gets called for non-masters!
    /// </summary>
    public override void OnDeserialization()
    {
        comboDisplay.text = s_Combo.ToString();
        if(local_judgementFont != s_judgementFont)
        {
            local_judgementFont = s_judgementFont;
            SetCustomJudgement(s_judgementFont);
        }
    }

    // Called from network events in SongPlayer@UpdateScore
    public void ShowComboCounter() => comboDisplay.gameObject.SetActive(true);
    public void HideComboCounter() => comboDisplay.gameObject.SetActive(false);
    public void PlayComboBurst() => comboBurst.Play();
    public void PlayFCBurst() => fcBurst.Play();

    public void ComboBounce() => scoreDisplay.Play("ComboBob", 2);
    

    public void SetCustomJudgement(int font)
    {
        s_judgementFont = font;

        switch (font)
        {
            case 0:
                missGraphic = judgementsSST[0];
                fantasticGraphic = judgementsSST[1];
                almostGraphic = judgementsSST[2];
                greatGraphic = judgementsSST[3];
                excellentGraphic = judgementsSST[4];
                almostGraphicL = judgementsSST[5];
                greatGraphicL = judgementsSST[6];
                excellentGraphicL = judgementsSST[7];
                break;
            case 1:
                missGraphic = judgementsITG2[0];
                fantasticGraphic = judgementsITG2[1];
                almostGraphic = judgementsITG2[2];
                greatGraphic = judgementsITG2[3];
                excellentGraphic = judgementsITG2[4];
                almostGraphicL = judgementsITG2[5];
                greatGraphicL = judgementsITG2[6];
                excellentGraphicL = judgementsITG2[7];
                break;
            case 2:
                missGraphic = judgements0b5vr[0];
                fantasticGraphic = judgements0b5vr[1];
                almostGraphic = judgements0b5vr[2];
                greatGraphic = judgements0b5vr[3];
                excellentGraphic = judgements0b5vr[4];
                almostGraphicL = judgements0b5vr[5];
                greatGraphicL = judgements0b5vr[6];
                excellentGraphicL = judgements0b5vr[7];
                break;
        }
    }

    #region Judgments display

        /// <summary>
        /// Shows a good judgment (without any rotation)
        /// Called from the network events below.
        /// </summary>
        protected void ShowJudgment(Sprite graphic)
        {
            judgement.textureSheetAnimation.SetSprite(0, graphic);
            judgement.Clear();
            // Reset the rotation that might have been applied from ShowPoorJudgment()
            judgement.gameObject.transform.localRotation = Quaternion.identity;
            judgement.Play();
        }

        public void ShowFantastic()      => ShowJudgment(fantasticGraphic);
        public void ShowExcellentEarly() => ShowJudgment(excellentGraphic);
        public void ShowExcellentLate()  => ShowJudgment(excellentGraphicL);


        /// <summary>
        /// Shows a poor judgment (with a rotation)
        /// Called from the network events below.
        /// </summary>
        protected void ShowPoorJudgment(Sprite graphic, float rotation)
        {
            judgement.textureSheetAnimation.SetSprite(0, graphic);
            judgement.Clear();
            judgement.gameObject.transform.localRotation = Quaternion.Euler(0, 0, judgementRot++ % 2 == 0 ? rotation : -rotation);
            judgement.Play();
        }

        public void ShowGreatEarly()     => ShowPoorJudgment(greatGraphic, 3.5f);
        public void ShowGreatLate()      => ShowPoorJudgment(greatGraphicL, 3.5f);
        public void ShowAlmostEarly()    => ShowPoorJudgment(almostGraphic, 7);
        public void ShowAlmostLate()     => ShowPoorJudgment(almostGraphicL, 7);
        public void ShowMiss()           => ShowPoorJudgment(missGraphic, 20);

    #endregion

    #region Score display

    public void DisplayScore()
    {
        vrTagDisplay.gameObject.SetActive(Networking.GetOwner(gameObject).IsUserInVR());
        comboDisplay.gameObject.SetActive(false);

        scoreSummary.text = ($@"<color=#0bd0e6>FA- {s_fantasticCount}</color>
<color=#e6c10b>EX- {s_excellentCount}</color>
<color=#26b013>GR- {s_greatCount}</color>
<color=#5a41e8>AL- {s_almostCount}</color>
<color=#ba3420>MS- {s_missCount}</color>");

        accuracyDisplay.text = s_Accuracy.ToString() + "%";

        maxComboText.text = s_maxCombo.ToString();

        earlyDisplay.text = $"Early\n" +
            $"{s_earlyCount}";
        lateDisplay.text = $"Late\n" +
    $"{s_lateCount}";

        scoreDisplay.Play("Display Rank", 1);
    }

    public void HideScore()
    {
        HideComboCounter();
        scoreDisplay.Play("RankHide", 1);
    }

    protected void SetRank(Sprite graphic)
    {
        rankDisplay.sprite = graphic;
    }

    public void RankS() => SetRank(rankSGraphic);
    public void RankA() => SetRank(rankAGraphic);
    public void RankB() => SetRank(rankBGraphic);
    public void RankC() => SetRank(rankCGraphic);
    public void RankD() => SetRank(rankDGraphic);
    public void RankF() => SetRank(rankFGraphic);

    #endregion

}
