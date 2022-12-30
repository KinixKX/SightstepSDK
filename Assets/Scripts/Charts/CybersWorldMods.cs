
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class CybersWorldMods : ModChart
{
    public GameObject backgroundAnimations;
    public GameObject lightAnimations;
    public GameObject receptorsExplosionParticles;

    public GameObject postArrowL;
    public GameObject postArrowD;
    public GameObject postArrowU;
    public GameObject postArrowR;

    //This must be asigned to the OG ModChart script in stageselector, then this is sent to the songplayer

    public override void InitMods()
    {
        LoadSimpleMods();
        LoadNormalMods();
        LoadHardMods();
        LoadUnsightedMods();

    }

    //TODO: Check for jank rotations on some parts! (mainly rotations resetting to 0

    public void LoadSimpleMods()
    {

        xmodS = 3.5f;
        simpleModChart = new object[][]
        {
            new object[]{0f, 1f, "toggleObject", backgroundAnimations },

            new object[]{0f, 1f, "toggleObject", Lights },
            new object[]{0f, 1f, "toggleObject", lightAnimations },

            new object[]{ 0f, .5f, "moveY", 2f, "Bounce", P1.ReceptorL},
            new object[]{0.5f, 1f, "playAnimation", "BlackFadeIn", backgroundAnimations},
            new object[]{ 0.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD},
            new object[]{ 1f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU},
            new object[]{ 1.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR},
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorL},
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorD },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorU },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorR },

            new object[]{2f, 1f, "toggleObject", BG },

            new object[]{21f, .4f, "moveX", -1.5f, "Flip:EaseOutQuad", P1.receptors, 3, 1f},
            new object[]{21f, .4f, "moveX", 1.5f, "Flip:EaseOutQuad", P1.spawners, 3, 1f},
            new object[]{21f, .4f, "rotateZ", 30f, "Flip:EaseOut", P1.ReceptorL, 3, 1f},
            new object[]{21f, .4f, "rotateZ", 30f, "Flip:EaseOut", P1.ReceptorD, 3, 1f},
            new object[]{21f, .4f, "rotateZ", 30f, "Flip:EaseOut", P1.ReceptorU, 3, 1f},
            new object[]{21f, .4f, "rotateZ", 30f, "Flip:EaseOut", P1.ReceptorR, 3, 1f},

            new object[]{21.5f, .4f, "moveX", 1.5f, "Flip:EaseOutQuad", P1.receptors, 2, 1f},
            new object[]{21.5f, .4f, "moveX", -1.5f, "Flip:EaseOutQuad", P1.spawners, 2, 1f},
            new object[]{21.5f, .4f, "rotateZ", -30f, "Flip:EaseOut", P1.ReceptorL, 2, 1f},
            new object[]{21.5f, .4f, "rotateZ", -30f, "Flip:EaseOut", P1.ReceptorD, 2, 1f},
            new object[]{21.5f, .4f, "rotateZ", -30f, "Flip:EaseOut", P1.ReceptorU, 2, 1f},
            new object[]{21.5f, .4f, "rotateZ", -30f, "Flip:EaseOut", P1.ReceptorR, 2, 1f},

            new object[]{32f, 0f, "setrotateZ", 0f, "EaseIn", P1.ReceptorL},
            new object[]{32f, 0f, "setrotateZ", 0f, "EaseIn", P1.ReceptorD},
            new object[]{32f, 0f, "setrotateZ", 0f, "EaseIn", P1.ReceptorU},
            new object[]{32f, 0f, "setrotateZ", 0f, "EaseIn", P1.ReceptorR},

            new object[]{32f, 0f, "setmoveX", P1._defaultReceptorL.x , "EaseIn", P1.ReceptorL},
            new object[]{32f, 0f, "setmoveX", P1._defaultReceptorD.x, "EaseIn", P1.ReceptorD},
            new object[]{32f, 0f, "setmoveX", P1._defaultReceptorU.x, "EaseIn", P1.ReceptorU},
            new object[]{32f, 0f, "setmoveX", P1._defaultReceptorR.x, "EaseIn", P1.ReceptorR},

             new object[]{36f, 0f, "togglePlayfield", P2.gameObject},
            new object[]{36f, 1f, "moveZ", 1f, "Bounce", P1.gameObject },
            new object[]{36f, 1f, "rotateZ", 25f, "BounceV2", P1.gameObject},
            new object[]{36f, 1f, "stealth", .8f, "BounceV2", P2.gameObject },


            //new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorL },
            //new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorD },
            //new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorU },
            //new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorR },
            //new object[]{ 4f, .45f, "moveX", -1.5f, "EaseInCirc", P1.ReceptorL },
            //new object[]{ 4.5f, .45f, "moveX", -0.5f, "EaseInCirc", P1.ReceptorD },
            //new object[]{ 5f, .45f, "moveX", 0.5f, "EaseInCirc", P1.ReceptorU },
            //new object[]{ 5.5f, .45f, "moveX", 1.5f, "EaseInCirc", P1.ReceptorR },
            //new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorL },
            //new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorD },
            //new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorU },
            //new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorR },

            //new object[]{ 7.5f, .5f, "moveX", 1.5f, "EaseInOutElastic", P1.ReceptorL },
            //new object[]{ 7.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorD },
            //new object[]{ 7.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorU },
            //new object[]{ 7.5f, .5f, "moveX", -1.5f, "EaseInOutElastic", P1.ReceptorR },


            //new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            //new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorD },
            //new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorU },
            //new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            //new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorL },
            //new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorD },
            //new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorU },
            //new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorR },

            //new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorL },
            //new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            //new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            //new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorR },
            //new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorL },
            //new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorD },
            //new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorU },
            //new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorR },

            //new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            //new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            //new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            //new object[]{ 12f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            //new object[]{ 12f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorR  },
            //new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            //new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            //new object[]{ 12.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            //new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            //new object[]{ 12.5f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorU  },
            //new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            //new object[]{ 13f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            //new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            //new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            //new object[]{ 13f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorD  },
            //new object[]{ 13.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            //new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            //new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            //new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            //new object[]{ 13.5f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorL  },

            //new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorL,  2, .5f},
            //new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorD,  2, .5f},
            //new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorU,  2, .5f},
            //new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorR,  2, .5f},

            //new object[]{ 15f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            //new object[]{ 15.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },

            //new object[]{ 16f, .2f, "moveX", .5f, "Bounce", P1.receptors,  4, 2f},
            //new object[]{ 17f, .2f, "moveX", -.5f, "Bounce", P1.receptors,  4, 2f},

            //new object[]{ 24f, 4f, "moveZ", -3.5f, "EaseIn", P1.gameObject  },

            //new object[]{ 28f, .1f, "moveX", -.5f, "Linear", P1.ReceptorL  },
            //new object[]{ 28f, .1f, "moveX", -.25f, "Linear", P1.ReceptorD  },
            //new object[]{ 28f, .1f, "moveX", .25f, "Linear", P1.ReceptorU  },
            //new object[]{ 28f, .1f, "moveX", .5f, "Linear", P1.ReceptorR  },

            new object[]{ 30f, 2f, "playAnimation", "FadeToWhite", backgroundAnimations, "Speed"},
            new object[]{ 30f, 2, "playAnimation", "LightToStage", lightAnimations, "Speed"},
            //new object[]{ 32f, .1f, "moveX", .5f, "Linear", P1.ReceptorL  },
            //new object[]{ 32f, .1f, "moveX", .25f, "Linear", P1.ReceptorD  },
            //new object[]{ 32f, .1f, "moveX", -.25f, "Linear", P1.ReceptorU  },
            //new object[]{ 32f, .1f, "moveX", -.5f, "Linear", P1.ReceptorR  },
            //new object[]{ 32f, .1f, "moveZ", 3.5f, "EaseIn", P1.gameObject  },

            new object[]{ 32f, 96f, "playAnimation", "RoadStartMultiplayer", backgroundAnimations, "WorldSpeed" },

            new object[]{ 32f, 1.25, "moveX", .5f, "EaseInOutQuad", P1.gameObject,  3, 4f},

            new object[]{33f, .75f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed", 95, 2f},

            //new object[]{ 33f, .25f, "moveX", -.5f, "EaseInOutQuad", P1.gameObject,  3, 4f},
            //new object[]{ 34f, .25f, "moveX", -.5f, "EaseInOutQuad", P1.gameObject,  3, 4f},
            //new object[]{ 35f, .25f, "moveX", .5f, "EaseInOutQuad", P1.gameObject,  3, 4f},

            //new object[]{ 44f, 1f, "moveZ", -2f, "Bounce", P1.gameObject,  4, 1f },

            //new object[]{ 48f, .25f, "moveX", -.5f, "EaseInOutQuad", P1.gameObject,  3, 4},
            //new object[]{ 49f, .25f, "moveX", .5f, "EaseInOutQuad", P1.gameObject,  3, 4},
            //new object[]{ 50f, .25f, "moveX", .5f, "EaseInOutQuad", P1.gameObject,  3, 4},
            //new object[]{ 51f, .25f, "moveX", -.5f, "EaseInOutQuad", P1.gameObject,  3, 4},

            //new object[]{ 60f, 1f, "moveZ", -1f, "Bounce", P1.gameObject,  4, 1f },

            //new object[]{ 64f, 2f, "moveY", 1f, "Linear", P1.receptors  },
            //new object[]{ 66.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 67.5f, 2f, "moveY", 1f, "Linear", P1.receptors  },
            //new object[]{ 70.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 71.5f, 2f, "moveY", 1f, "Linear", P1.receptors  },
            //new object[]{ 74.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 76.5f, .9f, "rotateZ", 20f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{ 77.5f, .9f, "rotateZ", -40f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{ 78.5f, .9f, "rotateZ", 20f, "EaseInOutQuad", P1.gameObject  },

            //new object[]{ 79.5f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },

            //new object[]{ 79.5f, 2f, "moveY", 1f, "Linear", P1.receptors  },
            //new object[]{ 82.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 83.5f, 2f, "moveY", 1f, "Linear", P1.receptors  },
            //new object[]{ 86.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 87.5f, .2f, "moveX", .1f, "Float", P1.receptors, 10, .2f },
            //new object[]{ 87.5f, 2f, "moveY", 1f, "Linear", P1.receptors },
            //new object[]{ 90.5f, .5f, "moveY", -1f, "EaseInOutQuad", P1.receptors  },

            //new object[]{ 92.5f, .9f, "rotateZ", -15f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{ 93.5f, .9f, "rotateZ", 30f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{ 94.5f, .9f, "rotateZ", -15f, "EaseInOutQuad", P1.gameObject  },

            //new object[]{ 96f, .2f, "moveX", .5f, "Bounce", P1.receptors,  6, 2f},
            //new object[]{ 97f, .2f, "moveX", -.5f, "Bounce", P1.receptors,  6, 2f},

            //new object[]{108.5f, 1f, "moveY", 1f, "Bounce", P1.gameObject,  3, 1f },

            //new object[]{ 112f, .2f, "moveY", .5f, "Bounce", P1.receptors,  6, 2f},
            //new object[]{ 113f, .2f, "moveY", -.5f, "Bounce", P1.receptors,  6, 2f},

            //new object[]{124.5f, 1f, "moveZ", 1f, "InstantIn", P1.gameObject  },
            //new object[]{125.5f, 1f, "moveZ", -1f, "InstantIn", P1.gameObject  },
            //new object[]{126.5f, 1f, "moveZ", 1f, "InstantIn", P1.gameObject  },

            //new object[]{128f, 4f, "moveX", 3f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{128f, 4f, "moveZ", -1f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{128f, 4f, "rotateY", 15f, "EaseInOutQuad", P1.gameObject  },
            ////new object[]{128f, 0.5f, "playParticles", postArrowR,  2, 2f},

            ////new object[]{144f, 0.5f, "playParticles", postArrowL,  2, 2f},
            //new object[]{144f, 4f, "moveX", -6f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{144f, 2f, "moveZ", 1f, "EaseOut", P1.gameObject  },
            //new object[]{144f, 4f, "rotateY", -30f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{146f, 2f, "moveZ", -1f, "EaseOut", P1.gameObject  },

            //new object[]{156f, 4f, "moveX", 3f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{156f, 4f, "moveZ", 1f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{156f, 4f, "rotateY", 15f, "EaseInOutQuad", P1.gameObject  },

            //new object[]{160f, 1f, "moveY", 1f, "Float", P1.spawners,  3, 1f },

            //new object[]{163.5f, 1f, "moveZ", 3f, "InstantIn", P1.receptors  },

            //new object[]{164.5f, 1f, "moveY", 1f, "Float", P1.spawners,  3, 1f },
            //new object[]{168f, 1f, "moveY", 1f, "Float", P1.spawners,  3, 1f },

            //new object[]{171.5f, 1f, "moveZ", -3f, "InstantIn", P1.receptors  },

            //new object[]{179.5f, 1f, "moveZ", 3f, "InstantIn", P1.receptors  },

            //new object[]{180.5f, 1f, "moveY", 1f, "Float", P1.spawners,  3, 1f },
            //new object[]{184f, 1f, "moveY", 1f, "Float", P1.spawners,  3, 1f },

            //new object[]{187.5f, 1f, "moveZ", -3f, "InstantIn", P1.receptors  },

            //new object[]{192f, .5f, "moveZ", -4f, "EaseIn", P1.gameObject},
            //new object[]{192f, 15f, "moveY", .25f, "Float", P1.receptors,  2, 15f },
            //new object[]{192f, 7.5f, "rotateZ", 6f, "Float", P1.receptors,  4, 7.5f },

            //new object[]{ 192f, 92f, "playAnimation", "RoadFinishMultiplayer", backgroundAnimations, "WorldSpeed"},

            //new object[]{224f, .5f, "moveZ", 4f, "EaseIn", P1.gameObject},

            //new object[]{224f, .25f, "moveZ", -7f, "EaseOut", P1.spawners},
            //new object[]{251.5f, .25f, "moveZ", 7f, "EaseOut", P1.spawners  },

            ////new object[]{256f, 1f, "playParticles", receptorsExplosionParticles,  6, 2f},
            //new object[]{256f, 1f, "moveX", -.5f, "Bounce", P1.ReceptorL,  6, 2f },
            //new object[]{256f, 1f, "moveX", -.25f, "Bounce", P1.ReceptorD,  6, 2f },
            //new object[]{256f, 1f, "moveX", .25f, "Bounce", P1.ReceptorU,  6, 2f },
            //new object[]{256f, 1f, "moveX", .5f, "Bounce", P1.ReceptorR,  6, 2f },

            //new object[]{257f, 1f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed", 14, 2f },

            //new object[]{268f, 2f, "moveY", 1f, "Float", P1.receptors,  2, 2f },

            //new object[]{272f, 2f, "moveX", .75f, "Float", P1.receptors,  6, 2f },

            //new object[]{287.5f, 3f, "moveY", 50f, "EaseIn", P1.receptors  },
};
    }

    public void LoadNormalMods()
    {

    }

    public void LoadHardMods()
    {
        xmodH = 2f;
        hardModChart = new object[][]
{
            new object[]{0f, 1f, "toggleObject", backgroundAnimations },

            new object[]{0f, 1f, "toggleObject", Lights },
            new object[]{0f, 1f, "toggleObject", lightAnimations },

            new object[]{ 0f, .5f, "moveY", 2f, "Bounce", P1.ReceptorL },
            new object[]{0.5f, 1f, "playAnimation", "BlackFadeIn", backgroundAnimations},
            new object[]{ 0.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            new object[]{ 1f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU },
            new object[]{ 1.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorL },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorD },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorU },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorR },

            new object[]{2f, 1f, "toggleObject", BG },

            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorL },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorD },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorU },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorR },
            new object[]{ 4f, .45f, "moveX", -1.5f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 4.5f, .45f, "moveX", -0.5f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 5f, .45f, "moveX", 0.5f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 5.5f, .45f, "moveX", 1.5f, "EaseInCirc", P1.ReceptorR },
            new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorL },
            new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorD },
            new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorU },
            new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorR },

            new object[]{ 7.5f, .5f, "moveX", 1.5f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 7.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 7.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 7.5f, .5f, "moveX", -1.5f, "EaseInOutElastic", P1.ReceptorR },


            new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 10f, .5f, "moveY", -1f, "WhipOut,3", P1.ReceptorL },
            new object[]{ 10f, .5f, "moveY", -1f, "WhipOut,3", P1.ReceptorD },

            new object[]{ 10f, .5f, "moveY", 1f, "WhipOut,3", P1.ReceptorU },
            new object[]{ 10f, .5f, "moveY", 1f, "WhipOut,3", P1.ReceptorR },

            new object[]{ 10f, .5f, "moveX", -1f, "WhipOut,3", P1.ReceptorL },
            new object[]{ 10f, .5f, "moveX", -1f, "WhipOut,3", P1.ReceptorD },

            new object[]{ 10f, .5f, "moveX", 1f, "WhipOut,3", P1.ReceptorU },
            new object[]{ 10f, .5f, "moveX", 1f, "WhipOut,3", P1.ReceptorR },

            new object[]{ 11.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 11.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 11.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 11.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 11.5f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 11.5f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 11.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 11.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 12f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 12f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorR  },
            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 12.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 12.5f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorU  },
            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 13f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 13f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorD  },
            new object[]{ 13.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 13.5f, .5f, "rotateZ", -45f, "Bounce", P1.ReceptorL  },

            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorL,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorD,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorU,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorR,  2, .5f},

            new object[]{ 15f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            new object[]{ 15.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },

            new object[]{ 16f, .2f, "moveX", .5f, "Bounce", P1.receptors,  4, 2f},
            new object[]{ 17f, .2f, "moveX", -.5f, "Bounce", P1.receptors,  4, 2f},

            new object[]{ 18f, .5f, "rotateZ", 180f, "Float", P1.ReceptorL,  2, .5f},
            new object[]{ 20f, .5f, "rotateZ", 180f, "Float", P1.ReceptorU,  2, .5f},
            new object[]{ 22f, .5f, "rotateZ", 180f, "Float", P1.ReceptorD,  2, .5f},

            new object[]{ 24f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 25f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 26f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 27f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 28f, 0.1f, "resetRotation", 0f, "Linear", P1.receptors  },

            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorR },
            new object[]{ 28f, .2f, "moveZ", 5f, "EaseInOutElastic", P1.receptors  },
            new object[]{ 28f, .2f, "moveY", -2f, "EaseInOutElastic", P1.receptors  },

            new object[]{ 28f, .25f, "moveX", .65f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 28f, .25f, "moveX", .2f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 28f, .25f, "moveX", -.2f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 28f, .25f, "moveX", -.65f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 30f, 2f, "playAnimation", "FadeToWhite", backgroundAnimations,"Speed"},
            new object[]{ 30f, 2f, "playAnimation", "LightToStage", lightAnimations,"Speed"},

            new object[]{ 32f, 0.1f, "moveY", 3f, "EaseInOutElastic", P1.receptors  },
            new object[]{ 32f, 0.1f, "moveZ", -5f, "EaseInOutElastic", P1.receptors  },

            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 32f, .1f, "moveX", -.65f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 32f, .1f, "moveX", -.2f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 32f, .1f, "moveX", .2f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 32f, .1f, "moveX", .65f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 32f, 96f, "playAnimation", "RoadStartMultiplayer", backgroundAnimations, "WorldSpeed" },

            //NEXT SECTION

            new object[]{32f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{32f, 1f, "moveX", 2.25f, "EaseOut", P1.gameObject },
            new object[]{32f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{32f, 1f, "rotateZ", 30f, "Linear", P1.gameObject },

            new object[]{33f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{33f, 1f, "moveX", -5.25f, "EaseOut", P1.gameObject },
            new object[]{33f, 1f, "moveZ", -1f, "EaseOut", P1.gameObject },
            new object[]{33f, 1f, "rotateZ", -60f, "Linear", P1.gameObject },

            new object[]{33f, .75f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed", 95, 2f },

            new object[]{34f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{34f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{34f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{34f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{34.5f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{34.5f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{34.5f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{34.5f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{36f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{36f, 1f, "moveX", 3f, "EaseOut", P1.gameObject },
            new object[]{36f, 1f, "moveZ", 2f, "EaseOut", P1.gameObject },
            new object[]{36f, 1f, "rotateZ", 75f, "Linear", P1.gameObject },
            new object[]{36.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{37f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{37f, 1f, "moveZ", 3f, "EaseOut", P1.gameObject },
            new object[]{37f, 1f, "rotateZ", -40f, "Linear", P1.gameObject },
            new object[]{37.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{38f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{38f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{38f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{38f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{38f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },

            new object[]{38.5f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{38.5f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{38.5f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{38.5f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorR  },


            new object[]{40f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{40f, 1f, "moveX", -1.5f, "EaseOut", P1.gameObject },
            new object[]{40f, 1f, "moveZ", -1f, "EaseOut", P1.gameObject },

            new object[]{40.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject },

            new object[]{41f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{41f, 1f, "moveX", 1.5f, "EaseOut", P1.gameObject },
            new object[]{41f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{41.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject},

            new object[]{ 42f, .5f, "moveY", 2f, "Bounce", P1.ReceptorL },
            new object[]{ 42.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU },
            new object[]{ 43f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },

            new object[]{ 44f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 44.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 45f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 45.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 46f, .5f, "moveX", -.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 46.5f, .5f, "moveX", .5f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 47f, .5f, "moveX", -.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 47.5f, .4f, "moveX", .5f, "EaseInOutElastic", P1.ReceptorU },

            new object[]{ 48f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 48f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 48f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 48f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{48f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{48f, 1f, "moveX", 2.25f, "EaseOut", P1.gameObject },
            new object[]{48f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{48f, 1f, "rotateZ", 35f, "Linear", P1.gameObject },


            new object[]{49f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{49f, 1f, "moveX", -1.25f, "EaseOut", P1.gameObject },
            new object[]{49f, 1f, "moveZ", 4f, "EaseOut", P1.gameObject },
            new object[]{49f, 1f, "rotateZ", -55f, "Linear", P1.gameObject },

            new object[]{50f, .4f, "scale", .25f, "EaseInCirc", P1.ReceptorL  }, //Change it to moving motions, like the beat 54 section
            new object[]{50.5f, .4f, "scale", .25f, "EaseInCirc", P1.ReceptorR  },
            new object[]{51f, .4f, "scale", 1f, "EaseInCirc", P1.ReceptorL  },
            new object[]{51f, .4f, "scale", 1f, "EaseInCirc", P1.ReceptorR  },

            new object[]{52f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{52f, 1f, "moveX", -4f, "EaseOut", P1.gameObject },
            new object[]{52f, 1f, "moveZ", 1f, "EaseOut", P1.gameObject },
            new object[]{52f, 1f, "rotateZ", -35f, "Linear", P1.gameObject },

            new object[]{53f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{53f, 1f, "moveX", 6f, "EaseOut", P1.gameObject },
            new object[]{53f, 1f, "moveZ", 3f, "EaseOut", P1.gameObject },
            new object[]{53f, 1f, "rotateZ", 85f, "Linear", P1.gameObject },
            new object[]{53.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{54f, .5f, "moveX", -1f, "EaseInOutElastic", P1.gameObject  },
            new object[]{54.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.gameObject  },
            new object[]{55f, .5f, "moveX", -1f, "EaseInOutElastic", P1.gameObject  },

            new object[]{56f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{56f, 1f, "moveX", -2.068705f, "EaseOut", P1.gameObject },
            new object[]{56f, 1f, "moveZ", -7f, "EaseOut", P1.gameObject },
            new object[]{56f, 1f, "rotateZ", -45f, "Linear", P1.gameObject },
            new object[]{56.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject },

            new object[]{57f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{57f, 1f, "moveX", 1f, "EaseOut", P1.gameObject },
            new object[]{57f, 1f, "moveZ", 2f, "EaseOut", P1.gameObject },
            new object[]{57f, 1f, "rotateZ", 15f, "Linear", P1.gameObject },
            new object[]{57.5f, .5f, "moveY", -2.146821f, "EaseOut", P1.gameObject },

            new object[]{58f, .1f, "resetPlayfield", 0f, "EaseInOutQuad", P1.gameObject },
            new object[]{58f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },                         ///RESET

            new object[]{58f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{58f, .5f, "moveY", 1f, "Bounce", P1.ReceptorR  },
            new object[]{58.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{58.5f, .5f, "moveY", 1f, "Bounce", P1.ReceptorL  },
            new object[]{59f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{59f, .5f, "moveY", 1f, "Bounce", P1.ReceptorR  },
            new object[]{59.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{59.5f, .5f, "moveY", 1f, "Bounce", P1.ReceptorL  },

            new object[]{60f, .5f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{60.5f, .5f, "rotateY", 90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{61f, .5f, "rotateY", 90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{61.5f, .5f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{62f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },                         ///RESET

            new object[]{62f, .5f, "moveY", 1f, "Bounce", P1.receptors,  2, .5f },

            new object[]{63f, .5f, "scale", 2f, "Bounce", P1.ReceptorD  },


            //NEXT SECTION

            new object[]{64f, 2f, "scale", 4f, "Linear", P1.ReceptorR  },
            new object[]{64f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorR  },

            new object[]{64f, 2f, "moveX", -.5f, "Linear", P1.ReceptorU  },
            new object[]{64f, 2f, "moveX", -1f, "Linear", P1.ReceptorD  },
            new object[]{64f, 2f, "moveX", -1.5f, "Linear", P1.ReceptorL  },

            new object[]{66f, .25f, "moveX", .5f, "Linear", P1.ReceptorU  },
            new object[]{66f, .25f, "moveX", 1f, "Linear", P1.ReceptorD  },
            new object[]{66f, .25f, "moveX", 1.5f, "Linear", P1.ReceptorL  },

            new object[]{66f, .25f, "scale", 1f, "EaseInCirc", P1.ReceptorR  },
            new object[]{66f, .25f, "moveZ", .25f, "Linear", P1.ReceptorR  },

            new object[]{66.5f, .5f, "moveZ", 4f, "Float", P1.ReceptorD  },
            new object[]{67f, .5f, "moveZ", 4f, "Float", P1.ReceptorU  },
            new object[]{67.5f, 2f, "scale", 4f, "Linear", P1.ReceptorL  },
            new object[]{67.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorL  },

            new object[]{67.5f, 2f, "moveX", .5f, "Linear", P1.ReceptorD  },
            new object[]{67.5f, 2f, "moveX", 1f, "Linear", P1.ReceptorU  },
            new object[]{67.5f, 2f, "moveX", 1.5f, "Linear", P1.ReceptorR  },

            new object[]{69.5f, .25f, "moveX", -.5f, "Linear", P1.ReceptorD  },
            new object[]{69.5f, .25f, "moveX", -1f, "Linear", P1.ReceptorU  },
            new object[]{69.5f, .25f, "moveX", -1.5f, "Linear", P1.ReceptorR  },

            new object[]{69.5f, .25f, "scale", 1f, "EaseInCirc", P1.ReceptorL  },
            new object[]{69.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorL  },

            new object[]{70.5f, .5f, "rotateZ", 90f, "EaseIn", P1.ReceptorD  },
            new object[]{70.5f, .5f, "rotateZ", 180f, "EaseIn", P1.ReceptorU  },
            new object[]{71.5f, 1f, "rotateZ", -90f, "EaseIn", P1.ReceptorD  },
            new object[]{71.5f, 1f, "rotateZ", -180f, "EaseIn", P1.ReceptorU  },

            new object[]{71.5f, 2f, "scale", 4f, "Linear", P1.ReceptorR  },
            new object[]{71.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorR  },

            new object[]{71.5f, 2f, "moveX", -.5f, "Linear", P1.ReceptorU  },
            new object[]{71.5f, 2f, "moveX", -1f, "Linear", P1.ReceptorD  },
            new object[]{71.5f, 2f, "moveX", -1.5f, "Linear", P1.ReceptorL  },

            new object[]{73.5f, .25f, "moveX", .5f, "Linear", P1.ReceptorU  },
            new object[]{73.5f, .25f, "moveX", 1f, "Linear", P1.ReceptorD  },
            new object[]{73.5f, .25f, "moveX", 1.5f, "Linear", P1.ReceptorL  },

            new object[]{73.5f, .25f, "scale", 1f, "Linear", P1.ReceptorR  },
            new object[]{73.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorR  },

            new object[]{74.5f, .5f, "moveY", -3f, "Bounce", P1.ReceptorL  },
            new object[]{75f, .5f, "moveY", -3f, "Bounce", P1.ReceptorD  },
            new object[]{75.5f, .5f, "moveY", -3f, "Bounce", P1.ReceptorU  },

            new object[]{ 76.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },

            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 77f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 77.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 78f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },

            new object[]{ 78.5f, .5f, "moveX", -.640f * 2f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 78.5f, .5f, "moveX", .640f * 2f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 79f, .5f, "moveX", .640f * 2f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 79f, .5f, "moveX", -.640f * 2f, "EaseInOutQuad", P1.ReceptorD  },

            new object[]{79.5f, .25f, "moveY", .5f, "Float", P1.ReceptorU,  8, .25f },
            new object[]{79.5f, 2f, "scale", 4f, "Linear", P1.ReceptorU  },
            new object[]{79.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorU  },

            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorL, 10, .2f },
            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorD, 10, .2f },
            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorR, 10, .2f },

            new object[]{81.5f, .25f, "scale", 1f, "Linear", P1.ReceptorU },
            new object[]{81.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorU },

            new object[]{82.5f, .4f, "scale", .25f, "Linear", P1.ReceptorD },
            new object[]{82.5f, .4f, "rotateZ", 180f, "Linear", P1.ReceptorD},

            new object[]{83f, .4f, "scale", .25f, "Linear", P1.ReceptorR },
            new object[]{83f, .4f, "rotateZ", 180f, "Linear", P1.ReceptorR },

            new object[]{83.5f, .1f, "scale", 1f, "Linear", P1.ReceptorD },
            new object[]{83.5f, .1f, "scale", 1f, "Linear", P1.ReceptorR },
            new object[]{83.5f, .5f, "rotateZ", -180f, "Linear", P1.ReceptorD},
            new object[]{83.5f, .5f, "rotateZ", -180f, "Linear", P1.ReceptorR},

            new object[]{83.5f, .25f, "moveY", .5f, "Float", P1.ReceptorU,  8, .25f },
            new object[]{83.5f, 2f, "scale", 4f, "Linear", P1.ReceptorU  },
            new object[]{83.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorU  },

            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorL ,10, .2f },
            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorD ,10, .2f },
            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorR ,10, .2f },

            new object[]{85.5f, .25f, "scale", 1f, "Linear", P1.ReceptorU},
            new object[]{85.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorU},

            new object[]{86.5f, .4f, "moveX", -3f, "Bounce", P1.ReceptorD},
            new object[]{87f, .4f, "moveX", -3f, "Bounce", P1.ReceptorL},

            new object[]{87.5f, .25f, "moveY", .75f, "Float", P1.ReceptorU,  8, .25f },
            new object[]{87.5f, 2f, "scale", 5f, "Linear", P1.ReceptorU  },
            new object[]{87.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorU  },

            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorL ,10, .2f },
            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorD ,10, .2f },
            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorR ,10, .2f },

            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorL },
            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorD },
            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorR },

            new object[]{89.5f, .25f, "scale", 1f, "Linear", P1.ReceptorU},
            new object[]{89.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorU},
            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorL },
            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorD },
            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorR },

            new object[]{90.5f, .5f, "moveX", -2f, "Bounce", P1.receptors  },
            new object[]{91f, .5f, "moveY", -1f, "EaseInCirc", P1.receptors  },
            new object[]{91.5f , 1f, "moveY", 1f, "Linear", P1.receptors  },
            new object[]{91.5f , .2f, "moveX", .2f, "Float", P1.receptors,  5, .2f },

            new object[]{ 92.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },

            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 93f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 93.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 94f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },

            new object[]{ 94.5f, .5f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{ 95f, .5f, "rotateZ", 45f, "Bounce", P1.gameObject },

               //NEXT SECTION??

            new object[]{96f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{96.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{97f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{98.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{99f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{99.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },

            new object[]{100.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{101f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{101.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{102f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{102.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{103f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{103.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },

            new object[]{104.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{105f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{105.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{106.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{107f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{107.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },

            new object[]{107.5f, 1f, "moveX", .960f, "Linear", P1.ReceptorL },
            new object[]{107.5f, 1f, "moveX", -.960f, "Linear", P1.ReceptorR },
            new object[]{107.5f, 1f, "moveX", .320f, "Linear", P1.ReceptorD },
            new object[]{107.5f, 1f, "moveX", -.320f, "Linear", P1.ReceptorU },
            new object[]{107.5f, 1f, "moveX", .960f, "Linear", P1.spawnL },
            new object[]{107.5f, 1f, "moveX", -.960f, "Linear", P1.spawnR },
            new object[]{107.5f, 1f, "moveX", .320f, "Linear", P1.spawnD },
            new object[]{107.5f, 1f, "moveX", -.320f, "Linear", P1.spawnU },

            new object[]{108.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{109f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{109.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{110f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{110.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{111f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{111.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{111.5f, 1f, "moveX", -.960f, "Linear", P1.ReceptorL },
            new object[]{111.5f, 1f, "moveX", .960f, "Linear", P1.ReceptorR },
            new object[]{111.5f, 1f, "moveX", -.320f, "Linear", P1.ReceptorD },
            new object[]{111.5f, 1f, "moveX", .320f, "Linear", P1.ReceptorU },
            new object[]{111.5f, 1f, "moveX", -.960f, "Linear", P1.spawnL },
            new object[]{111.5f, 1f, "moveX", .960f, "Linear", P1.spawnR },
            new object[]{111.5f, 1f, "moveX", -.320f, "Linear", P1.spawnD },
            new object[]{111.5f, 1f, "moveX", .320f, "Linear", P1.spawnU },

            new object[]{112.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },
            new object[]{113f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorD  },
            new object[]{113.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{114.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },
            new object[]{115f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorR  },
            new object[]{115.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorD  },

            new object[]{116.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{117f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{117.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{118f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{118.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{119f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{119.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{120.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },
            new object[]{121f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorR  },
            new object[]{121.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{122.5f, .9f, "moveY", -1f ,"Bounce", P1.ReceptorD  },
            new object[]{123f, .9f, "moveX", -1f ,"Bounce", P1.ReceptorL  },
            new object[]{123.5f, .9f, "moveX", 1f ,"Bounce", P1.ReceptorR  },

            new object[]{124.5f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{125f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{125.5f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{126f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{126.5f, .1f, "resetRotation", 0f, "Linear", P1.gameObject  },

            //NEW SECTION MULTIPLAYER

            new object[]{128f, 4f, "moveX", 6f, "EaseInOutQuad", P1.gameObject  },
            new object[]{128f, 4f, "moveZ", -2f, "EaseInOutQuad", P1.gameObject  },
            new object[]{128f, 4f, "rotateY", 45f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{128f, 0.5f, "playParticles", postArrowR,  4, 1f},

            new object[]{132f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorD  },
            new object[]{133f, 1f, "moveY", -2f, "Linear", P1.ReceptorD  },
            new object[]{133f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorD  },

            new object[]{134f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorU  },
            new object[]{135f, 1f, "moveY", -2f, "Linear", P1.ReceptorU  },
            new object[]{135f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorU  },

            new object[]{136f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorL  },
            new object[]{137f, 1f, "moveY", -2f, "Linear", P1.ReceptorL  },
            new object[]{137f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorL  },

            //new object[]{138f, 1f, "playParticles", postArrowD },
            //new object[]{139f, 1f, "playParticles", postArrowL },
            //new object[]{140f, 1f, "playParticles", postArrowD },
            //new object[]{141f, 1f, "playParticles", postArrowU },
            //new object[]{142f, 1f, "playParticles", postArrowR },
            //new object[]{142.5f, 1f, "playParticles", postArrowD },
            //new object[]{143f, 1f, "playParticles", postArrowR },

            //new object[]{144f, 0.5f, "playParticles", postArrowL,  4, 1f},

            new object[]{144f, 4f, "moveX", -12f, "EaseInOutQuad", P1.gameObject  },
            new object[]{144f, 2f, "moveZ", 2f, "EaseOut", P1.gameObject  },
            new object[]{144f, 4f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{146f, 2f, "moveZ", -2f, "EaseOut", P1.gameObject  },

            new object[]{148f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorU  },
            new object[]{149f, 1f, "moveY", -2f, "Linear", P1.ReceptorU  },
            new object[]{149f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorU  },

            new object[]{150f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorD  },
            new object[]{151f, 1f, "moveY", -2f, "Linear", P1.ReceptorD  },
            new object[]{151f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorD  },

            //new object[]{152f, 1f, "playParticles", postArrowR },
            //new object[]{153f, 1f, "playParticles", postArrowU },
            //new object[]{154f, 1f, "playParticles", postArrowD },
            //new object[]{155f, 1f, "playParticles", postArrowL },

            new object[]{156f, 1f, "moveY", 2f, "Bounce", P1.gameObject,  4, 1f},
            new object[]{156f, 4f, "moveX", 6f, "EaseInOutQuad", P1.gameObject  },
            new object[]{156f, 4f, "moveZ", 2f, "EaseInOutQuad", P1.gameObject  },
            new object[]{156f, 4f, "rotateY", 45f, "EaseInOutQuad", P1.gameObject  },
    
            //MULTIPLAYER VARIANT SECTION ENDS HERE

            new object[]{160f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },
            new object[]{160f, .1f, "resetPlayfield", 0f, "Linear", P1.gameObject },

            new object[]{160f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners }, //playfield1.spawnsRoot at -5
            new object[]{160.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{161f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{161.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{162f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{162.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },

            new object[]{163.5f, 1f, "scale", .5f, "InstantIn", P1.gameObject },
            new object[]{163.5f, 1f, "moveZ", 2f, "InstantIn", P1.gameObject },

            new object[]{165f, .5f, "rotateZ", 45f, "EaseInOutElastic", P1.gameObject },
            new object[]{165.5f, .5f, "rotateZ", -45f, "EaseInOutElastic", P1.gameObject },
            new object[]{166f, .5f, "rotateZ", -45f, "EaseInOutElastic", P1.gameObject },
            new object[]{166.5f, .5f, "rotateZ", 45f, "EaseInOutElastic", P1.gameObject },

            new object[]{168f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{168.5f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },
            new object[]{169f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },
            new object[]{169.5f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{170f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{170.5f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },

            new object[]{171.5f, 1f, "scale", 2f, "InstantIn", P1.receptors },
            new object[]{171.5f, 1f, "moveY", -1f, "InstantIn", P1.gameObject },
            new object[]{171.5f, 1f, "moveZ", -2f, "InstantIn", P1.gameObject },

            new object[]{172.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors }, //playfield1.receptorsRoot at 4
            new object[]{173f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{173.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{174f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{174.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{175f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },

            new object[]{176f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners }, //playfield1.spawnsRoot at -5
            new object[]{176.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{177f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{177.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{178f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{178.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },

            new object[]{179.5f, 1f, "scale", .5f, "InstantIn", P1.gameObject },
            new object[]{179.5f, 1f, "moveZ", 2f, "InstantIn", P1.gameObject },

            new object[]{181f, .5f, "rotateX", 90f, "EaseInOutElastic", P1.gameObject },
            new object[]{181.5f, .5f, "rotateX", -90f, "EaseInOutElastic", P1.gameObject },
            new object[]{182f, .5f, "rotateX", -90f, "EaseInOutElastic", P1.gameObject },
            new object[]{182.5f, .5f, "rotateX", 90f, "EaseInOutElastic", P1.gameObject },

            new object[]{184f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors }, //playfield1.receptorsRoot at 4
            new object[]{184.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{185f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{185.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{186f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{186.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },

            new object[]{187.5f, 1f, "scale", 2f, "InstantIn", P1.receptors },
            new object[]{187.5f, 1f, "moveY", -1f, "InstantIn", P1.gameObject },
            new object[]{187.5f, 1f, "moveZ", -2f, "InstantIn", P1.gameObject },

            new object[]{188.5f, .25f, "resetPosition",0f, "Linear", P1.receptors },
            new object[]{188.5f, .25f, "resetPosition",0f, "Linear", P1.spawners },
            new object[]{188.5f, .25f, "resetRotation", 0f, "Linear", P1.gameObject },

            new object[]{189f, .25f, "moveX", -2f, "Bounce", P1.receptors  },
            new object[]{189.5f, .25f, "moveX", 2f, "Bounce", P1.receptors  },

            new object[]{190f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{190f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{190f, .5f, "moveY", -.75f, "EaseOut", P1.receptors  },
            new object[]{190f, .5f, "moveY", .75f, "EaseOut", P1.spawners  },
            new object[]{190f, .5f, "moveZ", 1.5f, "EaseOut", P1.spawners  },

            new object[]{190.5f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{190.5f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{190.5f, .5f, "moveY", -.75f, "EaseOut", P1.receptors  },
            new object[]{190.5f, .5f, "moveY", .75f, "EaseOut", P1.spawners  },
            new object[]{190.5f, .5f, "moveZ", 1.5f, "EaseOut", P1.spawners  },

            new object[]{191f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{191f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{191f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{191f, .5f, "moveY", .5f, "EaseOut", P1.spawners  },
            new object[]{191f, .5f, "moveZ", 1f, "EaseOut", P1.spawners  },

            new object[]{191.5f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },
            new object[]{191.5f, 0f, "dark", 0.5f, "Linear" , P1.gameObject }, //TODO: Use the new system to use stealth

            new object[]{191.5f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{191.5f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{191.5f, .5f, "moveY", .5f, "EaseOut", P1.spawners  },
            new object[]{191.5f, .5f, "moveZ", 1f, "EaseOut", P1.spawners  },

            new object[]{192f, 15f, "moveY", .25f, "Float", P1.receptors,  2, 15f },
            new object[]{192f, 7.5f, "rotateZ", 6f, "Float", P1.receptors,  4, 7.5f },
            new object[]{192f, 4f, "moveY", 2.5f, "Float", P1.spawners,  7, 4f },

            new object[]{ 192f, 92f, "playAnimation", "RoadFinishMultiplayer", backgroundAnimations, "WorldSpeed"},

            new object[]{ 192f, .2f, "scale", 1.1f, "Float", P1.ReceptorR, 15, .2f }, //Vibrato effect?

            new object[]{192f, .25f, "moveY", -.25f, "InstantIn", P1.gameObject, 16, 2f},
            new object[]{193f, .25f, "moveY", .25f, "InstantIn", P1.gameObject, 16, 2f}, //Beat movement

            new object[]{195.5f, 1.4f, "scale", 1.5f, "Linear", P1.receptors  },
            new object[]{197f, .25f, "scale", 1f, "Linear", P1.receptors  },

            new object[]{200f, .2f, "scale", 1.1f, "Float", P1.ReceptorL, 15, .2f }, //Vibrato effect

            new object[]{203.5f, 1.4f, "scale", 1.5f, "Linear", P1.receptors  },
            new object[]{205f, .25f, "scale", 1f, "Linear", P1.receptors  },

            new object[]{206f, .5f, "moveX", .2f, "Bounce", P1.ReceptorR  },
            new object[]{206.5f, .5f, "moveX", -.2f, "Bounce", P1.ReceptorL  },
            new object[]{207f, .5f, "moveX", .2f, "Bounce", P1.ReceptorR  },

            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorL,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorD,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorU,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorR,  7, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorL,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorD,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorU,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorR,  8, 2f},

            new object[]{223f, .5f, "resetPosition", 0f, "EaseInOutQuad", P1.receptors  },
            new object[]{223f, .5f, "resetPosition", 0f, "EaseInOutQuad", P1.spawners  },

            new object[]{223f, 0f, "dark", 0f, "Linear" , P1.gameObject }, //TODO: Use the new system to use stealth

            //SECOND GIMMICK SECTION MULTIPLAYER - DPAD

            new object[]{224f, .25f, "moveZ", 6f, "Linear", P1.spawners  },
            new object[]{224f, .25f, "moveZ", -3f, "Linear", P1.gameObject  },


            new object[]{224f, .25f, "moveX", .96f, "Linear", P1.ReceptorL  }, //-.5
            new object[]{224f, .25f, "moveX", -.96f, "Linear", P1.ReceptorR  }, //-.5
            new object[]{224f, .25f, "moveX", .32f, "Linear", P1.ReceptorD  }, //-.5
            new object[]{224f, .25f, "moveX", -.32f, "Linear", P1.ReceptorU  }, //-.5

            new object[]{224f, .25f, "moveY", 5f, "Linear", P1.spawnL  }, //-.5
            new object[]{224f, .25f, "moveY", 5f, "Linear", P1.spawnR  }, //-.5
            new object[]{224f, .25f, "moveY", 10f, "Linear", P1.spawnU  }, //-.5

            new object[]{224f, .25f, "moveX", -4.04f, "Linear", P1.spawnL  }, //-.5
            new object[]{224f, .25f, "moveX", 4.04f, "Linear", P1.spawnR  }, //-.5
            new object[]{224f, .25f, "moveX", .32f, "Linear", P1.spawnD  }, //-.5
            new object[]{224f, .25f, "moveX", -.32f, "Linear", P1.spawnU  }, //-.5

            //Clumple/DPAD setup!

            new object[]{227.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{227.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{227.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{227.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //Spread

            new object[]{231.5f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorL  },
            new object[]{231.5f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorR  },
            new object[]{231.5f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorU  },
            new object[]{231.5f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorD  },

            //Clumple + spanwers reverse

            new object[]{235.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{235.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{235.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{235.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //spread reverse

            new object[]{240f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorL  },
            new object[]{240f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorR  },
            new object[]{240f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorU  },
            new object[]{240f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorD  },

            //clumple + spanwers to normal

            new object[]{243.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{243.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{243.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{243.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //spread normal

            new object[]{247.5f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorL  },
            new object[]{247.5f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorR  },
            new object[]{247.5f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorU  },
            new object[]{247.5f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorD  },

            //Clumple 

            new object[]{251.5f, .5f, "moveX", -0.96f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{251.5f, .5f, "moveX", -0.32f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{251.5f, .5f, "moveX", 0.32f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{251.5f, .5f, "moveX", 0.96f, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{251.5f, .25f, "moveX", 5f, "Linear", P1.spawnL  },
            new object[]{251.5f, .25f, "moveX", -5f, "Linear", P1.spawnR  },
            new object[]{251.5f, .25f, "moveY", -10f, "Linear", P1.spawnU  },
            new object[]{251.5f, .25f, "moveY", -5f, "Linear", P1.spawnL  },
            new object[]{251.5f, .25f, "moveY", -5f, "Linear", P1.spawnR  },

            new object[]{253f, .5f, "moveX", -0.96f, "Linear", P1.spawnL  },
            new object[]{253f, .5f, "moveX", -0.32f, "Linear", P1.spawnD  },
            new object[]{253f, .5f, "moveX", 0.32f, "Linear", P1.spawnU  },
            new object[]{253f, .5f, "moveX", 0.96f, "Linear", P1.spawnR  },

            //NORMAL


            new object[]{253f, .25f, "moveZ", 3f, "Linear", P1.gameObject  },
            new object[]{253f, .25f, "moveZ", -6f, "Linear", P1.spawners  },


            new object[]{253f, .5f, "moveY", 2.5f, "EaseInOutQuad", P1.receptors  },
            new object[]{254f, .4f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{254.5f, .4f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{255f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },

            //SECOND SECTION GIMMICK ENDS HERE 
            //new object[]{256f, 1f, "playParticles", receptorsExplosionParticles,  6, 2f},

            new object[]{256f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorL},
            new object[]{256f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{256f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorD},
            new object[]{256f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{256f, 0.9f, "moveX", .3f, "EaseIn", P1.ReceptorU},
            new object[]{256f, 0.9f, "moveY", .2f, "EaseIn", P1.ReceptorU},
            new object[]{256f, 0.9f, "moveX", .5f, "EaseIn", P1.ReceptorR},
            new object[]{256f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorR},

            new object[]{257f, 1f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed" , 14, 2f},

            new object[]{257f, 1f, "moveZ", -8f, "Bounce", P1.receptors,  6, 2f},
            new object[]{257f, 1f, "moveY", -1f, "Bounce", P1.receptors,  6, 2f},

            new object[]{257f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorL},
            new object[]{257f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{257f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorD},
            new object[]{257f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{257f, 0.9f, "moveX", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{257f, 0.9f, "moveY", -.2f, "EaseOut", P1.ReceptorU},
            new object[]{257f, 0.9f, "moveX", -.5f, "EaseOut", P1.ReceptorR},
            new object[]{257f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorR},

            new object[]{258f, 0.9f, "moveX", -.3f, "EaseIn", P1.ReceptorL},
            new object[]{258f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorL},
            new object[]{258f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{258f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorD},
            new object[]{258f, 0.9f, "moveX", .6f, "EaseIn", P1.ReceptorU},
            new object[]{258f, 0.9f, "moveY", -.2f, "EaseIn", P1.ReceptorU},
            new object[]{258f, 0.9f, "moveX", .2f, "EaseIn", P1.ReceptorR},
            new object[]{258f, 0.9f, "moveY", .6f, "EaseIn", P1.ReceptorR},

            new object[]{259f, 0.9f, "moveX", .3f, "EaseOut", P1.ReceptorL},
            new object[]{259f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorL},
            new object[]{259f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{259f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorD},
            new object[]{259f, 0.9f, "moveX", -.6f, "EaseOut", P1.ReceptorU},
            new object[]{259f, 0.9f, "moveY", .2f, "EaseOut", P1.ReceptorU},
            new object[]{259f, 0.9f, "moveX", -.2f, "EaseOut", P1.ReceptorR},
            new object[]{259f, 0.9f, "moveY", -.6f, "EaseOut", P1.ReceptorR},

            new object[]{260f, 0.9f, "moveX", -.7f, "EaseIn", P1.ReceptorL},
            new object[]{260f, 0.9f, "moveY", .1f, "EaseIn", P1.ReceptorL},
            new object[]{260f, 0.9f, "moveX", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{260f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{260f, 0.9f, "moveX", .1f, "EaseIn", P1.ReceptorU},
            new object[]{260f, 0.9f, "moveY", .4f, "EaseIn", P1.ReceptorU},
            new object[]{260f, 0.9f, "moveX", .7f, "EaseIn", P1.ReceptorR},
            new object[]{260f, 0.9f, "moveY", -.1f, "EaseIn", P1.ReceptorR},

            new object[]{261f, 0.9f, "moveX", .7f, "EaseOut", P1.ReceptorL},
            new object[]{261f, 0.9f, "moveY", -.1f, "EaseOut", P1.ReceptorL},
            new object[]{261f, 0.9f, "moveX", .4f, "EaseOut", P1.ReceptorD},
            new object[]{261f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{261f, 0.9f, "moveX", -.1f, "EaseOut", P1.ReceptorU},
            new object[]{261f, 0.9f, "moveY", -.4f, "EaseOut", P1.ReceptorU},
            new object[]{261f, 0.9f, "moveX", -.7f, "EaseOut", P1.ReceptorR},
            new object[]{261f, 0.9f, "moveY", .1f, "EaseOut", P1.ReceptorR},

            new object[]{262f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorL},
            new object[]{262f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{262f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{262f, 0.9f, "moveY", -.6f, "EaseIn", P1.ReceptorD},
            new object[]{262f, 0.9f, "moveX", .4f, "EaseIn", P1.ReceptorU},
            new object[]{262f, 0.9f, "moveY", -.5f, "EaseIn", P1.ReceptorU},
            new object[]{262f, 0.9f, "moveX", .2f, "EaseIn", P1.ReceptorR},
            new object[]{262f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorR},

            new object[]{263f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorL},
            new object[]{263f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{263f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{263f, 0.9f, "moveY", .6f, "EaseOut", P1.ReceptorD},
            new object[]{263f, 0.9f, "moveX", -.4f, "EaseOut", P1.ReceptorU},
            new object[]{263f, 0.9f, "moveY", .5f, "EaseOut", P1.ReceptorU},
            new object[]{263f, 0.9f, "moveX", -.2f, "EaseOut", P1.ReceptorR},
            new object[]{263f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorR},

            new object[]{264f, 0.9f, "moveX", -.4f, "EaseIn", P1.ReceptorL},
            new object[]{264f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorL},
            new object[]{264f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{264f, 0.9f, "moveY", .3f, "EaseIn", P1.ReceptorD},
            new object[]{264f, 0.9f, "moveX", .1f, "EaseIn", P1.ReceptorU},
            new object[]{264f, 0.9f, "moveY", .3f, "EaseIn", P1.ReceptorU},
            new object[]{264f, 0.9f, "moveX", .6f, "EaseIn", P1.ReceptorR},
            new object[]{264f, 0.9f, "moveY", -.2f, "EaseIn", P1.ReceptorR},

            new object[]{265f, 0.9f, "moveX", .4f, "EaseOut", P1.ReceptorL},
            new object[]{265f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorL},
            new object[]{265f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{265f, 0.9f, "moveY", -.3f, "EaseOut", P1.ReceptorD},
            new object[]{265f, 0.9f, "moveX", -.1f, "EaseOut", P1.ReceptorU},
            new object[]{265f, 0.9f, "moveY", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{265f, 0.9f, "moveX", -.6f, "EaseOut", P1.ReceptorR},
            new object[]{265f, 0.9f, "moveY", .2f, "EaseOut", P1.ReceptorR},

            new object[]{266f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorL},
            new object[]{266f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{266f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorD},
            new object[]{266f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{266f, 0.9f, "moveX", .3f, "EaseIn", P1.ReceptorU},
            new object[]{266f, 0.9f, "moveY", .2f, "EaseIn", P1.ReceptorU},
            new object[]{266f, 0.9f, "moveX", .5f, "EaseIn", P1.ReceptorR},
            new object[]{266f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorR},

            new object[]{267f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorL},
            new object[]{267f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{267f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorD},
            new object[]{267f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{267f, 0.9f, "moveX", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{267f, 0.9f, "moveY", -.2f, "EaseOut", P1.ReceptorU},
            new object[]{267f, 0.9f, "moveX", -.5f, "EaseOut", P1.ReceptorR},
            new object[]{267f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorR},

            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 268f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 268.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 269f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 269.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 270f, .5f, "moveY", -1f, "Bounce", P1.receptors  },
            new object[]{ 270.5f, .5f, "moveY", -1f, "Bounce", P1.receptors  },
            new object[]{ 271f, .5f, "moveX", -1f, "Bounce", P1.receptors  },
            new object[]{ 271.5f, .5f, "moveY", 1f, "Bounce", P1.receptors  },

            new object[]{272f, .95f, "moveY", 1.5f, "Bounce", P1.spawners, 12, 1f},
            new object[]{272f, .95f, "moveY", -1.5f, "Bounce", P1.receptors, 12, 1f},

            new object[]{272f, 1f, "moveZ", 5f, "Linear", P1.spawners,  6, 2f},
            new object[]{272f, 1f, "moveX", .25f, "Linear", P1.spawners,  3, 4f},

            new object[]{272f, 1f, "moveZ", -5f, "Linear", P1.receptors,  6, 2f},
            new object[]{272f, 1f, "moveX", -.25f, "Linear", P1.receptors,  3, 4f},

            new object[]{273f, 1f, "moveZ", 5f, "Linear", P1.receptors,  6, 2f},
            new object[]{273f, 1f, "moveX", .25f, "Linear", P1.receptors,  3, 4f},

            new object[]{273f, 1f, "moveZ", -5f, "Linear", P1.spawners,  6, 2f},
            new object[]{273f, 1f, "moveX", -.25f, "Linear", P1.spawners,  3, 4f},

            new object[]{274f, 1f, "moveX", .25f, "Linear", P1.receptors,  3, 4f},
            new object[]{274f, 1f, "moveX", -.25f, "Linear", P1.spawners,  3, 4f},
            new object[]{275f, 1f, "moveX", -.25f, "Linear", P1.receptors,  3, 4f},
            new object[]{275f, 1f, "moveX", .25f, "Linear", P1.spawners,  3, 4f},

            new object[]{284f, 0.5f, "rotateX", 45f, "Linear", P1.gameObject  },
            new object[]{284f, 0.5f, "moveY", .5f, "Linear", P1.gameObject  },
            new object[]{284f, 0.5f, "moveZ", -2f, "Linear", P1.gameObject  },
            new object[]{284f, 0.1f, "moveY", 1f, "Linear", P1.spawners  },

            new object[]{284.1f, 3.4f, "moveY", -6f, "Linear", P1.spawners  },
            new object[]{284.1f, 3.4f, "moveY", -6f, "Linear", P1.receptors  },

            new object[]{287.5f, 3f, "moveY", 50f, "EaseIn", P1.receptors  },
            
};
    }

    public void LoadUnsightedMods()
    {
        xmodU = 1.75f;
        unsightedModChart = new object[][]
        {
            new object[]{0f, 1f, "toggleObject", backgroundAnimations },

            new object[]{0f, 1f, "toggleObject", Lights },
            new object[]{0f, 1f, "toggleObject", lightAnimations },

            new object[]{ 0f, .5f, "moveY", 2f, "Bounce", P1.ReceptorL },

            new object[]{0.5f, 1f, "playAnimation", "BlackFadeIn", backgroundAnimations},
            new object[]{ 0.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            new object[]{ 1f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU },
            new object[]{ 1.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorL },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorD },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorU },
            new object[]{ 2f, 1f, "rotateZ", 45f, "Float", P1.ReceptorR },

            new object[]{2f, 1f, "toggleObject", BG },

            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorL },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorD },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorU },
            new object[]{ 3.5f, .5f, "scale", 1.5f, "Float", P1.ReceptorR },
            new object[]{ 4f, .45f, "moveX", -1.5f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 4.5f, .45f, "moveX", -0.5f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 5f, .45f, "moveX", 0.5f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 5.5f, .45f, "moveX", 1.5f, "EaseInCirc", P1.ReceptorR },
            new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorL },
            new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorD },
            new object[]{ 6f, 1f, "moveZ", 2f, "Float", P1.ReceptorU },
            new object[]{ 6f, 1f, "moveZ", -2f, "Float", P1.ReceptorR },

            new object[]{ 7.5f, .5f, "moveX", 1.5f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 7.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 7.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 7.5f, .5f, "moveX", -1.5f, "EaseInOutElastic", P1.ReceptorR },


            new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 8f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 8f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 8.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 8.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 9f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 9f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 9.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 9.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 10f, .5f, "moveY", -1f, "WhipOut,3", P1.ReceptorL },
            new object[]{ 10f, .5f, "moveY", -1f, "WhipOut,3", P1.ReceptorD },

            new object[]{ 10f, .5f, "moveY", 1f, "WhipOut,3", P1.ReceptorU },
            new object[]{ 10f, .5f, "moveY", 1f, "WhipOut,3", P1.ReceptorR },

            new object[]{ 10f, .5f, "moveX", -1f, "WhipOut,3", P1.ReceptorL },
            new object[]{ 10f, .5f, "moveX", -1f, "WhipOut,3", P1.ReceptorD },

            new object[]{ 10f, .5f, "moveX", 1f, "WhipOut,3", P1.ReceptorU },
            new object[]{ 10f, .5f, "moveX", 1f, "WhipOut,3", P1.ReceptorR },

            new object[]{ 11.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 11.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 11.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 11.5f, .5f, "moveY", -1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 11.5f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 11.5f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 11.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 11.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 12f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 12f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 12.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 12.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 13f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 13f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 13.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 13.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorL,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorD,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorU,  2, .5f},
            new object[]{ 14f, .5f, "scale", 1.5f, "Bounce", P1.ReceptorR,  2, .5f},

            new object[]{ 15f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            new object[]{ 15.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },

            new object[]{ 16f, .2f, "moveX", .5f, "Bounce", P1.receptors,  4, 2f},
            new object[]{ 17f, .2f, "moveX", -.5f, "Bounce", P1.receptors,  4, 2f},

            new object[]{ 24f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 25f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 26f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 27f, 1f, "rotateY", 180f, "Linear", P1.receptors  },
            new object[]{ 28f, 0.1f, "resetRotation", 0f, "Linear", P1.receptors  },

            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 28f, .5f, "scale", .25f, "EaseInCirc", P1.ReceptorR },
            new object[]{ 28f, .2f, "moveZ", 5f, "EaseInOutElastic", P1.receptors  },
            new object[]{ 28f, .2f, "moveY", -2f, "EaseInOutElastic", P1.receptors  },

            new object[]{ 28f, .25f, "moveX", .65f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 28f, .25f, "moveX", .2f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 28f, .25f, "moveX", -.2f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 28f, .25f, "moveX", -.65f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 30f, 2f, "playAnimation", "FadeToWhite", backgroundAnimations,"Speed"},
            new object[]{ 30f, 2f, "playAnimation", "LightToStage", lightAnimations, "Speed"},

            new object[]{ 32f, 0.1f, "moveY", 3f, "EaseInOutElastic", P1.receptors  },
            new object[]{ 32f, 0.1f, "moveZ", -5f, "EaseInOutElastic", P1.receptors  },

            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 32f, .5f, "scale", 1f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 32f, .1f, "moveX", -.65f, "EaseInCirc", P1.ReceptorL },
            new object[]{ 32f, .1f, "moveX", -.2f, "EaseInCirc", P1.ReceptorD },
            new object[]{ 32f, .1f, "moveX", .2f, "EaseInCirc", P1.ReceptorU },
            new object[]{ 32f, .1f, "moveX", .65f, "EaseInCirc", P1.ReceptorR },

            new object[]{ 32f, 96f, "playAnimation", "RoadStartMultiplayer", backgroundAnimations,"WorldSpeed" },

            //NEXT SECTION

            new object[]{32f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{32f, 1f, "moveX", 2.25f, "EaseOut", P1.gameObject },
            new object[]{32f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{32f, 1f, "rotateZ", 30f, "Linear", P1.gameObject },

            new object[]{33f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{33f, 1f, "moveX", -5.25f, "EaseOut", P1.gameObject },
            new object[]{33f, 1f, "moveZ", -1f, "EaseOut", P1.gameObject },
            new object[]{33f, 1f, "rotateZ", -60f, "Linear", P1.gameObject },

            new object[]{33f, .75f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed", 95, 2f},

            new object[]{34f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{34f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{34f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{34f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{34.5f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{34.5f, .5f, "moveX", -.640f * 2, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{34.5f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{34.5f, .5f, "moveX", .640f * 2, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{35.333f, .333f, "rotateZ", 180f, "EaseIn", P1.gameObject },
            new object[]{35.666f, .333f, "rotateZ", 180f, "EaseIn", P1.gameObject },


            new object[]{36f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{36f, 1f, "moveX", 3f, "EaseOut", P1.gameObject },
            new object[]{36f, 1f, "moveZ", 2f, "EaseOut", P1.gameObject },
            new object[]{36f, 1f, "rotateZ", 75f, "Linear", P1.gameObject },
            new object[]{36.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{37f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{37f, 1f, "moveZ", 3f, "EaseOut", P1.gameObject },
            new object[]{37f, 1f, "rotateZ", -40f, "Linear", P1.gameObject },
            new object[]{37.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{38f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{38f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{38f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{38f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{38f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },

            new object[]{38.5f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{38.5f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{38.5f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{38.5f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{39.333f, .333f, "rotateZ", 180f, "EaseIn", P1.gameObject },
            new object[]{39.666f, .333f, "rotateZ", 180f, "EaseIn", P1.gameObject },


            new object[]{40f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{40f, 1f, "moveX", -1.5f, "EaseOut", P1.gameObject },
            new object[]{40f, 1f, "moveZ", -1f, "EaseOut", P1.gameObject },

            new object[]{40.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject },

            new object[]{41f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{41f, 1f, "moveX", 1.5f, "EaseOut", P1.gameObject },
            new object[]{41f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{41.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject },

            new object[]{ 42f, .5f, "moveY", 2f, "Bounce", P1.ReceptorL },
            new object[]{ 42.333f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },
            new object[]{ 42.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU },
            new object[]{ 43f, .5f, "moveY", 2f, "Bounce", P1.ReceptorR },
            new object[]{ 43.333f, .5f, "moveY", 2f, "Bounce", P1.ReceptorU },
            new object[]{ 43.5f, .5f, "moveY", 2f, "Bounce", P1.ReceptorD },

            new object[]{ 44f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 44.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 45f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorR },
            new object[]{ 45.5f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 46f, .5f, "moveX", -.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 46.5f, .5f, "moveX", .5f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 47f, .5f, "moveX", -.5f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 47.5f, .4f, "moveX", .5f, "EaseInOutElastic", P1.ReceptorU },

            new object[]{ 48f, .5f, "moveX", 2f, "EaseInOutElastic", P1.ReceptorL },
            new object[]{ 48f, .5f, "moveX", 1f, "EaseInOutElastic", P1.ReceptorD },
            new object[]{ 48f, .5f, "moveX", -1f, "EaseInOutElastic", P1.ReceptorU },
            new object[]{ 48f, .5f, "moveX", -2f, "EaseInOutElastic", P1.ReceptorR },

            new object[]{48f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{48f, 1f, "moveX", 2.25f, "EaseOut", P1.gameObject },
            new object[]{48f, 1f, "moveZ", -2f, "EaseOut", P1.gameObject },
            new object[]{48f, 1f, "rotateZ", 35f, "Linear", P1.gameObject },


            new object[]{49f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{49f, 1f, "moveX", -1.25f, "EaseOut", P1.gameObject },
            new object[]{49f, 1f, "moveZ", 4f, "EaseOut", P1.gameObject },
            new object[]{49f, 1f, "rotateZ", -55f, "Linear", P1.gameObject },

            new object[]{50f, .4f, "scale", .25f, "EaseInCirc", P1.ReceptorR  }, //Change it to moving motions, like the beat 54 section
            new object[]{50.333f, .4f, "scale", .25f, "EaseInCirc", P1.ReceptorL  },
            new object[]{50.5f, .4f, "scale", .25f, "EaseInCirc", P1.ReceptorD  },
            new object[]{51f, .4f, "scale", 1f, "EaseInCirc", P1.ReceptorL  },
            new object[]{51f, .4f, "scale", 1f, "EaseInCirc", P1.ReceptorR  },
            new object[]{51f, .4f, "scale", 1f, "EaseInCirc", P1.ReceptorD  },


            new object[]{52f, 1f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{52f, 1f, "moveX", -4f, "EaseOut", P1.gameObject },
            new object[]{52f, 1f, "moveZ", 1f, "EaseOut", P1.gameObject },
            new object[]{52f, 1f, "rotateZ", -35f, "Linear", P1.gameObject },

            new object[]{53f, .5f, "moveY", 2f, "EaseIn", P1.gameObject },
            new object[]{53f, 1f, "moveX", 6f, "EaseOut", P1.gameObject },
            new object[]{53f, 1f, "moveZ", 3f, "EaseOut", P1.gameObject },
            new object[]{53f, 1f, "rotateZ", 85f, "Linear", P1.gameObject },
            new object[]{53.5f, .5f, "moveY", -1f, "EaseOut", P1.gameObject },

            new object[]{54f, .5f, "moveX", -1f, "EaseInOutElastic", P1.gameObject  },
            new object[]{54.5f, .5f, "moveY", 1f, "EaseInOutElastic", P1.gameObject  },
            new object[]{55f, .5f, "moveX", -1f, "EaseInOutElastic", P1.gameObject  },

            new object[]{56f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{56f, 1f, "moveX", -2.068705f, "EaseOut", P1.gameObject },
            new object[]{56f, 1f, "moveZ", -7f, "EaseOut", P1.gameObject },
            new object[]{56f, 1f, "rotateZ", -45f, "Linear", P1.gameObject },
            new object[]{56.5f, .5f, "moveY", -2f, "EaseOut", P1.gameObject },

            new object[]{57f, .5f, "moveY", 1f, "EaseIn", P1.gameObject },
            new object[]{57f, 1f, "moveX", 1f, "EaseOut", P1.gameObject },
            new object[]{57f, 1f, "moveZ", 2f, "EaseOut", P1.gameObject },
            new object[]{57f, 1f, "rotateZ", 15f, "Linear", P1.gameObject },
            new object[]{57.5f, .5f, "moveY", -2.146821f, "EaseOut", P1.gameObject },

            new object[]{58f, .1f, "resetPlayfield", 0f, "EaseInOutQuad", P1.gameObject },
            new object[]{58f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },                         ///RESET

            new object[]{58f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{58f, .5f, "moveY", 1f, "Bounce", P1.ReceptorR  },
            new object[]{58.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{58.5f, .5f, "moveY", 1f, "Bounce", P1.ReceptorL  },
            new object[]{59f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{59f, .5f, "moveY", 1f, "Bounce", P1.ReceptorR  },
            new object[]{59.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{59.5f, .5f, "moveY", 1f, "Bounce", P1.ReceptorL  },

            new object[]{60f, .5f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{60.5f, .5f, "rotateY", 90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{61f, .5f, "rotateY", 90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{61.5f, .5f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{62f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },                         ///RESET

            new object[]{62f, .5f, "moveY", 2f, "Bounce", P1.receptors,  2, .5f },

            new object[]{63f, .5f, "scale", 2f, "Bounce", P1.ReceptorR  },


            //NEXT SECTION

            new object[]{64f, 2f, "scale", 4f, "Linear", P1.ReceptorR  },
            new object[]{64f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorR  },

            new object[]{64f, 2f, "moveX", -.5f, "Linear", P1.ReceptorU  },
            new object[]{64f, 2f, "moveX", -1f, "Linear", P1.ReceptorD  },
            new object[]{64f, 2f, "moveX", -1.5f, "Linear", P1.ReceptorL  },

            new object[]{66f, .25f, "moveX", .5f, "Linear", P1.ReceptorU  },
            new object[]{66f, .25f, "moveX", 1f, "Linear", P1.ReceptorD  },
            new object[]{66f, .25f, "moveX", 1.5f, "Linear", P1.ReceptorL  },

            new object[]{66f, .25f, "scale", 1f, "EaseInCirc", P1.ReceptorR  },
            new object[]{66f, .25f, "moveZ", .25f, "Linear", P1.ReceptorR  },

            new object[]{66.5f, .5f, "moveZ", 4f, "Float", P1.ReceptorD  },
            new object[]{67f, .5f, "moveZ", 4f, "Float", P1.ReceptorU  },
            new object[]{67.5f, 2f, "scale", 4f, "Linear", P1.ReceptorL  },
            new object[]{67.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorL  },

            new object[]{67.5f, 2f, "moveX", .5f, "Linear", P1.ReceptorD  },
            new object[]{67.5f, 2f, "moveX", 1f, "Linear", P1.ReceptorU  },
            new object[]{67.5f, 2f, "moveX", 1.5f, "Linear", P1.ReceptorR  },

            new object[]{69.5f, .25f, "moveX", -.5f, "Linear", P1.ReceptorD  },
            new object[]{69.5f, .25f, "moveX", -1f, "Linear", P1.ReceptorU  },
            new object[]{69.5f, .25f, "moveX", -1.5f, "Linear", P1.ReceptorR  },

            new object[]{69.5f, .25f, "scale", 1f, "EaseInCirc", P1.ReceptorL  },
            new object[]{69.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorL  },

            new object[]{70.5f, .5f, "rotateZ", 90f, "EaseIn", P1.ReceptorD  },
            new object[]{70.5f, .5f, "rotateZ", 180f, "EaseIn", P1.ReceptorU  },
            new object[]{71.5f, 1f, "rotateZ", -90f, "EaseIn", P1.ReceptorD  },
            new object[]{71.5f, 1f, "rotateZ", -180f, "EaseIn", P1.ReceptorU  },

            new object[]{71.5f, 2f, "scale", 4f, "Linear", P1.ReceptorR  },
            new object[]{71.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorR  },

            new object[]{71.5f, 2f, "moveX", -.5f, "Linear", P1.ReceptorU  },
            new object[]{71.5f, 2f, "moveX", -1f, "Linear", P1.ReceptorD  },
            new object[]{71.5f, 2f, "moveX", -1.5f, "Linear", P1.ReceptorL  },

            new object[]{73.5f, .25f, "moveX", .5f, "Linear", P1.ReceptorU  },
            new object[]{73.5f, .25f, "moveX", 1f, "Linear", P1.ReceptorD  },
            new object[]{73.5f, .25f, "moveX", 1.5f, "Linear", P1.ReceptorL  },

            new object[]{73.5f, .25f, "scale", 1f, "Linear", P1.ReceptorR  },
            new object[]{73.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorR  },

            new object[]{74.5f, .5f, "moveY", -3f, "Bounce", P1.ReceptorL  },
            new object[]{75f, .5f, "moveY", -3f, "Bounce", P1.ReceptorU  },
            new object[]{75.5f, .5f, "moveY", -3f, "Bounce", P1.ReceptorD  },

            new object[]{ 76.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 76.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },

            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 77f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 77f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 77.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 77.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 78f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 78f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },

            new object[]{ 78.5f, .5f, "moveX", -.640f * 2f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 78.5f, .5f, "moveX", .640f * 2f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 79f, .5f, "moveX", .640f * 2f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 79f, .5f, "moveX", -.640f * 2f, "EaseInOutQuad", P1.ReceptorD  },

            new object[]{79.5f, .25f, "moveY", .5f, "Float", P1.ReceptorD,  8, .25f }, //Wait oh no
            new object[]{79.5f, 2f, "scale", 4f, "Linear", P1.ReceptorD  },
            new object[]{79.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorD  },

            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorL ,10, .2f },
            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorU ,10, .2f },
            new object[]{79.5f, .2f, "moveX", .35f, "Float", P1.ReceptorR ,10, .2f },

            new object[]{81.5f, .25f, "scale", 1f, "Linear", P1.ReceptorD},
            new object[]{81.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorD}, // HERE I AM

            new object[]{82.5f, .4f, "scale", .25f, "Linear", P1.ReceptorU},
            new object[]{82.5f, .4f, "rotateZ", 180f, "Linear", P1.ReceptorU},

            new object[]{83f, .4f, "scale", .25f, "Linear", P1.ReceptorR},
            new object[]{83f, .4f, "rotateZ", 180f, "Linear", P1.ReceptorR},

            new object[]{83.5f, .1f, "scale", 1f, "Linear", P1.ReceptorU},
            new object[]{83.5f, .1f, "scale", 1f, "Linear", P1.ReceptorR},
            new object[]{83.5f, .5f, "rotateZ", -180f, "Linear", P1.ReceptorU},
            new object[]{83.5f, .5f, "rotateZ", -180f, "Linear", P1.ReceptorR},

            new object[]{83.5f, .25f, "moveY", .5f, "Float", P1.ReceptorD,  8, .25f },
            new object[]{83.5f, 2f, "scale", 4f, "Linear", P1.ReceptorD  },
            new object[]{83.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorD  },

            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorL ,10, .2f },
            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorU ,10, .2f },
            new object[]{83.5f, .2f, "moveX", .35f, "Float", P1.ReceptorR ,10, .2f },

            new object[]{85.5f, .25f, "scale", 1f, "Linear", P1.ReceptorD },
            new object[]{85.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorD },

            new object[]{86.5f, .4f, "moveX", -3f, "Bounce", P1.ReceptorU }, 
            new object[]{87f, .4f, "moveX", -3f, "Bounce", P1.ReceptorD },

            new object[]{87.5f, .25f, "moveY", .75f, "Float", P1.ReceptorL,  8, .25f },
            new object[]{87.5f, 2f, "scale", 5f, "Linear", P1.ReceptorL  },
            new object[]{87.5f, 2f, "moveZ", -.25f, "Linear", P1.ReceptorL  },

            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorU ,10, .2f },
            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorD ,10, .2f },
            new object[]{87.5f, .2f, "moveX", .5f, "Float", P1.ReceptorR ,10, .2f },

            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorU },
            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorD },
            new object[]{87.5f, 2f, "moveY", 1f, "Linear", P1.ReceptorR },

            new object[]{89.5f, .25f, "scale", 1f, "Linear", P1.ReceptorL },
            new object[]{89.5f, .25f, "moveZ", .25f, "Linear", P1.ReceptorL  },

            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorU },
            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorD },
            new object[]{89.5f, .25f, "moveY", -1f, "Linear", P1.ReceptorR },

            new object[]{90.5f, .5f, "moveX", -2f, "Bounce", P1.receptors  },
            new object[]{91f, .5f, "moveY", -1f, "EaseInCirc", P1.receptors  },
            new object[]{91.5f , 1f, "moveY", 1f, "Linear", P1.receptors  },
            new object[]{91.5f , .2f, "moveX", .2f, "Float", P1.receptors,  5, .2f },

            new object[]{ 92.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 92.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },

            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 93f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 93f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 93.5f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 93.5f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 94f, .5f, "moveX", .640f * 3f, "EaseInOutQuad", P1.ReceptorR  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 94f, .5f, "moveX", -.640f, "EaseInOutQuad", P1.ReceptorL  },

            new object[]{ 94.5f, .5f, "moveY", 1f, "Bounce", P1.gameObject },
            new object[]{ 95f, .5f, "rotateZ", 45f, "Bounce", P1.gameObject },

               //NEXT SECTION??

            new object[]{96f, .5f, "moveX", .25f, "InstantIn", P1.gameObject,  6,2f},

            new object[]{96f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{96.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{97f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{97f, .5f, "moveX", -.25f, "InstantIn", P1.gameObject,  6,2f},

            new object[]{98.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{99f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{99.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },

            new object[]{100.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{101f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{101.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{102f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{102.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{103f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{103.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },

            new object[]{104.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{105f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{105.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{106.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{107f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{107.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },

            new object[]{107.5f, 1f, "moveX", .960f, "Linear", P1.ReceptorL },
            new object[]{107.5f, 1f, "moveX", -.960f, "Linear", P1.ReceptorR },
            new object[]{107.5f, 1f, "moveX", .320f, "Linear", P1.ReceptorD },
            new object[]{107.5f, 1f, "moveX", -.320f, "Linear", P1.ReceptorU },
            new object[]{107.5f, 1f, "moveX", .960f, "Linear", P1.spawnL },
            new object[]{107.5f, 1f, "moveX", -.960f, "Linear", P1.spawnR },
            new object[]{107.5f, 1f, "moveX", .320f, "Linear", P1.spawnD },
            new object[]{107.5f, 1f, "moveX", -.320f, "Linear", P1.spawnU },

            new object[]{108.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{109f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{109.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{110f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{110.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{111f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{111.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },

            new object[]{111.5f, 1f, "moveX", -.960f, "Linear", P1.ReceptorL },
            new object[]{111.5f, 1f, "moveX", .960f, "Linear", P1.ReceptorR },
            new object[]{111.5f, 1f, "moveX", -.320f, "Linear", P1.ReceptorD },
            new object[]{111.5f, 1f, "moveX", .320f, "Linear", P1.ReceptorU },
            new object[]{111.5f, 1f, "moveX", -.960f, "Linear", P1.spawnL },
            new object[]{111.5f, 1f, "moveX", .960f, "Linear", P1.spawnR },
            new object[]{111.5f, 1f, "moveX", -.320f, "Linear", P1.spawnD },
            new object[]{111.5f, 1f, "moveX", .320f, "Linear", P1.spawnU },

            new object[]{112f, .5f, "moveX", -.25f, "InstantIn", P1.gameObject,  6,2f},

            new object[]{112.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{113f, .5f, "moveX", .25f, "InstantIn", P1.gameObject,  6,2f},
            new object[]{113f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorD  },
            new object[]{113.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{114.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },
            new object[]{115f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorR  },
            new object[]{115.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorD  },

            new object[]{116.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{117f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },
            new object[]{117.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorD },
            new object[]{118f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{118.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorR },
            new object[]{119f, .9f, "moveY", 1f, "Bounce", P1.ReceptorL },
            new object[]{119.5f, .9f, "moveY", 1f, "Bounce", P1.ReceptorU },

            new object[]{120.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },
            new object[]{121f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorR  },
            new object[]{121.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{122.5f, .9f, "moveX", -1f ,"Bounce", P1.ReceptorL  },
            new object[]{123f, .9f, "moveY", -1f ,"Bounce", P1.ReceptorD  },
            new object[]{123.5f, .9f, "moveY", 1f ,"Bounce", P1.ReceptorU  },

            new object[]{124.5f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{125f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{125.5f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{126f, .5f, "rotateZ", 90f, "EaseInOutElastic", P1.gameObject  },
            new object[]{126.5f, .1f, "resetRotation", 0f, "Linear", P1.gameObject  },

            //NEW SECTION MULTIPLAYER 

            new object[]{128f, 4f, "moveX", 6f, "EaseInOutQuad", P1.gameObject  },
            new object[]{128f, 4f, "moveZ", -2f, "EaseInOutQuad", P1.gameObject  },
            new object[]{128f, 4f, "rotateY", 45f, "EaseInOutQuad", P1.gameObject  },
            //new object[]{128f, 0.5f, "playParticles", postArrowR,  4, 1f},

            new object[]{132f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorU  },
            new object[]{133f, 1f, "moveY", -2f, "Linear", P1.ReceptorU  },
            new object[]{133f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorU  },

            new object[]{134f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorD  },
            new object[]{135f, 1f, "moveY", -2f, "Linear", P1.ReceptorD  },
            new object[]{135f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorD  },

            new object[]{136f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorL  },
            new object[]{137f, 1f, "moveY", -2f, "Linear", P1.ReceptorL  },
            new object[]{137f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorL  },

            //new object[]{138f, 1f, "playParticles", postArrowU },
            //new object[]{139f, 1f, "playParticles", postArrowL },
            //new object[]{140f, 1f, "playParticles", postArrowU },
            //new object[]{141f, 1f, "playParticles", postArrowD },
            //new object[]{142f, 1f, "playParticles", postArrowR },
            //new object[]{142.5f, 1f, "playParticles", postArrowU },
            //new object[]{143f, 1f, "playParticles", postArrowR },

            //new object[]{144f, 0.5f, "playParticles", postArrowL,  4, 1f},

            new object[]{144f, 4f, "moveX", -12f, "EaseInOutQuad", P1.gameObject  },
            new object[]{144f, 2f, "moveZ", 2f, "EaseOut", P1.gameObject  },
            new object[]{144f, 4f, "rotateY", -90f, "EaseInOutQuad", P1.gameObject  },
            new object[]{146f, 2f, "moveZ", -2f, "EaseOut", P1.gameObject  },

            new object[]{148f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorD  },
            new object[]{149f, 1f, "moveY", -2f, "Linear", P1.ReceptorD  },
            new object[]{149f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorD  },

            new object[]{150f, 0.9f, "moveY", 2f, "EaseOut", P1.ReceptorU  },
            new object[]{151f, 1f, "moveY", -2f, "Linear", P1.ReceptorU  },
            new object[]{151f, 1f, "moveZ", -2f, "Bounce", P1.ReceptorU  },

            //new object[]{152f, 1f, "playParticles", postArrowR },
            //new object[]{153f, 1f, "playParticles", postArrowD },
            //new object[]{154f, 1f, "playParticles", postArrowU },
            //new object[]{155f, 1f, "playParticles", postArrowL },

            new object[]{156f, 1f, "moveY", 2f, "Bounce", P1.gameObject,  4, 1f},
            new object[]{156f, 4f, "moveX", 6f, "EaseInOutQuad", P1.gameObject  },
            new object[]{156f, 4f, "moveZ", 2f, "EaseInOutQuad", P1.gameObject  },
            new object[]{156f, 4f, "rotateY", 45f, "EaseInOutQuad", P1.gameObject  },
    
            //MULTIPLAYER VARIANT SECTION ENDS HERE

            new object[]{160f, .1f, "resetRotation", 0f, "Linear", P1.gameObject },
            new object[]{160f, .1f, "resetPlayfield", 0f, "Linear", P1.gameObject },

            new object[]{160f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners }, //playfield1.spawnsRoot at -5
            new object[]{160.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{161f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{161.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{162f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{162.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },

            new object[]{163.5f, 1f, "scale", .5f, "InstantIn", P1.gameObject },
            new object[]{163.5f, 1f, "moveZ", 2f, "InstantIn", P1.gameObject },

            new object[]{165f, .5f, "rotateZ", 45f, "EaseInOutElastic", P1.gameObject },
            new object[]{165.5f, .5f, "rotateZ", -45f, "EaseInOutElastic", P1.gameObject },
            new object[]{166f, .5f, "rotateZ", -45f, "EaseInOutElastic", P1.gameObject },
            new object[]{166.5f, .5f, "rotateZ", 45f, "EaseInOutElastic", P1.gameObject },

            new object[]{168f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{168.5f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },
            new object[]{169f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },
            new object[]{169.5f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{170f, .5f, "rotateY", -180f, "EaseInOutQuad", P1.gameObject },
            new object[]{170.5f, .5f, "rotateY", 180f, "EaseInOutQuad", P1.gameObject },

            new object[]{171.5f, 1f, "scale", 2f, "InstantIn", P1.receptors },
            new object[]{171.5f, 1f, "moveY", -1f, "InstantIn", P1.gameObject },
            new object[]{171.5f, 1f, "moveZ", -2f, "InstantIn", P1.gameObject },

            new object[]{172.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors }, 
            new object[]{173f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{173.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{174f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{174.5f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{175f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },

            new object[]{176f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners }, 
            new object[]{176.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{177f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{177.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },
            new object[]{178f, .5f, "moveY", 8f, "EaseInOutQuad", P1.spawners },
            new object[]{178.5f, .5f, "moveY", -8f, "EaseInOutQuad", P1.spawners },

            new object[]{179.5f, 1f, "scale", .5f, "InstantIn", P1.gameObject },
            new object[]{179.5f, 1f, "moveZ", 2f, "InstantIn", P1.gameObject },

            new object[]{181f, .5f, "rotateX", 90f, "EaseInOutElastic", P1.gameObject },
            new object[]{181.5f, .5f, "rotateX", -90f, "EaseInOutElastic", P1.gameObject },
            new object[]{182f, .5f, "rotateX", -90f, "EaseInOutElastic", P1.gameObject },
            new object[]{182.5f, .5f, "rotateX", 90f, "EaseInOutElastic", P1.gameObject },

            new object[]{184f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors }, 
            new object[]{184.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{185f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{185.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },
            new object[]{186f, .5f, "moveY", -4f, "EaseInOutQuad", P1.receptors },
            new object[]{186.5f, .5f, "moveY", 4f, "EaseInOutQuad", P1.receptors },

            new object[]{187.5f, 1f, "scale", 2f, "InstantIn", P1.receptors },
            new object[]{187.5f, 1f, "moveY", -1f, "InstantIn", P1.gameObject },
            new object[]{187.5f, 1f, "moveZ", -2f, "InstantIn", P1.gameObject },

            new object[]{188.5f, .25f, "resetPosition",0f, "Linear", P1.receptors },
            new object[]{188.5f, .25f, "resetPosition",0f, "Linear", P1.spawners },
            new object[]{188.5f, .25f, "resetRotation", 0f, "Linear", P1.gameObject },

            new object[]{189f, .25f, "moveX", -2f, "Bounce", P1.receptors  },
            new object[]{189.5f, .25f, "moveX", 2f, "Bounce", P1.receptors  },

            new object[]{190f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{190f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{190f, .5f, "moveY", -.75f, "EaseOut", P1.receptors  },
            new object[]{190f, .5f, "moveY", .75f, "EaseOut", P1.spawners  },
            new object[]{190f, .5f, "moveZ", 1.5f, "EaseOut", P1.spawners  },

            new object[]{190.5f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{190.5f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{190.5f, .5f, "moveY", -.75f, "EaseOut", P1.receptors  },
            new object[]{190.5f, .5f, "moveY", .75f, "EaseOut", P1.spawners  },
            new object[]{190.5f, .5f, "moveZ", 1.5f, "EaseOut", P1.spawners  },

            new object[]{191f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },

            new object[]{191f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{191f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{191f, .5f, "moveY", .5f, "EaseOut", P1.spawners  },
            new object[]{191f, .5f, "moveZ", 1f, "EaseOut", P1.spawners  },

            new object[]{191.5f, .5f, "scale", .5f, "InstantIn", P1.gameObject  },
            new object[]{191.5f, 0f, "dark", 0.5f, "Linear" , P1.gameObject }, //TODO: Use the new system to use stealth

            new object[]{191.5f, .5f, "moveZ", -1.5f, "EaseOut", P1.receptors  },
            new object[]{191.5f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{191.5f, .5f, "moveY", .5f, "EaseOut", P1.spawners  },
            new object[]{191.5f, .5f, "moveZ", 1f, "EaseOut", P1.spawners  },

            new object[]{192f, 15f, "moveY", .25f, "Float", P1.receptors,  2, 15f },
            new object[]{192f, 7.5f, "rotateZ", 6f, "Float", P1.receptors,  4, 7.5f },
            new object[]{192f, 4f, "moveY", 2.5f, "Float", P1.spawners,  7, 4f },     

            new object[]{ 192f, 92f, "playAnimation", "RoadFinishMultiplayer", backgroundAnimations, "WorldSpeed"},

            new object[]{ 192f, .2f, "scale", 1.1f, "Float", P1.ReceptorR, 15, .2f }, //Vibrato effect?

            new object[]{192f, .25f, "moveY", -.25f, "InstantIn", P1.gameObject, 16, 2f},
            new object[]{193f, .25f, "moveY", .25f, "InstantIn", P1.gameObject, 16, 2f}, //Beat movement

            new object[]{195.5f, 1.4f, "scale", 1.5f, "Linear", P1.receptors  },
            new object[]{197f, .25f, "scale", 1f, "Linear", P1.receptors  },

            new object[]{200f, .2f, "scale", 1.1f, "Float", P1.ReceptorL, 15, .2f }, //Vibrato effect

            new object[]{203.5f, 1.4f, "scale", 1.5f, "Linear", P1.receptors  },
            new object[]{205f, .25f, "scale", 1f, "Linear", P1.receptors  },

            new object[]{206f, .5f, "moveX", .2f, "Bounce", P1.ReceptorR  },
            new object[]{206.5f, .5f, "moveX", -.2f, "Bounce", P1.ReceptorL  },
            new object[]{207f, .5f, "moveX", .2f, "Bounce", P1.ReceptorR  },

            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorL,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorD,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorU,  7, 2f},
            new object[]{208f, 1f, "scale", .25f, "EaseOut", P1.ReceptorR,  7, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorL,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorD,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorU,  8, 2f},
            new object[]{209f, .5f, "scale", 1f, "EaseOutElastic", P1.ReceptorR,  8, 2f},

            new object[]{223f, .5f, "resetPosition", 0f, "EaseInOutQuad", P1.receptors  },
            new object[]{223f, .5f, "resetPosition", 0f, "EaseInOutQuad", P1.spawners  },

            new object[]{223f, 0f, "dark", 0f, "Linear" , P1.gameObject }, //TODO: Use the new system to use stealth

            //SECOND GIMMICK SECTION MULTIPLAYER - DPAD

            new object[]{224f, .25f, "moveZ", 6f, "Linear", P1.spawners  },
            new object[]{224f, .25f, "moveZ", -3f, "Linear", P1.gameObject  },


            new object[]{224f, .25f, "moveX", .96f, "Linear", P1.ReceptorL  }, //-.5
            new object[]{224f, .25f, "moveX", -.96f, "Linear", P1.ReceptorR  }, //-.5
            new object[]{224f, .25f, "moveX", .32f, "Linear", P1.ReceptorD  }, //-.5
            new object[]{224f, .25f, "moveX", -.32f, "Linear", P1.ReceptorU  }, //-.5

            new object[]{224f, .25f, "moveY", 5f, "Linear", P1.spawnL  }, //-.5
            new object[]{224f, .25f, "moveY", 5f, "Linear", P1.spawnR  }, //-.5
            new object[]{224f, .25f, "moveY", 10f, "Linear", P1.spawnU  }, //-.5

            new object[]{224f, .25f, "moveX", -4.04f, "Linear", P1.spawnL  }, //-.5
            new object[]{224f, .25f, "moveX", 4.04f, "Linear", P1.spawnR  }, //-.5
            new object[]{224f, .25f, "moveX", .32f, "Linear", P1.spawnD  }, //-.5
            new object[]{224f, .25f, "moveX", -.32f, "Linear", P1.spawnU  }, //-.5

            //Clumple/DPAD setup!

            new object[]{227.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{227.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{227.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{227.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //Spread

            new object[]{231.5f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorL  },
            new object[]{231.5f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorR  },
            new object[]{231.5f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorU  },
            new object[]{231.5f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorD  },


            new object[]{231.5f, .25f, "moveX", 10f, "Linear", P1.spawnL  },
            new object[]{231.5f, .25f, "moveX", -10f, "Linear", P1.spawnR  },
            new object[]{231.5f, .25f, "moveY", -10f, "Linear", P1.spawnU  },
            new object[]{231.5f, .25f, "moveY", 10f, "Linear", P1.spawnD  },

            //Clumple + spanwers reverse

            new object[]{235.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{235.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{235.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{235.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //spread reverse

            new object[]{240f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorL  },
            new object[]{240f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorR  },
            new object[]{240f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorU  },
            new object[]{240f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorD  },

            new object[]{240f, .25f, "moveX", -10f, "Linear", P1.spawnL  },
            new object[]{240f, .25f, "moveX", 10f, "Linear", P1.spawnR  },
            new object[]{240f, .25f, "moveY", 10f, "Linear", P1.spawnU  },
            new object[]{240f, .25f, "moveY", -10f, "Linear", P1.spawnD  },

            //clumple + spanwers to normal

            new object[]{243.5f, .5f, "moveX", -0.5f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{243.5f, .5f, "moveX", 0.5f, "EaseInOutElastic", P1.ReceptorR  },
            new object[]{243.5f, .5f, "moveY", 0.5f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{243.5f, .5f, "moveY", -0.5f, "EaseInOutElastic", P1.ReceptorD  },

            //spread normal

            new object[]{247.5f, .25f, "moveX", 0.5f, "Linear", P1.ReceptorL  },
            new object[]{247.5f, .25f, "moveX", -0.5f, "Linear", P1.ReceptorR  },
            new object[]{247.5f, .25f, "moveY", -0.5f, "Linear", P1.ReceptorU  },
            new object[]{247.5f, .25f, "moveY", 0.5f, "Linear", P1.ReceptorD  },

            //Clumple 

            new object[]{251.5f, .5f, "moveX", -0.96f, "EaseInOutElastic", P1.ReceptorL  },
            new object[]{251.5f, .5f, "moveX", -0.32f, "EaseInOutElastic", P1.ReceptorD  },
            new object[]{251.5f, .5f, "moveX", 0.32f, "EaseInOutElastic", P1.ReceptorU  },
            new object[]{251.5f, .5f, "moveX", 0.96f, "EaseInOutElastic", P1.ReceptorR  },

            new object[]{251.5f, .25f, "moveX", 5f, "Linear", P1.spawnL  },
            new object[]{251.5f, .25f, "moveX", -5f, "Linear", P1.spawnR  },
            new object[]{251.5f, .25f, "moveY", -10f, "Linear", P1.spawnU  },
            new object[]{251.5f, .25f, "moveY", -5f, "Linear", P1.spawnL  },
            new object[]{251.5f, .25f, "moveY", -5f, "Linear", P1.spawnR  },

            new object[]{253f, .5f, "moveX", -0.96f, "Linear", P1.spawnL  },
            new object[]{253f, .5f, "moveX", -0.32f, "Linear", P1.spawnD  },
            new object[]{253f, .5f, "moveX", 0.32f, "Linear", P1.spawnU  },
            new object[]{253f, .5f, "moveX", 0.96f, "Linear", P1.spawnR  },

            //NORMAL


            new object[]{253f, .25f, "moveZ", 3f, "Linear", P1.gameObject  },
            new object[]{253f, .25f, "moveZ", -6f, "Linear", P1.spawners  },


            new object[]{253f, .5f, "moveY", 2.5f, "EaseInOutQuad", P1.receptors  },
            new object[]{254f, .4f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{254.5f, .4f, "moveY", -.5f, "EaseOut", P1.receptors  },
            new object[]{255f, .5f, "moveY", -.5f, "EaseOut", P1.receptors  },

            //SECOND SECTION GIMMICK ENDS HERE 
            //new object[]{256f, 1f, "playParticles", receptorsExplosionParticles,  6, 2f},

            new object[]{256f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorL},
            new object[]{256f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{256f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorD},
            new object[]{256f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{256f, 0.9f, "moveX", .3f, "EaseIn", P1.ReceptorU},
            new object[]{256f, 0.9f, "moveY", .2f, "EaseIn", P1.ReceptorU},
            new object[]{256f, 0.9f, "moveX", .5f, "EaseIn", P1.ReceptorR},
            new object[]{256f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorR},

            new object[]{257f, 1f, "playAnimation", "CityFlash", backgroundAnimations, "FlashSpeed", 14, 2f },

            new object[]{257f, 1f, "moveZ", -8f, "Bounce", P1.receptors,  6, 2f},
            new object[]{257f, 1f, "moveY", -1f, "Bounce", P1.receptors,  6, 2f},

            new object[]{257f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorL},
            new object[]{257f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{257f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorD},
            new object[]{257f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{257f, 0.9f, "moveX", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{257f, 0.9f, "moveY", -.2f, "EaseOut", P1.ReceptorU},
            new object[]{257f, 0.9f, "moveX", -.5f, "EaseOut", P1.ReceptorR},
            new object[]{257f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorR},

            new object[]{258f, 0.9f, "moveX", -.3f, "EaseIn", P1.ReceptorL},
            new object[]{258f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorL},
            new object[]{258f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{258f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorD},
            new object[]{258f, 0.9f, "moveX", .6f, "EaseIn", P1.ReceptorU},
            new object[]{258f, 0.9f, "moveY", -.2f, "EaseIn", P1.ReceptorU},
            new object[]{258f, 0.9f, "moveX", .2f, "EaseIn", P1.ReceptorR},
            new object[]{258f, 0.9f, "moveY", .6f, "EaseIn", P1.ReceptorR},

            new object[]{259f, 0.9f, "moveX", .3f, "EaseOut", P1.ReceptorL},
            new object[]{259f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorL},
            new object[]{259f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{259f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorD},
            new object[]{259f, 0.9f, "moveX", -.6f, "EaseOut", P1.ReceptorU},
            new object[]{259f, 0.9f, "moveY", .2f, "EaseOut", P1.ReceptorU},
            new object[]{259f, 0.9f, "moveX", -.2f, "EaseOut", P1.ReceptorR},
            new object[]{259f, 0.9f, "moveY", -.6f, "EaseOut", P1.ReceptorR},

            new object[]{260f, 0.9f, "moveX", -.7f, "EaseIn", P1.ReceptorL},
            new object[]{260f, 0.9f, "moveY", .1f, "EaseIn", P1.ReceptorL},
            new object[]{260f, 0.9f, "moveX", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{260f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{260f, 0.9f, "moveX", .1f, "EaseIn", P1.ReceptorU},
            new object[]{260f, 0.9f, "moveY", .4f, "EaseIn", P1.ReceptorU},
            new object[]{260f, 0.9f, "moveX", .7f, "EaseIn", P1.ReceptorR},
            new object[]{260f, 0.9f, "moveY", -.1f, "EaseIn", P1.ReceptorR},

            new object[]{261f, 0.9f, "moveX", .7f, "EaseOut", P1.ReceptorL},
            new object[]{261f, 0.9f, "moveY", -.1f, "EaseOut", P1.ReceptorL},
            new object[]{261f, 0.9f, "moveX", .4f, "EaseOut", P1.ReceptorD},
            new object[]{261f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{261f, 0.9f, "moveX", -.1f, "EaseOut", P1.ReceptorU},
            new object[]{261f, 0.9f, "moveY", -.4f, "EaseOut", P1.ReceptorU},
            new object[]{261f, 0.9f, "moveX", -.7f, "EaseOut", P1.ReceptorR},
            new object[]{261f, 0.9f, "moveY", .1f, "EaseOut", P1.ReceptorR},

            new object[]{262f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorL},
            new object[]{262f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{262f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{262f, 0.9f, "moveY", -.6f, "EaseIn", P1.ReceptorD},
            new object[]{262f, 0.9f, "moveX", .4f, "EaseIn", P1.ReceptorU},
            new object[]{262f, 0.9f, "moveY", -.5f, "EaseIn", P1.ReceptorU},
            new object[]{262f, 0.9f, "moveX", .2f, "EaseIn", P1.ReceptorR},
            new object[]{262f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorR},

            new object[]{263f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorL},
            new object[]{263f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{263f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{263f, 0.9f, "moveY", .6f, "EaseOut", P1.ReceptorD},
            new object[]{263f, 0.9f, "moveX", -.4f, "EaseOut", P1.ReceptorU},
            new object[]{263f, 0.9f, "moveY", .5f, "EaseOut", P1.ReceptorU},
            new object[]{263f, 0.9f, "moveX", -.2f, "EaseOut", P1.ReceptorR},
            new object[]{263f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorR},

            new object[]{264f, 0.9f, "moveX", -.4f, "EaseIn", P1.ReceptorL},
            new object[]{264f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorL},
            new object[]{264f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorD},
            new object[]{264f, 0.9f, "moveY", .3f, "EaseIn", P1.ReceptorD},
            new object[]{264f, 0.9f, "moveX", .1f, "EaseIn", P1.ReceptorU},
            new object[]{264f, 0.9f, "moveY", .3f, "EaseIn", P1.ReceptorU},
            new object[]{264f, 0.9f, "moveX", .6f, "EaseIn", P1.ReceptorR},
            new object[]{264f, 0.9f, "moveY", -.2f, "EaseIn", P1.ReceptorR},

            new object[]{265f, 0.9f, "moveX", .4f, "EaseOut", P1.ReceptorL},
            new object[]{265f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorL},
            new object[]{265f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorD},
            new object[]{265f, 0.9f, "moveY", -.3f, "EaseOut", P1.ReceptorD},
            new object[]{265f, 0.9f, "moveX", -.1f, "EaseOut", P1.ReceptorU},
            new object[]{265f, 0.9f, "moveY", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{265f, 0.9f, "moveX", -.6f, "EaseOut", P1.ReceptorR},
            new object[]{265f, 0.9f, "moveY", .2f, "EaseOut", P1.ReceptorR},

            new object[]{266f, 0.9f, "moveX", -.2f, "EaseIn", P1.ReceptorL},
            new object[]{266f, 0.9f, "moveY", .5f, "EaseIn", P1.ReceptorL},
            new object[]{266f, 0.9f, "moveX", -.1f, "EaseIn", P1.ReceptorD},
            new object[]{266f, 0.9f, "moveY", -.4f, "EaseIn", P1.ReceptorD},
            new object[]{266f, 0.9f, "moveX", .3f, "EaseIn", P1.ReceptorU},
            new object[]{266f, 0.9f, "moveY", .2f, "EaseIn", P1.ReceptorU},
            new object[]{266f, 0.9f, "moveX", .5f, "EaseIn", P1.ReceptorR},
            new object[]{266f, 0.9f, "moveY", -.3f, "EaseIn", P1.ReceptorR},

            new object[]{267f, 0.9f, "moveX", .2f, "EaseOut", P1.ReceptorL},
            new object[]{267f, 0.9f, "moveY", -.5f, "EaseOut", P1.ReceptorL},
            new object[]{267f, 0.9f, "moveX", .1f, "EaseOut", P1.ReceptorD},
            new object[]{267f, 0.9f, "moveY", .4f, "EaseOut", P1.ReceptorD},
            new object[]{267f, 0.9f, "moveX", -.3f, "EaseOut", P1.ReceptorU},
            new object[]{267f, 0.9f, "moveY", -.2f, "EaseOut", P1.ReceptorU},
            new object[]{267f, 0.9f, "moveX", -.5f, "EaseOut", P1.ReceptorR},
            new object[]{267f, 0.9f, "moveY", .3f, "EaseOut", P1.ReceptorR},

            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 268f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 268f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 268.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 268.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 269f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 269f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 269.5f, .5f, "moveX", -.640f * 3f, "EaseInOutQuad", P1.ReceptorL  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorD  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorU  },
            new object[]{ 269.5f, .5f, "moveX", .640f, "EaseInOutQuad", P1.ReceptorR  },

            new object[]{ 270f, .5f, "moveY", -1f, "Bounce", P1.receptors  },
            new object[]{ 270.5f, .5f, "moveY", -1f, "Bounce", P1.receptors  },
            new object[]{ 271f, .5f, "moveX", -1f, "Bounce", P1.receptors  },
            new object[]{ 271.5f, .5f, "moveY", 1f, "Bounce", P1.receptors  },

            new object[]{272f, .95f, "moveY", 1.5f, "Bounce", P1.spawners, 12, 1f},
            new object[]{272f, .95f, "moveY", -1.5f, "Bounce", P1.receptors, 12, 1f},

            new object[]{272f, 1f, "moveZ", 5f, "Linear", P1.spawners,  6, 2f},
            new object[]{272f, 1f, "moveX", .25f, "Linear", P1.spawners,  3, 4f},

            new object[]{272f, 1f, "moveZ", -5f, "Linear", P1.receptors,  6, 2f},
            new object[]{272f, 1f, "moveX", -.25f, "Linear", P1.receptors,  3, 4f},

            new object[]{273f, 1f, "moveZ", 5f, "Linear", P1.receptors,  6, 2f},
            new object[]{273f, 1f, "moveX", .25f, "Linear", P1.receptors,  3, 4f},

            new object[]{273f, 1f, "moveZ", -5f, "Linear", P1.spawners,  6, 2f},
            new object[]{273f, 1f, "moveX", -.25f, "Linear", P1.spawners,  3, 4f},

            new object[]{274f, 1f, "moveX", .25f, "Linear", P1.receptors,  3, 4f},
            new object[]{274f, 1f, "moveX", -.25f, "Linear", P1.spawners,  3, 4f},
            new object[]{275f, 1f, "moveX", -.25f, "Linear", P1.receptors,  3, 4f},
            new object[]{275f, 1f, "moveX", .25f, "Linear", P1.spawners,  3, 4f},

            new object[]{284f, 0.5f, "rotateX", 45f, "Linear", P1.gameObject  },
            new object[]{284f, 0.5f, "moveY", .5f, "Linear", P1.gameObject  },
            new object[]{284f, 0.5f, "moveZ", -2f, "Linear", P1.gameObject  },
            new object[]{284f, 0.1f, "moveY", 1f, "Linear", P1.spawners  },

            new object[]{284.1f, 3.4f, "moveY", -6f, "Linear", P1.spawners  },
            new object[]{284.1f, 3.4f, "moveY", -6f, "Linear", P1.receptors  },

            new object[]{287.5f, 3f, "moveY", 50f, "EaseIn", P1.receptors  },

        };
    }

    public override void ResetMods()
    {
        base.ResetMods();
        backgroundAnimations.GetComponent<Animator>().Play("FadeWait");
        backgroundAnimations.GetComponent<Animator>().Play("RoadWait");
        lightAnimations.GetComponent<Animator>().Play("LightIdle", 1);

        backgroundAnimations.SetActive(false);
        lightAnimations.SetActive(false);

    }

}

