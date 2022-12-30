
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class AudioManager : UdonSharpBehaviour
{
    public AudioClip continueSFX;
    public AudioClip startSFX;
    public AudioClip countdownTickSFX;

    public AudioClip showResultsSFX;
    public AudioClip showMenusSFX;

    public AudioClip buttonSFX;
    public AudioClip readySFX;
    public AudioClip joinSFX;
    public AudioClip leaveSFX;
    public AudioClip tickOnSFX;
    public AudioClip tickOffSFX;

    public AudioClip normalDiffSFX;
    public AudioClip specialDiffSFX;

    public AudioClip fullComboSFX;

    public AudioClip titleIntroBGM;
    public AudioClip titleBGM;
    public AudioClip menuBGM;
    public AudioClip resultsBGM;

    public AudioSource soundPlayer;
    public AudioSource backgroundMusic;

    public bool playingIntro;

    public Animator volumeControl;

    public void PlayTitleMusic()
    {
        playingIntro = true;
        backgroundMusic.clip = titleIntroBGM;
        backgroundMusic.Play();
    }

    public void PlayMenuMusic()
    {
        backgroundMusic.clip = menuBGM;
        backgroundMusic.Play();
        FadeMusicIn();
    }

    public void PlayRankMusic()
    {
        backgroundMusic.loop = true;
        backgroundMusic.clip = resultsBGM;
        backgroundMusic.Play();
        FadeMusicIn();
    }

    public void InstantMusicOn() => volumeControl.Play("Full", 1);
    public void FadeMusicOut() => volumeControl.Play("FadeOut", 1);
    public void FadeMusicIn() => volumeControl.Play("FadeIn", 1);

    public void ShowMenuSound() => soundPlayer.PlayOneShot(showMenusSFX);
    public void ShowResultsSound() => soundPlayer.PlayOneShot(showResultsSFX);

    public void RegularDiffSound() => soundPlayer.PlayOneShot(normalDiffSFX);
    public void SpecialDiffSound() => soundPlayer.PlayOneShot(specialDiffSFX);

    public void FullComboSound() => soundPlayer.PlayOneShot(fullComboSFX);

    public void ContinueSound() => soundPlayer.PlayOneShot(continueSFX);
    public void StartSound() => soundPlayer.PlayOneShot(startSFX);
    public void CountSound() => soundPlayer.PlayOneShot(countdownTickSFX);

    public void ButtonSound() => soundPlayer.PlayOneShot(buttonSFX);
    public void ReadySound(bool isOn) => soundPlayer.PlayOneShot(isOn ? readySFX : buttonSFX);
    public void TickSound(bool isOn) => soundPlayer.PlayOneShot(isOn ? tickOnSFX : tickOffSFX);
    public void PlayerJoinSound() => soundPlayer.PlayOneShot(joinSFX);
    public void PlayerLeaveSound() => soundPlayer.PlayOneShot(leaveSFX);


    public void Update()
    {
        if (playingIntro) //If the continue button is pressed early, it will override this!!! update playingintro later on
        {
            if ((backgroundMusic.time >= backgroundMusic.clip.length - 0.02f)) //Compatible with any music that may have an intro!
            {
                if (backgroundMusic.clip == titleIntroBGM)
                {
                    playingIntro = false;
                    backgroundMusic.Stop();
                    backgroundMusic.clip = titleBGM;
                    backgroundMusic.loop = true;
                    backgroundMusic.Play();
                }
                else
                {
                    playingIntro = false;
                    backgroundMusic.loop = true;
                }
            }
        }


    }

}
