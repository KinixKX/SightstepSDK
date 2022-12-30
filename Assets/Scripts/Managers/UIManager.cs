
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class UIManager : UdonSharpBehaviour
{
    public ParticleSystem gameTitle;

    public Animator uiAnimator;
    public Animator backgroundAnimator;

    public Button[] vrInputButtons;

    public void Start()
    {
        if (!Networking.LocalPlayer.IsUserInVR())
        {
            foreach (Button b in vrInputButtons)
                b.interactable = false;
        }
    }

    public void ShowReadyButton() => uiAnimator.Play("ShowReady", 2);
    public void ShowStartButton() => uiAnimator.Play("ReadySwapToStart", 2);

    public void ShowHandPadOptions() => uiAnimator.Play("ShowHandpad", 3);
    public void ShowFBTPadOptions() => uiAnimator.Play("ShowFBTPad", 3);
    public void ShowKeyboardOptions() => uiAnimator.Play("ShowKeyboard", 3);

    //Until we get animations these go unused
    //public void ShowInputOptions() => uiAnimator.Play("DisplayInputOptions", 3);
    //public void ShowAudioOptions() => uiAnimator.Play("DisplayAudioOptions", 3);
    //public void ShowGraphicOptions() => uiAnimator.Play("DisplayGraphicsOptions", 3); 

    public void ShowWaitScreen() => uiAnimator.Play("WaitShow", 1);
    public void HideWaitScreen() => uiAnimator.Play("WaitHide", 1);

    public void HideMainMenu()
    {
        uiAnimator.Play("MainMenuOut", 1);
        backgroundAnimator.Play("HideDefault", 1);
    }
    public void HideSetupMenu() => uiAnimator.Play("ExplanationHide", 1);

    public void ShowTitleScreen()
    {
        uiAnimator.Play("IntroSequence", 1);
    }
    public void HideTitleScreen()
    {
        gameTitle.Play();
        uiAnimator.Play("IntroOut", 1);
    }

    public void ShowResults()
    {
        uiAnimator.Play("ShowThanks", 1);
    }
    public void HideResults()
    {
        uiAnimator.Play("BackToMainMenu", 1);
        backgroundAnimator.Play("ShowingEnvironment", 1);
    }
    public void ForceMainMenu() => uiAnimator.Play("MainMenuSequence", 1);

    public void ShowWorldBG()
    {
        backgroundAnimator.Play("ShowingEnvironment", 1);
    }

    public void ShakeLeft() => uiAnimator.Play("MoveLeft", 5);
    public void ShakeRight() => uiAnimator.Play("MoveRight", 5);

}
